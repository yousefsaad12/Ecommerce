using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Api.Interfaces
{
    public interface ITokenService
    {
        public string GenerateJwtToke<TUser>(TUser user, string role) where TUser : IdentityUser;
    }
}