using System.Data.SqlClient;
using Deadliner.Api.Models;
using Deadliner.Api.Storage;
using Deadliner.Storage.Ado.Helpers;
using Deadliner.Storage.Ado.Mappers;

namespace Deadliner.Storage.Ado.DataProviders;

public class UserDataProvider : IStorage<IUser>
{
    private readonly string _connectionString;
    
    public UserDataProvider(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public UserDataProvider()
    {
        _connectionString = "Data Source=THINKBOOK;Initial Catalog=DEADLINER;Integrated Security=True";
    }
    
    public void Dispose() { }

    public IEnumerable<IUser> Items()
    {
        var sqlQuery = XmlStrings.GetString(Tables.Users, "GetAll");
        
        var result = new DbHelper(_connectionString).GetData(
            new UserMapper(),
            sqlQuery);

        return result;
    }

    public IUser Get(int id)
    {
        var sqlQuery = XmlStrings.GetString(Tables.Users, "GetById");
        var idParam = new SqlParameter("@id", id);
        
        var result = new DbHelper(_connectionString).GetItem(
            new UserMapper(),
            sqlQuery,
            idParam);

        return result;
    }

    public void Create(IUser item)
    {
        var sqlQuery = XmlStrings.GetString(Tables.Users, "Create");
        var idParam = new SqlParameter("@id", item.Id);
        var usernameParam = new SqlParameter("@username", item.Username);
        var passwordParam = new SqlParameter("@password", item.Password);
        
        new DbHelper(_connectionString).UpdateItem(
            new UserMapper(),
            sqlQuery,
            idParam,
            usernameParam,
            passwordParam);
    }

    public void Update(IUser item)
    {
        var sqlQuery = XmlStrings.GetString(Tables.Users, "Update");
        var idParam = new SqlParameter("@id", item.Id);
        var usernameParam = new SqlParameter("@username", item.Username);
        var passwordParam = new SqlParameter("@password", item.Password);
        
        new DbHelper(_connectionString).UpdateItem(
            new UserMapper(),
            sqlQuery,
            idParam,
            usernameParam,
            passwordParam);
    }

    public void Delete(int id)
    {
        var sqlQuery = XmlStrings.GetString(Tables.Users, "Delete");
        var idParam = new SqlParameter("@id", id);
        
        new DbHelper(_connectionString).UpdateItem(
            new UserMapper(),
            sqlQuery,
            idParam);
    }

    public void Save()
    {
    }
}