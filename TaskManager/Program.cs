var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<DAL.Base.IRepository>(new DAL.Base.MySqlRepository()
{
    ConnectionString = builder.Configuration.GetConnectionString("MySql") ??
        throw new Exception("not found conection string in configuration")
});

builder.Services.AddSingleton<DAL.IProjectsDAL, DAL.ProjectsDAL>();
builder.Services.AddSingleton<DAL.ITasksDAL, DAL.TasksDAL>();
builder.Services.AddSingleton<DAL.ICommentsDAL, DAL.CommentsDAL>();

builder.Services.AddSingleton<BL.IProjects, BL.Projects>();
builder.Services.AddSingleton<BL.ITasks, BL.Tasks>();
builder.Services.AddSingleton<BL.IComments, BL.Comments>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
