using System.Data.SqlClient;
using Deadliner.Api.Models;
using Deadliner.Api.Models.Relationships;
using Deadliner.Models;
using Deadliner.Storage.Ado.DataProviders;
using Deadliner.Utils;

namespace Deadliner.Storage.Ado.Mappers;

public class UserToLocalActionMapper : IMapper<IUserToLocalAction>
{
    public IUserToLocalAction ReadItem(SqlDataReader rd)
    {
        var id = (int)rd["id"];
        var userId = (int)rd["userid"];
        var localActionId = (int)rd["localactionid"];
        var stateId = (int)rd["stateid"];
        var user = new UserDataProvider().Get(userId);
        ILocalAction? localAction1 = new LocalEventDataProvider().Get(localActionId);
        var localAction2 = new LocalTaskDataProvider().Get(localActionId);
        ILocalAction localAction = localAction1 != null ? localAction1 : localAction2;
        var state = StateIdTransformer.GetState(stateId);

        return new UserToLocalAction(id, user, localAction, state);
    }
}