using InnoShop.CommonLibrary.CommonDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.DTOs
{
    public class RefreshTokenDTO
    {
        public Guid Id { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime ExpireDate { get; set; }
        public Guid UserId { get; set; }
        public ParameterDTO? User { get; set; }
    }
}
