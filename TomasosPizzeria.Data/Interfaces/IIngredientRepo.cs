using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Data.Interfaces
{
    public interface IIngredientRepo
    {
        void Add(Ingredient ingredient);
        Task<List<Ingredient>> GetIngredientsAsync();
        Task<List<Ingredient>> GetIngredientsByIdsAsync(List<int> ids);
        Task SaveChangesAsync();
    }
}
