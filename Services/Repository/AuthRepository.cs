using AutoMapper;
using Market.Data;
using Market.Dtos.User;
using Market.Services.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Market.Services.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        private readonly IConfiguration _config;


        public AuthRepository(UserManager<IdentityUser> userManager,
            IConfiguration configuration,
            SignInManager<IdentityUser> signInManager,
            AppDbContext db,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = configuration;
            _db = db;
            _mapper = mapper;
        }
        public async Task<bool> Register(UserDto user)
        {

            // Shater Notes --> Naming and Intersection
            var identityUser = new IdentityUser
            {
                UserName = user.email,
                Email = user.email,
            };
            var result = await _userManager.CreateAsync(identityUser, user.password);
            if (user.role == "user")
            {
                await _userManager.AddToRoleAsync(identityUser, "user");
            }else if(user.role == "merchant")
            {
                await _userManager.AddToRoleAsync(identityUser, "merchant");
            }
            else
            {
                return !result.Succeeded;
            }
            return result.Succeeded;

        }

        public async Task<bool> Login(LoginUserDto user)
        {
            //var identityUser = await _userManager.FindByEmailAsync(user.Email);

            var identityUser = await _signInManager.PasswordSignInAsync(user.Email, user.Password, false, lockoutOnFailure: false);
            if (identityUser is null || identityUser == SignInResult.Failed)
            {
                return false;
            }
            return true;
        }

        //Generate Token after Login
        public string GenerateToken(LoginUserDto userDto)
        {
            var user = _userManager.FindByEmailAsync(userDto.Email).GetAwaiter().GetResult();
            var roles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier , user.Id),
                new Claim(ClaimTypes.Name, user.Email),
                //new Claim(ClaimTypes.Role, role)

            };
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            //claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));

            var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(249), // 240 minutes to be added in AddMinutes(240)
                issuer: _config.GetSection("Jwt:Issuer").Value,
                audience: _config.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCred
                );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }


    }
}
