using Microsoft.EntityFrameworkCore.Migrations;

namespace ReportingTool.DataAccessLayer.Migrations
{
    public partial class SeedReportingToolDatebase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleType" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Buyer" },
                    { 3, "Employee" },
                    { 4, "Seller" }
                });

            migrationBuilder.InsertData(
                table: "VoucherTypes",
                columns: new[] { "VoucherTypeId", "Type" },
                values: new object[,]
                {
                    { 1, "Credit" },
                    { 2, "Debit" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "VoucherTypes",
                keyColumn: "VoucherTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VoucherTypes",
                keyColumn: "VoucherTypeId",
                keyValue: 2);
        }
    }
}
