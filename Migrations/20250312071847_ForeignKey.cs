using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DefectRecord.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefectName",
                table: "DefectReports");

            migrationBuilder.AddColumn<string>(
                name: "DefectId",
                table: "DefectReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefectId",
                table: "DefectReports");

            migrationBuilder.AddColumn<string>(
                name: "DefectName",
                table: "DefectReports",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
