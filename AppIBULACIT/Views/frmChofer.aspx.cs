using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppPagoBus.Views
{
    public partial class frmChofer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodigoUsuario"] == null)
                    Response.Redirect("~/Login.aspx");
                else
                    InicializarControles();
            }
        }

        private void InicializarControles()
        {

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
           Response.Redirect("../Chofer.aspx");
           
        }
    }
}