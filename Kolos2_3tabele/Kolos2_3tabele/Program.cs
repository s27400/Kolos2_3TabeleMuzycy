using Kolos2_3tabele.Entities;
using Kolos2_3tabele.Repositories;
using Kolos2_3tabele.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<IMusicRepository, MusicRepository>();
builder.Services.AddScoped<IMusicService, MusicService>();

//dotnet new tool-manifest
//dotnet tool install dotnet-ef
//dotnet ef migrations add Init
//dotnet ef database update

//Microsoft.EntityFrameworkCore
//Microsoft.EntityFrameworkCore.SqlServer
//Microsoft.EntityFrameworkCore.Design

builder.Services.AddDbContext<MusicDbContext>(opt =>
{
    string con = builder.Configuration.GetConnectionString("Default");
    opt.UseSqlServer(con);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();
app.MapControllers();
app.Run();
