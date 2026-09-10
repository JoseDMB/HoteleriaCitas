using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AccessDB.Models
{
    public class Reservacion
    {
		public int Id { get; set; }

        [Required(ErrorMessage = "El código de la reserva es obligatorio para realizar el registro")]
        public string CodigoReserva { get; set; }

        [Required(ErrorMessage = "Debe de elegir un cliente para realizar el registro")]
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "Debe de seleccionar una habitacion para realizar el registro")]
        public int IdHabitacion { get; set; }

        public DateTime FechaReserva { get; set; } = DateTime.Now;

        public DateTime FechaIngreso { get; set; }

        public DateTime FechaSalida { get; set; }

        [Required(ErrorMessage = "La cédula es obligatoria para realizar el registro")]
        public int CantidadPersonas { get; set; }

        [Required(ErrorMessage = "La cédula es obligatoria para realizar el registro")]
        public decimal TarifaReservacion { get; set; }
        public string Solicitudes { get; set; }
        public int Descuento { get; set; }
        public decimal Total { get; set; }
        public int EstadoReservacion { get; set; }
    }
}
