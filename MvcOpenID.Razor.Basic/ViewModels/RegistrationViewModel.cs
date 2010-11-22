using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace MvcOpenID.Razor.Basic.ViewModels
{
    /// <summary>
    /// View model used for new user registration.
    /// </summary>
    public class RegistrationViewModel
    {
        public RegistrationViewModel(string openIdUrl)
        {
            Contract.Requires<ArgumentException>(!string.IsNullOrEmpty(openIdUrl));

            OpenIdUrl = openIdUrl;
        }

        public string OpenIdUrl { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }
}
