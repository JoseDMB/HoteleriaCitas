using AccessDB.Models;
using Microsoft.EntityFrameworkCore;

namespace APIdb
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DbSet<Reservacion> Reservaciones { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Habitacion> Habitaciones { get; set; }
        public DbSet<TipoHabitacion> TipoHabitaciones { get; set; }
        public DbSet<Canton> Cantones { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Distrito> Distritos { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<EstadoReserva> EstadosReservas { get; set; }
      

    }
}
