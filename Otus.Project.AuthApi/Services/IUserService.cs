using Otus.Project.AuthApi.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.AuthApi.Services
{
    public interface IUserService
    {
        Task<(Guid?, string)> Register(UserModel model, CancellationToken ct);

        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, CancellationToken ct);
    }
}
