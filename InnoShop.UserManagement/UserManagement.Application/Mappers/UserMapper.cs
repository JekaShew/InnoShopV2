using InnoShop.CommonLibrary.CommonDTOs;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.DTOs;
using UserManagement.Domain.Data.Models;

namespace UserManagement.Application.Mappers
{
    [Mapper]
    public static partial class UserMapper
    {
        [MapProperty([nameof(User.UserStatus), nameof(User.UserStatus.Id)],
            [nameof(UserDTO.UserStatus), nameof(UserDTO.UserStatus.Id)])]
        [MapProperty([nameof(User.UserStatus), nameof(User.UserStatus.Title)],
            [nameof(UserDTO.UserStatus), nameof(UserDTO.UserStatus.Text)])]

        [MapProperty([nameof(User.Role), nameof(User.Role.Id)],
            [nameof(UserDTO.Role), nameof(UserDTO.Role.Id)])]
        [MapProperty([nameof(User.Role), nameof(User.Role.Title)],
            [nameof(UserDTO.Role), nameof(UserDTO.Role.Text)])]
        public static partial UserDTO? UserToUserDTO(User? user);

        public static partial User? UserDTOToUser(UserDTO? userDTO);

        public static partial AuthorizationInfoDTO? UserToAuthorizationInfoDTO(User? user);

        public static partial LoginInfoDTO? UserToLoginInfoDTO(User? user);

        public static partial RegistrationInfoDTO? UserToRegistrationInfoDTO(User? user);

        public static partial UserDetailedDTO? RegistrationInfoDTOToUserDetailedDTO(RegistrationInfoDTO registrationInfoDTO);
        public static partial User? UserDetailedDTOToUser(UserDetailedDTO? userDetailedDTO);
    }
}
