<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="frmRuta.aspx.cs" Inherits="AppPagoBus.Views.frmRuta" %>
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
    <input id="myInput" Placeholder="Buscar" class="from-control" type="text"/>
    <asp:GridView ID="gvRutas" OnRowCommand="gvRutas_RowCommand" runat="server" AutoGenerateColumns="False" CssClass="table table-dark" CellPadding="4" GridLines="None" HeaderStyle-BackColor="Black" AlternatingRowStyle-BackColor="gray" HeaderStyle-ForeColor="LightGray">
        <Columns>
            <asp:BoundField HeaderText="Codigo" DataField="Codigo" />
            <asp:BoundField HeaderText="Costo" DataField="Costo" />
            <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField HeaderText="Provincia" DataField="Provincia" />
            <asp:ButtonField HeaderText="Modificar" CommandName="Modificar" ControlStyle-CssClass="btn btn-primary" ButtonType="Button" Text="Modificar" />
            <asp:ButtonField HeaderText="Eliminar" CommandName="Eliminar" ControlStyle-CssClass="btn btn-danger" ButtonType="Button" Text="Eliminar" />
        </Columns>
    </asp:GridView>
    <asp:LinkButton type="button" CssClass="btn btn-success" ID="btnNuevo" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-floppy-disk'></span> Nuevo" OnClick="btnNuevo_Click" />
    <br />
    <asp:Label ID="lblStatus" ForeColor="Maroon" runat="server" Visible="false" />

    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Mantenimiento de rutas</h4>
                </div>
                <div class="modal-body">
                    <p>
                        <asp:Literal ID="ltrModalMensaje" runat="server" /><asp:Label ID="lblCodigoEliminar" runat="server"></asp:Label></p>
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
                                <asp:Literal ID="ltrCodigoMant" Text="Codigo" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtCodigoMant" runat="server" Enabled="false" CssClass="form-control" /></td>
                        </tr>
                                                <tr>
                            <td>
                                <asp:Literal ID="ltrCosto" Text="Costo" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtCosto" runat="server" CssClass="form-control" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrDescripcion" Text="Descripcion" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrProvincia" Text="Provincia" runat="server" /></td>
                                                        <td>
                                <asp:TextBox ID="txtProvincia" runat="server" CssClass="form-control" /></td>
                            <td>
                                <asp:DropDownList OnSelectedIndexChanged="ddlProvincia_SelectedIndexChanged" ID="ddlProvincia" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="San Jose">San Jose</asp:ListItem>
                                    <asp:ListItem Value="Alajuela">Alajuela</asp:ListItem>
                                    <asp:ListItem Value="Cartago">Cartago</asp:ListItem>
                                    <asp:ListItem Value="Puntarenas">Puntarenas</asp:ListItem>
                                    <asp:ListItem Value="Guanacaste">Guanacaste</asp:ListItem>
                                    <asp:ListItem Value="Limon">Limon</asp:ListItem>
                                    <asp:ListItem Value="Heredia">Heredia</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="lblResultado" ForeColor="Maroon" Visible="False" runat="server" />
                </div>
                <div class="modal-footer">
                    <asp:LinkButton type="button" CssClass="btn btn-success" ID="btnAceptarMant" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Aceptar" OnClick="btnAceptarMant_Click" />
                    <asp:LinkButton type="button" CssClass="btn btn-danger" ID="btnCancelarMant" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-remove'></span> Cerrar" OnClick="btnCancelarMant_Click"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>