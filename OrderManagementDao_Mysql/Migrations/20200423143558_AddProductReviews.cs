using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderManagementDao_Mysql.Migrations
{
    public partial class AddProductReviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Authority_Account",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Authority_Account");
        }
    }
}
