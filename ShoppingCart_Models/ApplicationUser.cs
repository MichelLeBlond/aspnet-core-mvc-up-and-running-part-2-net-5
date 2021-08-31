using Microsoft.AspNetCore.Identity;

namespace ShoppingCart_Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FullName { get; set; }
    }
}
