using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetCollege.EFCore.Sample1.Migrations
{
    public partial class FunctionAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            Create FUNCTION GetProductsByPercentage(@percentValue int)
            RETURNS TABLE AS RETURN
            SELECT TOP (@percentValue) PERCENT Id as ProductId, Name as ProductName From Products ORDER BY ProductName DESC, ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP FUNCTION GetProductsByPercentage");
        }
    }
}
