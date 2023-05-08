using Microsoft.EntityFrameworkCore.Migrations;

namespace BaseCode.Data.Migrations
{
    public partial class deletefieldinwebsite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Website_Website_WebsiteId1",
                table: "Website");

            migrationBuilder.DropIndex(
                name: "IX_Website_WebsiteId1",
                table: "Website");

            migrationBuilder.DropColumn(
                name: "WebsiteId1",
                table: "Website");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WebsiteId1",
                table: "Website",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Website_WebsiteId1",
                table: "Website",
                column: "WebsiteId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Website_Website_WebsiteId1",
                table: "Website",
                column: "WebsiteId1",
                principalTable: "Website",
                principalColumn: "WebsiteId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
