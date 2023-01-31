using Microsoft.EntityFrameworkCore.Migrations;
using MyRecipe.Infrastructure.Helpers;

#nullable disable

namespace MyRecipe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdditingMainOkeiCodes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = MigrationHelper.GetSqlForMigrationFromFile("20230124_AdditingMainOkeiCodes.sql");
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = MigrationHelper.GetSqlForMigrationFromFile("20230124_RemovingMainOkeiCodes.sql");
            migrationBuilder.Sql(sql);
        }
    }
}
