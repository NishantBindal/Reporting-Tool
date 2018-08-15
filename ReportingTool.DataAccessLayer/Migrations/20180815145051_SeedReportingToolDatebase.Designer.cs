﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReportingTool.DataAccessLayer.EntityFramework;

namespace ReportingTool.DataAccessLayer.Migrations
{
    [DbContext(typeof(ReportingToolDbContext))]
    [Migration("20180815145051_SeedReportingToolDatebase")]
    partial class SeedReportingToolDatebase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReportingTool.DataAccessLayer.Models.Organization", b =>
                {
                    b.Property<int>("OrganizationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("varchar(500)");

                    b.Property<string>("GstNumber")
                        .HasColumnType("varchar(15)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("PanNumber")
                        .HasColumnType("varchar(10)");

                    b.HasKey("OrganizationId");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("ReportingTool.DataAccessLayer.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int?>("ProductCategoryId");

                    b.HasKey("ProductId");

                    b.HasIndex("ProductCategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ReportingTool.DataAccessLayer.Models.ProductCategory", b =>
                {
                    b.Property<int>("ProductCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("ProductCategoryId");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("ReportingTool.DataAccessLayer.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleType")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new { RoleId = 1, RoleType = "Admin" },
                        new { RoleId = 2, RoleType = "Buyer" },
                        new { RoleId = 3, RoleType = "Employee" },
                        new { RoleId = 4, RoleType = "Seller" }
                    );
                });

            modelBuilder.Entity("ReportingTool.DataAccessLayer.Models.Voucher", b =>
                {
                    b.Property<int>("VoucherId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount");

                    b.Property<int?>("CustomerOrganizationId");

                    b.Property<int?>("SoldByOrganizationId");

                    b.Property<DateTime>("VoucherDate");

                    b.Property<int?>("VoucherTypeId");

                    b.HasKey("VoucherId");

                    b.HasIndex("CustomerOrganizationId");

                    b.HasIndex("SoldByOrganizationId");

                    b.HasIndex("VoucherTypeId");

                    b.ToTable("Vouchers");
                });

            modelBuilder.Entity("ReportingTool.DataAccessLayer.Models.VoucherType", b =>
                {
                    b.Property<int>("VoucherTypeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("VoucherTypeId");

                    b.ToTable("VoucherTypes");

                    b.HasData(
                        new { VoucherTypeId = 1, Type = "Credit" },
                        new { VoucherTypeId = 2, Type = "Debit" }
                    );
                });

            modelBuilder.Entity("ReportingTool.DataAccessLayer.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("OrganizationId");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<int?>("RoleId");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("UserId");

                    b.HasIndex("OrganizationId")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ReportingTool.DataAccessLayer.VoucherDetail", b =>
                {
                    b.Property<int>("VoucherDetailId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ProductNameProductId");

                    b.Property<int>("Quantity");

                    b.Property<int?>("VoucherId1");

                    b.HasKey("VoucherDetailId");

                    b.HasIndex("ProductNameProductId");

                    b.HasIndex("VoucherId1");

                    b.ToTable("VoucherDetails");
                });

            modelBuilder.Entity("ReportingTool.DataAccessLayer.Models.Product", b =>
                {
                    b.HasOne("ReportingTool.DataAccessLayer.Models.ProductCategory", "ProductCategory")
                        .WithMany()
                        .HasForeignKey("ProductCategoryId");
                });

            modelBuilder.Entity("ReportingTool.DataAccessLayer.Models.Voucher", b =>
                {
                    b.HasOne("ReportingTool.DataAccessLayer.Models.Organization", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerOrganizationId");

                    b.HasOne("ReportingTool.DataAccessLayer.Models.Organization", "SoldBy")
                        .WithMany()
                        .HasForeignKey("SoldByOrganizationId");

                    b.HasOne("ReportingTool.DataAccessLayer.Models.VoucherType", "VoucherType")
                        .WithMany()
                        .HasForeignKey("VoucherTypeId");
                });

            modelBuilder.Entity("ReportingTool.DataAccessLayer.User", b =>
                {
                    b.HasOne("ReportingTool.DataAccessLayer.Models.Organization", "Organization")
                        .WithOne("ContactPerson")
                        .HasForeignKey("ReportingTool.DataAccessLayer.User", "OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ReportingTool.DataAccessLayer.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("ReportingTool.DataAccessLayer.VoucherDetail", b =>
                {
                    b.HasOne("ReportingTool.DataAccessLayer.Models.Product", "ProductName")
                        .WithMany()
                        .HasForeignKey("ProductNameProductId");

                    b.HasOne("ReportingTool.DataAccessLayer.Models.Voucher", "VoucherId")
                        .WithMany()
                        .HasForeignKey("VoucherId1");
                });
#pragma warning restore 612, 618
        }
    }
}
