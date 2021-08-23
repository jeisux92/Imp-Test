using Impexium.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Impexium.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<object> GetTokenAsync(IdentityUser user)
        {
            int minutes = _configuration.GetValue<int>("ExpiryTime");
            IList<string> roles = await _userManager.GetRolesAsync(user);
            string keyConfiguration = _configuration.GetValue<string>("Key");
            string issuer = _configuration.GetValue<string>("Issuer");
            string audience = _configuration.GetValue<string>("Audience");

            var claims = new List<Claim>();

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyConfiguration));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer,
                                             audience,
                                             claims,
                                             expires: DateTime.UtcNow.AddMinutes(minutes),
                                             signingCredentials: creds);

            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new
            {
                access_token = accessToken,
                expires_in = DateTime.UtcNow.AddMinutes(minutes),
                token_type = "bearer"
            };
        }
    }
}
