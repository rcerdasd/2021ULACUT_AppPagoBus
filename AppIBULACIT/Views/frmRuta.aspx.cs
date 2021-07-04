using AppIBULACIT.Controllers;
using AppIBULACIT.Models;
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
    public partial class frmRuta : System.Web.UI.Page
    {
        IEnumerable<Ruta> rutaList = new ObservableCollection<Ruta>();
        RutaManager rutaManager = new RutaManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Token"]==null)
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
                rutaList = await rutaManager.GetAll(Session["Token"].ToString());
                gvRutas.DataSource = rutaList.ToList();
                gvRutas.DataBind();
            }
            catch (Exception e)
            {

                lblStatus.Text = "Hubo un error al cargar la lista de rutas. Error: "+e.Message;
            }
        }

        protected void gvRutas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvRutas.Rows[index];
            switch (e.CommandName)
            {
                case "Modificar":
                    ltrTituloMantenimiento.Text = "Mantenimiento rutas";
                    btnAceptarMant.ControlStyle.CssClass = "btn btn-primary";
                    txtCodigoMant.Text = row.Cells[0].Text.Trim();
                    txtCosto.Text = row.Cells[1].Text.Trim();
                    txtDescripcion.Text = row.Cells[2].Text.Trim();
                    txtProvincia.Text = row.Cells[3].Text.Trim();
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
                lblResultado.Text = string.Empty;
                lblResultado.ForeColor = Color.Maroon;
                lblResultado.Visible = false;
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
            ltrTituloMantenimiento.Text = "Nueva ruta";
            btnAceptarMant.ControlStyle.CssClass = "btn btn-sucess";
            btnAceptarMant.Visible = true;
            ltrCodigoMant.Visible = true;
            txtCodigoMant.Visible = true;
            txtDescripcion.Visible = true;
            ltrDescripcion.Visible = true;
            ltrCosto.Visible = true;
            txtCosto.Visible = true;
            ltrProvincia.Visible = true;
            ddlProvincia.Visible = true;
            txtProvincia.Visible = true;
            txtCodigoMant.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            txtCosto.Text = string.Empty;
            txtProvincia.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento();});", true);
        }

        protected async void btnAceptarModal_Click(object sender, EventArgs e)
        {
            string resultado = string.Empty;
            resultado = await rutaManager.Eliminar(Session["Token"].ToString(), lblCodigoEliminar.Text);
            if (!string.IsNullOrEmpty(resultado))
            {
                lblCodigoEliminar.Text = string.Empty;
                ltrModalMensaje.Text = "Ruta eliminada";
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
                if (string.IsNullOrEmpty(txtCodigoMant.Text))
                {
                    Ruta ruta = new Ruta()
                    {
                        Costo = Convert.ToInt32(txtCosto.Text),
                        Descripcion = txtDescripcion.Text,
                        Provincia = txtProvincia.Text
                    };

                    Ruta rutaIngresada = await rutaManager.Ingresar(ruta, Session["Token"].ToString());

                    if (!string.IsNullOrEmpty(rutaIngresada.Descripcion))
                    {
                        lblResultado.Text = "Servicio ingresado con exito";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        btnAceptarMant.Visible = false;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Hubo un error al ingresar el servicio";
                        lblResultado.ForeColor = Color.Maroon;
                        lblResultado.Visible = true;
                        abrirMant();
                    }
                }
                else
                {
                    Ruta ruta = new Ruta()
                    {
                        Codigo = Convert.ToInt32(txtCodigoMant.Text),
                        Costo = Convert.ToInt32(txtCosto.Text),
                        Descripcion = txtDescripcion.Text,
                        Provincia = txtProvincia.Text

                    };

                    Ruta rutaModificada = await rutaManager.Actualizar(ruta, Session["Token"].ToString());

                    if (!string.IsNullOrEmpty(rutaModificada.Descripcion))
                    {
                        lblResultado.Text = "Ruta modificada con exito";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        btnAceptarMant.Visible = false;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Hubo un error al modificar la ruta";
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

        protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtProvincia.Text = ddlProvincia.SelectedItem.Text;
        }
    }
}