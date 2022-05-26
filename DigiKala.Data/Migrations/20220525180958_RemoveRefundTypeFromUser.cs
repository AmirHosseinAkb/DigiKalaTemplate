using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiKala.Data.Migrations
{
    public partial class RemoveRefundTypeFromUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefundType",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "RefundType",
                table: "Users",
                type: "tinyint",
                nullable: true);
        }
    }
}
