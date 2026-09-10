using AccessDB.Models;
using AccessDB;
using API_Hotel.Interfaces;

namespace API_Hotel.Services
{
    public class TipoHabitacionService: ITipoHabitacionService
    {
        private readonly ITipoHabRepository _repository;
        public TipoHabitacionService(ITipoHabRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<TipoHabitacion>> ObtenerTodasAsync()
        {
            return await _repository.ObtenerTodasAsync();
        }

        /// <summary>
        /// Obtiene una categoría por su ID
        /// </summary>
        public async Task<TipoHabitacion> ObtenerPorIdAsync(int id)
        {
            return await _repository.ObtenerPorIdAsync(id);
        }

       
    }
}
