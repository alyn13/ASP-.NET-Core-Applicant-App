using Microsoft.EntityFrameworkCore.Migrations;

namespace BaseCode.Data.Migrations
{
    public partial class deleteUserIDPropinUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "User");

            migrationBuilder.AlterColumn<bool>(
                name: "CurrentlyWorking",
                table: "Experience",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "User",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CurrentlyWorking",
                table: "Experience",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(bool));
        }
    }
}
