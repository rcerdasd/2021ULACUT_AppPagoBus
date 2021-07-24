<%@ Page Title="Home Page" Language="C#" Async="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AppPagoBus._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="width: max-content;">
        <div class="jumbotron">
            <h1>¡Bienvenido a DigiPay!</h1>
            <p class="lead">Plataforma digital para el pago de servicio de transporte en autobus</p>
        </div>
        <div style="border: 2px solid black; padding: 10px; width: min-content; align-items: center;">
            <div class="card-header">
                <h2>Saldo</h2>
            </div>
            <div class="card-body">
                <p>
                    <asp:Label ID="lblSaldoCliente" ForeColor="Black" Visible="false" runat="server" Text="Tu saldo"></asp:Label>
                </p>
                <a href="Views/frmRecarga.aspx" class="btn btn-success">Recarga tu saldo</a>
            </div>
        </div>
    </div>

</asp:Content>
