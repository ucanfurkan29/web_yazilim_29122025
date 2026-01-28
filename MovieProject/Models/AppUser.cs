using Microsoft.AspNetCore.Identity;

namespace MovieProject.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
