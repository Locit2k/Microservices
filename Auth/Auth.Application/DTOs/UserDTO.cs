namespace Auth.Application.DTOs
{
    public class UserDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime Birthday { get; set; } = DateTime.MinValue;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string UserID { get; set; } = string.Empty;
    }
}
