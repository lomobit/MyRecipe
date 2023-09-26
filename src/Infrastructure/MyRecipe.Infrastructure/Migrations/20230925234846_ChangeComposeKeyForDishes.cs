using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyRecipe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeComposeKeyForDishes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Dishes_Name",
                table: "Dishes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Dishes_Name",
                table: "Dishes",
                column: "Name");
        }
    }
}
