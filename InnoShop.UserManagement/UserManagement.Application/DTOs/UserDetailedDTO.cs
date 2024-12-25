using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.DTOs
{
    public class UserDetailedDTO
    {
        public Guid? Id { get; set; }
        [Required]
        public string FIO { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Required, MinLength(3)]
        public string Login { get; set; }
        [Required, MinLength(5)]
        public string PasswordHash { get; set; }
        [Required]
        public string SecurityStamp { get; set; }
        [Required]
        public string SecretWordHash { get; set; }
        public DateTime RegisterDate { get; set; }
        [Required]
        public Guid RoleId { get; set; }
        [Required]
        public Guid UserStatusId { get; set; }
    }
}
