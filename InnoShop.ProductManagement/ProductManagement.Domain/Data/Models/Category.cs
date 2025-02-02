﻿namespace ProductManagement.Domain.Data.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public List<SubCategory>? SubCategories { get; set; }
    }
}
