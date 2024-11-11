<%@ Page Title="Editar Producto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarProducto.aspx.cs" Inherits="TPInteg_UI.Pages.EditarProducto" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header">
            <h2>Editar Producto</h2>
        </div>
        <div class="card-body">
            <%-- Panel de Error --%>
            <asp:Panel ID="PanelErrorEditarProducto" runat="server" Visible="false" CssClass="alert alert-danger">
                <asp:Label ID="LabelErrorEditarProducto" runat="server"></asp:Label>
            </asp:Panel>

            <%-- Formulario de Edición de Producto --%>
            <div class="form-group">
                <label for="TextBoxCodigo">Código:</label>
                <asp:TextBox ID="TextBoxCodigo" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label for="TextBoxDescripcion">Descripción:</label>
                <asp:TextBox ID="TextBoxDescripcion" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label for="TextBoxPrecioUnitario">Precio Unitario:</label>
                <asp:TextBox ID="TextBoxPrecioUnitario" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label for="TextBoxStock">Stock:</label>
                <asp:TextBox ID="TextBoxStock" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label for="DropDownListProveedor">Proveedor:</label>
                <asp:DropDownList ID="DropDownListProveedor" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label for="DropDownListEstado">Estado:</label>
                <asp:DropDownList ID="DropDownListEstado" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Activo" Value="activo" />
                    <asp:ListItem Text="Inactivo" Value="inactivo" />
                </asp:DropDownList>
            </div>

            <asp:HiddenField ID="HiddenFieldProductoId" runat="server" />

            <%-- Botones de Acción --%>
            <div class="mt-3">
                <asp:Button ID="ButtonActualizar" runat="server" CssClass="btn btn-primary" Text="Actualizar" OnClick="ButtonActualizar_Click" />
                <asp:Button ID="ButtonCancelar" runat="server" CssClass="btn btn-secondary ms-2" Text="Cancelar" OnClick="ButtonCancelar_Click" />
            </div>
        </div>
    </div>
</asp:Content>