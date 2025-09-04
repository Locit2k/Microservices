using Core.Base;

namespace UserService.Domain.Entities
{
    public class Customers : BaseEntity
    {
        public Guid RecID { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime Birthday { get; set; } = DateTime.Now;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string UserID { get; set; } = string.Empty;
    }
}
