using AppIBULACIT.Models;
using AppIBULACIT.Controllers;
using AppPagoBus.Controllers;
using AppPagoBus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppIBULACIT
{
    public partial class Ruta : System.Web.UI.Page
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
                    RutaManager rutaManager = new RutaManager();

                    RutaModel ruta = new RutaModel()
                    {
                        Costo = Convert.ToInt32(txtCosto.Text),
                        Descripcion = txtDescripcion.Text,
                    };

                  
                        RutaModel rutaRegistrada = await rutaManager.Ingresar(ruta, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(rutaRegistrada.Descripcion))
                            Response.Redirect("Ruta.aspx");
                        else
                        {
                            lblStatus.Text = "Hubo un error al registrar la ruta.";
                            lblStatus.Visible = true;
                        }  
                    
                }
                catch (Exception)
                {
                    lblStatus.Text = "Hubo un error al registrar la ruta.";
                    lblStatus.Visible = true;
                }
            }
        }

    }
}