using NUnit.Framework;

using System;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Resources;
using System.Xml.Linq;

namespace DotNetCollege.EFCore.Sample5.Tests
{
    public class Tests
    {
        private const string ResxFilename = @"C:\Users\maxim\source\repos\DotNetCollege.EFCore.Samples\src\DotNetCollege.EFCore.Sample5\Migrations\202108191446449_InitialCreate.resx";
       
        [Test]
        public void DecompressResxMigrationFile()
        {
            var reader = new ResXResourceReader(ResxFilename);
            IDictionaryEnumerator resources = reader.GetEnumerator();

            while (resources.MoveNext())
            {
                if ("Target".Equals(resources.Key))
                {
                    XDocument target = Decompress(Convert.FromBase64String(resources.Value.ToString()));

                    Console.Write(target);
                }
            }

            Console.ReadKey();
        }

        [Test]
        public void DecompressDatabaseMigration()
        {
            const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB; Initial Catalog=DotNetCollege.EFCore.Sample5; Integrated Security=true";
            var sqlToExecute = String.Format("select model from __MigrationHistory where migrationId like '%{0}'", "202108191446449_InitialCreate");

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(sqlToExecute, connection);

                var reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    throw new Exception("Now Rows to display. Probably migration name is incorrect");
                }

                while (reader.Read())
                {
                    var model = (byte[])reader["model"];
                    var decompressed = Decompress(model);
                    Console.WriteLine(decompressed);
                }
            }
        }

        /// <summary>
        /// Stealing decomposer from EF itself:
        /// http://entityframework.codeplex.com/SourceControl/latest#src/EntityFramework/Migrations/Edm/ModelCompressor.cs
        /// </summary>
        public virtual XDocument Decompress(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    return XDocument.Load(gzipStream);
                }
            }
        }

    }
}