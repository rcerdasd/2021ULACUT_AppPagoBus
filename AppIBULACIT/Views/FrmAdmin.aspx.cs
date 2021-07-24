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
    public partial class FrmAdmin : System.Web.UI.Page
    {
        IEnumerable<Persona> choferList = new ObservableCollection<Persona>();
        AdminManager personaManager = new AdminManager();
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
                choferList = await personaManager.GetAll(Session["Token"].ToString());
                gvAdmin.DataSource = choferList.ToList();
                gvAdmin.DataBind();
            }
            catch (Exception e)
            {

                lblStatus.Text = "Hubo un error al cargar la lista de administradores. Error: " + e.Message;
            }
        }

        private void limpiarlblResultado()
        {
            lblResultado.Visible = false;
            lblResultado.Text = string.Empty;
        }
        protected void gvAdmin_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvAdmin.Rows[index];
            switch (e.CommandName)
            {
                case "Modificar":
                    limpiarlblResultado();
                    ltrTituloMantenimiento.Text = "Mantenimiento de administradores";
                    btnAceptarMant.ControlStyle.CssClass = "btn btn-primary";
                    txtCodigo.Text = row.Cells[0].Text.Trim();
                    txtNombre.Text = row.Cells[1].Text.Trim();
                    txtApellido.Text = row.Cells[2].Text.Trim();
                    txtIdentificacion.Text = row.Cells[3].Text.Trim();
                    txtFechaNacimiento.Text = row.Cells[4].Text.Trim();
                    txtUsuario.Text = row.Cells[5].Text.Trim();
                    txtContrasena.Text = row.Cells[6].Text.Trim();
                    txtEmail.Text = row.Cells[7].Text.Trim();

                    btnAceptarMant.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento();});", true);
                    break;
                case "Eliminar":
                    lblCodigoEliminar.Text = row.Cells[0].Text.Trim();
                    ltrModalMensaje.Text = "Esta seguro que desea eliminar el administrador " + lblCodigoEliminar.Text + "?";
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
            ltrTituloMantenimiento.Text = "Nuevo administrador";
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento();});", true);
        }

        protected async void btnAceptarModal_Click(object sender, EventArgs e)
        {
            string resultado = string.Empty;
            resultado = await personaManager.Eliminar(Session["Token"].ToString(), lblCodigoEliminar.Text);
            if (!string.IsNullOrEmpty(resultado))
            {
                lblCodigoEliminar.Text = string.Empty;
                ltrModalMensaje.Text = "Administrador eliminado";
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
                    Persona admin = new Persona()
                    {
                        Nombre = txtNombre.Text,
                        Apellido = txtApellido.Text,
                        Identificacion = txtIdentificacion.Text,
                        FechaNacimiento = Convert.ToDateTime(txtFechaNacimiento.Text),
                        Usuario = txtUsuario.Text,
                        Contrasena = txtContrasena.Text,
                        Email = txtEmail.Text,
                        Tipo = "1"
                    };

                    Persona adminIngresado = await personaManager.Ingresar(admin, Session["Token"].ToString());

                    if (!string.IsNullOrEmpty(adminIngresado.Usuario))
                    {
                        lblResultado.Text = "Administrador ingresado con exito";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        btnAceptarMant.Visible = false;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Hubo un error al ingresar el administrador";
                        lblResultado.ForeColor = Color.Maroon;
                        lblResultado.Visible = true;
                        abrirMant();
                    }
                }
                else
                {
                    Persona admin = new Persona()
                    {
                        Codigo = Convert.ToInt32(txtCodigo.Text),
                        Nombre = txtNombre.Text,
                        Apellido = txtApellido.Text,
                        Identificacion = txtIdentificacion.Text,
                        FechaNacimiento = Convert.ToDateTime(txtFechaNacimiento.Text),
                        Usuario = txtUsuario.Text,
                        Contrasena = txtContrasena.Text,
                        Email = txtEmail.Text,
                        Tipo = "1"
                    };

                    Persona adminModificado = await personaManager.Actualizar(admin, Session["Token"].ToString());

                    if (!string.IsNullOrEmpty(adminModificado.Usuario))
                    {
                        lblResultado.Text = "Administrador modificado con exito";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        btnAceptarMant.Visible = false;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Hubo un error al modificar el administrador";
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