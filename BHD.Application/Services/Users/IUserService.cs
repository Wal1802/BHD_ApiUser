using BHD.Application.Dtos.User;

namespace BHD.Application.Services.Users
{
    public interface IUserService
    {
        CreatedUserDto Get(string Id);
        CreatedUserDto Create(UserDto model);
        CreatedUserDto Update(UserDto model);
        CreatedUserDto Delete(string Id);
    }
}
