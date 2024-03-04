using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WorkshopApi.Contexts;
using WorkshopApi.Interfaces;
using WorkshopApi.Repositories;
using WorkshopApi.Services;

var CorsOriginsName = "CorsOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsOriginsName, policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddScoped<ICollaboratorRepository, CollaboratorRepository>();
builder.Services.AddScoped<IWorkshopRepository, WorkshopRepository>();
builder.Services.AddScoped<WorkshopService>();
builder.Services.AddScoped<CollaboratorService>();
builder.Services.AddDbContext<WorkshopContext>(options =>
{
    var connString = builder.Configuration.GetConnectionString("WorkshopContext");
    options.UseMySql(connString, ServerVersion.AutoDetect(connString));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Workhshop API",
        Description = "Uma API para gerenciar ata de presença em Workshops",
        Contact = new OpenApiContact
        {
            Name = "Guilherme",
            Url = new Uri("https://portfolio-three-xi-47.vercel.app/")
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(CorsOriginsName);

app.UseAuthorization();

app.MapControllers();

app.Run();
