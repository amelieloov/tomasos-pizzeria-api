using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Data.Interfaces
{
    public interface IDishRepo
    {
        Task<List<Dish>> GetDishesAsync();
        void Add(Dish dish);
        Task<Dish?> GetDishByIdAsync(int id);
        Task SaveChangesAsync();
    }
}
