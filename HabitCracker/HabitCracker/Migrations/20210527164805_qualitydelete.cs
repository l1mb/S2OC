using Microsoft.EntityFrameworkCore.Migrations;

namespace HabitCracker.Migrations
{
    public partial class qualitydelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonQuality",
                table: "People");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PersonQuality",
                table: "People",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
