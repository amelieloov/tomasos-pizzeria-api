using TomasosPizzeria.Core.Interfaces;
using TomasosPizzeria.Core.Services;

namespace TomasosPizzeria.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IDishService, DishService>();
            services.AddScoped<IIngredientService, IngredientService>();

            return services;
        }
    }
}
