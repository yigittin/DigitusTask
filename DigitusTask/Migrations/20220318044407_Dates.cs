using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DigitusTask.Migrations
{
    public partial class Dates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RegisterDate",
                schema: "Identity",
                table: "User",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VerificationDate",
                schema: "Identity",
                table: "User",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisterDate",
                schema: "Identity",
                table: "User");

            migrationBuilder.DropColumn(
                name: "VerificationDate",
                schema: "Identity",
                table: "User");
        }
    }
}
