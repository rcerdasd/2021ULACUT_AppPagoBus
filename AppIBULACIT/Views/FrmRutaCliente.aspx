<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" Async="true" AutoEventWireup="true" CodeBehind="FrmRutaCliente.aspx.cs" Inherits="AppIBULACIT.Views.FrmRutaCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><asp:Label Text="Rutas" runat="server"></asp:Label></h1>
    <input id="myInput" Placeholder="Buscar" class="from-control" type="text"/>
    <asp:GridView ID="gvRutas" runat="server" AutoGenerateColumns="False" CssClass="table table-dark" CellPadding="4" GridLines="None" HeaderStyle-BackColor="Black" AlternatingRowStyle-BackColor="gray" HeaderStyle-ForeColor="LightGray">
        <Columns>
            <asp:BoundField HeaderText="Costo" DataField="Costo" />
            <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField HeaderText="Provincia" DataField="Provincia" />
            <asp:ButtonField HeaderText="ObtenerTicket" CommandName="ObtenerTicket" ControlStyle-CssClass="btn btn-primary" ButtonType="Button" Text="Obtener Ticket" />
        </Columns>
    </asp:GridView>
    <br/>
    <asp:Label ID="lblStatus" ForeColor="Maroon" runat="server" Visible="false" />

</asp:Content>
