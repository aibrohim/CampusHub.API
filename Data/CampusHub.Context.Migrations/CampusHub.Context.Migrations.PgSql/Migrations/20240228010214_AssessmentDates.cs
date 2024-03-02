using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusHub.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class AssessmentDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "assessments",
                newName: "StartDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "assessments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "assessments");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "assessments",
                newName: "DateTime");
        }
    }
}
