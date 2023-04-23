using System.Data;
using System.Data.SqlClient;
using Deadliner.Api.Models;
using Deadliner.Api.Storage;
using Deadliner.Storage.Ado.Helpers;
using Deadliner.Storage.Ado.Mappers;
using Deadliner.Utils;

namespace Deadliner.Storage.Ado.DataProviders;

public class LocalTaskDataProvider : IStorage<ILocalTask>
{
    private readonly string _connectionString;
    
    public LocalTaskDataProvider(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public LocalTaskDataProvider()
    {
        _connectionString = "Data Source=THINKBOOK;Initial Catalog=DEADLINER;Integrated Security=True";
    }
    
    public void Dispose() { }

    public IEnumerable<ILocalTask> Items()
    {
        var sqlQuery = XmlStrings.GetString(Tables.LocalTasks, "GetAll");
        
        var result = new DbHelper(_connectionString).GetData(
            new LocalTaskMapper(),
            sqlQuery);

        return result;
    }

    public ILocalTask Get(int id)
    {
        var sqlQuery = XmlStrings.GetString(Tables.LocalTasks, "GetById");
        var idParam = new SqlParameter("@id", id);
        
        var result = new DbHelper(_connectionString).GetItem(
            new LocalTaskMapper(),
            sqlQuery,
            idParam);

        return result;
    }

    public void Create(ILocalTask item)
    {
        var sqlQuery = XmlStrings.GetString(Tables.LocalTasks, "Create");
        var idParam = new SqlParameter("@id", item.Id);
        var titleParam = new SqlParameter("@title", item.Title);
        var descriptionParam = new SqlParameter("@description", item.Description);
        var parentParam = item.Parent is null
            ? new SqlParameter("@parent", DBNull.Value)
            : new SqlParameter("@parent", item.Parent.Id);
        var groupParam = new SqlParameter("@dgroup", item.Group.Id);
        var stateParam = new SqlParameter("@state", StateIdTransformer.GetStateId(item.State));
        var typeParam = new SqlParameter("@type", LocalActionTypeTransformer.GetTypeId(item));
        var creationdtParam = new SqlParameter("@creationdt", item.CreationDateTime);
        var deadlineParam = new SqlParameter("@deadline", item.Deadline);
        
        new DbHelper(_connectionString).UpdateItem(
            new LocalTaskMapper(),
            sqlQuery,
            idParam,
            titleParam,
            descriptionParam,
            parentParam,
            groupParam,
            stateParam,
            typeParam,
            creationdtParam,
            deadlineParam);
    }

    public void Update(ILocalTask item)
    {
        var sqlQuery = XmlStrings.GetString(Tables.LocalTasks, "Update");
        var idParam = new SqlParameter("@id", item.Id);
        var titleParam = new SqlParameter("@title", item.Title);
        var descriptionParam = new SqlParameter("@description", item.Description);
        var parentParam = item.Parent is null
            ? new SqlParameter("@parent", DBNull.Value)
            : new SqlParameter("@parent", item.Parent.Id);
        var groupParam = new SqlParameter("@dgroup", item.Group.Id);
        var stateParam = new SqlParameter("@state", StateIdTransformer.GetStateId(item.State));
        var typeParam = new SqlParameter("@type", LocalActionTypeTransformer.GetTypeId(item));
        var creationdtParam = new SqlParameter("@creationdt", item.CreationDateTime);
        var deadlineParam = new SqlParameter("@deadline", item.Deadline);
        
        new DbHelper(_connectionString).UpdateItem(
            new LocalTaskMapper(),
            sqlQuery,
            idParam,
            titleParam,
            descriptionParam,
            parentParam,
            groupParam,
            stateParam,
            typeParam,
            creationdtParam,
            deadlineParam);
    }

    public void Delete(int id)
    {
        var sqlQuery = XmlStrings.GetString(Tables.LocalTasks, "Delete");
        var idParam = new SqlParameter("@id", id);
        
        new DbHelper(_connectionString).UpdateItem(
            new LocalTaskMapper(),
            sqlQuery,
            idParam);
    }

    public void Save()
    {
    }
}