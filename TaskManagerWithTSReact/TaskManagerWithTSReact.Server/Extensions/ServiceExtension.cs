using Domain;
using Microsoft.Extensions.Configuration;

namespace TaskManagerWithTSReact.Server.Extensions
{
    public static class ServiceExtension
    {
        public static void AddProviderDb(this IServiceCollection services, ConfigurationManager configuration, ProviderDb provider)
        {
            services.AddScoped<DAL.Base.IDbStringConnection>(provider => new DAL.Base.MySqlStringConnection(configuration.GetConnectionString(provider.ToString()!)));
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
            services.AddScoped<BL.IProjects, BL.Projects>();
            services.AddScoped<BL.ITasks, BL.Tasks>();
            services.AddScoped<BL.IComments, BL.Comments>();
        }
    }
}
