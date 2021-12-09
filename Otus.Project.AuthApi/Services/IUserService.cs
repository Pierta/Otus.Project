using Otus.Project.AuthApi.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.AuthApi.Services
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, CancellationToken ct);
    }
}
