using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Data.Interfaces
{
    public interface IOrderRepo
    {
        void AddOrder(Order order);
        List<Order> GetOrders();
    }
}
