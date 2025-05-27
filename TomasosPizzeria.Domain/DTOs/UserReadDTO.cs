namespace TomasosPizzeria.Domain.DTOs
{
    public class UserReadDTO
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Role { get; set; }
        public int? BonusPoints { get; set; }
    }
}
