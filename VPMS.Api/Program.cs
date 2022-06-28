using Serilog;
using Serilog.Events;
using VPMS.Api.DIServiceExtensions;
using VPMS.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    Log.Logger = new LoggerConfiguration()
       .ReadFrom.Configuration(builder.Configuration)
       .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs/log-.txt"), restrictedToMinimumLevel: LogEventLevel.Error, rollingInterval: RollingInterval.Day)
       .CreateLogger();
}
else
{
    Log.Logger = new LoggerConfiguration()
       .MinimumLevel.Error()
       .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs/log-.txt"), rollingInterval: RollingInterval.Day)
       .CreateLogger();
}

builder.Host.UseSerilog();

// Add services to the container.
var services = builder.Services;

services.AddControllerConfig();

services.AddSwaggerConfig();

services.AddCorsConfig();

var app = builder.Build();

app.UseCustomExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        //c.RoutePrefix = string.Empty;
    });
}

else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

//app.UseCors();

//app.UseAuthentication();

//app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();





//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseRouting();


//app.MapControllers();

//app.MapFallbackToFile("index.html"); ;

//app.Run();
