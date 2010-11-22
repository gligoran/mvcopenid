using System;
using System.Web;
using System.Web.Mvc;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using MvcOpenID.WebForms.Basic.Models;
using System.Web.Security;
using MvcOpenID.WebForms.Basic.ViewModels;

namespace MvcOpenID.WebForms.Basic.Controllers
{
    public class UserController : Controller
    {
        #region Fields
        OpenIdRelyingParty openId;
        UserRepository users;
        UserInfo userInfo;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        public UserController()
            : this(null)
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="users">User repository.</param>
        public UserController(UserRepository users = null)
        {
            this.users = users ?? new UserRepository();

            openId = new OpenIdRelyingParty();
        }
        #endregion

        #region Initialize
        /// <summary>
        /// Controller initialization.
        /// </summary>
        /// <param name="requestContext">Request Context</param>
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            // If the user is authentifated let's set the userInfo so we don't have to cast User.Identity every time.
            // It would be a really good idea to make a BaseController that inherits from Controller,
            // implement this in there and then make every class inherit from BaseController rather than Controller.
            // This way you don't need to implement initialization in every controller separately.
            if (Request.IsAuthenticated)
                userInfo = ((OpenIdIdentity)User.Identity).UserInfo;
        }
        #endregion

        #region Actions

        #region Index
        /// <summary>
        /// Displays info about the user.
        /// </summary>
        /// <returns>Index view.</returns>
        [Authorize]
        public ActionResult Index()
        {
            return View(users.GetUser(userInfo.UserId));
        }
        #endregion

        #region Login
        /// <summary>
        /// Performs the login process.
        /// POST will be fired by a postback from our own form.
        /// GET will be fired when we first come to the login form and when the OpenID provider makes a callback.
        /// </summary>
        /// <param name="returnUrl">The URL we will return to when the loing process is complete.</param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get), ValidateInput(false)]
        public ActionResult Login(string openid_identifier, string returnUrl, bool rememberMe = false)
        {
            var response = openId.GetResponse();

            // If this is the postback from our login form response will be null as we haven't yet dealt with the OpenID provider.
            if (response == null)
            {
                //*************************************************************************************************
                //* Step 1: Here we just display the login form and let the user fill in the OpenID provider URL. *
                //*************************************************************************************************

                // This happens if OpenID identifier isn't set which means that the user didn't fill out the login form yet.
                // All we need to do is display the login form.
                if (string.IsNullOrEmpty(openid_identifier))
                    return View();

                //**********************************************************
                //* Step 2: Send the request to the OpenId provider server *
                //**********************************************************
                return SendRequestToOpenIdProvider(openid_identifier);
            }
            else //If response is not null this is the callback from the OpenID provider.
            {
                //******************************************************          
                //* Step 3: OpenId provider sending assertion response *          
                //******************************************************

                //This is the point right after the user dealt with the OpenId provider
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:
                        // We first need to get the identifier that the user has logged in with
                        // DotNetOpenAuth gives us a nice serialized identifier so we don't have to deal with
                        // differences between username.myopenid.com and http://www.myopenid.com/gligoran
                        string identifier = response.ClaimedIdentifier;

                        var user = users.GetUser(identifier);
                        if (user != null)
                        {
                            // This user is already registered.                            
                            // Now we need to let ASP.NET know about the user.
                            IssueFormsAuthenticationTicket(user, rememberMe);

                            // Now let's actually get to where we wanted to go or to the homepage
                            if (string.IsNullOrEmpty(returnUrl))
                                return RedirectToAction("Index", "Home");
                            else
                                return Redirect(returnUrl);
                        }
                        else
                        {
                            // No user with this OpenID was found, so the user will have to register first.
                            // Note that this step can be completely automatic if you get everything you need from OpenID provider.
                            // But as some providers don't return everything you asked for a form for a user to fill out the missing data is a nice addition.
                            var registrationModel = new RegistrationViewModel(identifier)
                            {
                                ReturnUrl = returnUrl
                            };

                            // First let's see which of the requested data we actually got.
                            // We can prefill the registration form with this.                            
                            var simpleReg = response.GetExtension<ClaimsResponse>();

                            if (simpleReg != null)
                            {
                                if (!string.IsNullOrEmpty(simpleReg.Email))
                                    registrationModel.Email = simpleReg.Email;

                                if (!string.IsNullOrEmpty(simpleReg.FullName))
                                    registrationModel.FullName = simpleReg.FullName;

                                if (!string.IsNullOrEmpty(simpleReg.Nickname))
                                    registrationModel.Username = simpleReg.Nickname;
                            }

                            // Now let's display the registration form
                            return View("Register", registrationModel);
                        }

                    case AuthenticationStatus.Canceled:
                        ModelState.AddModelError("openid_identifier", "Authetication canceled");
                        break;
                    case AuthenticationStatus.Failed:
                        ModelState.AddModelError("openid_identifier", "Authetication failed");
                        break;
                }
            }

            // Something went wrong, so we'll redisplay the Login form which will also hold the error message
            return View();
        }
        #endregion

        #region Register
        /// <summary>
        /// To register you must first identify yourself with an OpenID.
        /// If that OpenID doesn't yet exist in the database you'll be taken to the registration form.
        /// </summary>
        /// <returns>Redirect to the Login form.</returns>
        public ActionResult Register()
        {
            return RedirectToAction("Login");
        }

        /// <summary>
        /// Perform the registration process.
        /// </summary>
        /// <param name="user">User to be registered.</param>
        /// <param name="identifier">The OpenID identifier to be used.</param>
        /// <param name="returnUrl">The URL that the user will be returned to after finishing the registration.</param>
        /// <param name="rememberMe">Determines if the user login is persistant or not.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(User user, string identifier, string returnUrl, bool rememberMe)
        {
            //The registration form has been submitted
            try
            {
                if (ModelState.IsValid)
                {
                    OpenId openid = new OpenId
                    {
                        OpenIdUrl = identifier,
                        User = user
                    };

                    users.AddOpenId(openid);
                    users.SaveChanges();

                    // Now let's login out user to out application
                    IssueFormsAuthenticationTicket(user, rememberMe);

                    // We're done, let's get back to where we started from.
                    if (string.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");
                    else
                        return Redirect(returnUrl);
                }

                var registrationModel = new RegistrationViewModel(identifier)
                {
                    Username = user.Username,
                    Email = user.Email,
                    FullName = user.FullName,
                    ReturnUrl = returnUrl
                };

                return View(registrationModel);
            }
            catch
            {
                var registrationModel = new RegistrationViewModel(identifier)
                {
                    Username = user.Username,
                    Email = user.Email,
                    FullName = user.FullName,
                    ReturnUrl = returnUrl
                };

                return View(registrationModel);
            }
        }
        #endregion

        #region Edit
        /// <summary>
        /// Loads the user data and displays the edit form.
        /// </summary>
        /// <returns>View with a form filled with user's current data.</returns>
        [Authorize]
        public ActionResult Edit()
        {
            return View(users.GetUser(userInfo.UserId));
        }

        /// <summary>
        /// If all data is OK, saves the data to the database. Otherwise returs the view.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost, Authorize]
        public ActionResult Edit(User user)
        {
            try
            {
                // Let's get the user with this id
                var u = users.GetUser(user.UserId);

                // Let's try and update it.
                if (TryUpdateModel<User>(u))
                {
                    // If the update goes through we need to save the changes to the database...
                    users.SaveChanges();

                    // ... and return the user to the User/Index
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("general_error", "Something went wrong while updating the data.");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("general_error", e);
            }

            // Whatever goes wrong we redisplay the user edit form with as much data as we can get.
            return View(user);
        }
        #endregion

        #region Delete
        /// <summary>
        /// Displays the conformation view for deleting a user.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Delete()
        {
            return View(users.GetUser(userInfo.UserId));
        }

        /// <summary>
        /// Deletes the users from the database.
        /// </summary>
        /// <param name="user">User to be deleted.</param>
        /// <returns></returns>
        [HttpPost, Authorize]
        public ActionResult Delete(int userId)
        {
            var user = users.GetUser(userId);
            try
            {
                users.RemoveUser(user);
                users.SaveChanges();

                // We also need to sign out the user.
                FormsAuthentication.SignOut();

                return RedirectToAction("Index", "Home");
            }
            catch
            { }

            return View(user);
        }
        #endregion

        #region Logout
        /// <summary>
        /// Logs out the user.
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            // First we logout the user.
            FormsAuthentication.SignOut();

            // Then we return him to the originating URL.
            return Redirect(Request.UrlReferrer.LocalPath);
        }
        #endregion

        #region AddOpenId
        /// <summary>
        /// Performs the process of assocciation a new OpenID provider with this user.
        /// </summary>
        /// <param name="openid_identifier">OpenID identifier to be associated.</param>
        /// <returns></returns>
        public ActionResult AddOpenId(string openid_identifier)
        {
            // This action acts almost exactly the same as the Login action.
            // The only difference is that after the provider's callback we need to check if the
            // OpenID identifier already exists in the database.

            var response = openId.GetResponse();

            if (response == null)
            {
                if (string.IsNullOrEmpty(openid_identifier))
                    return View();

                return SendRequestToOpenIdProvider(openid_identifier);
            }
            else
            {
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:
                        string identifier = response.ClaimedIdentifier;

                        if (users.GetOpenId(identifier) == null)
                        {
                            User user = users.GetUser(userInfo.UserId);
                            user.OpenIds.Add(new OpenId { OpenIdUrl = identifier });
                            users.SaveChanges();

                            return RedirectToAction("Index");
                        }

                        ModelState.AddModelError("openid_identifier", "This OpenID identifier has already been assocciated with an account.");
                        break;

                    case AuthenticationStatus.Canceled:
                        ModelState.AddModelError("openid_identifier", "Authetication canceled");
                        break;
                    case AuthenticationStatus.Failed:
                        ModelState.AddModelError("openid_identifier", "Authetication failed");
                        break;
                }
            }

            return View();
        }
        #endregion

        #region RemoveOpenId
        /// <summary>
        /// Removes the OpenID provider from user's profile.
        /// </summary>
        /// <param name="identifier">OpenID identifier to be removed.</param>
        /// <returns></returns>
        public ActionResult RemoveOpenId(string identifier)
        {
            users.RemoveOpenId(identifier);
            users.SaveChanges();

            return RedirectToAction("Index");
        }
        #endregion

        #endregion

        #region Helper Methods

        #region SendRequestToOpenIdProvider
        /// <summary>
        /// Make the request to the OpenID provider.
        /// </summary>
        /// <param name="openid_identifier">Identifier to send the request to.</param>
        /// <returns></returns>
        private ActionResult SendRequestToOpenIdProvider(string identifier)
        {
            Identifier id;
            if (Identifier.TryParse(identifier, out id))
            {
                try
                {
                    IAuthenticationRequest request = openId.CreateRequest(Identifier.Parse(identifier));

                    // Next we have to specify which data we want
                    request.AddExtension(new ClaimsRequest
                    {
                        Email = DemandLevel.Require,
                        Nickname = DemandLevel.Require,
                        FullName = DemandLevel.Request
                    });

                    // Finally we'll make a request to the OpenID provider
                    return request.RedirectingResponse.AsActionResult();
                }
                catch (ProtocolException e)
                {
                    ModelState.AddModelError("openid_identifier", e);
                }
            }
            else
            {
                ModelState.AddModelError("openid_identifier", "Invalid identifier");
            }

            return View();
        }
        #endregion

        #region IssueFormsAuthenticationTicket
        /// <summary>
        /// Issues the FormsAuthenticationTicket to let ASP.NET know that a user is logged in.
        /// </summary>
        /// <param name="user">User that has logged in.</param>
        private void IssueFormsAuthenticationTicket(User user, bool? rememberMe)
        {
            // We need to make a FormsAuthenticationTicket.
            // To store UserInfo data in it we use the 2nd overload.
            // Note that if rememberMe is false the cookie is stored in the session and will expire when the session is over.
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1,
                user.Username,
                DateTime.Now,
                DateTime.Now.AddDays(14),
                rememberMe ?? false,
                UserInfo.FromUser(user).ToString());

            // Now we encrypt the ticket so no one can read it...
            string encTicket = FormsAuthentication.Encrypt(ticket);

            // ...make a cookie and add it. ASP.NET will now know that our user is logged in.
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
        }
        #endregion

        #endregion
    }
}
