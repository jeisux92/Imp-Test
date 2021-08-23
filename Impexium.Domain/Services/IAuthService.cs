using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Impexium.Domain.Services
{
    public interface IAuthService
    {
        Task<object> GetTokenAsync(IdentityUser user);
    }
}
