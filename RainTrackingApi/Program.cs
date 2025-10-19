using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RainTrackingApi.Data;
using RainTrackingApi.Mappings;
using RainTrackingApi.Repositories;
using RainTrackingApi.Repositories.Interfaces;
using RainTrackingApi.Services;
using RainTrackingApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Rain Tracking API", Version = "v1" });
});

builder.Services.AddDbContext<RainTrackingDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IRainTrackingRepository, RainTrackingRepository>();

builder.Services.AddScoped<IRainLogService, RainLogService>();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapperProfiles>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
