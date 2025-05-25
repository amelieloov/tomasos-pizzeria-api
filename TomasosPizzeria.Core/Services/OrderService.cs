using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TomasosPizzeria.Core.Interfaces;
using TomasosPizzeria.Data.DataModels;
using TomasosPizzeria.Domain.DTOs;
using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly PizzaAppContext _context;
        private UserManager<ApplicationUser> _userManager;

        public OrderService(PizzaAppContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task AddOrderAsync(string userId, List<DishAddDTO> dishDtos)
        {
            decimal totalPrice = 0;
            var orderItems = new List<OrderItem>();

            foreach (var item in dishDtos)
            {
                var dish = await _context.Dishes.FindAsync(item.DishId);
                if (dish != null)
                {
                    totalPrice += dish.Price * item.Quantity;
                }
            }

            var user = await _userManager.FindByIdAsync(userId);
            var userRoles = await _userManager.GetRolesAsync(user);

            var dishQuantity = dishDtos.Sum(d => d.Quantity);

            if (userRoles.Contains("PremiumUser") && dishQuantity >= 3)
            {
                //20% discount
                totalPrice = totalPrice * 0.8m;
            }

            foreach (var item in dishDtos)
            {
                orderItems.Add(new OrderItem { DishId = item.DishId, Quantity = item.Quantity });
                user.BonusPoints += 10;
            }

            if (user.BonusPoints >= 100)
            {
                orderItems.Add(new OrderItem { DishId = 2, Quantity = 1});
                user.BonusPoints -= 100;
            }

            var order = new Order()
            {
                UserId = userId,
                Status = "Running",
                TotalPrice = (decimal)totalPrice,
                OrderItems = orderItems
            };

            var result = await _userManager.UpdateAsync(user);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderDTO>> GetOrdersByUserAsync(string userId)
        {
            var orders = await _context.Orders.Where(o => o.User.Id == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Dish)
                .ToListAsync();

            var orderDtos = orders.Select(o => new OrderDTO
            {
                TotalPrice = o.TotalPrice,
                Status = o.Status,
                Dishes = o.OrderItems.Select(oi => new DishDTO
                {
                    Name = oi.Dish.Name,
                    Description = oi.Dish.Description,
                    Price = oi.Dish.Price,
                    CategoryId = oi.Dish.CategoryId
                }).ToList()
            }).ToList();

            return orderDtos;
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(o => o.OrderId == orderId);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStatusAsync(int orderId, string newStatus)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(o => o.OrderId == orderId);

            order.Status = newStatus;
            _context.Entry(order).Property(u => u.Status).IsModified = true;

            await _context.SaveChangesAsync();
        }
    }
}
