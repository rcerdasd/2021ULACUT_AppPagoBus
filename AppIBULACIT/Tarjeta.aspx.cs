using AppPagoBus.Controllers;
using AppPagoBus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppPagoBus
{
    public partial class Tarjeta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    TarjetaManager tarjetaManager = new TarjetaManager();

                    TarjetaModel tarjeta = new TarjetaModel()
                    {
                       Numero = txtNumero.Text,
                       CCV = txtNumero.Text,
                       Nombre = txtNombre.Text,
                       Predeterminado = txtNumero.Text

                    };

                    TarjetaModel tarjetaRegistrada = await tarjetaManager.Ingresar(tarjeta, Session["Token"].ToString());

                    if (!string.IsNullOrEmpty(tarjetaRegistrada.Nombre))
                        Response.Redirect("Views/frmChofer.aspx");
                    else
                    {
                        lblStatus.Text = "Hubo un error al registrar la tarjeta.";
                        lblStatus.Visible = true;
                    }
                }
                catch (Exception)
                {
                    lblStatus.Text = "Hubo un error al registrar la tarjeta.";
                    lblStatus.Visible = true;
                }
            }
        }

        protected void btnFechaExp_Click(object sender, EventArgs e)
        {
            cldFechaExpiracion.Visible = true;
        }

        protected void cldFechaExpiracion_SelectionChanged(object sender, EventArgs e)
        {
            txtFechaExpiracion.Text = cldFechaExpiracion.SelectedDate.ToString("dd/MM/yyyy");
            cldFechaExpiracion.Visible = false;
        }
    }
}