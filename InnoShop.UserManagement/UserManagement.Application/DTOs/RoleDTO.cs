﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.DTOs
{
    public class RoleDTO
    {
        public Guid? Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}
