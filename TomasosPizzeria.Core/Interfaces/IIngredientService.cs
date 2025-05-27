using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Core.Interfaces
{
    public interface IIngredientService
    {
        Task<List<Ingredient>> GetIngredientsAsync();
        Task AddIngredientAsync(string name);
    }
}
