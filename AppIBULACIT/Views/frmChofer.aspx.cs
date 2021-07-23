using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppPagoBus.Controllers;
using AppPagoBus.Models;
using System.Collections.ObjectModel;
using System.Drawing;

namespace AppPagoBus.Views
{
    public partial class frmChofer : System.Web.UI.Page
    {
        IEnumerable<Persona> choferList = new ObservableCollection<Persona>();
        PersonaManager personaManager = new PersonaManager();
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
                gvChofer.DataSource = choferList.ToList();
                gvChofer.DataBind();
            }
            catch (Exception e)
            {

                lblStatus.Text = "Hubo un error al cargar la lista de chofer. Error: " + e.Message;
            }
        }

        private void limpiarlblResultado()
        {
            lblResultado.Visible = false;
            lblResultado.Text = string.Empty;
        }

        protected void gvChofer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvChofer.Rows[index];
            switch (e.CommandName)
            {
                case "Modificar":
                    limpiarlblResultado();
                    ltrTituloMantenimiento.Text = "Mantenimiento chofer";
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
            ltrTituloMantenimiento.Text = "Nuevo chofer";
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
                ltrModalMensaje.Text = "Chofer eliminado";
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
                    Persona chofer = new Persona()
                    {
                        Nombre = txtNombre.Text,
                        Apellido = txtApellido.Text,
                        Identificacion = txtIdentificacion.Text,
                        FechaNacimiento = Convert.ToDateTime(txtFechaNacimiento.Text),
                        Usuario = txtUsuario.Text,
                        Contrasena = txtContrasena.Text,
                        Email = txtEmail.Text,
                        Tipo = "3"
                    };

                    Persona choferIngresado = await personaManager.Ingresar(chofer, Session["Token"].ToString());

                    if (!string.IsNullOrEmpty(choferIngresado.Usuario))
                    {
                        lblResultado.Text = "Chofer ingresado con exito";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        btnAceptarMant.Visible = false;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Hubo un error al ingresar el chofer";
                        lblResultado.ForeColor = Color.Maroon;
                        lblResultado.Visible = true;
                        abrirMant();
                    }
                }
                else
                {
                    Persona chofer = new Persona()
                    { 
                        Codigo = Convert.ToInt32(txtCodigo.Text),
                        Nombre = txtNombre.Text,
                        Apellido = txtApellido.Text,
                        Identificacion = txtIdentificacion.Text,
                        FechaNacimiento = Convert.ToDateTime(txtFechaNacimiento.Text),
                        Usuario = txtUsuario.Text,
                        Contrasena = txtContrasena.Text,
                        Email = txtEmail.Text,
                        Tipo = "3"
                    };

                    Persona choferModificado = await personaManager.Actualizar(chofer, Session["Token"].ToString());

                    if (!string.IsNullOrEmpty(choferModificado.Usuario))
                    {
                        lblResultado.Text = "Chofer modificado con exito";
                        lblResultado.ForeColor = Color.Green;
                        lblResultado.Visible = true;
                        btnAceptarMant.Visible = false;
                        InicializarControles();
                    }
                    else
                    {
                        lblResultado.Text = "Hubo un error al modificar el chofer";
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