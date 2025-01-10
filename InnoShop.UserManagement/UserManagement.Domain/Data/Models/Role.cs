namespace UserManagement.Domain.Data.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}
