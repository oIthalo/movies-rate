using Microsoft.Data.SqlClient;
using System.Data;

namespace MoviesRate.Infrastructure.DataAccess.DataContexts;

public class MoviesRateDbContextDapper : IDisposable
{
    public MoviesRateDbContextDapper(string connectionString)
    {
        Connection = new SqlConnection(connectionString);
        Connection.Open();
    }

    public SqlConnection Connection { get; set; }

    public void Dispose()
    {
        if (Connection.State is not ConnectionState.Closed)
            Connection.Close();
    }
}