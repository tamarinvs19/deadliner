using System.Data.SqlClient;
using Deadliner.Api.Models;
using Deadliner.Api.Models.Relationships;
using Deadliner.Api.Storage;
using Deadliner.Storage.Ado.Helpers;
using Deadliner.Storage.Ado.Mappers;

namespace Deadliner.Storage.Ado.DataProviders;

public class UserToSuperGroupDataProvider : IStorage<IUserToSuperGroup>
{
    private readonly string _connectionString;
    
    public UserToSuperGroupDataProvider(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public UserToSuperGroupDataProvider()
    {
        _connectionString = "Data Source=THINKBOOK;Initial Catalog=DEADLINER;Integrated Security=True";
    }
    
    public void Dispose() { }

    public IEnumerable<IUserToSuperGroup> Items()
    {
        var sqlQuery = XmlStrings.GetString(Tables.UserToSuperGroup, "GetAll");
        
        var result = new DbHelper(_connectionString).GetData(
            new UserToSuperGroupMapper(),
            sqlQuery);

        return result;
    }

    public IUserToSuperGroup Get(int id)
    {
        var sqlQuery = XmlStrings.GetString(Tables.UserToSuperGroup, "GetById");
        var idParam = new SqlParameter("@id", id);
        
        var result = new DbHelper(_connectionString).GetItem(
            new UserToSuperGroupMapper(),
            sqlQuery,
            idParam);

        return result;
    }

    public void Create(IUserToSuperGroup item)
    {
        var sqlQuery = XmlStrings.GetString(Tables.UserToSuperGroup, "Create");
        var idParam = new SqlParameter("@id", item.Id);
        var userParam = new SqlParameter("@userid", item.User.Id);
        var groupParam = new SqlParameter("@supergroupid", item.SuperGroup.Id);
        
        new DbHelper(_connectionString).UpdateItem(
            new UserToSuperGroupMapper(),
            sqlQuery,
            idParam,
            userParam,
            groupParam);
    }

    public void Update(IUserToSuperGroup item)
    {
        var sqlQuery = XmlStrings.GetString(Tables.UserToSuperGroup, "Update");
        var idParam = new SqlParameter("@id", item.Id);
        var userParam = new SqlParameter("@userid", item.User.Id);
        var groupParam = new SqlParameter("@supergroupid", item.SuperGroup.Id);
        
        new DbHelper(_connectionString).UpdateItem(
            new UserToSuperGroupMapper(),
            sqlQuery,
            idParam,
            userParam,
            groupParam);
    }

    public void Delete(int id)
    {
        var sqlQuery = XmlStrings.GetString(Tables.UserToSuperGroup, "Delete");
        var idParam = new SqlParameter("@id", id);
        
        new DbHelper(_connectionString).UpdateItem(
            new UserToSuperGroupMapper(),
            sqlQuery,
            idParam);
    }

    public void Save()
    {
    }
}