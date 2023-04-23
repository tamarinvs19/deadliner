using System.Data.SqlClient;
using Deadliner.Api.Models;
using Deadliner.Api.Models.Relationships;
using Deadliner.Api.Storage;
using Deadliner.Storage.Ado.Helpers;
using Deadliner.Storage.Ado.Mappers;

namespace Deadliner.Storage.Ado.DataProviders;

public class UserToGroupDataProvider : IStorage<IUserToGroup>
{
    private readonly string _connectionString;
    
    public UserToGroupDataProvider(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public UserToGroupDataProvider()
    {
        _connectionString = "Data Source=THINKBOOK;Initial Catalog=DEADLINER;Integrated Security=True";
    }
    
    public void Dispose() { }

    public IEnumerable<IUserToGroup> Items()
    {
        var sqlQuery = XmlStrings.GetString(Tables.UserToGroup, "GetAll");
        
        var result = new DbHelper(_connectionString).GetData(
            new UserToGroupMapper(),
            sqlQuery);

        return result;
    }

    public IUserToGroup Get(int id)
    {
        var sqlQuery = XmlStrings.GetString(Tables.UserToGroup, "GetById");
        var idParam = new SqlParameter("@id", id);
        
        var result = new DbHelper(_connectionString).GetItem(
            new UserToGroupMapper(),
            sqlQuery,
            idParam);

        return result;
    }

    public void Create(IUserToGroup item)
    {
        var sqlQuery = XmlStrings.GetString(Tables.UserToGroup, "Create");
        var idParam = new SqlParameter("@id", item.Id);
        var userParam = new SqlParameter("@userid", item.User.Id);
        var groupParam = new SqlParameter("@groupid", item.Group.Id);
        
        new DbHelper(_connectionString).UpdateItem(
            new UserToSuperGroupMapper(),
            sqlQuery,
            idParam,
            userParam,
            groupParam);
    }

    public void Update(IUserToGroup item)
    {
        var sqlQuery = XmlStrings.GetString(Tables.UserToGroup, "Update");
        var idParam = new SqlParameter("@id", item.Id);
        var userParam = new SqlParameter("@userid", item.User.Id);
        var groupParam = new SqlParameter("@groupid", item.Group.Id);
        
        new DbHelper(_connectionString).UpdateItem(
            new UserToGroupMapper(),
            sqlQuery,
            idParam,
            userParam,
            groupParam);
    }

    public void Delete(int id)
    {
        var sqlQuery = XmlStrings.GetString(Tables.UserToGroup, "Delete");
        var idParam = new SqlParameter("@id", id);
        
        new DbHelper(_connectionString).UpdateItem(
            new UserToGroupMapper(),
            sqlQuery,
            idParam);
    }

    public void Save()
    {
    }
}