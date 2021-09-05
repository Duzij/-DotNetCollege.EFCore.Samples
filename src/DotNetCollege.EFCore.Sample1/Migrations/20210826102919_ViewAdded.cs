using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetCollege.EFCore.Sample1.Migrations
{
    public partial class ViewAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE VIEW ProductView AS SELECT TOP (100) PERCENT Id as ProductId, Name as ProductName From Products ORDER BY ProductName DESC, ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW ProductView");
        }
    }
}
