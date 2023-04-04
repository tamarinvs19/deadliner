using Deadliner.Api.Models;
using Deadliner.Api.Storage;
using Deadliner.Storage.Ado.Helpers;
using Deadliner.Storage.Ado.Mappers;

namespace Deadliner.Storage.Ado.DataProviders;

public class UserDataProvider : IStorage<IUser>
{
    private string _connectionString;
    
    public UserDataProvider(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public void Dispose() { }

    public IEnumerable<IUser> Items()
    {
        var sqlQuery = XmlStrings.GetString(Tables.Users, "GetAllUsers");
        
        var result = new DbHelper(_connectionString).GetData(
            new UserMapper(),
            sqlQuery);

        return result;
    }

    public IUser Get(int id)
    {
        var sqlQuery = XmlStrings.GetString(Tables.Users, "GetUserById");
        
        var result = new DbHelper(_connectionString).GetItem(
            new UserMapper(),
            sqlQuery);

        return result;
    }

    public void Create(IUser item)
    {
        throw new NotImplementedException();
    }

    public void Update(IUser item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public void Save()
    {
        throw new NotImplementedException();
    }
}