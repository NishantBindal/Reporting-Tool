using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReportingTool.DataAccessLayer.Models;
using RepositoryTool.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportingTool.DataAccessLayer.EntityFramework.Configurations
{
    class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(new Role() { RoleId = 1, RoleType = RoleTypeConstant.Admin }
            ,new Role() { RoleId = 2, RoleType = RoleTypeConstant.Buyer }
            ,new Role() { RoleId = 3, RoleType = RoleTypeConstant.Employee }
            ,new Role() { RoleId = 4, RoleType = RoleTypeConstant.Seller });
        }
    }
}
