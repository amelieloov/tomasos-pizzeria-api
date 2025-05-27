using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Domain.DTOs
{
    public class OrderReadDTO
    {
        public decimal TotalPrice { get; set; }

        public string Status { get; set; }

        public List<DishReadDTO> Dishes { get; set; }
    }
}
