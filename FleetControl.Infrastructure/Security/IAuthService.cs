namespace FleetControl.Infrastructure.Security
{
    public interface IAuthService
    {
        string ComputeHash(string password);
        string GenerateToken(string email, string role, string name);
    }
}
