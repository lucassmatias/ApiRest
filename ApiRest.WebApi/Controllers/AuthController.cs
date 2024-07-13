using ApiRest.Services;
using ApiRest.WebApi.Configuration;
using ApiRest.WebApi.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        ITokenHandlerService _service;
        public AuthController(UserManager<IdentityUser> manager, ITokenHandlerService service)
        {
            _userManager = manager;
            _service = service;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequestDTO user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if(existingUser != null) return BadRequest("Existing email");
                var isCreated = await _userManager.CreateAsync(new IdentityUser() { Email = user.Email, UserName = user.Email }, user.Password);
                if (isCreated.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(isCreated.Errors.Select(x => x.Description).ToList());
                }
            }
            else
            {
                return BadRequest("User registering error");
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequestDTO user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if(existingUser == null) 
                {
                    return BadRequest(new LoginUserResponseDTO()
                    {
                        Login = false,
                        Errors = new List<string>()
                        {
                            "Usuario o contraseña incorrecto."
                        }
                    });
                }
                else
                {
                    var IsCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);
                    if (IsCorrect)
                    {
                        var pars = new TokenParameters()
                        {
                            Id = existingUser.Id,
                            PasswordHash = existingUser.PasswordHash,
                            UserName = existingUser.UserName
                        };
                        var jwtToken = _service.GenerateJwtToken(pars);
                        return Ok(new LoginUserResponseDTO()
                        {
                            Login = true,
                            Token = jwtToken
                        });
                    }
                    else
                    {
                        return BadRequest(new LoginUserResponseDTO()
                        {
                            Login = false,
                            Errors = new List<string>()
                        {
                            "Usuario o contraseña incorrecto."
                        }
                        });
                    }
                }
            }
            else
            {
                return BadRequest(new LoginUserResponseDTO()
                {
                    Login = false,
                    Errors = new List<string>()
                        {
                            "Usuario o contraseña incorrecto."
                        }
                });
            }
        }
    }
}
