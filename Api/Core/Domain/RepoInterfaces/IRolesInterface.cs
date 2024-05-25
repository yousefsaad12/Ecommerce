using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.Domain
{
    public interface IRolesInterface
    {
        public Task<bool>SeedingRoles();
    }
}