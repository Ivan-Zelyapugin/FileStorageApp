using FileStorageApp.Application.Interfaces;
using FileStorageApp.Application.Users.Login;
using FileStorageApp.Application.Users.Register;
using Microsoft.AspNetCore.Mvc;

namespace FileStorageApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;
        public UserController(IUser user)
        {
            _user = user;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> LogUserIn(LoginDTO loginDTO)
        {
            var result = await _user.LoginUserAsync(loginDTO);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<LoginResponse>> RegisterUser(RegisterUserDTO registerUserDTO)
        {
            var result = await _user.RegisterUserAsync(registerUserDTO);
            return Ok(result);
        }
    }
}
