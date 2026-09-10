using Microsoft.AspNetCore.Builder;
using AccessDB.Models;
using AccessDB;
using MVC.Services;
using MVC.Interfaces;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Leer la URL base de la API desde configuración (appsettings.Development.json)
var apiBaseUrl = builder.Configuration.GetValue<string>("ApiBaseUrl") ?? "http://localhost:5050";

//CATEGORIA
builder.Services.AddScoped<ICategoriaApiService, CategoriaApiService>();
builder.Services.AddHttpClient<ICategoriaApiService, CategoriaApiService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

//CLIENTE
builder.Services.AddScoped<IClienteApiService, ClienteApiService>();
builder.Services.AddHttpClient<IClienteApiService, ClienteApiService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

//EMPLEADO
builder.Services.AddScoped<IEmpleadoService, EmpleadoApiService>();
builder.Services.AddHttpClient<IEmpleadoService, EmpleadoApiService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

//ESTADO RESERVACION
builder.Services.AddScoped<IEstadoApiService, EstadoApiService>();
builder.Services.AddHttpClient<IEstadoApiService, EstadoApiService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

//HABITACION
builder.Services.AddScoped<IHabitacionService, HabitacionService>();
builder.Services.AddHttpClient<IHabitacionService, HabitacionService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

//RESERVACION
builder.Services.AddScoped<IReservacionApiService, ReservacionApiService>();
builder.Services.AddHttpClient<IReservacionApiService, ReservacionApiService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

//TIPO HABITACION
builder.Services.AddScoped<ITipoHabitacionService, TipoHabitacionService>();
builder.Services.AddHttpClient<ITipoHabitacionService, TipoHabitacionService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

//UBICACION
builder.Services.AddScoped<IUbicacionService, UbicacionService>();
builder.Services.AddHttpClient<IUbicacionService, UbicacionService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
