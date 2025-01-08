namespace UserManagement.Application.DTOs
{
    public class AuthorizationInfoDTO
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string SecurityStamp { get; set; }
        public string PasswordHash { get; set; }
        public string SecretWordHash { get; set; }

    }
}
