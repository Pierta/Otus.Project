using Otus.Project.CrudApi.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.CrudApi.Services
{
    public interface IUserService
    {
        Task<List<UserVm>> GetUsers(CancellationToken ct);

        Task<UserVm> GetUser(Guid userId, CancellationToken ct);

        Task<Guid> AddUser(UserModel user, CancellationToken ct);

        Task UpdateUser(Guid userId, UserModel user, CancellationToken ct);

        Task DeleteUser(Guid userId, CancellationToken ct);
    }
}
