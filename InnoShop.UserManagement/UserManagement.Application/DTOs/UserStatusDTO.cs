using System.ComponentModel.DataAnnotations;

namespace UserManagement.Application.DTOs
{
    public class UserStatusDTO
    {
        public Guid? Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}
