using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetCollege.EFCore.Sample02.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Product");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BasicProduct",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ComputedOnServerProperty = table.Column<string>(type: "nvarchar(max)", nullable: true, computedColumnSql: "isnull(N'Name is '+CONVERT([nvarchar](200),LEN(Name)) + ' chars long',N'*** ERROR ***')"),
                    ProductDescription = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasicProduct_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasicProduct_CategoryId",
                schema: "Product",
                table: "BasicProduct",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasicProduct",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
