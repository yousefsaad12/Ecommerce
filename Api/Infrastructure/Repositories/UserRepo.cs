using Api.Core.Domain;
using Api.Core.Models;
using Api.Infrastructer.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public class UserRepo : IUserInterface
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public UserRepo(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            
        }
        public async Task<User?> GetUser(string id)
        {       
            if(id == null)
                return null;

            return await _applicationDbContext.User.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _applicationDbContext.User.ToListAsync();
        }

        public Task<User> UpdateUser(string id)
        {
            throw new NotImplementedException();
        }
    }
}