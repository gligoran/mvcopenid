using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MvcOpenID.Razor.Simple.Models
{
    /// <summary>
    /// DbContext class for dealing with users.
    /// </summary>
    public class UserContext : DbContext
    {
        // TChange the string if you want you database to have a custom name.
        public UserContext()
            : base("MvcOpenID")
        { }

        // We have separate tables for users and OpenIds as one user can have multiple OpenIds
        public DbSet<User> Users { get; set; }
        public DbSet<OpenId> OpenIds { get; set; }
    }
}