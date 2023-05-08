using Microsoft.EntityFrameworkCore.Migrations;

namespace BaseCode.Data.Migrations
{
    public partial class updateapplicantModelskill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skill_Skill_SkillId1",
                table: "Skill");

            migrationBuilder.DropIndex(
                name: "IX_Skill_SkillId1",
                table: "Skill");

            migrationBuilder.DropColumn(
                name: "SkillId1",
                table: "Skill");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SkillId1",
                table: "Skill",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skill_SkillId1",
                table: "Skill",
                column: "SkillId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Skill_Skill_SkillId1",
                table: "Skill",
                column: "SkillId1",
                principalTable: "Skill",
                principalColumn: "SkillId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
