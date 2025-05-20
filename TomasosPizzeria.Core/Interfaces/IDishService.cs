using TomasosPizzeria.Domain.DTOs;
using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Core.Interfaces
{
    public interface IDishService
    {
        Task AddDishAsync(DishDTO dishDto);
    }
}
