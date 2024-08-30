﻿using API.BLL.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.BLL.Services.Implementations;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(IEnumerable<Claim> claims)
    {
        var jwtConfig = _configuration.GetSection("Jwt");
        var signingKey = jwtConfig.GetValue<string>("SigningKey");
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey!));

        var singningCreds = new SigningCredentials(securityKey,
            SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddHours(1),
            SigningCredentials = singningCreds,
            Issuer = _configuration.GetSection("Jwt:Issuer").Value!,
            Audience = _configuration.GetSection("Jwt:Audience").Value!
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
