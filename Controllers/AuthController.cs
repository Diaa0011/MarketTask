using AutoMapper;
using Market.Dtos.User;
using Market.Services.Repository;
using Market.Services.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        private readonly IAuthService _authService;

        public AuthController(IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<IdentityUser> userManager,
            IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("fix your data first!");
            }
            
            var newUser = await _authService.Register(user);

            return CreatedAtAction(nameof(RegisterUser), new {
                id = newUser.Id,
                email = newUser.Email,
                message = "Registration successful. Welcome to our eCommerce platform!"

            });
            
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _authService.Login(user);

            
            return result.Token != null
             ? Ok(result.Token) 
             : Unauthorized(result.ErrorMessage);
        }
    }
}
