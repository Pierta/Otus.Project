using Otus.Project.Domain.Model;

namespace Otus.Project.CrudApi.Model
{
    public static class MappingExtensions
    {
        public static UserVm ConvertToVm(this User user)
        {
            return new UserVm
            {
                Id = user.Id,
                CreatedDate = user.CreatedDate,
                UpdatedDate = user.UpdatedDate,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                Email = user.Email,
                CellPhone = user.CellPhone,
                IsEmailNotificationEnabled = user.IsEmailNotificationEnabled
            };
        }

        public static User ConvertToModel(this UserModel user)
        {
            return new User
            {
                CreatedDate = user.CreatedDate,
                UpdatedDate = user.UpdatedDate,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                Email = user.Email,
                CellPhone = user.CellPhone,
                Password = user.Password,
                IsEmailNotificationEnabled = user.IsEmailNotificationEnabled
            };
        }

        public static void ApplyChangesToExistingUser(this UserModel updatedUser, User existingUser)
        {
            existingUser.CreatedDate = updatedUser.CreatedDate;
            existingUser.UpdatedDate = updatedUser.UpdatedDate;
            existingUser.FirstName = updatedUser.FirstName;
            existingUser.LastName = updatedUser.LastName;
            existingUser.MiddleName = updatedUser.MiddleName;
            existingUser.Email = updatedUser.Email;
            existingUser.CellPhone = updatedUser.CellPhone;
            existingUser.Password = updatedUser.Password;
            existingUser.IsEmailNotificationEnabled = updatedUser.IsEmailNotificationEnabled;
        }
    }
}
