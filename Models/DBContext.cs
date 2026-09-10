using AccessDB.Models;
using Microsoft.EntityFrameworkCore;

namespace AccessDB
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
        public DbSet<Reservacion> Reservaciones { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Habitacion> Habitacion { get; set; }
        public DbSet<TipoHabitacion> TipoHabitacion { get; set; }
        public DbSet<Canton> Canton { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Distrito> Distrito { get; set; }
        public DbSet<Provincia> Provincia { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<EstadoReserva> EstadoReserva { get; set; }
      

    }
}
