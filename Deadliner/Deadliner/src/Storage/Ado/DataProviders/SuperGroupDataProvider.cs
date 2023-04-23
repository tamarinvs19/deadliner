using System.Data;
using System.Data.SqlClient;
using Deadliner.Api.Models;
using Deadliner.Api.Storage;
using Deadliner.Storage.Ado.Helpers;
using Deadliner.Storage.Ado.Mappers;

namespace Deadliner.Storage.Ado.DataProviders;

public class SuperGroupDataProvider : IStorage<ISuperGroup>
{
    private readonly string _connectionString;
    
    public SuperGroupDataProvider(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public SuperGroupDataProvider()
    {
        _connectionString = "Data Source=THINKBOOK;Initial Catalog=DEADLINER;Integrated Security=True";
    }
    
    public void Dispose() { }

    public IEnumerable<ISuperGroup> Items()
    {
        var sqlQuery = XmlStrings.GetString(Tables.SuperGroups, "GetAll");
        
        var result = new DbHelper(_connectionString).GetData(
            new SuperGroupMapper(),
            sqlQuery);

        return result;
    }

    public ISuperGroup Get(int id)
    {
        var sqlQuery = XmlStrings.GetString(Tables.SuperGroups, "GetById");
        var idParam = new SqlParameter("@id", id);
        
        var result = new DbHelper(_connectionString).GetItem(
            new SuperGroupMapper(),
            sqlQuery,
            idParam);

        return result;
    }

    public void Create(ISuperGroup item)
    {
        var sqlQuery = XmlStrings.GetString(Tables.SuperGroups, "Create");
        var idParam = new SqlParameter("@id", item.Id);
        var titleParam = new SqlParameter("@title", item.Title);
        var descriptionParam = new SqlParameter("@description", item.Description);
        var accessKeyParam = item.AccessKey is null
            ? new SqlParameter("@accesskey", DBNull.Value)
            : new SqlParameter("@accesskey", item.AccessKey);
        var ownerParam = new SqlParameter("@owner", item.Owner.Id);
        
        new DbHelper(_connectionString).UpdateItem(
            new SuperGroupMapper(),
            sqlQuery,
            idParam,
            titleParam,
            descriptionParam,
            accessKeyParam,
            ownerParam);
    }

    public void Update(ISuperGroup item)
    {
        var sqlQuery = XmlStrings.GetString(Tables.SuperGroups, "Update");
        var idParam = new SqlParameter("@id", item.Id);
        var titleParam = new SqlParameter("@title", item.Title);
        var descriptionParam = new SqlParameter("@description", item.Description);
        var accessKeyParam = item.AccessKey is null
            ? new SqlParameter("@accesskey", DBNull.Value)
            : new SqlParameter("@accesskey", item.AccessKey);
        var ownerParam = new SqlParameter("@owner", item.Owner.Id);
        
        new DbHelper(_connectionString).UpdateItem(
            new SuperGroupMapper(),
            sqlQuery,
            idParam,
            titleParam,
            descriptionParam,
            accessKeyParam,
            ownerParam);
    }

    public void Delete(int id)
    {
        var sqlQuery = XmlStrings.GetString(Tables.SuperGroups, "Delete");
        var idParam = new SqlParameter("@id", id);
        
        new DbHelper(_connectionString).UpdateItem(
            new SuperGroupMapper(),
            sqlQuery,
            idParam);
    }

    public void Save()
    {
    }
}