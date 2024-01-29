using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyRecipe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameFieldForUserState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FistName",
                table: "UserStates",
                newName: "FirstName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "UserStates",
                newName: "FistName");
        }
    }
}
