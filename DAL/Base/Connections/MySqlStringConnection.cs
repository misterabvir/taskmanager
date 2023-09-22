using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Data;

namespace DAL.Base;

public class MySqlStringConnection : IDbStringConnection
{

    public MySqlStringConnection(string connectionString)
    {
        this.ConnectionString = connectionString;
    }

    public string ConnectionString {get;set;}

    public IDbConnection GetDbConnection()
    {
        return new MySqlConnection(ConnectionString);
    }

    public void SetOptions(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(ConnectionString);
    }
}
