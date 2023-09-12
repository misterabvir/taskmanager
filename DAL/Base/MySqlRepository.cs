using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Base;

/// <summary> service for work with db </summary>
public class MySqlRepository : IRepository
{    
    public string ConnectionString { get; set; }
    
    public async Task ExecuteAsync(string sql, object model)
    {
        using (var connection = new MySqlConnection(ConnectionString))
        {
            await connection.OpenAsync();
            await connection.ExecuteAsync(sql, model);
        }
    }
   
    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object model)
    {
        using (var connection = new MySqlConnection(ConnectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync<T>(sql, model);
        }
    }
   
    public async Task<T> QuerySingleOrDefaultAsync<T>(string sql, object model)
    {
        using (var connection = new MySqlConnection(ConnectionString))
        {
            await connection.OpenAsync();
            return await connection.QuerySingleOrDefaultAsync<T>(sql, model);
        }
    }
}
