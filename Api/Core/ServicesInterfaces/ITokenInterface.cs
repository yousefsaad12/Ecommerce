using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Identity;

namespace Api.Core
{
    public interface ITokenInterface
    {
        public string GenerateJwtToke(AppUser appUser, string role);
    }
}