using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MeetHut.Backend.Migrations
{
    public partial class addroomuserproperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomUser_Rooms_RoomId",
                table: "RoomUser");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomUser_Users_UserId",
                table: "RoomUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomUser",
                table: "RoomUser");

            migrationBuilder.RenameTable(
                name: "RoomUser",
                newName: "RoomUsers");

            migrationBuilder.RenameIndex(
                name: "IX_RoomUser_UserId",
                table: "RoomUsers",
                newName: "IX_RoomUsers_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 3,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "Added",
                table: "RoomUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "AdderId",
                table: "RoomUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "RoomUsers",
                type: "int",
                nullable: false,
                defaultValue: 4);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomUsers",
                table: "RoomUsers",
                columns: new[] { "RoomId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_RoomUsers_AdderId",
                table: "RoomUsers",
                column: "AdderId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomUsers_Rooms_RoomId",
                table: "RoomUsers",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomUsers_Users_AdderId",
                table: "RoomUsers",
                column: "AdderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomUsers_Users_UserId",
                table: "RoomUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomUsers_Rooms_RoomId",
                table: "RoomUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomUsers_Users_AdderId",
                table: "RoomUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomUsers_Users_UserId",
                table: "RoomUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomUsers",
                table: "RoomUsers");

            migrationBuilder.DropIndex(
                name: "IX_RoomUsers_AdderId",
                table: "RoomUsers");

            migrationBuilder.DropColumn(
                name: "Added",
                table: "RoomUsers");

            migrationBuilder.DropColumn(
                name: "AdderId",
                table: "RoomUsers");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "RoomUsers");

            migrationBuilder.RenameTable(
                name: "RoomUsers",
                newName: "RoomUser");

            migrationBuilder.RenameIndex(
                name: "IX_RoomUsers_UserId",
                table: "RoomUser",
                newName: "IX_RoomUser_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 3);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomUser",
                table: "RoomUser",
                columns: new[] { "RoomId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_RoomUser_Rooms_RoomId",
                table: "RoomUser",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomUser_Users_UserId",
                table: "RoomUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
