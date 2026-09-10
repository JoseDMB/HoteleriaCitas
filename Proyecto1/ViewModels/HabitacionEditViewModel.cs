using System.Collections.Generic;
using AccessDB.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Proyecto1.ViewModels
{
    public class HabitacionEditViewModel
    {
         public Habitacion Habitacion { get; set; } = new Habitacion();

        [BindNever]
        public List<TipoHabitacion> TiposHabitacion { get; set; } = new List<TipoHabitacion>();
       
    }
}
