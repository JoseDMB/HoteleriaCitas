using API_Hotel.Interfaces;
using API_Hotel.Services;
using AccessDB.Repositories;
using AccessDB.Models;
using AccessDB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AccessDB.DBContext>(x => x.UseSqlServer(connectionString));

//REPOSITORIES
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IHabitacionRepository, HabitacionRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IReservacionRepository, ReservacionRepository>();
builder.Services.AddScoped<IEmpleadosRepository, EmpleadosRepository>();
builder.Services.AddScoped<IEstadoRepository, EstadoRepository>();
builder.Services.AddScoped<ITipoHabRepository, TipoHabRepository>();
builder.Services.AddScoped<IUbicacionRepository, UbicacionRepository>();

//SERVICES
builder.Services.AddScoped<IHabitacionService, HabitacionService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IReservacionService, ReservacionService>();
builder.Services.AddScoped<IEmpleadosService, EmpleadosService>();
builder.Services.AddScoped<IUbicacionService, UbicacionService>();
builder.Services.AddScoped<ITipoHabitacionService, TipoHabitacionService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IEstadoService, EstadoResevService>();







//// Repositories (in-memory) - usar Singleton para mantener estado en memoria entre peticiones
//builder.Services.AddSingleton<AccessDB.Interfaces.ICategoriaRepository, AccessDB.Repositories.CategoriaRepository>();
//builder.Services.AddSingleton<API_Hotel.Repositories.IHabitacionRepository, API_Hotel.Repositories.HabitacionRepository>();
//builder.Services.AddSingleton<API_Hotel.Repositories.IClienteRepository, API_Hotel.Repositories.ClienteRepository>();
//builder.Services.AddSingleton<API_Hotel.Repositories.IReservacionRepository, API_Hotel.Repositories.ReservacionRepository>();
//builder.Services.AddSingleton<API_Hotel.Repositories.ICategoriaRepository, API_Hotel.Repositories.CategoriaRepository>();
//builder.Services.AddSingleton<API_Hotel.Repositories.IEmpleadosRepository, API_Hotel.Repositories.EmpleadosRepository>();


//// Services - dependencias coherentes con los repositorios (Singleton)
//builder.Services.AddSingleton<IHabitacionService, API_Hotel.Services.HabitacionService>();
//builder.Services.AddSingleton<IClienteService, API_Hotel.Services.ClienteService>();
//builder.Services.AddSingleton<IReservacionService, API_Hotel.Services.ReservacionService>();
//builder.Services.AddSingleton<IEmpleadosService, API_Hotel.Services.EmpleadosService>();
//builder.Services.AddSingleton<API_Hotel.Services.EmpleadosService>();
//builder.Services.AddSingleton<API_Hotel.Services.TipoHabitacionService>();
//builder.Services.AddSingleton<API_Hotel.Services.CategoriaService>();
//builder.Services.AddSingleton<API_Hotel.Services.EstadoResevService>();
//builder.Services.AddSingleton<API_Hotel.Services.UbicacionService>();




var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("AllowLocal");

app.UseAuthorization();

app.MapControllers();

app.Run();
