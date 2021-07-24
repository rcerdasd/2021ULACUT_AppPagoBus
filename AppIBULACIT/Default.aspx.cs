using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppPagoBus
{
    public partial class _Default : Page
    {

        protected override void OnInit(EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.MinValue);

            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CodigoUsuario"] == null)
            {
                Response.Redirect("~/Login.aspx");

            }else if(Session["Tipo"].ToString() == "1")
            {
                Response.Redirect("~/AdministratorPage.aspx");
            }
            else if (Session["Tipo"].ToString() == "3")
            {
                Response.Redirect("~/ChoferPage.aspx");
            }
            
             inicializarSaldo();
        }

        protected void inicializarSaldo()
        {
            try
            {
                string tipoUsuario = Session["Tipo"].ToString();
                if (tipoUsuario.Equals("2"))
                {
                    lblSaldoCliente.Text = "Tu saldo es de ₡" + Session["Saldo"].ToString();
                    lblSaldoCliente.Visible = true;
                }
            }
            catch (Exception)
            {
                lblSaldoCliente.Text = "No se pudo cargar el saldo";
                lblSaldoCliente.Visible = true;
            }

        }
    }
}