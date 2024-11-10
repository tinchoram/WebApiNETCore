<%@ Page Title="Editar Proveedor" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarProveedor.aspx.cs" Inherits="TPInteg_UI.Pages.EditarProveedor" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header">
            <h2>Editar Proveedor</h2>
        </div>
        <div class="card-body">
            <%-- Panel de Error --%>
            <asp:Panel ID="PanelErrorEditarProveedor" runat="server" Visible="false" CssClass="alert alert-danger">
                <asp:Label ID="LabelErrorEditarProveedor" runat="server"></asp:Label>
            </asp:Panel>

            <%-- Formulario de Edición de Proveedor --%>
            <div class="form-group">
                <label for="TextBoxNombre">Nombre:</label>
                <asp:TextBox ID="TextBoxNombre" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label for="TextBoxEmail">Email:</label>
                <asp:TextBox ID="TextBoxEmail" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label for="TextBoxTelefono">Teléfono:</label>
                <asp:TextBox ID="TextBoxTelefono" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label for="TextBoxDireccion">Dirección:</label>
                <asp:TextBox ID="TextBoxDireccion" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label for="DropDownListLocalidad">Localidad:</label>
                <asp:DropDownList ID="DropDownListLocalidad" runat="server" CssClass="form-control" />
            </div>

            <asp:HiddenField ID="HiddenFieldProveedorId" runat="server" />

            <%-- Botones de Acción --%>
            <asp:Button ID="ButtonActualizar" runat="server" CssClass="btn btn-primary" Text="Actualizar" OnClick="ButtonActualizar_Click" />
            <asp:Button ID="ButtonCancelar" runat="server" CssClass="btn btn-secondary" Text="Cancelar" OnClick="ButtonCancelar_Click" />
        </div>
    </div>
</asp:Content>
