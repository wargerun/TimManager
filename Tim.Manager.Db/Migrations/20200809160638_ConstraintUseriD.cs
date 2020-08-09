using Microsoft.EntityFrameworkCore.Migrations;

namespace Tim.Manager.Db.Migrations
{
    public partial class ConstraintUseriD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PassItems_AspNetUsers_UserId",
                table: "PassItems");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "PassItems",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PassItems_AspNetUsers_UserId",
                table: "PassItems",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PassItems_AspNetUsers_UserId",
                table: "PassItems");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "PassItems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_PassItems_AspNetUsers_UserId",
                table: "PassItems",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
