﻿using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Infrastructure.Data
{
    public class ProductManagementDBContext(DbContextOptions<ProductManagementDBContext> options):DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductStatus> ProductStatuses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
    }
}
