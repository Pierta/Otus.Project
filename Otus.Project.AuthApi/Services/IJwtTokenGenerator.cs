using System;

namespace Otus.Project.AuthApi.Services
{
    public interface IJwtTokenGenerator
    {
        string GenerateJwtToken(Guid userId);
    }
}
