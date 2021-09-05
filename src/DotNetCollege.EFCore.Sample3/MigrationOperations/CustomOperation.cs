using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Sample3.MigrationOperations
{
    public static class CustomOperation
    {
        public static OperationBuilder<SqlOperation> CreateLogin(
        this MigrationBuilder migrationBuilder,
        string login,
        string password)
        {
            if (migrationBuilder.ActiveProvider == "Microsoft.EntityFrameworkCore.SqlServer")
            {
               return migrationBuilder.Sql($"CREATE LOGIN {login} WITH PASSWORD = '{password}';  ");
            }
            else
            {
                throw new Exception("Unexpected provider.");
            }
        }

        public static OperationBuilder<SqlOperation> CreateUser(
       this MigrationBuilder migrationBuilder,
       string username,
       string login)
        {
            if (migrationBuilder.ActiveProvider == "Microsoft.EntityFrameworkCore.SqlServer")
            {
                return migrationBuilder.Sql($"CREATE USER {username} FOR LOGIN {login};  ");
            }
            else
            {
                throw new Exception("Unexpected provider.");
            }
        }
    }
}
