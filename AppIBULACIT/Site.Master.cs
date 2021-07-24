using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppPagoBus
{
    public partial class SiteMaster : MasterPage
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
            inicializarHeaderTools();
        }

        protected void lnkCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("~/Login.aspx");
        }

        protected void inicializarHeaderTools()
        {
            if (Session["Tipo"].ToString() != "1")
            {
                navBarCliente.Visible = false;
                navBarAdmin.Visible = false;
                navBarChofer.Visible = false;
                navBarRuta.Visible = false;
            }
            if (Session["Tipo"].ToString() != "2")
            {
                navBarTarjeta.Visible = false;
            }
        }
    }
}