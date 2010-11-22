using System;
using System.Linq;
using System.Diagnostics.Contracts;

namespace MvcOpenID.WebForms.Basic.Models
{
    public class UserRepository
    {
        UserContext userDb;

        /// <summary>
        /// Constructor.
        /// </summary>
        public UserRepository()
            : this(null)
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="userDb">UserContext.</param>
        public UserRepository(UserContext userDb)
        {
            this.userDb = userDb ?? new UserContext();
        }

        /// <summary>
        /// Save the changes to the database.
        /// </summary>
        public void SaveChanges()
        {
            userDb.SaveChanges();
        }

        /// <summary>
        /// Gets the User by UserId.
        /// </summary>
        /// <param name="id">UserId of the user.</param>
        /// <returns>User with a specific UserId.</returns>
        public User GetUser(int id)
        {
            return (userDb.Users.SingleOrDefault(u => u.UserId == id));
        }

        /// <summary>
        /// Gets the user with the specific identifier.
        /// </summary>
        /// <param name="identifier">Identifier of the user.</param>
        /// <returns>User that is assocciated with the specific identifier. If the identifier is not found in the database, return null.</returns>
        public User GetUser(string identifier)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(identifier));

            var openid = userDb.OpenIds.SingleOrDefault(o => o.OpenIdUrl == identifier);
            if (openid != null)
                return openid.User;

            return null;
        }

        /// <summary>
        /// Removes user from the database.
        /// </summary>
        /// <param name="user">User to be removed.</param>
        /// <remarks>User deletion is cascading which means that all of user's OpenIDs will get deleted from the database when the user is deleted.</remarks>
        public void RemoveUser(User user)
        {
            //userDb.Users.Remove(user); //EFCTP4
            userDb.Users.DeleteObject(user); //EF4
        }

        /// <summary>
        /// Gets OpenId from the OpenID identifier.
        /// </summary>
        /// <param name="identifier">OpenID identifier.</param>
        /// <returns></returns>
        public OpenId GetOpenId(string identifier)
        {
            return userDb.OpenIds.SingleOrDefault(o => o.OpenIdUrl == identifier);
        }

        /// <summary>
        /// Adds a new OpenId to the database.
        /// </summary>
        /// <param name="openid">OpenId to be added.</param>
        public void AddOpenId(OpenId openid)
        {
            Contract.Requires<ArgumentNullException>(openid != null);

            //userDb.OpenIds.Add(openid); //EFCTP4
            userDb.OpenIds.AddObject(openid); //EF4
        }

        /// <summary>
        /// Removes the OpenId from the database.
        /// </summary>
        /// <param name="identifier">Identifier to be removed.</param>
        public void RemoveOpenId(string identifier)
        {
            var openid = userDb.OpenIds.SingleOrDefault(o => o.OpenIdUrl == identifier);
            if (openid.User.OpenIds.Count > 1)
            {
                //userDb.OpenIds.Remove(openid); //EFTCP4
                userDb.OpenIds.DeleteObject(openid); //EF4
            }
            else
                throw new Exception("Cannot delete the last OpenID identifier. Every user account has to be associated with at least on OpenID.");
        }
    }
}