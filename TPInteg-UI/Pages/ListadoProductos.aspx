<%@ Page Title="Listado de Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoProductos.aspx.cs" Inherits="TPInteg_UI.Pages.ListadoProductos" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header">
            <h2>Listado de Productos</h2>
        </div>
        <div class="card-body">
            <asp:UpdatePanel ID="UpdatePanelProductos" runat="server">
                <ContentTemplate>
                    <%-- Panel de Error --%>
                    <asp:Panel ID="PanelErrorProductos" runat="server" Visible="false" CssClass="alert alert-danger">
                        <div class="d-flex justify-content-between align-items-center">
                            <asp:Label ID="LabelErrorProductos" runat="server"></asp:Label>
                            <asp:Button ID="ButtonReintentarProductos" runat="server" Text="Reintentar" 
                                OnClick="ButtonReintentarProductos_Click" CssClass="btn btn-outline-danger btn-sm" />
                        </div>
                    </asp:Panel>

                    <%-- Panel Sin Datos --%>
                    <asp:Panel ID="NoDataPanelProductos" runat="server" Visible="false" CssClass="alert alert-info">
                        <i class="fas fa-info-circle me-2"></i>No hay productos disponibles.
                    </asp:Panel>

                    <%-- GridView de Productos --%>
                    <div class="table-responsive">
                        <asp:GridView ID="GridViewProductos" runat="server" 
                            CssClass="table table-striped table-hover" 
                            AutoGenerateColumns="false"
                            EmptyDataText="No se encontraron productos."
                            EmptyDataCssClass="alert alert-info text-center">
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="ID" />
                                <asp:BoundField DataField="codigo" HeaderText="Código" />
                                <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                                <asp:TemplateField HeaderText="Precio Unitario">
                                    <ItemTemplate>
                                        <%# string.Format("${0:N2}", Eval("precioUnitario")) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="stock" HeaderText="Stock" />
                                <asp:BoundField DataField="proveedorId" HeaderText="ID Proveedor" />
                                <asp:TemplateField HeaderText="Fecha Alta">
                                    <ItemTemplate>
                                        <%# Eval("fechaAlta") != null ? Convert.ToDateTime(Eval("fechaAlta")).ToString("dd/MM/yyyy") : "-" %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                        <span class='<%# Eval("fechaBaja") == null ? "estado-badge estado-activo" : "estado-badge estado-inactivo" %>'>
                                            <%# Eval("fechaBaja") == null ? "Activo" : "Inactivo" %>
                                        </span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <%-- Indicador de Progreso --%>
            <asp:UpdateProgress ID="UpdateProgressProductos" runat="server" DisplayAfter="0">
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