using Market.Dtos.User;

namespace Market.Services.Repository.IRepository
{
    public interface IAuthRepository
    {
        Task<bool> Login(LoginUserDto user);
        Task<bool> Register(UserDto user);
        string GenerateToken(LoginUserDto user);
    }
}
