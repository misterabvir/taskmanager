using DAL;
using DAL.Base;

namespace TaskManagerTests.Base;

public class DALTestBase
{
    protected IRepository repository;
    protected IProjectsDAL projectDAL;
    protected ITasksDAL taskDAL;
    protected ICommentsDAL commentsDAL;

    public DALTestBase()
    {
        repository = new MySqlRepository() { ConnectionString = "Server=localhost;Port=3306;Database=TaskManagerDb;Uid=root;Pwd=password;" };
        projectDAL = new ProjectsDAL(repository);
        taskDAL = new TasksDAL(repository);
        commentsDAL = new CommentsDAL(repository);
    }   
}
