using His.Reception.DTO.User;
using His.Reception.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace His.Reception.Application.Mapper
{
    public class UserMapper
    {
        public static Users Map(UserDto userDto)
        {
            Users user = new Users();

            user.UserName = userDto.UserName;
            user.UserName = userDto.Password;
            user.PersonId = userDto.PersonId;
            user.IsLimitByIp = userDto.IsLimitByIp;

            return user;
        }
    }
}
