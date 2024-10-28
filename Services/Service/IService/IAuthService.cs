using Market.Dtos.User;
using Microsoft.AspNetCore.Identity;

namespace Market.Services.Repository.IRepository
{
    public interface IAuthService
    {
        Task<IdentityUser> Register(UserDto user);
        Task<(string? Token,string? ErrorMessage)> Login(LoginUserDto user);
    }
}