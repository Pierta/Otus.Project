using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Otus.Project.AuthApi.Model;
using Otus.Project.AuthApi.Settings;
using Otus.Project.Domain.Model;
using Otus.Project.Orm.Repository;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.AuthApi.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly IRepository<User, Guid> _userRepository;

        public UserService(IOptions<AppSettings> appSettings,
            IRepository<User, Guid> userRepository)
        {
            _appSettings = appSettings.Value;
            _userRepository = userRepository;
        }

        public async Task<(Guid?, string)> Register(UserModel model, CancellationToken ct)
        {
            var newUser = model.ConvertToDomainModel();
            if (string.IsNullOrEmpty(newUser.Email) || string.IsNullOrEmpty(newUser.Password))
            {
                return (null, "Email and Password must be provided!");
            }

            var existingUser = await _userRepository.FindByExpression(u => u.Email.ToLower() == newUser.Email.ToLower());
            if (existingUser != null)
            {
                return (null, "There is another existing user with such an Email!");
            }
            
            _userRepository.Add(newUser);
            await _userRepository.CommitChangesAsync(ct);
            return (newUser.Id, null);
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, CancellationToken ct)
        {
            var user = await _userRepository.FindByExpression(x => 
                x.Email == model.Email && 
                x.Password == model.Password);
            if (user == null) return null;

            var token = GenerateJwtToken(user);

            return new AuthenticateResponse { Token = token };
        }

        private string GenerateJwtToken(User user)
        {
            var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
