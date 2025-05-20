using Microsoft.EntityFrameworkCore;
using TomasosPizzeria.Core.Interfaces;
using TomasosPizzeria.Data.DataModels;
using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly PizzaAppContext _context;

        public OrderService(PizzaAppContext context)
        {
            _context = context;
        }

        public async Task AddOrderAsync(int dishIds)
        {
            var order = new Order()
            {

            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetOrdersByUserAsync(string userId)
        {
            return await _context.Orders.Where(o => o.User.Id == userId).ToListAsync();
        }
    }
}
