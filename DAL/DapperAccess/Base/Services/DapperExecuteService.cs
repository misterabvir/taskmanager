using DAL.Base;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.DapperAccess.Base;

public class DapperExecuteService : IExecuteService
{
    private IDbStringConnection connectionFactory;

    public DapperExecuteService(IDbStringConnection connectionFactory)
    {
        this.connectionFactory = connectionFactory;
        SqlMapper.AddTypeHandler(new GuidTypeHandler());
    }

    public async Task ExecuteAsync(string sql, object model)
    {
        using (var connection = connectionFactory.GetDbConnection())
        {
            connection.Open();
            await connection.ExecuteAsync(sql, model);
        }
    }

    public async Task<IEnumerable<T>> QueryAsync<T, E>(string sql, Func<T, E, T> map, string splitOn, object model = null)
    {
        using (var connection = connectionFactory.GetDbConnection())
        {
            connection.Open();
            return await connection.QueryAsync(sql: sql, map: map, splitOn: splitOn, param: model);
        }
    }

    public async Task<T> QuerySingleAsync<T>(string sql, object model)
    {
        using (var connection = connectionFactory.GetDbConnection())
        {
            connection.Open();
            var c = await connection.QueryFirstAsync<T>(sql, model);
            return c;
        }
    }
}
