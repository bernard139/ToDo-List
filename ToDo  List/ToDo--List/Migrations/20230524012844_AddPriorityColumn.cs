using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDo__List.Migrations
{
    public partial class AddPriorityColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "Categories",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Categories");
        }
    }
}
