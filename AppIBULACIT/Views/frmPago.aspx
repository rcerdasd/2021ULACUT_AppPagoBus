<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmPago.aspx.cs" Inherits="AppIBULACIT.Views.frmPago" %>

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
        <asp:Label Text="Tarjetas" runat="server"></asp:Label></h1>
    <input id="myInput" placeholder="Buscar" class="from-control" type="text" />
    <asp:GridView ID="gvTarjetas" OnRowCommand="gvTarjetas_RowCommand" runat="server" AutoGenerateColumns="False" CssClass="table table-dark" CellPadding="4" GridLines="None" HeaderStyle-BackColor="Black" AlternatingRowStyle-BackColor="gray" HeaderStyle-ForeColor="LightGray">
        <Columns>
            <asp:BoundField HeaderText="Codigo" DataField="Codigo" />
            <asp:BoundField HeaderText="Numero" DataField="Numero" />
            <asp:BoundField HeaderText="CCV" DataField="CCV" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField HeaderText="MesExpiracion" DataField="MesExpiracion" />
                        <asp:BoundField HeaderText="Año Expiracion" DataField="AnioExpiracion" />
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Predeterminado" DataField="Predeterminado" />
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
                    <h4 class="modal-title">Mantenimiento de tarjetas</h4>
                </div>
                <div class="modal-body">
                    <p>
                        <asp:Literal ID="ltrModalMensaje" runat="server" /><asp:Label ID="lblCodigoEliminar" Visible="false" runat="server"></asp:Label>
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
                                <asp:Literal ID="ltrCodigoMant" Text="Codigo" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtCodigoMant" runat="server" Enabled="false" CssClass="form-control" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrNumero" Text="Numero" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control" />
                                <asp:CustomValidator ID="cvNumeroLength" ControlToValidate="txtNumero" OnServerValidate="cvNumeroLength_ServerValidate" runat="server" ErrorMessage="Debe ingresar al menos 15 digitos"></asp:CustomValidator>
                                <asp:RegularExpressionValidator ID="regNumericNum" ControlToValidate="txtNumero" ValidationExpression="^\d+$" runat="server" ErrorMessage="Solo se puede ingresar numeros"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrCcv" Text="CCV" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtCcv" runat="server" CssClass="form-control" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrMesExpiracion" Text="Mes Expiracion" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtMesExpiracion" runat="server" CssClass="form-control" /></td>
                        </tr>
                                                <tr>
                            <td>
                                <asp:Literal ID="ltrAnioExpiracion" Text="Anio Expiracion" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtAnioExpiracion" runat="server" CssClass="form-control" />
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrNombre" Text="Nombre" runat="server" /></td>
                            <td>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrPredeterminado" Text="Predeterminado" runat="server" /></td>

                            <td>
                                <asp:DropDownList ID="ddlPredeterminado" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="1">Si</asp:ListItem>
                                    <asp:ListItem Value="0">No</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="lblResultado" ForeColor="Maroon" Visible="False" runat="server" />
                </div>
                <div class="modal-footer">
                    <asp:LinkButton type="button" CausesValidation="true" CssClass="btn btn-success" ID="btnAceptarMant" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Aceptar" OnClick="btnAceptarMant_Click" />
                    <asp:LinkButton type="button" CssClass="btn btn-danger" ID="btnCancelarMant" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-remove'></span> Cerrar" OnClick="btnCancelarMant_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
