using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.UserDTOS;
using Api.Models;

namespace Api.Mappers
{
    public  static class UserMapper
    {
        public static UserResponse ToUserResponse (this User user)
        {
            return new UserResponse ()
            {
                UserName = user.UserName,
                Email = user.Email,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
            };
        }
    }
}