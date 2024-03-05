using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusHub.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class GroupCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "groups",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_groups_CourseId",
                table: "groups",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_groups_courses_CourseId",
                table: "groups",
                column: "CourseId",
                principalTable: "courses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_groups_courses_CourseId",
                table: "groups");

            migrationBuilder.DropIndex(
                name: "IX_groups_CourseId",
                table: "groups");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "groups");
        }
    }
}
