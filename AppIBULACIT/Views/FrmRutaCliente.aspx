<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="FrmRutaCliente.aspx.cs" Inherits="AppIBULACIT.Views.FrmRutaCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }

        function openModalMantenimiento() {
            $('#myModalMantenimiento').modal('show');
        }

        function CloseModal() {
            $('#myModal').modal('hide');
        }

        function CloseMantenimiento() {
            $('#myModalMantenimiento').modal('hide');
        }

        function ShowMe() {
            //Code to get the selected value from RadioButtonList
            var selectedvalue = $('#<%= rb.ClientID %> input:checked').val();
            //var x = document.getElementById("rbExperience").value;
            if (selectedvalue == 1) {
                // Show the TR
                $("#trTarjeta").show();
            }
            else {
                //Hide the TR
                $("#trTarjeta").hide();
            }
        }


        $(document).ready(function () {
            $("#myInput").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $("#MainContent_gvServicios tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });
    </script>

    <h1>
        <asp:Label Text="Rutas" runat="server"></asp:Label></h1>

    <input id="myInput" placeholder="Buscar" class="from-control" type="text" />
    <asp:GridView ID="gvRutas" OnRowCommand="gvRutas_RowCommand" runat="server" AutoGenerateColumns="False" CssClass="table table-dark" CellPadding="4" GridLines="None" HeaderStyle-BackColor="Black" AlternatingRowStyle-BackColor="gray" HeaderStyle-ForeColor="LightGray">
        <Columns>
            <asp:BoundField HeaderText="Codigo de la ruta" DataField="Codigo" />
            <asp:BoundField HeaderText="Costo" DataField="Costo" />
            <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField HeaderText="Provincia" DataField="Provincia" />
            <asp:ButtonField HeaderText="ObtenerTicket" CommandName="ObtenerTicket" ControlStyle-CssClass="btn btn-primary" ButtonType="Button" Text="Obtener Ticket" />
        </Columns>
    </asp:GridView>
    <br />
    <asp:Label ID="lblStatus" ForeColor="Maroon" runat="server" Visible="false" />


    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Compra tiquete</h4>
                </div>
                <div class="modal-body">
                    <p>
                        <asp:Literal ID="ltrModalMensaje" runat="server" /><asp:Label ID="lblCodigoEliminar" runat="server"></asp:Label>
                    </p>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton type="button" CssClass="btn btn-success" ID="btnAceptarModal" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Aceptar" OnClick="btnAceptarModal_Click" />
                    <asp:LinkButton type="button" CssClass="btn btn-danger" ID="btnCancelarModal" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-remove'></span> Cerrar" OnClick="btnCancelarModal_Click" />
                </div>
            </div>
        </div>
    </div>

    <div id="myModalMantenimiento" class="modal fade" role="dialog">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">
                        <asp:Literal ID="ltrTituloMantenimiento" runat="server"></asp:Literal></h4>
                </div>
                <div class="modal-body">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:Literal ID="ltrRuta" Text="Ruta" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtRuta" Enabled="false" runat="server" CssClass="form-control" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrCodigoRuta" Text="Codigo de la ruta" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtCodigoRuta" Enabled="false" runat="server" CssClass="form-control" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltr" Text="Metodo de pago" runat="server" /></td>
                            <td>
                                <asp:RadioButtonList ID="rb" onchange="ShowMe()" runat="server">
                                    <asp:ListItem runat="server" ID="liSaldo" Value="0">Saldo</asp:ListItem>
                                    <asp:ListItem runat="server" ID="liTarjeta" Value="1">Tarjeta</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr id="trTarjeta">
                            <td>
                                <asp:Literal ID="ltrTarjeta" Text="Tarjeta" runat="server" /></td>
                            <td>
                                <asp:DropDownList ID="ddlTarjeta" Visible="true" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltlMonto" Text="Monto" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtMonto" Enabled="false" runat="server" CssClass="form-control" /></td>
                        </tr>
                    </table>
                    <asp:Label ID="lblResultado" ForeColor="Maroon" Visible="False" runat="server" />
                </div>
                <div class="modal-footer">
                    <asp:LinkButton type="button" CssClass="btn btn-success" ID="btnAceptarMant" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Aceptar" OnClick="btnAceptarMant_Click" />
                    <asp:LinkButton type="button" CssClass="btn btn-danger" ID="btnCancelarMant" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-remove'></span> Cerrar" OnClick="btnCancelarMant_Click" />
                </div>
            </div>
        </div>
    </div>



</asp:Content>
