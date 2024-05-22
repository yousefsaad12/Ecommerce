using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Interfaces
{
    public interface IUserInterface
    {
        public Task<User?> GetUser(string id);
        public Task<List<User>> GetUsers();
        public Task<User> UpdateUser(string id);
    }
}