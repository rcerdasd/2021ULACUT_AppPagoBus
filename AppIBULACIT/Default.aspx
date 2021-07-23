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
                <a href="#" class="btn btn-success">Recarga tu saldo</a>
            </div>
        </div>
    </div>
    <!--
    <div class="row">
        <div class="col-md-4">
            <h2>Getting started</h2>
            <p>
                ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301948">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Get more libraries</h2>
            <p>
                NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Web Hosting</h2>
            <p>
                You can easily find a web hosting company that offers the right mix of features and price for your applications.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
            </p>
        </div>
      
    </div>
  -->
</asp:Content>
