using BHD.Application.Dtos.Security;
using BHD.Application.Dtos.User;
using BHD.Application.Repositories;

namespace BHD.Application.Services.Users
{
    public interface IUserService
    {
        CreatedUserDto Get(string Id);

        bool Login(LoginModel model);
        CreatedUserDto Create(UserDto model);
        CreatedUserDto Update(UserDto model);
        CreatedUserDto Delete(string Id);
    }
}
