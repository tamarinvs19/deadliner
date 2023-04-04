using System.Data.SqlClient;
using Deadliner.Api.Models;
using Deadliner.Models;

namespace Deadliner.Storage.Ado.Mappers;

public class UserMapper : IMapper<IUser>
{
    public IUser ReadItem(SqlDataReader rd)
    {
        return new User
        {
            Id = (int)rd["id"],
            Username = (string)rd["username"],
            Password = (string)rd["password"]
        };
    }
}