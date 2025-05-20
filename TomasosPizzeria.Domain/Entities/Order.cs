using System.ComponentModel.DataAnnotations;

namespace TomasosPizzeria.Domain.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int TotalPrice { get; set; }
        public string Status { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public List<Dish> Dishes { get; set; }
    }
}
