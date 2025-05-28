using Microsoft.EntityFrameworkCore;
using TomasosPizzeria.Data.DataModels;
using TomasosPizzeria.Data.Interfaces;
using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Data.Repos
{
    public class DishRepo : IDishRepo
    {
        private readonly PizzaAppContext _context;

        public DishRepo(PizzaAppContext context)
        {
            _context = context;
        }

        public async Task<List<Dish>> GetDishesAsync()
        {
            return await _context.Dishes
                .Include(d => d.Category)
                .Include(d => d.Ingredients)
                .ToListAsync();
        }

        public void Add(Dish dish)
        {
            _context.Dishes.Add(dish);
        }

        public async Task<Dish?> GetDishByIdAsync(int id)
        {
            return await _context.Dishes
                .Include(d => d.Ingredients)
                .SingleOrDefaultAsync(d => d.DishId == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
