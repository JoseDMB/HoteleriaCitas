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

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria para realizar el registro")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "La fecha de ingreso es obligatoria para realizar el registro")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha ingreso")]
        public DateTime FechaIngreso { get; set; }

        [Required(ErrorMessage = "El salario mensual es obligatorio para realizar el registro")]
        public decimal SalarioMensual { get; set; }

        [Required(ErrorMessage = "Debe de seleccionar una categoria para realizar el registro")]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "El distrito es obligatorio para realizar el registro")]
        public int DistritoId { get; set; }

        [Required(ErrorMessage = "La direccion es obligatoria para realizar el registro")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "La provincia es obligatoria para realizar el registro")]
        public int ProvinciaId { get; set; }
        [Required(ErrorMessage = "El canton es obligatorio para realizar el registro")]
        public int CantonId { get; set; }
    }
}
