using Api.Core.Dtos.UserDTOS;
using Api.Core.Models;

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