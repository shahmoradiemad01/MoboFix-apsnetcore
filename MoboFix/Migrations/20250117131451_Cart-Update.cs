using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoboFix.Migrations
{
    public partial class CartUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FinalisedDate",
                table: "Carts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalisedDate",
                table: "Carts");
        }
    }
}
