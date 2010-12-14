using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace MvcOpenID.ASPX.Basic.Models
{
    /// <summary>
    /// IIdentity implementation of OpenId logins.
    /// </summary>
    public class OpenIdIdentity : IIdentity
    {
        private System.Web.Security.FormsAuthenticationTicket ticket;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ticket">Ticket to be used to construct the IIdentity.</param>
        public OpenIdIdentity(System.Web.Security.FormsAuthenticationTicket ticket)
        {
            this.ticket = ticket;
            UserInfo = UserInfo.FromString(ticket.UserData);
        }

        /// <summary>
        /// Authentication Type.
        /// </summary>
        /// <remarks>You will most likely want to rename this to reflect your project's name</remarks>
        public string AuthenticationType
        {
            get { return "MvcOpenID"; }
        }

        /// <summary>
        /// Return if the user is authenticated.
        /// </summary>
        /// <remarks>This always returns true as User.Identity in controllers only be castable to OpenIdIdentity if we actually set it in the Global.asax.cs. See the MvcApplication_PostAuthenticateRequest method.</remarks>
        public bool IsAuthenticated
        {
            get { return true; }
        }

        /// <summary>
        /// Username of the user.
        /// </summary>
        public string Name
        {
            get { return ticket.Name; }
        }

        /// <summary>
        /// User info of the user that is logged in.
        /// </summary>
        public UserInfo UserInfo
        {
            get;
            private set;
        }
    }
}