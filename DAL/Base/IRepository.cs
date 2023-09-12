using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Base;

/// <summary> interface for work with db </summary>
public interface IRepository
{
    /// <summary> connection string </summary>
    string ConnectionString { get; set; }
    
    /// <summary> non-return query for inserts and updates </summary>
    Task ExecuteAsync(string sql, object model);
    
    /// <summary> request to get all possible rows </summary>
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object model);
    
    /// <summary> request to get one row if it exists </summary>
    Task<T> QuerySingleOrDefaultAsync<T>(string sql, object model);
}
