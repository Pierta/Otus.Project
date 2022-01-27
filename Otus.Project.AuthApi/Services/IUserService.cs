using Otus.Project.AuthApi.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.AuthApi.Services
{
    public interface IUserService
    {
        Task<(UserIdVm, string)> Register(UserModel model, CancellationToken ct);

        Task<(UserIdVm, string)> RegisterAndCreateBillingAccount(UserModel model, CancellationToken ct);

        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, CancellationToken ct);
    }
}
