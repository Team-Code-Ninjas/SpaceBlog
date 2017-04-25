namespace SpaceBlog.Models
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;  
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Suspended = false;
            this.DateRegistered = DateTime.Now;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The Full Name field is required")]
        public string FullName { get; set; }

        [Required]
        public DateTime DateRegistered { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            /// Add custom user claims here

            return userIdentity;
        }

        public bool Suspended
        {
            get;
            set;
        }
    }
}