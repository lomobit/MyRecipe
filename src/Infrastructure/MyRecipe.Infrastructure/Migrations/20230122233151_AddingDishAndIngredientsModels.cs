using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyRecipe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingDishAndIngredientsModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dishes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dishes", x => x.Id);
                    table.UniqueConstraint("AK_Dishes_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Okeis",
                columns: table => new
                {
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ConventionDesignationNational = table.Column<string>(type: "text", nullable: true),
                    ConventionDesignationInternational = table.Column<string>(type: "text", nullable: true),
                    CodeDesignationNational = table.Column<string>(type: "text", nullable: true),
                    CodeDesignationInternational = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Okeis", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "IngredientsForDishes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IngredientId = table.Column<int>(type: "integer", nullable: false),
                    DishId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    OkeiCode = table.Column<string>(type: "text", nullable: false),
                    Condition = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientsForDishes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientsForDishes_Dishes_DishId",
                        column: x => x.DishId,
                        principalTable: "Dishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientsForDishes_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientsForDishes_Okeis_OkeiCode",
                        column: x => x.OkeiCode,
                        principalTable: "Okeis",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsForDishes_DishId",
                table: "IngredientsForDishes",
                column: "DishId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsForDishes_IngredientId",
                table: "IngredientsForDishes",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsForDishes_OkeiCode",
                table: "IngredientsForDishes",
                column: "OkeiCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientsForDishes");

            migrationBuilder.DropTable(
                name: "Dishes");

            migrationBuilder.DropTable(
                name: "Okeis");
        }
    }
}
