using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DAL.Base;

public class MsSqlStringConnection : IDbStringConnection
{
    public string ConnectionString { get; private set; }
    public MsSqlStringConnection(string connectionString)
    {
        this.ConnectionString = connectionString;
    }

    public IDbConnection GetDbConnection()
    {
        return new SqlConnection(ConnectionString);
    }

    public void SetOptions(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString);
    }
}
