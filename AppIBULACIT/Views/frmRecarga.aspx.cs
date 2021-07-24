using AppIBULACIT.Controllers;
using AppPagoBus.Controllers;
using AppPagoBus.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppIBULACIT.Views
{
    public partial class frmRecarga : System.Web.UI.Page
    {
        TarjetaManager tarjetaManager = new TarjetaManager();
        IEnumerable<TarjetaModel> tarjetas = new ObservableCollection<TarjetaModel>();
        UsuarioManager usuarioManager = new UsuarioManager();
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

        protected async void InicializarControles()
        {
            try
            {
                ltrlSaldo.Text = Session["Saldo"].ToString();

                txtRecarga.Text = string.Empty;
                tarjetas = await tarjetaManager.GetId(Session["Token"].ToString(), Session["CodigoUsuario"].ToString());
                ddlTarjeta.DataSource = tarjetas.ToList();
                ddlTarjeta.DataTextField = "Numero";
                ddlTarjeta.DataValueField = "Codigo";
                ddlTarjeta.DataBind();
            }
            catch (Exception)
            {

                lblStatus.Text = "Hubo un error";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
            }
        }

        protected async void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                decimal nuevoSaldo = Convert.ToDecimal(Session["Saldo"].ToString()) + Convert.ToDecimal(txtRecarga.Text.Trim());
                Persona persona = new Persona()
                {
                    Codigo = Convert.ToInt32(Session["CodigoUsuario"].ToString()),
                    Saldo = nuevoSaldo
                };

                Persona usuarioActualizado = new Persona();

                usuarioActualizado = await usuarioManager.Actualizar(persona, Session["Token"].ToString());

                if (usuarioActualizado.Codigo == Convert.ToInt32(Session["CodigoUsuario"].ToString()))
                {
                    Session["Saldo"] = nuevoSaldo;
                    
                    lblStatus.Text = "Saldo actualizado con exito";
                    lblStatus.ForeColor = Color.Green;
                    lblStatus.Visible = true;
                    InicializarControles();
                }

            }
            catch (Exception)
            {

                lblStatus.Text = "Hubo un error";
                lblStatus.ForeColor = Color.Maroon;
                lblStatus.Visible = true;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}