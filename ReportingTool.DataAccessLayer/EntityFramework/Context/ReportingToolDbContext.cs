using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReportingTool.DataAccessLayer.EntityFramework.Configurations;
using ReportingTool.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportingTool.DataAccessLayer.EntityFramework
{
    public class ReportingToolDbContext:DbContext
    {
        public ReportingToolDbContext(DbContextOptions options):base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new VoucherTypeConfiguration());
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<VoucherDetail> VoucherDetails { get; set; }
        public DbSet<VoucherType> VoucherTypes { get; set; }
    }
}
