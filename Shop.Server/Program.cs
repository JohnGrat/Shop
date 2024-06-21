using FastEndpoints;
using FastEndpoints.Swagger;
using Shop.Application;
using Shop.Domain.Repositories.Interfaces;
using Shop.Infrastructure;
using Shop.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFastEndpoints()
   .SwaggerDocument();


var app = builder.Build();

await app.Services.GetRequiredService<IEventStoreRepository>().ReplayAsync();

app.UseDefaultFiles();
app.UseStaticFiles();

app
    .UseFastEndpoints()
    .UseSwaggerGen();

app.UseHttpsRedirection();
app.MapFallbackToFile("/index.html");
app.Run();
