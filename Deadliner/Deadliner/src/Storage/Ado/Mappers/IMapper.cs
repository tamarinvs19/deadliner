using System.Data.SqlClient;

namespace Deadliner.Storage.Ado.Mappers;

public interface IMapper<out T>
{
    T ReadItem(SqlDataReader rd);
}