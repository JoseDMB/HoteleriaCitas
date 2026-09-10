namespace MVC
{
    public class EmpleadoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        // Ubicación
        public int? ProvinciaId { get; set; }
        public int? CantonId { get; set; }
        public int? DistritoId { get; set; }
    }
}
