using System.ComponentModel.DataAnnotations;

namespace UserManagement.Application.DTOs
{
    public class LoginInfoDTO
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
