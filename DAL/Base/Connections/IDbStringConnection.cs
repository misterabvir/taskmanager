using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DAL.Base
{
    public interface IDbStringConnection
    {
        string ConnectionString { get; }
        IDbConnection GetDbConnection();
        void SetOptions(DbContextOptionsBuilder optionsBuilder);
    }
}
