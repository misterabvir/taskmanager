using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Base;

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

    public async Task<IEnumerable<T>> QueryAsync<T, E>(string sql, Func<T, E, T> map, string splitOn, object model = null)
    {
        using (var connection = new MySqlConnection(ConnectionString))
        {
            await connection.OpenAsync();
            return await connection.QueryAsync(sql: sql, map: map, splitOn: splitOn, param: model);
        }
    }
}
