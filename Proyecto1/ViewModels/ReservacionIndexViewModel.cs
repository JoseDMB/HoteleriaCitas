using System;

namespace Proyecto1.ViewModels
{
    public class ReservacionIndexViewModel
    {
        public int Id { get; set; }
        public string CodigoReserva { get; set; }
        public string ClienteNombre { get; set; }
        public string HabitacionNumero { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaSalida { get; set; }
        public decimal TarifaReservacion { get; set; }
        public decimal Total { get; set; }
        public int EstadoReservacionId { get; set; }
        public string EstadoNombre { get; set; }
    }
}
