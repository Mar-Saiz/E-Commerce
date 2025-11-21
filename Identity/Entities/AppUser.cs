using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Identity.Entities
{
    public class AppUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string Cedula { get; set; }
        [Required]
        [StringLength(11)]
        public string Phone { get; set; }

        public bool Status { get; set; } // true = Activo, false = Inactivo

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? ProfilePicture { get; set; }



    }
}
