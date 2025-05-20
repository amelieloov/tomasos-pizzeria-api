using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Data.Identity
{
    public class ApplicationUserContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationUserContext(DbContextOptions<ApplicationUserContext> options) : base(options)
        {
        }
    }
}
