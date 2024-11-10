<%@ Page Title="Listado de Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoProductos.aspx.cs" Inherits="TPInteg_UI.ListadoProductos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Listado de Productos</h2>
    <!-- GridView para mostrar la lista de productos -->
    <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="False">
        <!-- Definir columnas de GridView aquí -->
    </asp:GridView>
</asp:Content>
