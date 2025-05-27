using TomasosPizzeria.Domain.DTOs;
using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Core.Interfaces
{
    public interface IDishService
    {
        Task<List<Dish>> GetDishesAsync();
        Task AddDishAsync(DishDTO dishDto);
        Task UpdateDishAsync(int dishId, DishDTO dishDto);
    }
}
