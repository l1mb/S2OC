using Microsoft.EntityFrameworkCore.Migrations;

namespace HabitCracker.Migrations
{
    public partial class personEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HabitProgress_Habits_HabitId",
                table: "HabitProgress");

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_PersonId",
                table: "Events",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_People_PersonId",
                table: "Events",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HabitProgress_Habits_HabitId",
                table: "HabitProgress",
                column: "HabitId",
                principalTable: "Habits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_People_PersonId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_HabitProgress_Habits_HabitId",
                table: "HabitProgress");

            migrationBuilder.DropIndex(
                name: "IX_Events_PersonId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Events");

            migrationBuilder.AddForeignKey(
                name: "FK_HabitProgress_Habits_HabitId",
                table: "HabitProgress",
                column: "HabitId",
                principalTable: "Habits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
