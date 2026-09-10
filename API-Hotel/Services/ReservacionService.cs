using API_Hotel.Interfaces;
using AccessDB.Models;
using AccessDB;

namespace API_Hotel.Services
{
    public class ReservacionService : IReservacionService
    {
        private readonly IReservacionRepository _reservacionRepository;
        private readonly IHabitacionRepository _habitacionService;

        public ReservacionService(IReservacionRepository reservacionRepository, IHabitacionRepository habitacionService)
        {
            _reservacionRepository = reservacionRepository;
            _habitacionService = habitacionService;
        }

        public async Task<List<Reservacion>> ObtenerTodosAsync()
        {
            try
            {
                var reservaciones = await _reservacionRepository.ObtenerTodosAsync();
                
                return reservaciones ?? new List<Reservacion>();
            }
            catch (Exception ex)
            {
           
                throw new Exception("Ocurrió un error al obtener las reservaciones.", ex);
            }
        }

        public async Task<Reservacion> ObtenerPorIdAsync(int id)
        {
            try
            {
                var reservacion = await _reservacionRepository.ObtenerPorIdAsync(id);
                if (reservacion == null)
                    throw new KeyNotFoundException($"No se encontró la reservación con ID {id}");
                return reservacion;
            }
            catch (Exception ex)
            {
                // Manejar la excepción según sea necesario, por ejemplo, registrarla o lanzarla nuevamente
                throw new Exception($"Ocurrió un error al obtener la reservación con ID {id}.", ex);

            }
        }

        public async Task<Reservacion> ObtenerPorCodigoAsync(string codigo)
        {
            try
            {
                var reservacion = await _reservacionRepository.ObtenerPorCodigoAsync(codigo);
                if (reservacion == null)
                    throw new KeyNotFoundException($"No se encontró la reservación con código {codigo}");
                return reservacion;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error al obtener la reservación con código {codigo}.", ex);
            }
        }

        public async Task<Reservacion> CrearAsync(Reservacion reservacion)
        {
          
                var reservacionExistente = await _reservacionRepository.ObtenerPorCodigoAsync(reservacion.CodigoReserva);
                if (reservacionExistente != null)
                {
                    throw new InvalidOperationException(
                        "Ya existe una reservación con el mismo código");
                }
                if (reservacion.FechaIngreso >= reservacion.FechaSalida)
                {
                    throw new ArgumentException(
                        "La fecha de ingreso debe ser anterior a la fecha de salida.");
                }
                var habitacion = await _habitacionService.GetHabitacionByIdAsync(reservacion.IdHabitacion);
                if (habitacion == null)
                {
                    throw new KeyNotFoundException(
                        $"No se encontró la habitación con ID {reservacion.IdHabitacion}");
                }
                var disponible = await HabitacionDisponibleAsync(reservacion.IdHabitacion, reservacion.FechaIngreso, reservacion.FechaSalida);
                if (!disponible)
                {
                    throw new InvalidOperationException($"La habitación {reservacion.IdHabitacion} no está disponible de {reservacion.FechaIngreso} a {reservacion.FechaSalida}.");
                }

                //CALCULO TOTAL
                int noches = (int)(reservacion.FechaSalida.Date - reservacion.FechaIngreso.Date).TotalDays;

                reservacion.TarifaReservacion = habitacion.TarifaxNoche * noches;

                var descuento = reservacion.TarifaReservacion * reservacion.Descuento / 100m;

                var tarifaConDescuento = reservacion.TarifaReservacion - descuento;

                var iva = tarifaConDescuento * 0.13m; reservacion.Total = tarifaConDescuento + iva;
                return await _reservacionRepository.CrearAsync(reservacion);
           
           
        }

        public async Task ActualizarAsync(Reservacion reservacion)
        {
            try 
            {
                var reservacionExistente = await _reservacionRepository.ObtenerPorIdAsync(reservacion.Id);
                if (reservacionExistente == null)
                {
                    throw new KeyNotFoundException($"No se encontró la reservación con ID {reservacion.Id}");
                }
                if (reservacionExistente.CodigoReserva != reservacion.CodigoReserva)
                {
                    var otraReservacion = await _reservacionRepository.ObtenerPorCodigoAsync(reservacion.CodigoReserva);
                    if (otraReservacion != null && otraReservacion.Id != reservacion.Id)
                    {
                        throw new InvalidOperationException("Ya existe otra reservación con el mismo código");
                    }
                }
                if (reservacion.FechaIngreso >= reservacion.FechaSalida)
                {
                    throw new ArgumentException("La fecha de ingreso debe ser anterior a la fecha de salida.");
                }
                if (!await HabitacionDisponibleAsync(reservacion.IdHabitacion, reservacion.FechaIngreso, reservacion.FechaSalida, reservacion.Id))
                {
                    //throw new InvalidOperationException($"La habitación{reservacion.} esta ocupada de {}.");
                }
                reservacionExistente.CodigoReserva = reservacion.CodigoReserva;
                reservacionExistente.IdCliente = reservacion.IdCliente;
                reservacionExistente.IdHabitacion = reservacion.IdHabitacion;
                reservacionExistente.FechaReserva = reservacion.FechaReserva;
                reservacionExistente.FechaIngreso = reservacion.FechaIngreso;
                reservacionExistente.FechaSalida = reservacion.FechaSalida;
                reservacionExistente.CantidadPersonas = reservacion.CantidadPersonas;
                reservacionExistente.TarifaReservacion = reservacion.TarifaReservacion;
                reservacionExistente.Solicitudes = reservacion.Solicitudes;
                reservacionExistente.Descuento = reservacion.Descuento;

                // Recalcular el total
                var descuento = reservacionExistente.TarifaReservacion * reservacionExistente.Descuento / 100m;
                var tarifaConDescuento = reservacionExistente.TarifaReservacion - descuento;
                var iva = tarifaConDescuento * 0.13m;
                reservacionExistente.Total = tarifaConDescuento + iva;

                await _reservacionRepository.ActualizarAsync(reservacion);
                return;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la reservación.", ex);
            }

        }

        public async Task EliminarAsync(int id)
        {
            try
            {
                var reservacionExistente = await _reservacionRepository.ObtenerPorIdAsync(id);
                if (reservacionExistente == null)
                {
                    throw new KeyNotFoundException($"No se encontró la reservación con ID {id}");
                }
                await _reservacionRepository.EliminarAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la reservación.", ex);
            }
        }
            

        public async Task<int> ObtenerTotalAsync() => await _reservacionRepository.ObtenerTotalAsync();


        public async Task<bool> HabitacionDisponibleAsync(int numeroHabitacion, DateTime inicio, DateTime fin, int? excludeReservacionId = null)
        {
            if (inicio >= fin)
                throw new ArgumentException(
                    "La fecha de ingreso debe ser anterior a la fecha de salida.");
            return await _reservacionRepository.HabitacionDisponibleAsync(numeroHabitacion, inicio, fin, excludeReservacionId);
        }
    }
}
