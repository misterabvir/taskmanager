var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<DAL.Base.IRepository>(new DAL.Base.MySqlRepository() 
    {ConnectionString = builder.Configuration.GetConnectionString("Default") ??
        throw new Exception("not found conection string in configuration")});

builder.Services.AddSingleton<DAL.IProjectDAL, DAL.ProjectDAL>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Main/Error");    
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Main}/{action=Index}/{id?}");

app.Run();
