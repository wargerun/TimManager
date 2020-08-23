using Microsoft.EntityFrameworkCore.Migrations;

namespace Tim.Manager.Db.Migrations
{
    public partial class Alter_PK_AndIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PassItems",
                table: "PassItems");

            migrationBuilder.DropIndex(
                name: "IDX_PASS_ITEM_USER_ID_AND_NAME",
                table: "PassItems");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PassItems",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PassItem",
                table: "PassItems",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IDX_PASS_ITEM_USER_ID_AND_NAME",
                table: "PassItems",
                columns: new[] { "UserId", "Name" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PassItem",
                table: "PassItems");

            migrationBuilder.DropIndex(
                name: "IDX_PASS_ITEM_USER_ID_AND_NAME",
                table: "PassItems");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PassItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PassItems",
                table: "PassItems",
                columns: new[] { "UserId", "Name" });

            migrationBuilder.CreateIndex(
                name: "IDX_PASS_ITEM_USER_ID_AND_NAME",
                table: "PassItems",
                columns: new[] { "UserId", "Name" });
        }
    }
}
