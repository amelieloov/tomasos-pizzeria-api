using Microsoft.AspNetCore.Identity;
using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public List<Order> Orders { get; set; }
    }
}
