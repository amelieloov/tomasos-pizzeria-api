using TomasosPizzeria.Core.Interfaces;
using TomasosPizzeria.Data.Interfaces;
using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Core.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepo _repo;

        public IngredientService(IIngredientRepo repo)
        {
            _repo = repo;
        }

        public async Task<List<Ingredient>> GetIngredientsAsync()
        {
            return await _repo.GetIngredientsAsync();
        }

        public async Task AddIngredientAsync(string name)
        {
            var ingredient = new Ingredient
            {
                Name = name
            };

            _repo.Add(ingredient);
            await _repo.SaveChangesAsync();
        }
    }
}
