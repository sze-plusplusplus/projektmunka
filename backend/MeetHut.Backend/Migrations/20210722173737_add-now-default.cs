using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MeetHut.Backend.Migrations
{
    /// <inheritdoc />
    public partial class addnowdefault : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Added",
                table: "RoomUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Added",
                table: "RoomUsers",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "NOW()");
        }
    }
}
