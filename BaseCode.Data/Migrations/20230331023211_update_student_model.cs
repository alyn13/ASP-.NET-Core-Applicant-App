using Microsoft.EntityFrameworkCore.Migrations;

namespace BaseCode.Data.Migrations
{
    public partial class update_student_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudentName2",
                table: "Student",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentName3",
                table: "Student",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentName2",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "StudentName3",
                table: "Student");
        }
    }
}
