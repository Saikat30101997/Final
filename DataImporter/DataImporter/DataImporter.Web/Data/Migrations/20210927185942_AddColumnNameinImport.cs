using Microsoft.EntityFrameworkCore.Migrations;

namespace DataImporter.Web.Migrations.ImporterDb
{
    public partial class AddColumnNameinImport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColumnName",
                table: "Imports",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColumnName",
                table: "Imports");
        }
    }
}
