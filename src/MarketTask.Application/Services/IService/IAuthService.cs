using MarketTask.Application.Dtos.User;
using Microsoft.AspNetCore.Identity;

namespace MarketTask.Application.Services.IService
{
    public interface IAuthService
    {
        Task<IdentityUser> Register(UserDto user);
        Task<(string? Token,string? ErrorMessage)> Login(LoginUserDto user);
    }
}