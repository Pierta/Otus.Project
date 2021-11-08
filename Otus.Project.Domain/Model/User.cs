using System;

namespace Otus.Project.Domain.Model
{
    public class User
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string Email { get; set; }

        public string CellPhone { get; set; }

        public string Password { get; set; }

        public bool IsEmailNotificationEnabled { get; set; }
    }
}
