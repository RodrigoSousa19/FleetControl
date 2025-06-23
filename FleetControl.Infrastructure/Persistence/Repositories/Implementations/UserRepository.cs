using FleetControl.Core.Entities;
using FleetControl.Infrastructure.Persistence.Repositories.Generic;
using FleetControl.Infrastructure.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FleetControl.Infrastructure.Persistence.Repositories.Implementations
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(FleetControlDbContext context) : base(context)
        { }

        public async Task<User?> GetUserDetailsLogin(string email)
        {
            return await _dataSet.Where(u => u.Email.Equals(email)).FirstOrDefaultAsync();
        }
    }
}
