using DatingApp.API.Entities;
using DatingApp.API.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DatingApp.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            string? tokenKey = config["TokenKey"];
            _key = new(Encoding.UTF8.GetBytes(tokenKey!));
        }

        public string CreateToken(AppUser user)
        {
            try
            {
                List<Claim> claims =
                [
                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
                ];

                SigningCredentials credentials = new(_key, SecurityAlgorithms.HmacSha512Signature);
                SecurityTokenDescriptor tokenDescriptor = new()
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(7),
                    SigningCredentials = credentials
                };

                JwtSecurityTokenHandler tokenHandler = new();
                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);

            }catch (Exception ex)
            {
                throw;
            }
        }
    }
}
