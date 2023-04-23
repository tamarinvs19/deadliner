using System.Data.SqlClient;
using Deadliner.Api.Models;
using Deadliner.Models;

namespace Deadliner.Storage.Ado.Mappers;

public class SuperGroupMapper : IMapper<ISuperGroup>
{
    public ISuperGroup ReadItem(SqlDataReader rd)
    {
        var user = new User
        {
            Id = (int)rd["Owner"],
            Username = (string)rd["Username"],
            Password = (string)rd["Password"]
        };
        
        return new SuperGroup
        (
            (int)rd["id"],
            (string)rd["title"],
            (string)rd["description"],
            rd["accesskey"] is DBNull ? null: (string)rd["accesskey"],
            user
        );
    }
}