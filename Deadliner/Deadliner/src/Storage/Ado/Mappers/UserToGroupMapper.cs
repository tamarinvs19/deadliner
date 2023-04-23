using System.Data.SqlClient;
using Deadliner.Api.Models;
using Deadliner.Api.Models.Relationships;
using Deadliner.Models;
using Deadliner.Storage.Ado.DataProviders;

namespace Deadliner.Storage.Ado.Mappers;

public class UserToGroupMapper : IMapper<IUserToGroup>
{
    public IUserToGroup ReadItem(SqlDataReader rd)
    {
        var id = (int)rd["id"];
        var userId = (int)rd["userid"];
        var groupId = (int)rd["groupid"];
        var user = new UserDataProvider().Get(userId);
        var group = new GroupDataProvider().Get(groupId);

        return new UserToGroup(id, user, group);
    }
}