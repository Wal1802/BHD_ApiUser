using BHD.Application.Repositories;
using BHD.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace BHD.Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BHDContext context) : base(context)
        {

        }
        public User Create(User user)
        {
            user.LastLogin = DateTime.Now;
            user.IsActive = true;
            return Add(user);
        }

        public bool EmailExists(string email) => Exists(x => x.Email == email);
       
    }
}
