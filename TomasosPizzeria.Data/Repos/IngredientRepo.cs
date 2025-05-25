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

        public async Task AddIngredientAsync(Ingredient ingredient)
        {
            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Ingredient>> GetIngredientsByIdsAsync(List<int> ids)
        {
            return await _context.Ingredients
                .Where(i => ids.Contains(i.IngredientId))
                .ToListAsync();
        }
    }
}
