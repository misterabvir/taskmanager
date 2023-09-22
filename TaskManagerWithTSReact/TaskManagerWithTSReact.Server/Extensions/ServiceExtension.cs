using Domain;

namespace TaskManagerWithTSReact.Server.Extensions
{
    public static class ServiceExtension
    {
        public static void AddProviderDb(this IServiceCollection services, ConfigurationManager configuration, ProviderDb provider)
        {
            if(provider == ProviderDb.MsSql)
                services.AddSingleton<DAL.Base.IDbStringConnection>(provider => new DAL.Base.MsSqlStringConnection(configuration.GetConnectionString("MsSql")));
            else if(provider == ProviderDb.MySql)
                services.AddSingleton<DAL.Base.IDbStringConnection>(provider => new DAL.Base.MySqlStringConnection(configuration.GetConnectionString("MySql")));
        }

        public static void AddDAL(this IServiceCollection services)
        {
            services.AddScoped<DAL.Base.IRepository<ProjectModel>, DAL.ProjectsDAL>();
            services.AddScoped<DAL.Base.IRepository<TaskModel>, DAL.TasksDAL>();
            services.AddScoped<DAL.Base.IRepository<CommentModel>, DAL.CommentsDAL>();
        }

        public static void AddOrm(this IServiceCollection services, Orm orm)
        {
            if (orm == Orm.Dapper)
            {
                services.AddScoped<DAL.DapperAccess.Base.IExecuteService, DAL.DapperAccess.Base.DapperExecuteService>();
                services.AddScoped<DAL.Base.IDataAccess<CommentModel>, DAL.DapperAccess.CommentsDapperDAL>();
                services.AddScoped<DAL.Base.IDataAccess<ProjectModel>, DAL.DapperAccess.ProjectsDapperDAL>();
                services.AddScoped<DAL.Base.IDataAccess<TaskModel>, DAL.DapperAccess.TasksDapperDAL>();
            }
            else if (orm == Orm.Ef)
            {
                services.AddScoped<DAL.EFAccess.TaskManagerDbContext>();
                services.AddScoped<DAL.Base.IDataAccess<CommentModel>, DAL.EFAccess.CommentEFDal>();
                services.AddScoped<DAL.Base.IDataAccess<ProjectModel>, DAL.EFAccess.ProjectEFDal>();
                services.AddScoped<DAL.Base.IDataAccess<TaskModel>, DAL.EFAccess.TaskEFDal>();
            }
            
        }

        public static void AddBL(this IServiceCollection services)
        {
            services.AddTransient<BL.IProjects, BL.Projects>();
            services.AddTransient<BL.ITasks, BL.Tasks>();
            services.AddTransient<BL.IComments, BL.Comments>();
        }
    }
}
