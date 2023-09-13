var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<DAL.Base.IRepository>(new DAL.Base.MySqlRepository()
{
    ConnectionString = builder.Configuration.GetConnectionString("Default") ??
        throw new Exception("not found conection string in configuration")
});

builder.Services.AddSingleton<DAL.IProjectsDAL, DAL.ProjectsDAL>();

builder.Services.AddSingleton<BL.IProjects, BL.Projects>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
