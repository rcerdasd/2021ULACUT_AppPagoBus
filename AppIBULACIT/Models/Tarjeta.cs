using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppIBULACIT.Models
{
    public class Tarjeta
    {
        public int Codigo { get; set; }
        public int Numero { get; set; }
        public int ccv { get; set; }
        public System.DateTime FechaExpiracion { get; set; }
        public string Nombre { get; set; }
        public string Predeterminado { get; set; }
    }
}