using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Role_Playing_Game_API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCharacterSkillAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterSkill_Skills_skillsId",
                table: "CharacterSkill");

            migrationBuilder.RenameColumn(
                name: "skillsId",
                table: "CharacterSkill",
                newName: "SkillsId");

            migrationBuilder.RenameIndex(
                name: "IX_CharacterSkill_skillsId",
                table: "CharacterSkill",
                newName: "IX_CharacterSkill_SkillsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterSkill_Skills_SkillsId",
                table: "CharacterSkill",
                column: "SkillsId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterSkill_Skills_SkillsId",
                table: "CharacterSkill");

            migrationBuilder.RenameColumn(
                name: "SkillsId",
                table: "CharacterSkill",
                newName: "skillsId");

            migrationBuilder.RenameIndex(
                name: "IX_CharacterSkill_SkillsId",
                table: "CharacterSkill",
                newName: "IX_CharacterSkill_skillsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterSkill_Skills_skillsId",
                table: "CharacterSkill",
                column: "skillsId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
