using DAL;
using DAL.Base;

namespace TaskManagerTests.Base;

public class DALTestBase
{
    protected IRepository repository;
    protected IProjectDAL projectDAL;

    public DALTestBase()
    {
        repository = new MySqlRepository() { ConnectionString = "Server=localhost;Port=3306;Database=TaskManagerDb;Uid=root;Pwd=password;" };
        projectDAL = new ProjectDAL(repository);
    }   
}
