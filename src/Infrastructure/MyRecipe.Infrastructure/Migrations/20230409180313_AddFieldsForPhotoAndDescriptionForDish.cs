using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyRecipe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsForPhotoAndDescriptionForDish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Dishes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "DishPhotoGuid",
                table: "Dishes",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "DishPhotoGuid",
                table: "Dishes");
        }
    }
}
