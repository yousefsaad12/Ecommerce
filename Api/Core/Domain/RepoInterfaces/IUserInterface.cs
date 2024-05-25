using Api.Core.Models;

namespace Api.Core.Domain
{
    public interface IUserInterface
    {
        public Task<User?> GetUser(string id);
        public Task<List<User>> GetUsers();
        public Task<User> UpdateUser(string id);
    }
}