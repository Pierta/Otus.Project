using System.ComponentModel.DataAnnotations;

namespace Otus.Project.AuthApi.Model
{
    public class AuthenticateRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
