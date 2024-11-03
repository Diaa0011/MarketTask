using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Market.Dtos.User;
using Market.Model;
using Market.Services.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Model.Client;

namespace Market.Services.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _config;

        public AuthService(IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration config)
        {
            _unitOfWork = unitOfWork;
           // _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        public async Task<IdentityUser> Register(UserDto user)
        {
            if (user.role == "merchant")
            {
                var merchantToCreate = new Merchant
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.email,
                    Email = user.email,
                    Role = user.role // This might need to be adjusted if Role is handled differently
                };

                var result = await _userManager.CreateAsync(merchantToCreate, user.password);
                
                if (result.Succeeded)
                {
                    // The role assignment may be redundant since you're already creating a Merchant instance
                    await _userManager.AddToRoleAsync(merchantToCreate, "merchant");

                    Console.WriteLine($"id {merchantToCreate.Id}, email {merchantToCreate.Email} and he is a {merchantToCreate.Role}");
                    return merchantToCreate;
                }
            }
            else
            {
                var clientToCreate = new Client
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.email,
                    Email = user.email,
                    Role = user.role // Adjust if necessary
                };

                var result = await _userManager.CreateAsync(clientToCreate, user.password);
                
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(clientToCreate, "client");

                    Console.WriteLine($"id {clientToCreate.Id}, email {clientToCreate.Email} and he is a {clientToCreate.Role}");
                    return clientToCreate;
                }

            }
             return null;
        }

        public async Task<(string? Token,string? ErrorMessage)> Login(LoginUserDto user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return (null, "Email and password are required.");
            }
            var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, false, false);
            if(result.Succeeded)
            {
                var token = GenerateToken(user);
                return (token, null);
            }else{
                return (null, "Invalid email or password.");
            }   
            
        }

        private string GenerateToken(LoginUserDto userDto)
        {
            var user = _userManager.FindByEmailAsync(userDto.Email).GetAwaiter().GetResult();
            var roles = _userManager.GetRolesAsync(user).GetAwaiter().GetResult();
            //var role = _userManager.GetRoleAsync(user);
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