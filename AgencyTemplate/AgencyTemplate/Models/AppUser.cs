using Microsoft.AspNetCore.Identity;

namespace AgencyTemplate.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName  { get; set; }
    }
}
