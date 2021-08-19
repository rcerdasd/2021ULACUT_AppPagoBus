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
    public partial class frmReporteViajes : System.Web.UI.Page
    {
        IEnumerable<Transaccion> transaccionLista = new ObservableCollection<Transaccion>();

        TransaccionManager transaccionManager = new TransaccionManager();

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
                transaccionLista = await transaccionManager.GetAll(Session["Token"].ToString(), Session["CodigoUsuario"].ToString());
                gvViajes.DataSource = transaccionLista.ToList();
                gvViajes.DataBind();
            }
            catch (Exception e)
            {
                lblStatus.Text = "Hubo un error al cargar la lista de rutas. Error: " + e.Message;
            }
        }

    }
}