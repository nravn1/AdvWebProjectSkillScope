using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSCI3110KRTERMPROJ.Migrations
{
    /// <inheritdoc />
    public partial class Mig01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleSkill_RoleSkill_RoleSkillId",
                table: "RoleSkill");

            migrationBuilder.DropIndex(
                name: "IX_RoleSkill_RoleSkillId",
                table: "RoleSkill");

            migrationBuilder.DropColumn(
                name: "RoleSkillId",
                table: "RoleSkill");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleSkillId",
                table: "RoleSkill",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleSkill_RoleSkillId",
                table: "RoleSkill",
                column: "RoleSkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleSkill_RoleSkill_RoleSkillId",
                table: "RoleSkill",
                column: "RoleSkillId",
                principalTable: "RoleSkill",
                principalColumn: "Id");
        }
    }
}
