using AppPagoBus.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppPagoBus.Models
{
    public class Persona
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Identificacion { get; set; }
        public System.DateTime FechaNacimiento { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public string Email { get; set; }
        public string Tipo { get; set; }
        public Nullable<decimal> Saldo { get; set; }

        public string Estado { get; set; }

        public string Token { get; set; }

        public static implicit operator Persona(PersonaManager v)
        {
            throw new NotImplementedException();
        }
    }
}