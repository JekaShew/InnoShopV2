using System.ComponentModel.DataAnnotations;

namespace UserManagement.Application.DTOs
{
    public class RegistrationInfoDTO
    {
        public Guid? Id { get; set; }
        [Required]
        public string FIO { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Required, MinLength(3)]
        public string Login { get; set; }
        [Required, MinLength(5)]
        public string Password { get; set; }
        [Required, MinLength(5)]
        public string SecretWord { get; set; }
    }
}
