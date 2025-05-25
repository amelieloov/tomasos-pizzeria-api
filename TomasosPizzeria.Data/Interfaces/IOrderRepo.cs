using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Data.Interfaces
{
    public interface IOrderRepo
    {
        Task<List<Order>> GetOrdersByUserIdAsync(string userId);
        Task<Order?> GetByIdAsync(int orderId);
        void Add(Order order);
        void UpdateStatus(Order order);
        void Remove(Order order);
        Task SaveChangesAsync();
    }
}
