using Microsoft.EntityFrameworkCore.Migrations;

namespace BaseCode.Data.Migrations
{
    public partial class updatefieldinapplicant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicant_Job_JobId",
                table: "Applicant");

            migrationBuilder.DropIndex(
                name: "IX_Applicant_JobId",
                table: "Applicant");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Applicant");

            migrationBuilder.AddColumn<string>(
                name: "JobApplied",
                table: "Applicant",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobApplied",
                table: "Applicant");

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "Applicant",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Applicant_JobId",
                table: "Applicant",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicant_Job_JobId",
                table: "Applicant",
                column: "JobId",
                principalTable: "Job",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
