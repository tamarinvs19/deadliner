using System.Data.SqlClient;
using Deadliner.Api.Models;
using Deadliner.Api.Models.Relationships;
using Deadliner.Api.Storage;
using Deadliner.Storage.Ado.Helpers;
using Deadliner.Storage.Ado.Mappers;
using Deadliner.Utils;

namespace Deadliner.Storage.Ado.DataProviders;

public class UserToLocalActionDataProvider : IStorage<IUserToLocalAction>
{
    private readonly string _connectionString;
    
    public UserToLocalActionDataProvider(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public UserToLocalActionDataProvider()
    {
        _connectionString = "Data Source=THINKBOOK;Initial Catalog=DEADLINER;Integrated Security=True";
    }
    
    public void Dispose() { }

    public IEnumerable<IUserToLocalAction> Items()
    {
        var sqlQuery = XmlStrings.GetString(Tables.UserToLocalAction, "GetAll");
        
        var result = new DbHelper(_connectionString).GetData(
            new UserToLocalActionMapper(),
            sqlQuery);

        return result;
    }

    public IUserToLocalAction Get(int id)
    {
        var sqlQuery = XmlStrings.GetString(Tables.UserToLocalAction, "GetById");
        var idParam = new SqlParameter("@id", id);
        
        var result = new DbHelper(_connectionString).GetItem(
            new UserToLocalActionMapper(),
            sqlQuery,
            idParam);

        return result;
    }

    public void Create(IUserToLocalAction item)
    {
        var sqlQuery = XmlStrings.GetString(Tables.UserToLocalAction, "Create");
        var idParam = new SqlParameter("@id", item.Id);
        var userParam = new SqlParameter("@userid", item.User.Id);
        var localActionParam = new SqlParameter("@localactionid", item.LocalAction.Id);
        var stateParam = new SqlParameter("@stateid", StateIdTransformer.GetStateId(item.State));
        
        new DbHelper(_connectionString).UpdateItem(
            new UserToSuperGroupMapper(),
            sqlQuery,
            idParam,
            userParam,
            localActionParam,
            stateParam);
    }

    public void Update(IUserToLocalAction item)
    {
        var sqlQuery = XmlStrings.GetString(Tables.UserToLocalAction, "Update");
        var idParam = new SqlParameter("@id", item.Id);
        var userParam = new SqlParameter("@userid", item.User.Id);
        var localActionParam = new SqlParameter("@localactionid", item.LocalAction.Id);
        var stateParam = new SqlParameter("@stateid", StateIdTransformer.GetStateId(item.State));
        
        new DbHelper(_connectionString).UpdateItem(
            new UserToLocalActionMapper(),
            sqlQuery,
            idParam,
            userParam,
            localActionParam,
            stateParam);
    }

    public void Delete(int id)
    {
        var sqlQuery = XmlStrings.GetString(Tables.UserToLocalAction, "Delete");
        var idParam = new SqlParameter("@id", id);
        
        new DbHelper(_connectionString).UpdateItem(
            new UserToLocalActionMapper(),
            sqlQuery,
            idParam);
    }

    public void Save()
    {
    }
}