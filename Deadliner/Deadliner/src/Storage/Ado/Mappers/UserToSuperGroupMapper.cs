using System.Data.SqlClient;
using Deadliner.Api.Models;
using Deadliner.Api.Models.Relationships;
using Deadliner.Models;
using Deadliner.Storage.Ado.DataProviders;

namespace Deadliner.Storage.Ado.Mappers;

public class UserToSuperGroupMapper : IMapper<IUserToSuperGroup>
{
    public IUserToSuperGroup ReadItem(SqlDataReader rd)
    {
        var id = (int)rd["id"];
        var userId = (int)rd["userid"];
        var groupId = (int)rd["groupid"];
        var user = new UserDataProvider().Get(userId);
        var group = new SuperGroupDataProvider().Get(groupId);

        return new UserToSuperGroup(id, user, group);
    }
}