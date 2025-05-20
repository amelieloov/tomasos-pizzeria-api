using Microsoft.EntityFrameworkCore;
using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Data.DataModels
{
    public class PizzaAppContext : DbContext
    {
        public PizzaAppContext(DbContextOptions<PizzaAppContext> options) : base(options)
        {
        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Dish> Dishes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");

            modelBuilder.Entity<Dish>()
                .HasMany(d => d.Ingredients)
                .WithMany(i => i.Dishes)
                .UsingEntity(j => j.ToTable("DishIngredient"));
        }
    }
}
