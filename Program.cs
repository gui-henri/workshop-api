using Microsoft.EntityFrameworkCore;
using WorkshopApi.Contexts;
using WorkshopApi.Interfaces;
using WorkshopApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ICollaboratorRepository, CollaboratorRepository>();
builder.Services.AddDbContext<WorkshopContext>(options =>
{
    var connString = builder.Configuration.GetConnectionString("WorkshopContext");
    options.UseMySql(connString, ServerVersion.AutoDetect(connString));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
