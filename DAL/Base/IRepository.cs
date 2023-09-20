using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Base;

public interface IRepository
{
    string ConnectionString { get; set; }
    
    Task ExecuteAsync(string sql, object model);

    Task<IEnumerable<T>> QueryAsync<T, E>(string sql, Func<T, E, T> map, string splitOn, object model = null);
}
