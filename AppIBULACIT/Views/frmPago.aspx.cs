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
    public partial class frmPago : System.Web.UI.Page
    {

        IEnumerable<TarjetaModel> tarjetaList = new ObservableCollection<TarjetaModel>();
        TarjetaManager tarjetaManager = new TarjetaManager();
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
                tarjetaList = await tarjetaManager.GetId(Session["Token"].ToString(),Session["CodigoUsuario"].ToString());
                gvTarjetas.DataSource = tarjetaList.ToList();
                gvTarjetas.DataBind();
            }
            catch (Exception e)
            {

                lblStatus.Text = "Hubo un error al cargar la lista de tarjetas. Error: " + e.Message;
            }
        }

        private void limpiarlblResultado()
        {
            lblResultado.Visible = false;
            lblResultado.Text = string.Empty;
        }

        protected void gvTarjetas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvTarjetas.Rows[index];
            switch (e.CommandName)
            {
                case "Modificar":
                    limpiarlblResultado();
                    ltrTituloMantenimiento.Text = "Mantenimiento tarjetas";
                    btnAceptarMant.ControlStyle.CssClass = "btn btn-primary";
                    txtCodigoMant.Text = row.Cells[0].Text.Trim();
                    txtNumero.Text = row.Cells[1].Text.Trim();
                    txtCcv.Text = row.Cells[2].Text.Trim();
                    txtMesExpiracion.Text = row.Cells[3].Text.Trim();
                    txtAnioExpiracion.Text = row.Cells[4].Text.Trim();
                    txtNombre.Text = row.Cells[5].Text.Trim();
                    ddlPredeterminado.SelectedValue = row.Cells[6].Text.Trim();
                    btnAceptarMant.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento();});", true);
                    break;
                case "Eliminar":
                    lblCodigoEliminar.Text = row.Cells[0].Text.Trim();
                    ltrModalMensaje.Text = "Esta seguro que desea eliminar el servicio " + lblCodigoEliminar.Text + "?";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() {openModal(); });", true);
                    break;
                default:
                    break;
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {

            try
            {
                txtCodigoMant.Text = string.Empty;
                limpiarlblResultado();
                abrirMant();
            }
            catch (Exception)
            {
                lblStatus.Text = "Error";
                lblStatus.Visible = true;
                lblStatus.ForeColor = Color.Maroon;
            }

        }

        private void abrirMant()
        {
            ltrTituloMantenimiento.Text = "Nueva tarjeta";
            btnAceptarMant.ControlStyle.CssClass = "btn btn-sucess";
            btnAceptarMant.Visible = true;
            ltrCodigoMant.Visible = true;
            txtCodigoMant.Visible = true;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento();});", true);
        }

        protected async void btnAceptarModal_Click(object sender, EventArgs e)
        {
            string resultado = string.Empty;
            resultado = await tarjetaManager.Eliminar(Session["Token"].ToString(), lblCodigoEliminar.Text);
            if (!string.IsNullOrEmpty(resultado))
            {
                lblCodigoEliminar.Text = string.Empty;
                ltrModalMensaje.Text = "TarjetaModel eliminada";
                btnAceptarModal.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() {openModal();})", true);
            }
        }

        protected void btnCancelarModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { CloseModal(); });", true);
            InicializarControles();
        }

        protected async void btnAceptarMant_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCodigoMant.Text))
                {
                    TarjetaModel tarjeta = new TarjetaModel()
                    {
                        Numero = txtNumero.Text,
                        CCV = txtCcv.Text,
                        MesExpiracion = Convert.ToInt32(txtMesExpiracion.Text),
                        AnioExpiracion = Convert.ToInt32(txtAnioExpiracion.Text),
                        Nombre = txtNombre.Text,
                        Predeterminado = ddlPredeterminado.SelectedValue,
                        CodigoCliente = Convert.ToInt32(Session["CodigoUsuario"].ToString())
                    };

                    TarjetaModel tarjetaIngresada = await tarjetaManager.Ingresar(tarjeta, Session["Token"].ToString());

                    if (!string.IsNullOrEmpty(tarjetaIngresada.Nombre))
                    {
                        lblResultado.Text = "Tarjeta ingresada con exito";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        btnAceptarMant.Visible = false;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Hubo un error al ingresar la tarjeta";
                        lblResultado.ForeColor = Color.Maroon;
                        lblResultado.Visible = true;
                        abrirMant();
                    }
                }
                else//Modificar
                {
                    TarjetaModel tarjeta = new TarjetaModel()
                    {
                        Codigo = Convert.ToInt32(txtCodigoMant.Text),
                        Numero = txtNumero.Text,
                        CCV = txtCcv.Text,
                        MesExpiracion = Convert.ToInt32(txtMesExpiracion.Text),
                        AnioExpiracion = Convert.ToInt32(txtAnioExpiracion.Text),
                        Nombre = txtNombre.Text,
                        Predeterminado = ddlPredeterminado.SelectedValue,
                        CodigoCliente = Convert.ToInt32(Session["CodigoUsuario"].ToString())

                    };

                    TarjetaModel tarjetaModificada = await tarjetaManager.Actualizar(tarjeta, Session["Token"].ToString());

                    if (!string.IsNullOrEmpty(tarjetaModificada.Nombre))
                    {
                        lblResultado.Text = "TarjetaModel modificada con exito";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        btnAceptarMant.Visible = false;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Hubo un error al modificar la tarjeta";
                        lblResultado.ForeColor = Color.Maroon;
                        lblResultado.Visible = true;
                    }
                }
            }
            catch (Exception)
            {

                lblResultado.Text = "Datos invalidos";
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = true;
                abrirMant();
            }
        }

        protected void btnCancelarMant_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() {​​​ CloseMantenimiento(); }​​​);", true);
        }

    }
}