using DataLayer.Dtos;
using DataLayer.Entities;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Mapping
{
    public static class UserMappingExtensions
    {
        public static List<UserDto> ToUserDtos(this List<User> users)
        {
            var results = users.Select(e => e.ToUserDto()).ToList();

            return results;
        }

        public static UserDto ToUserDto(this User user)
        {
            if (user == null) return null;

            var result = new UserDto();
            result.Id = user.Id;
            result.Username = user.Username;
            result.Email = user.Email;
            result.RoleId = user.RoleId;
            result.Role = user.Role;
            
            return result;
        }
    }
}
