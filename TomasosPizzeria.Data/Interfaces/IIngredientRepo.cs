using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Data.Interfaces
{
    public interface IIngredientRepo
    {
        Task AddIngredientAsync(Ingredient ingredient);
        Task<List<Ingredient>> GetIngredientsByIdsAsync(List<int> ids);
    }
}
