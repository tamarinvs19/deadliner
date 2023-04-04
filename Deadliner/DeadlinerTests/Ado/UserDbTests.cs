using System.Data.SqlClient;
using Deadliner.Storage.Ado;
using Deadliner.Storage.Ado.DataProviders;
using Deadliner.Storage.Ado.Mappers;
using NUnit.Framework;

namespace DeadlinerTests.Ado;

[TestFixture]
public class UserDbTests
{
    [Test]
    public void TestConnection()
    {
        var connectionString = "Server=deadlinerms;Port=1433;" +
                               "Database=DeadlinerDB;User ID=SA;" +
                               "Password=Deadliner@Passw0rd;Encrypt=True;" +
                               "TrustServerCertificate=False;Connection Timeout=30;";
        var provider = new UserDataProvider(connectionString);
        provider.Items();
    }
}