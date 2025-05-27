using TomasosPizzeria.Core.Interfaces;
using TomasosPizzeria.Core.Services;
using TomasosPizzeria.Data.Interfaces;
using TomasosPizzeria.Data.Repos;

namespace TomasosPizzeria.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            //Add repos
            services.AddScoped<IOrderRepo, OrderRepo>();
            services.AddScoped<IDishRepo, DishRepo>();
            services.AddScoped<IIngredientRepo, IngredientRepo>();

            //Add services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IDishService, DishService>();
            services.AddScoped<IIngredientService, IngredientService>();

            return services;
        }
    }
}
