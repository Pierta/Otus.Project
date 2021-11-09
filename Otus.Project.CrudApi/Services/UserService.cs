using Microsoft.EntityFrameworkCore;
using Otus.Project.Domain.Model;
using Otus.Project.Orm.Repository;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.CrudApi.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User, Guid> _userRepository;

        public UserService(IRepository<User, Guid> userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<List<User>> GetUsers(CancellationToken ct)
        {
            return _userRepository.FindAll()
                .ToListAsync(ct);
        }
    }
}
