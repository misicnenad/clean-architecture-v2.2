using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.Infrastructure.Models;

using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CleanArchitectureDbContext _dbContext;

        public UserRepository(CleanArchitectureDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> AddAsync(User user)
        {
            try
            {
                var newUser = await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return newUser.Entity;
            }
            catch 
            {
                return null;
            }
        }
    }
}
