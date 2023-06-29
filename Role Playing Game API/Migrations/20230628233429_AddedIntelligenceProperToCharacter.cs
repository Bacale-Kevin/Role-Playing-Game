using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Role_Playing_Game_API.Migrations
{
    /// <inheritdoc />
    public partial class AddedIntelligenceProperToCharacter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MyProperty",
                table: "Characters",
                newName: "Intelligence");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Intelligence",
                table: "Characters",
                newName: "MyProperty");
        }
    }
}
