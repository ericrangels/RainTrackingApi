using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RainTrackingApi.Data;
using RainTrackingApi.Mappings;
using RainTrackingApi.Repositories;
using RainTrackingApi.Repositories.Interfaces;
using RainTrackingApi.Services;
using RainTrackingApi.Services.Interfaces;
using RainTrackingApi.Swagger;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<RainTrackingDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IRainTrackingRepository, RainTrackingRepository>();

builder.Services.AddScoped<IRainLogService, RainLogService>();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapperProfiles>());

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Rain Tracking API", Version = "v1" });
    c.EnableAnnotations();

    // include XML comments for controller/method summaries
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        c.IncludeXmlComments(xmlPath);

    // add the operation filter to describe the x-userId header
    c.OperationFilter<AddUserIdHeaderOperationFilter>();

    // enable example filters from Swashbuckle.Filters
    c.ExampleFilters();
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rain Tracking API v1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
