using Microsoft.EntityFrameworkCore.Migrations;

namespace BaseCode.Data.Migrations
{
    public partial class highschool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_HighSchoolEducation_HighSchoolEducId",
                table: "Applicant");

            migrationBuilder.DropIndex(
                name: "IX_HighSchoolEducation_ApplicantId",
                table: "HighSchoolEducation");

            migrationBuilder.DropIndex(
                name: "IX_Applicant_HighSchoolEducId",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "HighSchoolEducId",
                table: "Applicant");

            migrationBuilder.CreateIndex(
                name: "IX_HighSchoolEducation_ApplicantId",
                table: "HighSchoolEducation",
                column: "ApplicantId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HighSchoolEducation_ApplicantId",
                table: "HighSchoolEducation");

            migrationBuilder.AddColumn<int>(
                name: "HighSchoolEducId",
                table: "Applicant",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_HighSchoolEducation_ApplicantId",
                table: "HighSchoolEducation",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_HighSchoolEducId",
                table: "Applicant",
                column: "HighSchoolEducId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_HighSchoolEducation_HighSchoolEducId",
                table: "Applicant",
                column: "HighSchoolEducId",
                principalTable: "HighSchoolEducation",
                principalColumn: "HighSchoolEducId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
