using System;
using System.Collections.Generic;
using System.Text;

namespace AccessDB.Models
{
    public class Canton
    {
        public int Id { get; set; } 
        public int IdProvincia { get; set; }
        public string Nombre { get; set; }
    }
}
