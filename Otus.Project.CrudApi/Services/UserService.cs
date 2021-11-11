using Microsoft.EntityFrameworkCore;
using Otus.Project.CrudApi.Model;
using Otus.Project.Domain.Model;
using Otus.Project.Orm.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task<List<UserVm>> GetUsers(CancellationToken ct)
        {
            return _userRepository.FindAll()
                .Select(u => u.ConvertToVm())
                .ToListAsync(ct);
        }

        public async Task<UserVm> GetUser(Guid userId, CancellationToken ct)
        {
            var existingUser = await _userRepository.FindByID(userId, ct);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("User is not found in a database!");
            }

            return existingUser.ConvertToVm();
        }

        public async Task<Guid> AddUser(UserModel user, CancellationToken ct)
        {
            var newUser = user.ConvertToModel();
            _userRepository.Add(newUser);
            await _userRepository.CommitChangesAsync(ct);
            return newUser.Id;
        }

        public async Task UpdateUser(Guid userId, UserModel user, CancellationToken ct)
        {
            var existingUser = await _userRepository.FindByID(userId, ct);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("User is not found in a database!");
            }

            user.ApplyChangesToExistingUser(existingUser);
            _userRepository.Update(existingUser);
            await _userRepository.CommitChangesAsync(ct);
        }

        public async Task DeleteUser(Guid userId, CancellationToken ct)
        {
            var existingUser = await _userRepository.FindByID(userId, ct);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("User is not found in a database!");
            }

            _userRepository.Delete(existingUser);
            await _userRepository.CommitChangesAsync(ct);
        }
    }
}
