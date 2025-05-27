using Microsoft.EntityFrameworkCore;
using TomasosPizzeria.Data.DataModels;
using TomasosPizzeria.Data.Interfaces;
using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Data.Repos
{
    public class IngredientRepo : IIngredientRepo
    {
        private readonly PizzaAppContext _context;

        public IngredientRepo(PizzaAppContext context)
        {
            _context = context;
        }

        public void Add(Ingredient ingredient)
        {
            _context.Ingredients.Add(ingredient);
        }

        public async Task<List<Ingredient>> GetIngredientsAsync()
        {
            return await _context.Ingredients.ToListAsync();
        }

        public async Task<List<Ingredient>> GetIngredientsByIdsAsync(List<int> ids)
        {
            return await _context.Ingredients
                .Where(i => ids.Contains(i.IngredientId))
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
