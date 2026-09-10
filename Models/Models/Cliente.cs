using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml;

namespace AccessDB.Models
{
    public class Cliente
    {
		public int Id { get; set; }

        [Required(ErrorMessage ="La cédula es obligatoria para realizar el registro")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio para realizar el registro")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El primer apellido es obligatorio para realizar el registro")]
        public string PrimerApellido { get; set; }

        [Required(ErrorMessage = "El primer apellido es obligatorio para realizar el registro")]
        public string SegundoApellido { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
