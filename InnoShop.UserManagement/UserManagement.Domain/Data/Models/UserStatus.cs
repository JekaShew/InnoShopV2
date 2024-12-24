﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.Data.Models
{
    public class UserStatus
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}
