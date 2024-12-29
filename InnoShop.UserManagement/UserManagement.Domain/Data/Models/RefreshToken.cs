﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.Data.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime ExpireDate { get; set; }
        public Guid UserId { get; set; }

        public User? User { get; set; }
    }
}
