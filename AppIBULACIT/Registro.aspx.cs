using AppPagoBus.Controllers;
using AppPagoBus.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                    var engCultureInfo = CultureInfo.CreateSpecificCulture("en-US");

                    Persona persona = new Persona()
                    {
                        Nombre = txtNombre.Text,
                        Apellido = txtApellido.Text,
                        Identificacion = txtIdentificacion.Text,
                        //FechaNacimiento = Convert.ToDateTime(txtFechaNacimiento.Text, "dd/MM/yyy"),
                        //FechaNacimiento = DateTime.ParseExact(txtFechaNacimiento.Text+" 00:00:00","dd/MM/yyy HH:mm:ss",engCultureInfo),
                        FechaNacimiento = cldFechaNacimiento.SelectedDate,
                        Usuario = txtUsername.Text,
                        Contrasena = txtPassword.Text,
                        Email = txtEmail.Text,
                        Tipo = "2",
                        Saldo = 0
                    };

                    Persona usuarioRegistrado = await personaManager.Registrar(persona);

                    if (!string.IsNullOrEmpty(usuarioRegistrado.Identificacion))
                        Response.Redirect("Login.aspx");
                    else
                    {
                        lblStatus.Text = "Hubo un error al registrar el usuario.";
                        lblStatus.Visible = true;
                    }


                }
                catch (Exception ex)
                {
                    lblStatus.Text = "Hubo un error al registrar el usuario.";
                    lblStatus.Visible = true;
                }
            }
        }

        protected void cvPasswordLength_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (args.Value.Length >= 9)
                args.IsValid = true;
            else
                args.IsValid = false;
        }

        protected void cvCalendario_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int index = txtFechaNacimiento.Text.Length - 4;
            int year = Convert.ToInt32(txtFechaNacimiento.Text.Substring(index, 4));

            if (year <= (DateTime.Now.Year - 13))
                args.IsValid = true;
            else
                args.IsValid = false;
        }

        protected void cvPasswordLengthValidation_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (args.Value.Length >= 8)
                args.IsValid = true;
            else
                args.IsValid = false;
        }
    }
}