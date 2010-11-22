using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics.Contracts;

namespace MvcOpenID.WebForms.Basic.Models
{
    /// <summary>
    /// User info that is inside the authentication cookie.
    /// It is easily converted to string and back.
    /// </summary>
    public class UserInfo
    {
        public int UserId { get; set; }
        public string  Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }

        /// <summary>
        /// Returns the string that can be used to feed as UserData into the FromsAuthentificationTicket.
        /// </summary>
        /// <returns>String serialization.</returns>
        public override string ToString()
        {
            string s = UserId.ToString() + ";" +
                Username + ";" +
                Email + ";" +
                FullName;
            return s;
        }

        /// <summary>
        /// Creates a UserInfo class from the User class.
        /// </summary>
        /// <param name="user">User who's data is used to create the UserInfo.</param>
        /// <returns>UserInfo created from User.</returns>
        public static UserInfo FromUser(User user)
        {
            Contract.Requires<ArgumentNullException>(user != null);

            return new UserInfo
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                FullName = user.FullName
            };
        }

        /// <summary>
        /// Creates a UserInfo class from the string.
        /// </summary>
        /// <param name="user">Serialized string.</param>
        /// <returns>UserInfo created from the provided string.</returns>
        /// <exception cref="ArgumentException">When string is not a serialized UserInfo string.</exception>
        public static UserInfo FromString(string user)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(user));

            string[] data = user.Split(';');
            if (data.Length != 4)
                throw new ArgumentException("This string is not a serialized UserInfo class.");

            return new UserInfo
            {
                UserId = int.Parse(data[0]),
                Username = data[1],
                Email = data[2],
                FullName = data[3]
            };
        }
    }
}