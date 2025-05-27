using Microsoft.AspNetCore.Identity;
using TomasosPizzeria.Core.Interfaces;
using TomasosPizzeria.Data.Interfaces;
using TomasosPizzeria.Domain.DTOs;
using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IDishRepo _dishRepo;
        private readonly IOrderRepo _orderRepo;
        private UserManager<ApplicationUser> _userManager;

        public OrderService(IDishRepo dishRepo, IOrderRepo orderRepo, UserManager<ApplicationUser> userManager)
        {
            _dishRepo = dishRepo;
            _orderRepo = orderRepo;
            _userManager = userManager;
        }

        public async Task AddOrderAsync(string userId, List<DishAddDTO> dishDtos)
        {
            var totalPrice = await CalculateTotalPrice(dishDtos);
            var user = await _userManager.FindByIdAsync(userId);
            var userRoles = await _userManager.GetRolesAsync(user);
            var dishQuantity = dishDtos.Sum(d => d.Quantity);
            user.BonusPoints += 10 * dishQuantity;

            totalPrice = ApplyPremiumUserDiscount(totalPrice, userRoles, dishQuantity);
            var orderItems = GenerateOrderItems(dishDtos, user);
            orderItems = ApplyBonusIfEligible(orderItems, user);

            var order = new Order()
            {
                UserId = userId,
                Status = "Running",
                TotalPrice = (decimal)totalPrice,
                OrderItems = orderItems
            };

            await _userManager.UpdateAsync(user);

            _orderRepo.Add(order);
            await _orderRepo.SaveChangesAsync();
        }

        private async Task<decimal> CalculateTotalPrice(List<DishAddDTO> dishDtos)
        {
            decimal totalPrice = 0;

            foreach (var item in dishDtos)
            {
                var dish = await _dishRepo.GetDishByIdAsync(item.DishId);

                if (dish != null)
                {
                    totalPrice += dish.Price * item.Quantity;
                }
            }

            return totalPrice;
        }

        private decimal ApplyPremiumUserDiscount(decimal totalPrice, IList<string> userRoles, int dishQuantity)
        {
            if (userRoles.Contains("PremiumUser") && dishQuantity >= 3)
            {
                totalPrice = totalPrice * 0.8m;
            }

            return totalPrice;
        }

        private List<OrderItem> GenerateOrderItems(List<DishAddDTO> dishDtos, ApplicationUser user)
        {
            var orderItems = new List<OrderItem>();

            foreach (var item in dishDtos)
            {
                orderItems.Add(new OrderItem { DishId = item.DishId, Quantity = item.Quantity });
            }

            return orderItems;
        }

        private List<OrderItem> ApplyBonusIfEligible(List<OrderItem> orderItems, ApplicationUser user)
        {
            if (user.BonusPoints >= 100)
            {
                orderItems.Add(new OrderItem { DishId = 2, Quantity = 1 });
                user.BonusPoints -= 100;
            }

            return orderItems;
        }

        public async Task<List<OrderReadDTO>> GetOrdersByUserAsync(string userId)
        {
            var orders = await _orderRepo.GetOrdersByUserIdAsync(userId);

            var orderDtos = orders.Select(o => new OrderReadDTO
            {
                TotalPrice = o.TotalPrice,
                Status = o.Status,
                Dishes = o.OrderItems.Select(oi => new DishReadDTO
                {
                    Name = oi.Dish.Name,
                    Quantity = oi.Quantity
                }).ToList()
            }).ToList();

            return orderDtos;
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await _orderRepo.GetByIdAsync(orderId);
            _orderRepo.Remove(order);
            await _orderRepo.SaveChangesAsync();
        }

        public async Task UpdateStatusAsync(int orderId, string newStatus)
        {
            var order = await _orderRepo.GetByIdAsync(orderId);

            order.Status = newStatus;

            _orderRepo.UpdateStatus(order);
            await _orderRepo.SaveChangesAsync();
        }
    }
}
