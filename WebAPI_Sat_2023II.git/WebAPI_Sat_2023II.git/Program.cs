using Microsoft.EntityFrameworkCore;
using WebAPI_Sat_2023II.git.DAL;
using WebAPI_Sat_2023II.git.Domain.Interfaces;
using WebAPI_Sat_2023II.git.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Esta linea me crea el contexto de la Bd a la hora de correr esta API
builder.Services.AddDbContext<DataBaseContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//Funciones anonimas (x => x...) Arrow Functions / Lambda Functions 

builder.Services.AddScoped<ICountryService, CountryService>();
//Por cada nuevo servicio/interfaz que yo creo en mi API, debo crear aquí una nieva dependencia.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
