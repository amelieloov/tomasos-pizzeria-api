using System.ComponentModel.DataAnnotations;

namespace TomasosPizzeria.Domain.Entities
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        [Required]
        public Category Category { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
