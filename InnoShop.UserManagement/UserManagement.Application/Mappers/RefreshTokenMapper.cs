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
    public static partial class RefreshTokenMapper
    {
        [MapProperty([nameof(RefreshToken.User), nameof(RefreshToken.User.Id)],
            [nameof(RefreshTokenDTO.User), nameof(RefreshTokenDTO.User.Id)])]
        [MapProperty([nameof(RefreshToken.User), nameof(RefreshToken.User.Login)],
            [nameof(RefreshTokenDTO.User), nameof(RefreshTokenDTO.User.Text)])]
        public static partial RefreshTokenDTO? RefreshTokenToRefreshTokenDTO(RefreshToken? refreshToken);

        public static partial RefreshToken? RefreshTokenDTOToRefreshToken(RefreshTokenDTO? refreshTokenDTO);
    }
}
