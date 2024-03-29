using System.Data;
using System.Data.SqlClient;
using Deadliner.Storage.Ado.Mappers;

namespace Deadliner.Storage.Ado;

public class DbHelper
{
    private readonly string _connectionString;

    public DbHelper(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<T> GetData<T>(IMapper<T> mapper, string queryString, params SqlParameter[] args)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            return GetData(connection, mapper, queryString, args);
        }
    }

    private static List<T> GetData<T>(
        SqlConnection connection,
        IMapper<T> mapper, string queryString,
        params SqlParameter[] args)
    {
        var result = new List<T>();

        try
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddRange(args);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                result.Add(mapper.ReadItem(reader));
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error from DBHelper (GetData): {ex.Message}");
        }

        return result;
    }

    public T GetItem<T>(IMapper<T> mapper, string queryString, params SqlParameter[] args)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            return GetItem(connection, mapper, queryString, args);
        }
    }

    private static T GetItem<T>(SqlConnection connection, IMapper<T> mapper, string queryString,
        params SqlParameter[] args)
    {
        T result = default(T);

        try
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddRange(args);

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            result = mapper.ReadItem(reader);

            reader.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error from DBHelper (GetItem): {ex.Message}");
        }

        return result;
    }
    
    public void UpdateItem<T>(IMapper<T> mapper, string queryString, params SqlParameter[] args)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        UpdateItem(connection, mapper, queryString, args);
    }

    private static void UpdateItem<T>(SqlConnection connection, IMapper<T> mapper, string queryString,
        params SqlParameter[] args)
    {
        try
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddRange(args);

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            mapper.ReadItem(reader);

            reader.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error from DBHelper (Update): {ex.Message}");
        }
    }
}