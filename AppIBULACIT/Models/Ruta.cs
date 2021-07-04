using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppIBULACIT.Models
{
    public class Ruta
    {
        public int Codigo { get; set; }
        public int Costo { get; set; }
        public string Descripcion { get; set; }
        public string Provincia { get; set; }
    }
}