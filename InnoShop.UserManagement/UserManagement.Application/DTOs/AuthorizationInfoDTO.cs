using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.DTOs
{
    public class AuthorizationInfoDTO
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string SecurityStamp { get; set; }
    }
}
