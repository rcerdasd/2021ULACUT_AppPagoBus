using AppPagoBus.Controllers;
using AppPagoBus.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppIBULACIT.Views
{
    public partial class FrmRutaCliente : System.Web.UI.Page
    {
        IEnumerable<Ruta> rutaList = new ObservableCollection<Ruta>();
        RutaManager rutaManager = new RutaManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodigoUsuario"] == null)
                    Response.Redirect("~/Login.aspx");
                else
                    InicializarControles();
            }
        }

        private async void InicializarControles()
        {
            try
            {
                rutaList = await rutaManager.GetAll(Session["Token"].ToString());
                gvRutas.DataSource = rutaList.ToList();
                gvRutas.DataBind();
            }
            catch (Exception e)
            {

                lblStatus.Text = "Hubo un error al cargar la lista de rutas. Error: " + e.Message;
            }
        }
    }
}