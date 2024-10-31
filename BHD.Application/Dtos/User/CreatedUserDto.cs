namespace BHD.Application.Dtos.User
{
    public class CreatedUserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public DateTime Last_Login { get; set; }
        public string Token { get; set; }
        public bool IsActive { get; set; }
    }
}
