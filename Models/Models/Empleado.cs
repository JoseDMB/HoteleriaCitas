using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AccessDB.Models
{
    public class Empleado
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La cédula es obligatoria para realizar el registro")]
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
        [Display(Name = "Fecha ingreso")]
        public DateTime FechaIngreso { get; set; }
        public decimal SalarioMensual { get; set; }
        public int CategoriaId { get; set; }
        public int DistritoId { get; set; }
        public string Direccion { get; set; }
        public int ProvinciaId { get; set; }
        public int CantonId { get; set; }
    }
}
