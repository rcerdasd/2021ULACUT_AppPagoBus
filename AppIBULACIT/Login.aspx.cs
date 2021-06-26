using AppPagoBus.Controllers;
using AppPagoBus.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppPagoBus
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    try
                    {
                        LoginRequest loginRequest = new LoginRequest() { Username = txtUsername.Text, Password = txtPassword.Text };
                        PersonaManager personaManager = new PersonaManager();
                        Persona persona = new Persona();
                        persona = await personaManager.Autenticar(loginRequest);

                        if (persona != null)
                        {
                            JwtSecurityToken jwtSecurityToken;
                            var jwtHandler = new JwtSecurityTokenHandler();
                            jwtSecurityToken = jwtHandler.ReadJwtToken(persona.Token);

                            Session["CodigoUsuario"] = persona.Codigo;
                            Session["Identificacion"] = persona.Identificacion;
                            Session["Nombre"] = persona.Nombre;
                            Session["Email"] = persona.Email;
                            Session["Saldo"] = persona.Saldo;
                            Session["Token"] = persona.Token;

                            FormsAuthentication.RedirectFromLoginPage(persona.Usuario, false);

                        }
                        else
                        {
                            lblStatus.Text = "Credenciales invalidas";
                            lblStatus.Visible = true;
                        }
                    }
                    catch (Exception)
                    {
                        lblStatus.Text = "Hubo un error al iniciar sesion. Contacte al administrador del sistema";
                        lblStatus.Visible = true;
                    }
                }
            }
            catch (Exception)
            {

                lblStatus.Text = "Hubo un error";
                lblStatus.Visible = true;
            }
        }
    }
}