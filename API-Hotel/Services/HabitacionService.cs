using AccessDB;
using AccessDB.Models;
using AccessDB.Repositories;
using API_Hotel.Interfaces;

namespace API_Hotel.Services
{
    public class HabitacionService: IHabitacionService
    {
        private readonly IHabitacionRepository _habitacionRepository;
        private readonly IReservacionRepository _reservacionRepository;

        public HabitacionService(IHabitacionRepository habitacionRepository, IReservacionRepository reservacionRepository)
        {
            _habitacionRepository = habitacionRepository;
            _reservacionRepository = reservacionRepository;
        }
        public async Task< List<Habitacion>> ObtenerTodosAsync()
        {
            try
            {
                return await _habitacionRepository.ObtenerTodosAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las habitaciones.", ex);
            }
        }
        public async Task<Habitacion> GetHabitacionByNumeroAsync(int numeroHabitacion)
        {
            try
            {
                var habitacion = await _habitacionRepository.GetHabitacionByNumeroAsync(numeroHabitacion);
                // Devolver null si no existe; el controller API maneja NotFound
                return habitacion;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Error al buscar la habitación por número.", ex);
            }
        }
        public async Task<Habitacion> GetHabitacionByIdAsync(int id)
        {
            try
            {
                var habitacion = await _habitacionRepository.GetHabitacionByIdAsync(id);
                // Devolver null si no existe; el controller API maneja NotFound
                return habitacion;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar la habitación.", ex);
            }
        }
        public async Task<Habitacion> CreateAsync(Habitacion habitacion)
        {
            try
            {
                var existingHabitacion =await _habitacionRepository.GetHabitacionByNumeroAsync(habitacion.NumeroHabitacion);
                if (existingHabitacion != null)
                {
                    throw new InvalidOperationException($"Ya existe una habitación registrada con el número: " +$"{habitacion.NumeroHabitacion}");
                }
                return await _habitacionRepository.CreateAsync(habitacion);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la habitación.", ex);
            }
        }
        public async Task UpdateAsync(Habitacion habitacion)
        {
            try
            {
                var habitacionExistente =await GetHabitacionByNumeroAsync(habitacion.NumeroHabitacion);
                if (habitacionExistente == null)
                {
                    throw new InvalidOperationException($"No existe habitación con el número: " +$"{habitacion.NumeroHabitacion}");
                }

                if (habitacion.TipoHabitacion != null)
                {
                    habitacionExistente.TipoHabitacion =habitacion.TipoHabitacion;
                }
                if (habitacion.TarifaxNoche > 0)
                {
                    habitacionExistente.TarifaxNoche =habitacion.TarifaxNoche;
                }
                habitacionExistente.Tv_Satelital =habitacion.Tv_Satelital;

                if (habitacion.PendientesMantenimiento != null)
                {
                    habitacionExistente.PendientesMantenimiento =habitacion.PendientesMantenimiento;
                }

                await _habitacionRepository.UpdateAsync(habitacionExistente);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la habitación.", ex);
            }
        }
        public async Task DeleteAsync(int id)
        {
            try
            {
                var habitacion =await _habitacionRepository.GetHabitacionByIdAsync(id);
                if (habitacion == null)
                {
                    throw new InvalidOperationException("Habitación no encontrada.");
                }
                bool tieneReservaciones =await _reservacionRepository.ExisteReservacionPorHabitacionAsync(id);
                if (tieneReservaciones)
                {
                    throw new InvalidOperationException("No se puede eliminar la habitación porque tiene reservaciones asociadas.");
                }
                await _habitacionRepository.DeleteAsync(id);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la habitación.", ex);
            }
        }
        public async Task<int> totalHabitacionesAsync()
        {
            try
            {
                return await _habitacionRepository.totalHabitacionesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el total de habitaciones.", ex);
            }
        }

    }
}
