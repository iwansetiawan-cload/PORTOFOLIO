using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PORTOFOLIO.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addDescInTableAboutUs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AboutUs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "AboutUs");
        }
    }
}
