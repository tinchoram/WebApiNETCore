<%@ Page Title="Editar Localidad" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarLocalidad.aspx.cs" Inherits="TPInteg_UI.Pages.EditarLocalidad" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header">
            <h2>Editar Localidad</h2>
        </div>
        <div class="card-body">
            <asp:UpdatePanel ID="UpdatePanelEditarLocalidad" runat="server">
                <ContentTemplate>
                    <%-- Panel de Error --%>
                    <asp:Panel ID="PanelErrorEditarLocalidad" runat="server" Visible="false" CssClass="alert alert-danger">
                        <asp:Label ID="LabelErrorEditarLocalidad" runat="server"></asp:Label>
                    </asp:Panel>

                    <%-- Formulario de Edición de Localidad --%>
                    <div class="form-group">
                        <label for="<%= TextBoxNombre.ClientID %>">Nombre:</label>
                        <asp:TextBox ID="TextBoxNombre" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="<%= TextBoxCodigoPostal.ClientID %>">Código Postal:</label>
                        <asp:TextBox ID="TextBoxCodigoPostal" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="mt-3">
                        <asp:HiddenField ID="HiddenFieldLocalidadId" runat="server" />
                        <asp:Button ID="ButtonActualizar" runat="server" Text="Actualizar" OnClick="ButtonActualizar_Click" CssClass="btn btn-primary" />
                        <asp:Button ID="ButtonCancelar" runat="server" Text="Cancelar" OnClick="ButtonCancelar_Click" CssClass="btn btn-secondary ms-2" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <%-- Indicador de Progreso --%>
            <asp:UpdateProgress ID="UpdateProgressEditarLocalidad" runat="server" DisplayAfter="0">
                <ProgressTemplate>
                    <div class="progress-overlay">
                        <div class="spinner-border text-primary" role="status">
                            <span class="sr-only">Cargando...</span>
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </div>
</asp:Content>