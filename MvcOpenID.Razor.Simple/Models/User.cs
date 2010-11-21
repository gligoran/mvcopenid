using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace MvcOpenID.Razor.Simple.Models
{
    /// <summary>
    /// Entity class for storing Users.
    /// </summary>
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MinLength(6)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        public string FullName { get; set; }

        public virtual ICollection<OpenId> OpenIds { get; set; }
    }
}