using System.Data;
using System.Data.SqlClient;
using Deadliner.Api.Models;
using Deadliner.Api.Storage;
using Deadliner.Storage.Ado.Helpers;
using Deadliner.Storage.Ado.Mappers;

namespace Deadliner.Storage.Ado.DataProviders;

public class GroupDataProvider : IStorage<IGroup>
{
    private readonly string _connectionString;
    
    public GroupDataProvider(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public GroupDataProvider()
    {
        _connectionString = "Data Source=THINKBOOK;Initial Catalog=DEADLINER;Integrated Security=True";
    }
    
    public void Dispose() { }

    public IEnumerable<IGroup> Items()
    {
        var sqlQuery = XmlStrings.GetString(Tables.Groups, "GetAll");
        
        var result = new DbHelper(_connectionString).GetData(
            new GroupMapper(),
            sqlQuery);

        return result;
    }

    public IGroup Get(int id)
    {
        var sqlQuery = XmlStrings.GetString(Tables.Groups, "GetById");
        var idParam = new SqlParameter("@id", id);
        
        var result = new DbHelper(_connectionString).GetItem(
            new GroupMapper(),
            sqlQuery,
            idParam);

        return result;
    }

    public void Create(IGroup item)
    {
        var sqlQuery = XmlStrings.GetString(Tables.Groups, "Create");
        var idParam = new SqlParameter("@id", item.Id);
        var titleParam = new SqlParameter("@title", item.Title);
        var descriptionParam = new SqlParameter("@description", item.Description);
        var accessKeyParam = item.AccessKey is null
            ? new SqlParameter("@accesskey", DBNull.Value)
            : new SqlParameter("@accesskey", item.AccessKey);
        var ownerParam = new SqlParameter("@owner", item.Owner.Id);
        var superGroupParam = new SqlParameter("@superGroup", item.SuperGroup.Id);
        
        new DbHelper(_connectionString).UpdateItem(
            new GroupMapper(),
            sqlQuery,
            idParam,
            titleParam,
            descriptionParam,
            accessKeyParam,
            ownerParam,
            superGroupParam);
    }

    public void Update(IGroup item)
    {
        var sqlQuery = XmlStrings.GetString(Tables.Groups, "Update");
        var idParam = new SqlParameter("@id", item.Id);
        var titleParam = new SqlParameter("@title", item.Title);
        var descriptionParam = new SqlParameter("@description", item.Description);
        var accessKeyParam = item.AccessKey is null
            ? new SqlParameter("@accesskey", DBNull.Value)
            : new SqlParameter("@accesskey", item.AccessKey);
        var ownerParam = new SqlParameter("@owner", item.Owner.Id);
        var superGroupParam = new SqlParameter("@supergroup", item.SuperGroup.Id);
        
        new DbHelper(_connectionString).UpdateItem(
            new GroupMapper(),
            sqlQuery,
            idParam,
            titleParam,
            descriptionParam,
            accessKeyParam,
            ownerParam,
            superGroupParam);
    }

    public void Delete(int id)
    {
        var sqlQuery = XmlStrings.GetString(Tables.Groups, "Delete");
        var idParam = new SqlParameter("@id", id);
        
        new DbHelper(_connectionString).UpdateItem(
            new GroupMapper(),
            sqlQuery,
            idParam);
    }

    public void Save()
    {
    }
}