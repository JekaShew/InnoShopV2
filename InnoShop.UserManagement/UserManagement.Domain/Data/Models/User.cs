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
        public string SecretWordHash { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.UtcNow;
        public Guid RoleId { get; set; }
        public Role? Role { get; set; }

        public Guid UserStatusId { get; set; }
        public UserStatus? UserStatus { get; set; }

        public List<RefreshToken>? RefreshTokens { get; set; }

    }
}
