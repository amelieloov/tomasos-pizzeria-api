using System.ComponentModel.DataAnnotations;

namespace TomasosPizzeria.Domain.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public List<Dish> Dishes { get; set; }
    }
}
