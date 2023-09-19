var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<DAL.Base.IRepository>(provider =>
    new DAL.Base.MySqlRepository()
    {
        ConnectionString = builder.Configuration.GetConnectionString("MySql") ??
            throw new Exception("not found conection string in configuration")
    }   
);

builder.Services.AddScoped<DAL.IProjectsDAL, DAL.ProjectsDAL>();
builder.Services.AddScoped<DAL.ITasksDAL, DAL.TasksDAL>();
builder.Services.AddScoped<DAL.ICommentsDAL, DAL.CommentsDAL>();

builder.Services.AddScoped<BL.IProjects, BL.Projects>();
builder.Services.AddScoped<BL.ITasks, BL.Tasks>();
builder.Services.AddScoped<BL.IComments, BL.Comments>();


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
