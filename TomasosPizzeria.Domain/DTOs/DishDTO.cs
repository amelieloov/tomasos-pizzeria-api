using System.ComponentModel.DataAnnotations;
using TomasosPizzeria.Domain.Entities;

namespace TomasosPizzeria.Domain.DTOs
{
    public class DishDTO
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public int? CategoryId { get; set; }

        public List<int>? IngredientIds { get; set; }
    }
}
