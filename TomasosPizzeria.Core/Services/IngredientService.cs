using TomasosPizzeria.Core.Interfaces;
using TomasosPizzeria.Data.DataModels;
using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Core.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly PizzaAppContext _context;

        public IngredientService(PizzaAppContext context)
        {
            _context = context;
        }

        public async Task AddIngredientAsync(string name)
        {
            var ingredient = new Ingredient
            {
                Name = name
            };

            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();
        }
    }
}
