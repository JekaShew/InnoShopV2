using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.Data.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FIO { get; set; }
        public string? Email { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string SecretWord { get; set; }
        public DateTime RegisterDate { get; set; }
        public Guid RoleId { get; set; }
        public Role? Role { get; set; }

        public Guid UserStatusId { get; set; }
        public UserStatus? UserStatus { get; set; }

        public List<RefreshToken>? RefreshTokens { get; set; }

    }
}
