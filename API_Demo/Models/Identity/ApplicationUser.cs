using Microsoft.AspNetCore.Identity;

namespace API_Demo_V2.Models.Identity
{
    public class ApplicationUser:IdentityUser
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }
    }
}
