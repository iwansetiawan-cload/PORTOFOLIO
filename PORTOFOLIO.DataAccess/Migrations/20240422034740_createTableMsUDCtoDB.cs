using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PORTOFOLIO.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class createTableMsUDCtoDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MsUDC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntryKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Text1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Text2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Text3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inum1 = table.Column<int>(type: "int", nullable: true),
                    Inum2 = table.Column<int>(type: "int", nullable: true),
                    Mnum1 = table.Column<double>(type: "float", nullable: true),
                    Mnum2 = table.Column<double>(type: "float", nullable: true),
                    Creator = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastModifyUser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsUDC", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MsUDC");
        }
    }
}
