using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyRecipe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteAlternativeKeyFromIngredient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Ingredients_Name",
                table: "Ingredients");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Ingredients_Name",
                table: "Ingredients",
                column: "Name");
        }
    }
}
