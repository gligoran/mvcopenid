using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcOpenID.WebForms.Basic.Models
{
    /// <summary>
    /// Entity class for storing OpenIDs.
    /// </summary>
    public class OpenId
    {
        [Key]
        public int OpenIdId { get; set; }

        [Required]
        public string OpenIdUrl { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}