using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Paycompute.Persistence.Migrations
{
    public partial class FullNameColumnPaymentRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "HourlyRate",
                table: "PaymentRecords",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "PaymentRecords",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "PaymentRecords");

            migrationBuilder.AlterColumn<decimal>(
                name: "HourlyRate",
                table: "PaymentRecords",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");
        }
    }
}
