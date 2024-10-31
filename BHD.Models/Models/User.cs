using BHD.Domain.Models;

namespace BHD.Models.Models
{
    public class User: Entity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
        public string Token { get; set; }
        public bool IsActive { get; set; }
    }
}
