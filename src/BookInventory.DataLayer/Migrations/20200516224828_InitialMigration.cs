using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookInventory.DataLayer.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Author = table.Column<string>(type: "varchar(100)", nullable: false),
                    Title = table.Column<string>(type: "varchar(200)", nullable: false),
                    Publisher = table.Column<string>(type: "varchar(200)", nullable: false),
                    Isbn = table.Column<string>(type: "varchar(100)", nullable: false),
                    Year = table.Column<string>(type: "varchar(10)", nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModifiedTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
