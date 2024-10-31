using BHD.Models.Models;

namespace BHD.Application.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User Create(User user);

        bool EmailExists(string email);
    }
}
