using AccessDB.Models;
using API_Hotel.Interfaces;
using AccessDB;

namespace API_Hotel.Services
{
    public class EmpleadosService : IEmpleadosService
    {
        private readonly IEmpleadosRepository _empleadosRepository;

        public EmpleadosService(IEmpleadosRepository empleadosRepository)
        {
            _empleadosRepository = empleadosRepository;
        }
        public async Task<List<Empleado>> ObtenerTodosAsync()
        {
            return await _empleadosRepository.ObtenerTodosAsync();
        }
        public async Task<Empleado> GetByIdAsync(int id)
        {
            var empleado = await _empleadosRepository.GetByIdAsync(id);
            if (empleado == null)
            {
                throw new Exception("Empleado no encontrado");
            }
            return empleado;
        }
        public async Task<Empleado> GetByIdCardAsync(string cedula)
        {
            var empleado = await _empleadosRepository.GetByIdCardAsync(cedula);
            if(empleado == null)
            {
                throw new Exception("Empleado no encontrado");
            }
            return empleado;
        }

        public async Task<Empleado> CreateAsync(Empleado empleado)
        {
            var existingEmpleado = await _empleadosRepository.GetByIdCardAsync(empleado.Cedula);
            if(existingEmpleado != null)
            {
                throw new Exception("Este número de cédula ya está registrado");
            }
            return await _empleadosRepository.CreateAsync(empleado);
        }

        public async Task UpdateAsync(Empleado empleado)
        {
            var existingEmpleado = await _empleadosRepository.GetByIdAsync(empleado.Id);
            if (existingEmpleado == null)
            {
                throw new Exception("Este número de cédula ya está registrado");
            }
            var clienteConMismaCedula = await _empleadosRepository.GetByIdCardAsync(empleado.Cedula);

            if (clienteConMismaCedula != null && clienteConMismaCedula.Id != empleado.Id)
            {
                throw new InvalidOperationException(
                    "Esta cédula ya está registrada por otro empleado.");
            }
            existingEmpleado.Nombre = empleado.Nombre;
            existingEmpleado.Cedula = empleado.Cedula;
            existingEmpleado.PrimerApellido = empleado.PrimerApellido;
            existingEmpleado.SegundoApellido = empleado.SegundoApellido;
            existingEmpleado.FechaNacimiento = empleado.FechaNacimiento;
            existingEmpleado.FechaIngreso = empleado.FechaIngreso;
            existingEmpleado.SalarioMensual = empleado.SalarioMensual;
            existingEmpleado.CategoriaId = empleado.CategoriaId;
            existingEmpleado.ProvinciaId = empleado.ProvinciaId;
            existingEmpleado.CantonId = empleado.CantonId;
            existingEmpleado.DistritoId = empleado.DistritoId;
            existingEmpleado.Direccion = empleado.Direccion;
            await _empleadosRepository.UpdateAsync(existingEmpleado);
        }

        public async Task DeleteAsync(int id)
        {
            var empleado = await _empleadosRepository.GetByIdAsync(id);
            if (empleado == null)
            {
                throw new Exception("Empleado no encontrado");
            }
            await _empleadosRepository.DeleteAsync(id);
        }
        public async Task<int> totalEmpleadosAsync()
        {
            return await _empleadosRepository.totalEmpleadosAsync();
        }

    }
}
