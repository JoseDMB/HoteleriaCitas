using AccessDB.Models;
using Microsoft.EntityFrameworkCore;

namespace AccessDB
{
    public class EmpleadosRepository : IEmpleadosRepository
    {
        private readonly DBContext _empleados;

        public EmpleadosRepository(DBContext empleados)
        {
            _empleados = empleados;
        }
        public async Task<List<Empleado>> ObtenerTodosAsync()
        {
            return await _empleados.Empleados.AsNoTracking().OrderByDescending(c => c.FechaIngreso).ToListAsync();
        }
        public async Task<Empleado> GetByIdAsync(int id)
        {
            return await _empleados.Empleados.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Empleado> GetByIdCardAsync(string cedula)
        {
            return await _empleados.Empleados.AsNoTracking().FirstOrDefaultAsync(c => c.Cedula == cedula);
        }

        public async Task<Empleado> CreateAsync(Empleado empleados)
        {
            await _empleados.Empleados.AddAsync(empleados);
            await _empleados.SaveChangesAsync();
            return empleados;
        }

        public async Task UpdateAsync(Empleado empleado)
        {
           _empleados.Empleados.Update(empleado);
            await _empleados.SaveChangesAsync();    
        }

        public async Task DeleteAsync(int id)
        {
            var empleado = await _empleados.Empleados.FirstOrDefaultAsync(c => c.Id == id);
            _empleados.Empleados.Remove(empleado);
            await _empleados.SaveChangesAsync();
        }
        public async Task<int> totalEmpleadosAsync()
        {
            return await _empleados.Empleados.CountAsync();
        }
    }
}
