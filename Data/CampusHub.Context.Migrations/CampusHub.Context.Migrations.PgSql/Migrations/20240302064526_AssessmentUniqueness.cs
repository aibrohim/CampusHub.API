using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampusHub.Context.Migrations.PgSql.Migrations
{
    /// <inheritdoc />
    public partial class AssessmentUniqueness : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_assessments_Title",
                table: "assessments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_assessments_Title",
                table: "assessments",
                column: "Title",
                unique: true);
        }
    }
}
