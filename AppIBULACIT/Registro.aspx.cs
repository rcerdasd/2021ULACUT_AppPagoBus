using AppPagoBus.Controllers;
using AppPagoBus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppPagoBus
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnFechaNac_Click(object sender, EventArgs e)
        {
            cldFechaNacimiento.Visible = true;
        }

        protected void cldFechaNacimiento_SelectionChanged(object sender, EventArgs e)
        {
            txtFechaNacimiento.Text = cldFechaNacimiento.SelectedDate.ToString("dd/MM/yyyy");
            cldFechaNacimiento.Visible = false;
        }

        protected async void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    PersonaManager personaManager = new PersonaManager();

                    Persona persona = new Persona()
                    {
                        Nombre = txtNombre.Text,
                        Apellido = txtApellido.Text,
                        Identificacion = txtIdentificacion.Text,
                        FechaNacimiento = Convert.ToDateTime(txtFechaNacimiento.Text),
                        Usuario = txtUsername.Text,
                        Contrasena = txtPassword.Text,
                        Email = txtEmail.Text,
                        Tipo = "2",
                        Saldo = 0
                    };

                    Persona usuarioRegistrado = await personaManager.Registrar(persona);

                    if (!string.IsNullOrEmpty(persona.Identificacion))
                        Response.Redirect("Login.aspx");
                    else
                    {
                        lblStatus.Text = "Hubo un error al registrar el usuario.";
                        lblStatus.Visible = true;
                    }
                }
                catch (Exception)
                {
                    lblStatus.Text = "Hubo un error al registrar el usuario.";
                    lblStatus.Visible = true;
                }
            }
        }
    }
}