using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyRecipe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEnittiesForEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_MealSchedules_MealScheduleId",
                table: "Meals");

            migrationBuilder.DropTable(
                name: "MealSchedules");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "StartAt",
                table: "Meals");

            migrationBuilder.RenameColumn(
                name: "MealScheduleId",
                table: "Meals",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Meals_MealScheduleId",
                table: "Meals",
                newName: "IX_Meals_EventId");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "Meals",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_Events_EventId",
                table: "Meals",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_Events_EventId",
                table: "Meals");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Meals");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Meals",
                newName: "MealScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_Meals_EventId",
                table: "Meals",
                newName: "IX_Meals_MealScheduleId");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Meals",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartAt",
                table: "Meals",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MealSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealSchedules", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_MealSchedules_MealScheduleId",
                table: "Meals",
                column: "MealScheduleId",
                principalTable: "MealSchedules",
                principalColumn: "Id");
        }
    }
}
