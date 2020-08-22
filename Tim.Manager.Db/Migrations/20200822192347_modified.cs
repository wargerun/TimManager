using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tim.Manager.Db.Migrations
{
    public partial class modified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PassItems");

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "PassItems",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Modified",
                table: "PassItems");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PassItems",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
