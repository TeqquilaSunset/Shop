using AuthenticatorAPI.Models;
using System.Security.Claims;

namespace AuthenticatorAPI.Services
{
    public interface ITokenService
    {
        public void SetRefreshToken(User user, RefreshToken newRefreshToken);
        public RefreshToken GenerateRefreshToken();
        public string CreateToken(User user);
        public ClaimsPrincipal? GetPrincipalFromExpiredToken(IConfiguration configuration, string? token);

    }
}
