using Microsoft.EntityFrameworkCore;
using TomasosPizzeria.Data.DataModels;
using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Data.Repos
{
    public class DishRepo
    {
        private readonly PizzaAppContext _context;

        public DishRepo(PizzaAppContext context)
        {
            _context = context;
        }

        public async Task AddDishAsync(Dish dish)
        {
            _context.Dishes.Add(dish);
            await _context.SaveChangesAsync();
        }

        public async Task<Dish?> GetDishByIdAsync(int id)
        {
            return await _context.Dishes
                .Include(d => d.Ingredients) // include if needed
                .SingleOrDefaultAsync(d => d.DishId == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
