using AppIBULACIT.Controllers;
using AppPagoBus;
using AppPagoBus.Controllers;
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
    public partial class FrmRutaCliente : System.Web.UI.Page
    {
        IEnumerable<Ruta> rutaList = new ObservableCollection<Ruta>();
        IEnumerable<TarjetaModel> listaTarjetas = new ObservableCollection<TarjetaModel>();

        TarjetaManager tarjetaManager = new TarjetaManager();
        RutaManager rutaManager = new RutaManager();
        UsuarioManager usuarioManager = new UsuarioManager();
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

        private async void InicializarControles()
        {
            try
            {
                rutaList = await rutaManager.GetAll(Session["Token"].ToString());
                gvRutas.DataSource = rutaList.ToList();
                gvRutas.DataBind();
            }
            catch (Exception e)
            {

                lblStatus.Text = "Hubo un error al cargar la lista de rutas. Error: " + e.Message;
            }
        }

        protected async void gvRutas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvRutas.Rows[index];
            switch (e.CommandName)
            {
                case "ObtenerTicket":
                    ltrTituloMantenimiento.Text = "Compra de tiquetes";
                    btnAceptarMant.ControlStyle.CssClass = "btn btn-primary";
                    txtRuta.Text = row.Cells[2].Text.Trim();
                    txtCodigoRuta.Text = row.Cells[0].Text.Trim();
                    listaTarjetas = await tarjetaManager.GetId(Session["Token"].ToString(), Session["CodigoUsuario"].ToString());
                    ddlTarjeta.DataSource = listaTarjetas.ToList();
                    ddlTarjeta.DataTextField = "Numero";
                    ddlTarjeta.DataValueField = "Codigo";
                    ddlTarjeta.DataBind();
                    txtMonto.Text = row.Cells[1].Text.Trim();
                    btnAceptarMant.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento();});", true);
                    break;
                default:
                    break;
            }
        }

        protected void openModal(string message, bool btnAceptar)
        {
            btnAceptarModal.Visible = btnAceptar;
            ltrModalMensaje.Text = message;
            ltrModalMensaje.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function(){openModal(); } );", true);
        }

        protected void btnCancelarModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { CloseModal(); });", true);
        }

        protected void btnCancelarMant_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() {​​​ CloseMantenimiento(); }​​​);", true);
        }


        protected void btnAceptarModal_Click(object sender, EventArgs e)
        {

        }


        protected async void btnAceptarMant_Click(object sender, EventArgs e)
        {
            try
            {

                Transaccion transaccion = new Transaccion();
                transaccion.ClienteId = Convert.ToInt32(Session["CodigoUsuario"].ToString());
                transaccion.RutaId = Convert.ToInt32(txtCodigoRuta.Text.ToString());
                transaccion.TarjetaClienteId = Convert.ToInt32(ddlTarjeta.SelectedValue.ToString());
                transaccion.Fecha = DateTime.Now;
                transaccion.Monto = Convert.ToDecimal(txtMonto.Text);
                transaccion.Estado = "1";

                TransaccionManager transaccionManager = new TransaccionManager();
                Transaccion transaccionInsertada = new Transaccion();

                if (rb.SelectedIndex == 0)
                {
                    if (Convert.ToDecimal(Session["Saldo"].ToString()) < transaccion.Monto)
                    {
                        openModal("No tienes suficiente saldo. Intenta recargar o hacer el pago con tarjeta de credito.", false);
                    }
                    else
                    {
                        transaccionInsertada = await transaccionManager.Ingresar(transaccion, Session["Token"].ToString());
                        if (transaccionInsertada.Estado.Equals("1"))
                        {
                            decimal nuevoSaldo = Convert.ToDecimal(Session["Saldo"].ToString()) - transaccion.Monto;
                            Persona persona = new Persona()
                            {
                                Codigo = Convert.ToInt32(Session["CodigoUsuario"].ToString()),
                                Saldo = nuevoSaldo
                            };

                            Persona usuarioActualizado = new Persona();

                            usuarioActualizado = await usuarioManager.Actualizar(persona, Session["Token"].ToString());

                            if (usuarioActualizado.Codigo == Convert.ToInt32(Session["CodigoUsuario"].ToString()))
                            {
                                Session["Saldo"] = nuevoSaldo;
                            }
                            openModal("Transaccion realizada", false);
                            InicializarControles();
                        }
                        else
                        {
                            openModal("No se pudo completar la transaccion", false);
                            InicializarControles();
                        }
                    }
                }
                else
                {
                    transaccionInsertada = await transaccionManager.Ingresar(transaccion, Session["Token"].ToString());
                    if (transaccionInsertada.Estado.Equals("1"))
                    {
                        openModal("Transaccion realizada", false);
                        InicializarControles();
                    }
                    else
                    {
                        openModal("No se pudo completar la transaccion", false);
                        InicializarControles();
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

        private void abrirMant()
        {
            ltrTituloMantenimiento.Text = "Compra de tiquetes";
            btnAceptarMant.ControlStyle.CssClass = "btn btn-sucess";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento();});", true);
        }


    }
}