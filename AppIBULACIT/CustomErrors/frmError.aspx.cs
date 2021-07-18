using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppIBULACIT.CustomErrors
{
    public partial class frmError : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Exception err = Session["LastError"] as Exception;
            //Exception err = Server.GetLastError();
            if (err != null)
            {
                err = err.GetBaseException();
                lblError.Text = err.Message;
                Session["LastError"] = null;
            }
        }
    }
}