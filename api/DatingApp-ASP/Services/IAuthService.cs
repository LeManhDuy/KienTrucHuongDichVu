using DatingApp.API.DTOs;
using DatingApp_ASP.DTOs;

namespace DatingApp_ASP.Services
{
    public interface IAuthService
    {
        public string Login(AuthUserDto authUserDto);
        public string Register(RegisterUserDto authUserDto);
    }
}