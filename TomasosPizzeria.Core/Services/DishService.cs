using Microsoft.EntityFrameworkCore;
using TomasosPizzeria.Core.Interfaces;
using TomasosPizzeria.Data.DataModels;
using TomasosPizzeria.Domain.DTOs;
using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Core.Services
{
    public class DishService : IDishService
    {
        private readonly PizzaAppContext _context;

        public DishService(PizzaAppContext context)
        {
            _context = context;
        }

        public async Task AddDishAsync(DishDTO dishDto)
        {
            var ingredients = await _context.Ingredients
                .Where(i => dishDto.IngredientIds.Contains(i.IngredientId))
                .ToListAsync();

            var dish = new Dish()
            {
                Name = dishDto.Name,
                Description = dishDto.Description,
                Price = dishDto.Price,
                CategoryId = dishDto.CategoryId,
                Ingredients = ingredients
            };

            _context.Dishes.Add(dish);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDishAsync(int dishId, DishDTO dishDto)
        {
            var dish = new Dish()
            {
                Name = dishDto.Name,
                Description = dishDto.Description,
                Price = dishDto.Price,
                CategoryId = dishDto.CategoryId,
                //Ingredients = ingredients
            };

            var dishOrg = _context.Dishes
                .SingleOrDefault(e => e.DishId == dishId);

            _context.Entry(dishOrg).CurrentValues.SetValues(dish);
            await _context.SaveChangesAsync();
        }
    }
}
