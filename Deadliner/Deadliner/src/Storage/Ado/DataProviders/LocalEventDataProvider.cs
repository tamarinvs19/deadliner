using System.Data;
using System.Data.SqlClient;
using Deadliner.Api.Models;
using Deadliner.Api.Storage;
using Deadliner.Storage.Ado.Helpers;
using Deadliner.Storage.Ado.Mappers;
using Deadliner.Utils;

namespace Deadliner.Storage.Ado.DataProviders;

public class LocalEventDataProvider : IStorage<ILocalEvent>
{
    private readonly string _connectionString;
    
    public LocalEventDataProvider(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public LocalEventDataProvider()
    {
        _connectionString = "Data Source=THINKBOOK;Initial Catalog=DEADLINER;Integrated Security=True";
    }
    
    public void Dispose() { }

    public IEnumerable<ILocalEvent> Items()
    {
        var sqlQuery = XmlStrings.GetString(Tables.LocalEvents, "GetAll");
        
        var result = new DbHelper(_connectionString).GetData(
            new LocalEventMapper(),
            sqlQuery);

        return result;
    }

    public ILocalEvent Get(int id)
    {
        var sqlQuery = XmlStrings.GetString(Tables.LocalEvents, "GetById");
        var idParam = new SqlParameter("@id", id);
        
        var result = new DbHelper(_connectionString).GetItem(
            new LocalEventMapper(),
            sqlQuery,
            idParam);

        return result;
    }

    public void Create(ILocalEvent item)
    {
        var sqlQuery = XmlStrings.GetString(Tables.LocalEvents, "Create");
        var idParam = new SqlParameter("@id", item.Id);
        var titleParam = new SqlParameter("@title", item.Title);
        var descriptionParam = new SqlParameter("@description", item.Description);
        var parentParam = item.Parent is null
            ? new SqlParameter("@parent", DBNull.Value)
            : new SqlParameter("@parent", item.Parent.Id);
        var groupParam = new SqlParameter("@dgroup", item.Group.Id);
        var stateParam = new SqlParameter("@state", StateIdTransformer.GetStateId(item.State));
        var typeParam = new SqlParameter("@type", LocalActionTypeTransformer.GetTypeId(item));
        var datetimeParam = new SqlParameter("@datetime", item.DateTime);
        
        new DbHelper(_connectionString).UpdateItem(
            new LocalEventMapper(),
            sqlQuery,
            idParam,
            titleParam,
            descriptionParam,
            parentParam,
            groupParam,
            stateParam,
            typeParam,
            datetimeParam);
    }

    public void Update(ILocalEvent item)
    {
        var sqlQuery = XmlStrings.GetString(Tables.LocalEvents, "Update");
        var idParam = new SqlParameter("@id", item.Id);
        var titleParam = new SqlParameter("@title", item.Title);
        var descriptionParam = new SqlParameter("@description", item.Description);
        var parentParam = item.Parent is null
            ? new SqlParameter("@parent", DBNull.Value)
            : new SqlParameter("@parent", item.Parent.Id);
        var groupParam = new SqlParameter("@dgroup", item.Group.Id);
        var stateParam = new SqlParameter("@state", StateIdTransformer.GetStateId(item.State));
        var typeParam = new SqlParameter("@type", LocalActionTypeTransformer.GetTypeId(item));
        var datetimeParam = new SqlParameter("@datetime", item.DateTime);
        
        new DbHelper(_connectionString).UpdateItem(
            new LocalEventMapper(),
            sqlQuery,
            idParam,
            titleParam,
            descriptionParam,
            parentParam,
            groupParam,
            stateParam,
            typeParam,
            datetimeParam);
    }

    public void Delete(int id)
    {
        var sqlQuery = XmlStrings.GetString(Tables.LocalEvents, "Delete");
        var idParam = new SqlParameter("@id", id);
        
        new DbHelper(_connectionString).UpdateItem(
            new LocalEventMapper(),
            sqlQuery,
            idParam);
    }

    public void Save()
    {
    }
}