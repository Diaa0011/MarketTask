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
        private readonly IAuthRepository _authRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<IdentityUser> userManager,
            IAuthRepository authRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _authRepository = authRepository;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("fix your data first!");
            }
            if (await _authRepository.Register(user))
            {
                return Ok("User Added Successfully");
            }
            return BadRequest("Something Went Wrong");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (await _authRepository.Login(user))
            {
                var tokenString = _authRepository.GenerateToken(user);
                return Ok(tokenString);
            }
            return Unauthorized("Wrong email or password!");

        }
    }
}
