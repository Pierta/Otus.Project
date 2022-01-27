using Otus.Project.AuthApi.Model;
using Otus.Project.Domain.Model;
using Otus.Project.Orm.Repository;
using Polly;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.AuthApi.Services
{
    public class UserService : IUserService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IRepository<User, Guid> _userRepository;
        private readonly IBillingApiClient _billingApiClient;
        private readonly AsyncPolicy _asyncClientServicePolicy;

        public UserService(IJwtTokenGenerator jwtTokenGenerator,
            IRepository<User, Guid> userRepository,
            IBillingApiClient billingApiClient)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
            _billingApiClient = billingApiClient;

            _asyncClientServicePolicy = Policy
                .Handle<TaskCanceledException>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(3, retryAttempt)));
        }

        public async Task<(UserIdVm, string)> Register(UserModel model, CancellationToken ct)
        {
            var (newUser, errorMessage) = await RegisterInternal(model, ct);

            return (newUser, errorMessage);
        }

        public async Task<(UserIdVm, string)> RegisterAndCreateBillingAccount(UserModel model, CancellationToken ct)
        {
            var (newUser, errorMessage) = await RegisterInternal(model, ct);

            // Create billing account:
            // 3 retry attempts will be performed
            await _asyncClientServicePolicy.ExecuteAsync(async () =>
            {
                await _billingApiClient.CreateNewBillingAccount(newUser.Id, ct);
            });

            return (newUser, errorMessage);
        }

        private async Task<(UserIdVm, string)> RegisterInternal(UserModel model, CancellationToken ct)
        {
            var newUser = model.ConvertToDomainModel();
            if (string.IsNullOrEmpty(newUser.Email) || string.IsNullOrEmpty(newUser.Password))
            {
                return (null, "Email and Password must be provided!");
            }

            var existingUser = await _userRepository.FindByExpression(u => u.Email.ToLower() == newUser.Email.ToLower(), ct);
            if (existingUser != null)
            {
                return (null, "There is another existing user with such an Email!");
            }

            _userRepository.Add(newUser);
            await _userRepository.CommitChangesAsync(ct);

            return (new UserIdVm { Id = newUser.Id }, null);
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, CancellationToken ct)
        {
            var user = await _userRepository.FindByExpression(x => 
                x.Email == model.Email && 
                x.Password == model.Password, ct);
            if (user == null) return null;

            var token = _jwtTokenGenerator.GenerateJwtToken(user.Id);

            return new AuthenticateResponse { Token = token };
        }
    }
}
