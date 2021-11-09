using Otus.Project.Domain.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.CrudApi.Services
{
    public interface IUserService
    {
        Task<List<User>> GetUsers(CancellationToken ct);
    }
}
