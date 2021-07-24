using AppIBULACIT.Controllers;
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
    public partial class frmCliente : System.Web.UI.Page
    {

        IEnumerable<Persona> personaList = new ObservableCollection<Persona>();
        UsuarioManager personaManager = new UsuarioManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodigoUsuario"] == null)
                    Response.Redirect("~/Login.aspx");
                else
                {
                    InicializarControles();
                }

            }
        }

        private async void InicializarControles()
        {
            try
            {
                personaList = await personaManager.GetAll(Session["Token"].ToString());
                gvPersona.DataSource = personaList.ToList();
                gvPersona.DataBind();
            }
            catch (Exception e)
            {

                lblStatus.Text = "Hubo un error al cargar la lista de persona. Error: " + e.Message;
            }
        }

        private void limpiarlblResultado()
        {
            lblResultado.Visible = false;
            lblResultado.Text = string.Empty;
        }

        protected void gvPersona_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvPersona.Rows[index];
            switch (e.CommandName)
            {
                case "Modificar":
                    limpiarlblResultado();
                    ltrTituloMantenimiento.Text = "Mantenimiento persona";
                    btnAceptarMant.ControlStyle.CssClass = "btn btn-primary";
                    txtCodigo.Text = row.Cells[0].Text.Trim();
                    txtNombre.Text = row.Cells[1].Text.Trim();
                    txtApellido.Text = row.Cells[2].Text.Trim();
                    txtIdentificacion.Text = row.Cells[3].Text.Trim();
                    txtFechaNacimiento.Text = row.Cells[4].Text.Trim();
                    txtUsuario.Text = row.Cells[5].Text.Trim();
                    txtEmail.Text = row.Cells[6].Text.Trim();
                    txtSaldo.Text = row.Cells[7].Text.Trim();
                    ddlEstadoMant.SelectedValue = row.Cells[8].Text.Trim().ToLower();
                    ltrContrasena.Visible = false;
                    txtContrasena.Visible = false;

                    btnAceptarMant.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento();});", true);
                    break;
                case "Eliminar":
                    lblCodigoEliminar.Text = row.Cells[0].Text.Trim();
                    ltrModalMensaje.Text = "Esta seguro que desea eliminar el persona " + lblCodigoEliminar.Text + "?";
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
                txtCodigo.Text = string.Empty;
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
            ltrTituloMantenimiento.Text = "Nuevo persona";
            btnAceptarMant.ControlStyle.CssClass = "btn btn-sucess";
            btnAceptarMant.Visible = true;
            ltrCodigo.Visible = true;
            txtCodigo.Visible = true;
            ltrNombre.Visible = true;
            txtNombre.Visible = true;
            ltrApellido.Visible = true;
            txtApellido.Visible = true;
            ltrIdentificacion.Visible = true;
            txtIdentificacion.Visible = true;
            ltrFechaNacimiento.Visible = true;
            txtFechaNacimiento.Visible = true;
            ltrUsuario.Visible = true;
            txtUsuario.Visible = true;
            ltrContrasena.Visible = true;
            txtContrasena.Visible = true;
            ltrEmail.Visible = true;
            txtEmail.Visible = true;
            ltrContrasena.Visible = true;
            txtContrasena.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento();});", true);
        }

        protected async void btnAceptarModal_Click(object sender, EventArgs e)
        {
            string resultado = string.Empty;
            resultado = await personaManager.Eliminar(Session["Token"].ToString(), lblCodigoEliminar.Text);
            if (!string.IsNullOrEmpty(resultado))
            {
                lblCodigoEliminar.Text = string.Empty;
                ltrModalMensaje.Text = "Persona eliminado";
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
                if (string.IsNullOrEmpty(txtCodigo.Text))
                {
                    Persona persona = new Persona()
                    {
                        Nombre = txtNombre.Text,
                        Apellido = txtApellido.Text,
                        Identificacion = txtIdentificacion.Text,
                        FechaNacimiento = Convert.ToDateTime(txtFechaNacimiento.Text),
                        Usuario = txtUsuario.Text,
                        Contrasena = txtContrasena.Text,
                        Email = txtEmail.Text,
                        Tipo = "2",
                        Saldo = Convert.ToDecimal(txtSaldo.Text),
                        Estado = ddlEstadoMant.SelectedValue
                    };

                    Persona personaIngresado = await personaManager.Ingresar(persona, Session["Token"].ToString());

                    if (!string.IsNullOrEmpty(personaIngresado.Usuario))
                    {
                        lblResultado.Text = "Persona ingresado con exito";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        btnAceptarMant.Visible = false;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Hubo un error al ingresar el persona";
                        lblResultado.ForeColor = Color.Maroon;
                        lblResultado.Visible = true;
                        abrirMant();
                    }
                }
                else
                {
                    Persona persona = new Persona()
                    {
                        Codigo = Convert.ToInt32(txtCodigo.Text),
                        Nombre = txtNombre.Text,
                        Apellido = txtApellido.Text,
                        Identificacion = txtIdentificacion.Text,
                        FechaNacimiento = Convert.ToDateTime(txtFechaNacimiento.Text),
                        Usuario = txtUsuario.Text,
                        Email = txtEmail.Text,
                        Tipo = "2",
                        Saldo = Convert.ToDecimal(txtSaldo.Text),
                        Estado = ddlEstadoMant.SelectedValue
                    };

                    Persona personaModificado = await personaManager.Actualizar(persona, Session["Token"].ToString());

                    if (!string.IsNullOrEmpty(personaModificado.Usuario))
                    {
                        lblResultado.Text = "Persona modificado con exito";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        btnAceptarMant.Visible = false;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Hubo un error al modificar el persona";
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