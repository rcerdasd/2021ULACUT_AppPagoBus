using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppPagoBus.Models
{
    public class Transaccion
    {
        public int Codigo { get; set; }
        public int ClienteId { get; set; }
        public int RutaId { get; set; }
        public int TarjetaClienteId { get; set; }
        public System.DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public string Estado { get; set; }
        public string Descripcion { get; set; }

    }
}