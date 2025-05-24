using System.ComponentModel.DataAnnotations;

namespace TomasosPizzeria.Domain.Entities
{
    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public ICollection<Dish> Dishes { get; set; }
    }
}
