using System.Data.SqlClient;
using Deadliner.Api.Models;
using Deadliner.Models;

namespace Deadliner.Storage.Ado.Mappers;

public class GroupMapper : IMapper<IGroup>
{
    public IGroup ReadItem(SqlDataReader rd)
    {
        var user = new User
        {
            Id = (int)rd["goid"],
            Username = (string)rd["gousername"],
            Password = (string)rd["gopassword"]
        };
        
        var superGroup = new SuperGroup(
            (int)rd["sgid"],
            (string)rd["sgtitle"],
            (string)rd["sgdescription"],
            rd["sgkey"] is DBNull ? null: (string)rd["sgkey"],
            user
        );
        
        return new Group
        (
            (int)rd["groupid"],
            (string)rd["grouptitle"],
            (string)rd["groupdescription"],
            rd["groupkey"] is DBNull ? null: (string)rd["groupkey"],
            user,
            superGroup
        );
    }
}