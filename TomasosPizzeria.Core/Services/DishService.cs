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

        public async Task<List<DishDetailedReadDTO>> GetDishesAsync()
        {
            var dishes = await _dishRepo.GetDishesAsync();

            var dishDtos = dishes.Select(d => new DishDetailedReadDTO()
            {
                Name = d.Name,
                Description = d.Description,
                Price = d.Price,
                Category = d.Category.Name,
                Ingredients = d.Ingredients.Select(i => i.Name).ToList()
            }).ToList();

            return dishDtos;
        }

        public async Task AddDishAsync(DishDTO dishDto)
        {
            var ingredients = await _ingredientRepo.GetIngredientsByIdsAsync(dishDto.IngredientIds);

            var dish = new Dish()
            {
                Name = dishDto.Name,
                Description = dishDto.Description,
                Price = (decimal)dishDto.Price,
                CategoryId = (int)dishDto.CategoryId,
                Ingredients = ingredients
            };

            _dishRepo.Add(dish);
            await _dishRepo.SaveChangesAsync();
        }

        public async Task UpdateDishAsync(int dishId, DishDTO dishDto)
        {
            var existingDish = await _dishRepo.GetDishByIdAsync(dishId);

            if (existingDish == null)
                throw new Exception("Dish not found");

            if (!string.IsNullOrEmpty(dishDto.Name))
                existingDish.Name = dishDto.Name;

            if (!string.IsNullOrEmpty(dishDto.Description))
                existingDish.Description = dishDto.Description;

            if (dishDto.Price.HasValue)
                existingDish.Price = (decimal)dishDto.Price;

            if (dishDto.CategoryId.HasValue)
                existingDish.CategoryId = (int)dishDto.CategoryId;

            if (dishDto.IngredientIds != null && dishDto.IngredientIds.Any())
            {
                var newIngredients = await _ingredientRepo.GetIngredientsByIdsAsync(dishDto.IngredientIds);
                existingDish.Ingredients = newIngredients;
            }

            await _dishRepo.SaveChangesAsync();
        }
    }
}
