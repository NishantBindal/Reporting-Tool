using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models=ReportingTool.DataAccessLayer.Models;
using RepositoryTool.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportingTool.DataAccessLayer.EntityFramework.Configurations
{
    class VoucherTypeConfiguration : IEntityTypeConfiguration<Models.VoucherType>
    {
        public void Configure(EntityTypeBuilder<Models.VoucherType> builder)
        {
            builder.HasData(new Models.VoucherType() { VoucherTypeId = 1, Type = VoucherTypeConstant.Credit }
            ,new Models.VoucherType() { VoucherTypeId = 2, Type = VoucherTypeConstant.Debit });
        }
    }
}
