using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using TestSystem.Application;
using TestSystem.Application.Common.Mappings;
using TestSystem.Application.Interfaces;
using TestSystem.Domain.Data.Entities;
using TestSystem.Persistence;
using TestSystem.WebApi;
using TestSystem.WebApi.Middleware.CustomExceptionMiddleware;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(ITestSystemDbContext).Assembly));
});
builder.Services.AddPersistence(configuration);
builder.Services.AddApplication(configuration);
builder.Services.AddWebApi(configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DbInitializer>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("cors");
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseCustomExceptionHandler();
app.MapControllers();

using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    await DbInitializer.InitializeAsync(roleManager);
}

app.Run();
