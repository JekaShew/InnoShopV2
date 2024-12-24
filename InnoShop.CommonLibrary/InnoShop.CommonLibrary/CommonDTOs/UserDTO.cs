using System.ComponentModel.DataAnnotations;

namespace InnoShop.CommonLibrary.CommonDTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string FIO { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Required, MinLength(3)]
        public string Login { get; set; }
        public string PasswordHash { get; set; }

        public DateTime RegisterDate { get; set; }

        public Guid RoleId { get; set; }
        public ParameterDTO? Role { get; set; }

        public Guid UserStatusId { get; set; }
        public ParameterDTO? UserStatus { get; set; }
    }
}
