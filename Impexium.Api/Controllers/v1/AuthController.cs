using Impexium.Api.ViewModels;
using Impexium.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Impexium.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public AuthController(SignInManager<IdentityUser> signInManager,
                            UserManager<IdentityUser> userManager,
                            IConfiguration configuration,
                            IAuthService authService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AuthAsync(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, lockoutOnFailure: true);

            string error = string.Empty;

            if (result.Succeeded)
            {
                IdentityUser user = await _userManager.FindByEmailAsync(model.UserName);
                var tokenGenerated = await _authService.GetTokenAsync(user);
                return Ok(tokenGenerated);
            }
            error = result.IsLockedOut ? "User account locked out." : "Incorrect Password";
            return BadRequest(error);
        }




        [HttpGet]
        public IActionResult Get() => Ok(new { hola = "Hello man" });
    }
}
