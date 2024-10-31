using BHD.Models.Models;

namespace BHD.Application.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User Create(User user);
        User GetByEmail(string email);
        bool EmailExists(string email);
    }
}
