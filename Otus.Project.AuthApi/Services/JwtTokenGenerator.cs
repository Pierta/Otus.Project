using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Otus.Project.AuthApi.Settings;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Otus.Project.AuthApi.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private const string IdClaim = "id";
        
        private readonly AppSettings _appSettings;

        public JwtTokenGenerator(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        
        public string GenerateJwtToken(Guid userId)
        {
            var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(IdClaim, userId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
