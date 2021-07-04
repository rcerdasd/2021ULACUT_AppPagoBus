using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppPagoBus.Models
{
    public class TarjetaModel
    {
        public int Codigo { get; set; }
        public int Numero { get; set; }
        public int ccv { get; set; }
        public System.DateTime FechaExpiracion { get; set; }
        public string Nombre { get; set; }
        public string Predeterminado { get; set; }
    }
}