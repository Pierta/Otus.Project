using Otus.Project.Domain.Model;
using System;

namespace Otus.Project.AuthApi.Model
{
    public static class MappingExtensions
    {
        public static User ConvertToDomainModel(this UserModel user)
        {
            return new User
            {
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                Email = user.Email,
                CellPhone = user.CellPhone,
                Password = user.Password,
                IsEmailNotificationEnabled = user.IsEmailNotificationEnabled
            };
        }
    }
}
