using FleetControl.Core.Entities;
using FleetControl.Core.Interfaces.Generic;

namespace FleetControl.Infrastructure.Persistence.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetUserDetailsLogin(string email, string passwordHash);
    }
}
