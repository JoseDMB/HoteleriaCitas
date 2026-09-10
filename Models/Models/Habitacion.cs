using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccessDB.Models
{
    public class Habitacion
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El número de habitación es obligatorio")]
        public int NumeroHabitacion { get; set; }

        [Required(ErrorMessage = "El tipo de habitación es obligatorio")]
        public int TipoHabitacion { get; set; }

        [Required(ErrorMessage = "La tarifa por noche de la habitación es obligatoria")]
        public decimal TarifaxNoche { get; set; }
        public string PendientesMantenimiento { get; set; }
        public bool Tv_Satelital { get; set; }
    }
}
