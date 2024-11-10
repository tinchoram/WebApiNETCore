<%@ Page Title="Listado de Proveedores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoProveedores.aspx.cs" Inherits="TPInteg_UI.ListadoProveedores" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header">
            <h2>Listado de Proveedores</h2>
        </div>
        <div class="card-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <%-- Panel de Error --%>
                    <asp:Panel ID="PanelError" runat="server" Visible="false" CssClass="alert alert-danger">
                        <div class="d-flex justify-content-between align-items-center">
                            <asp:Label ID="LabelError" runat="server"></asp:Label>
                            <asp:Button ID="ButtonReintentar" runat="server" Text="Reintentar" 
                                OnClick="ButtonReintentar_Click" CssClass="btn btn-outline-danger btn-sm" />
                        </div>
                    </asp:Panel>

                    <%-- Panel Sin Datos --%>
                    <asp:Panel ID="NoDataPanel" runat="server" Visible="false" CssClass="alert alert-info">
                        <i class="fas fa-info-circle me-2"></i>No hay proveedores disponibles.
                    </asp:Panel>

                    <%-- GridView de Proveedores --%>
                    <div class="table-responsive">
                        <asp:GridView ID="GridViewProveedores" runat="server" 
                            CssClass="table table-striped table-hover" 
                            AutoGenerateColumns="false"
                            EmptyDataText="No se encontraron proveedores."
                            EmptyDataCssClass="alert alert-info text-center">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="ID" />
                                <asp:TemplateField HeaderText="Nombre">
                                    <ItemTemplate>
                                        <%# string.IsNullOrEmpty(Eval("Nombre").ToString()) ? "-" : Eval("Nombre") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                                <asp:TemplateField HeaderText="Email">
                                    <ItemTemplate>
                                        <%# string.IsNullOrEmpty(Eval("Email").ToString()) ? "-" : Eval("Email") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Teléfono">
                                    <ItemTemplate>
                                        <%# string.IsNullOrEmpty(Eval("Telefono").ToString()) ? "-" : Eval("Telefono") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="LocalidadId" HeaderText="ID Localidad" />
                                <asp:TemplateField HeaderText="Fecha Alta">
                                    <ItemTemplate>
                                        <%# Eval("FechaAlta") != null ? Convert.ToDateTime(Eval("FechaAlta")).ToString("dd/MM/yyyy") : "-" %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                        <span class='<%# Eval("FechaBaja") == null ? "estado-badge estado-activo" : "estado-badge estado-inactivo" %>'>
                                            <%# Eval("FechaBaja") == null ? "Activo" : "Inactivo" %>
                                        </span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <%-- Indicador de Progreso --%>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
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