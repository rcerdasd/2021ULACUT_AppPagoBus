<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmRecarga.aspx.cs" Inherits="AppIBULACIT.Views.frmRecarga" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        <asp:Label Text="Recarga" runat="server"></asp:Label>
    </h1>
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Literal ID="ltrSaldoActual" Text="Saldo actual" runat="server" /></td>
            <td>
                <asp:Literal ID="ltrlSaldo" runat="server" /></td>
        </tr>
        <tr>
            <td>
                <asp:Literal ID="ltrRecarga" Text="Monto a recargar" runat="server" /></td>
            <td>
                <asp:TextBox ID="txtRecarga" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvRecarga" ControlToValidate="txtRecarga" runat="server" ErrorMessage="Ingrese el monto"></asp:RequiredFieldValidator>
                
            </td>
        </tr>
        <tr>
            <td>
                <asp:Literal ID="ltrTarjeta" Text="Seleccione un metodo de pago" runat="server" />

            </td>

            <td>
                <asp:DropDownList ID="ddlTarjeta" CssClass="form-control" runat="server">
                </asp:DropDownList>

            </td>

        </tr>
        <tr>
            <td>
                <asp:LinkButton type="button" CssClass="btn btn-success" ID="btnAceptar" runat="server" Text="<span aria-hidden='false' class='glyphicon glyphicon-ok'></span> Aceptar" OnClick="btnAceptar_Click" /></td>
            <td>
                <asp:LinkButton type="button" CssClass="btn btn-danger" CausesValidation="false" ID="btnCancelar" runat="server" Text="<span aria-hidden='false' class='glyphicon glyphicon-remove'></span> Cerrar" OnClick="btnCancelar_Click" /></td>
        </tr>
    </table>
    <asp:Label ID="lblStatus" runat="server" Visible="false" Text="Label"></asp:Label>
</asp:Content>
