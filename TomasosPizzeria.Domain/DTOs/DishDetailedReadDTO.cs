using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Domain.DTOs
{
    public class DishDetailedReadDTO
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public string? Category { get; set; }

        public ICollection<string>? Ingredients { get; set; }
    }
}
