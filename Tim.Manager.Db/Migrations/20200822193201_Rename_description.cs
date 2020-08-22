using Microsoft.EntityFrameworkCore.Migrations;

namespace Tim.Manager.Db.Migrations
{
    public partial class Rename_description : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discription",
                table: "PassItems",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "PassItems",
                newName: "Discription");
        }
    }
}
