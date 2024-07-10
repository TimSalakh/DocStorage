using System.Security.Claims;

namespace API.BLL.Services.Interfaces;

public interface ITokenService
{
    public string GenerateToken(IEnumerable<Claim> claims);
}
