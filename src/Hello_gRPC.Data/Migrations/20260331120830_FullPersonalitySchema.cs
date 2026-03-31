using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hello_gRPC.Data.Migrations
{
    /// <inheritdoc />
    public partial class FullPersonalitySchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Personalities",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Personalities",
                newName: "Nationality");

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Personalities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "BirthDate",
                table: "Personalities",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Personalities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Personalities",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateOnly>(
                name: "DeathDate",
                table: "Personalities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Personalities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Personalities",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Personalities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Personalities");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Personalities");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Personalities");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Personalities");

            migrationBuilder.DropColumn(
                name: "DeathDate",
                table: "Personalities");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Personalities");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Personalities");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Personalities");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Personalities",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Nationality",
                table: "Personalities",
                newName: "Description");
        }
    }
}
