using Microsoft.EntityFrameworkCore;
using TomasosPizzeria.Data.DataModels;
using TomasosPizzeria.Data.Interfaces;
using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Data.Repos
{
    public class OrderRepo : IOrderRepo
    {
        private readonly PizzaAppContext _context;

        public OrderRepo(PizzaAppContext context)
        {
            _context = context;
        }

        public void Add(Order order)
        {
            _context.Orders.Add(order);
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
        {
            var orders = await _context.Orders.Where(o => o.User.Id == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Dish)
                .ToListAsync();

            return orders;
        }

        public async Task<Order?> GetByIdAsync(int orderId)
        {
            return await _context.Orders.SingleOrDefaultAsync(o => o.OrderId == orderId);
        }

        public void Remove(Order order)
        {
            _context.Orders.Remove(order);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateStatus(Order order)
        {
            _context.Entry(order).Property(u => u.Status).IsModified = true;
        }
    }
}
