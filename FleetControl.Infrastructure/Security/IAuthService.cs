using FleetControl.Core.Enums.User;

namespace FleetControl.Infrastructure.Security
{
    public interface IAuthService
    {
        string ComputeHash(string password);
        string GenerateToken(string email, Role role, string name);
        bool VerifyPassword(string password, string storedHash);
    }
}
