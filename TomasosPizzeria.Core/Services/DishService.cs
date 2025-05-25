using TomasosPizzeria.Core.Interfaces;
using TomasosPizzeria.Data.Interfaces;
using TomasosPizzeria.Domain.DTOs;
using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Core.Services
{
    public class DishService : IDishService
    {
        private readonly IDishRepo _dishRepo;
        private readonly IIngredientRepo _ingredientRepo;

        public DishService(IDishRepo dishRepo, IIngredientRepo ingredientRepo)
        {
            _dishRepo = dishRepo;
            _ingredientRepo = ingredientRepo;
        }

        public async Task AddDishAsync(DishDTO dishDto)
        {
            var ingredients = await _ingredientRepo.GetIngredientsByIdsAsync(dishDto.IngredientIds);

            var dish = new Dish()
            {
                Name = dishDto.Name,
                Description = dishDto.Description,
                Price = dishDto.Price,
                CategoryId = dishDto.CategoryId,
                Ingredients = ingredients
            };

            await _dishRepo.AddDishAsync(dish);
        }

        public async Task UpdateDishAsync(int dishId, DishDTO dishDto)
        {
            var existingDish = await _dishRepo.GetDishByIdAsync(dishId);

            if (existingDish == null)
            {
                throw new Exception("Dish not found");
            }

            existingDish.Name = dishDto.Name;
            existingDish.Description = dishDto.Description;
            existingDish.Price = dishDto.Price;
            existingDish.CategoryId = dishDto.CategoryId;

            // optionally update ingredients here too

            await _dishRepo.SaveChangesAsync();
        }
    }
}
