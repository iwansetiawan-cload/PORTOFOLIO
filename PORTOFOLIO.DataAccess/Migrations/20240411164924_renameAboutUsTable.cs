using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PORTOFOLIO.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class renameAboutUsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "AboutUs",
                newName: "Vision");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "AboutUs",
                newName: "Mission");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Vision",
                table: "AboutUs",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Mission",
                table: "AboutUs",
                newName: "Content");
        }
    }
}
