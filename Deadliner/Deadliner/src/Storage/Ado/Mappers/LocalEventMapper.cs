using System.Data.SqlClient;
using Deadliner.Api.Models;
using Deadliner.Models;
using Deadliner.Storage.Ado.DataProviders;
using Deadliner.Utils;

namespace Deadliner.Storage.Ado.Mappers;

public class LocalEventMapper : IMapper<ILocalEvent>
{
    public ILocalEvent ReadItem(SqlDataReader rd)
    {
        var user = new User
        {
            Id = (int)rd["dgroupownerid"],
            Username = (string)rd["dgroupownerusername"],
            Password = (string)rd["dgroupownerpassword"]
        };
        
        var sguser = new User
        {
            Id = (int)rd["sgownerid"],
            Username = (string)rd["sgownerusername"],
            Password = (string)rd["sgownerpassword"]
        };

        var superGroup = new SuperGroup(
            (int)rd["supergroupid"],
            (string)rd["supergrouptitle"],
            (string)rd["supergroupdescription"],
            rd["supergroupkey"] is DBNull ? null: (string)rd["supergroupkey"],
            sguser
        );
        
        var group = new Group(
            (int)rd["dgroupid"],
            (string)rd["dgrouptitle"],
            (string)rd["dgroupdescription"],
            rd["dgroupkey"] is DBNull ? null: (string)rd["dgroupkey"],
            user,
            superGroup
        );

        var state = StateIdTransformer.GetState((int)rd["actionstate"]);
        ILocalAction? parent = null;
        if (rd["actionparent"] is not DBNull)
        {
            parent = new LocalTaskDataProvider().Get((int)rd["actionparent"]);
        }
        
        return new LocalEvent
        (
            (int)rd["actionid"],
            (string)rd["actiontitle"],
            (string)rd["actiondescription"],
            (DateTime)rd["eventdt"],
            group,
            state,
            parent
        );
    }
}