using TomasosPizzeria.Domain.DTOs;
using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Core.Interfaces
{
    public interface IOrderService
    {
        Task AddOrderAsync(string userId, List<DishAddDTO> dishDtos);
        Task<List<OrderReadDTO>> GetOrdersByUserAsync(string userId);
        Task DeleteOrderAsync(int orderId);
        Task UpdateStatusAsync(int orderId, string newStatus);
    }
}
