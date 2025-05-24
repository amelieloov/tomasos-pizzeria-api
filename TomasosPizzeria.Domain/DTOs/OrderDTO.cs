using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Domain.DTOs
{
    public class OrderDTO
    {
        public decimal TotalPrice { get; set; }

        public string Status { get; set; }

        public List<DishDTO> Dishes { get; set; }
    }
}
