using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeopleManagerApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonSoftDeleteAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "People",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "People",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "People");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "People");
        }
    }
}
