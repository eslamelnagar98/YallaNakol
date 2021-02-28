using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace YallaNakol.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Addresses = new HashSet<Address>();
        }
        [Required,PersonalData,MinLength(5)]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [Required,PersonalData,MinLength(5)]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
