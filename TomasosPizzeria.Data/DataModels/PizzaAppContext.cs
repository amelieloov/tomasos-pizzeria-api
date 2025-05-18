using Microsoft.EntityFrameworkCore;
using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Data.DataModels
{
    public class PizzaAppContext : DbContext
    {
        public PizzaAppContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<ApplicationUser> Users { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Dish> Dishes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
    }
}
