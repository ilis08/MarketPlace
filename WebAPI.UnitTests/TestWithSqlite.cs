using Data.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.UnitTests.Helpers;

namespace WebAPI.UnitTests;

[TestFixture]
public abstract class TestWithSqlite : IDisposable
{
    protected DbConnection _connection;
    protected DbContextOptions<RepositoryContext> _contextOptions;

    public TestWithSqlite()
    {

    }

    protected async Task CreateDatabaseAsync()
    {
        // Create and open a connection. This creates the SQLite in-memory database, which will persist until the connection is closed
        // at the end of the test (see Dispose below).
        _connection = new SqliteConnection("Filename=:memory:");
        await _connection.OpenAsync();

        // These options will be used by the context instances in this test suite, including the connection opened above.
        _contextOptions = new DbContextOptionsBuilder<RepositoryContext>()
            .UseSqlite(_connection)
            .Options;


        // Create the schema and seed some data
        using var context = CreateContext();

        if (context.Database.EnsureCreated())
        {
            using var viewCommand = context.Database.GetDbConnection().CreateCommand();
            viewCommand.CommandText = @"
CREATE VIEW AllResources AS
SELECT Value
FROM Categories;";
            await viewCommand.ExecuteNonQueryAsync();
        }

        await DatabaseInitializer.InitializeAsync(context);


        await context.SaveChangesAsync();
    }

    protected RepositoryContext CreateContext() => new RepositoryContext(_contextOptions);

    public void Dispose() => _connection.Dispose();
}
