using TaskManagerWithTSReact.Server.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();


builder.Services.AddProviderDb(builder.Configuration, ProviderDb.MsSql);
//builder.Services.AddProviderDb(builder.Configuration, ProviderDb.MySql);

builder.Services.AddDAL();

builder.Services.AddOrm(Orm.Ef);
//builder.Services.AddOrm(Orm.Dapper);

builder.Services.AddBL();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");


app.Run();

