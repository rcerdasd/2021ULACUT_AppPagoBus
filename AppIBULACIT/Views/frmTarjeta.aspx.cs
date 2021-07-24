using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppIBULACIT.Controllers;
using AppPagoBus.Models;
using System.Collections.ObjectModel;
using System.Drawing;
using AppPagoBus.Controllers;

namespace AppPagoBus.Views
{
    public partial class frmTarjeta : System.Web.UI.Page
    {
        IEnumerable<TarjetaModel> tarjetaList = new ObservableCollection<TarjetaModel>();
        TarjetaManager tarjetaManager = new TarjetaManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Token"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            else
            {
                InicializarControles();
            }
        }

        private async void InicializarControles()
        {
            try
            {
                tarjetaList = await tarjetaManager.GetAll(Session["Token"].ToString());
                gvTarjeta.DataSource = tarjetaList.ToList();
                gvTarjeta.DataBind();
            }
            catch (Exception e)
            {

                lblStatus.Text = "Hubo un error al cargar la lista los meétodos de pago. Error: " + e.Message;
            }
        }

        private void limpiarlblResultado()
        {
            lblResultado.Visible = false;
            lblResultado.Text = string.Empty;
        }

        protected void gvTarjeta_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvTarjeta.Rows[index];
            switch (e.CommandName)
            {
                case "Modificar":
                    limpiarlblResultado();
                    ltrTituloMantenimiento.Text = "Mantenimiento chofer";
                    btnAceptarMant.ControlStyle.CssClass = "btn btn-primary";
                    txtCodigo.Text = row.Cells[0].Text.Trim();
                    txtNumero.Text = row.Cells[1].Text.Trim();
                    txtccv.Text = row.Cells[2].Text.Trim();
                    txtFechaExpiracion.Text = row.Cells[3].Text.Trim();
                    txtNombre.Text = row.Cells[4].Text.Trim();
                    ddlPredeterminado.Text = row.Cells[5].Text.Trim();

                    btnAceptarMant.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento();});", true);
                    break;
                case "Eliminar":
                    lblCodigoEliminar.Text = row.Cells[0].Text.Trim();
                    ltrModalMensaje.Text = "Esta seguro que desea eliminar el chofer " + lblCodigoEliminar.Text + "?";
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
            ltrCodigo.Visible = true;
            txtCodigo.Visible = true;
            ltrNumero.Visible = true;
            txtNumero.Visible = true;
            ltrccv.Visible = true;
            txtccv.Visible = true;
            ltrFechaExpiracion.Visible = true;
            txtFechaExpiracion.Visible = true;
            ltrNombre.Visible = true;
            txtNombre.Visible = true;
            ltrPredeterminado.Visible = true;
            ddlPredeterminado.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento();});", true);
        }

        protected async void btnAceptarModal_Click(object sender, EventArgs e)
        {
            string resultado = string.Empty;
            resultado = await tarjetaManager.Eliminar(Session["Token"].ToString(), lblCodigoEliminar.Text);
            if (!string.IsNullOrEmpty(resultado))
            {
                lblCodigoEliminar.Text = string.Empty;
                ltrModalMensaje.Text = "Tarjeta eliminada";
                btnAceptarModal.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() {openModal();})", true);
            }
        }

        protected void btnCancelarModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { CloseModal(); });", true);
        }
        protected async void btnAceptarMant_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCodigo.Text))
                {
                    TarjetaModel tarj = new TarjetaModel()
                    {
                        Numero = Convert.ToInt32(txtNumero.Text),
                        ccv = Convert.ToInt32(txtccv.Text),
                        FechaExpiracion = Convert.ToDateTime(txtFechaExpiracion.Text),
                        Nombre = txtNombre.Text,
                        Predeterminado = ddlPredeterminado.Text
                    };

                    TarjetaModel tarjetaIngresado = await tarjetaManager.Ingresar(tarj, Session["Token"].ToString());

                    if (!string.IsNullOrEmpty(tarjetaIngresado.Numero.ToString()))
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
                else
                {
                    TarjetaModel chofer = new TarjetaModel()
                    {
                        Numero = Convert.ToInt32(txtNumero.Text),
                        ccv = Convert.ToInt32(txtccv.Text),
                        FechaExpiracion = Convert.ToDateTime(txtFechaExpiracion.Text),
                        Nombre = txtNombre.Text,
                        Predeterminado = ddlPredeterminado.Text
                    };

                    TarjetaModel choferModificado = await tarjetaManager.Actualizar(chofer, Session["Token"].ToString());

                    if (!string.IsNullOrEmpty(choferModificado.Numero.ToString()))
                    {
                        lblResultado.Text = "Tarjeta modificada con exito";
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

        protected void ddlPredeterminado_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlPredeterminado.Text = ddlPredeterminado.SelectedValue.ToString();

        }
    }
}