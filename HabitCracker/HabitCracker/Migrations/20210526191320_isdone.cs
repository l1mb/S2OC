using Microsoft.EntityFrameworkCore.Migrations;

namespace HabitCracker.Migrations
{
    public partial class isdone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_People_PersonId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_PersonId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "IsDone",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Events");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDone",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
        }
    }
}