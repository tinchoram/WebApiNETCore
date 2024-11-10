<%@ Page Title="Listado de Proveedores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoProveedores.aspx.cs" Inherits="TPInteg_UI.Pages.ListadoProveedores" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header">
            <h2>Listado de Proveedores</h2>
        </div>
        <div class="card-body">
            <asp:UpdatePanel ID="UpdatePanelProveedores" runat="server">
                <ContentTemplate>
                    <%-- Panel de Error --%>
                    <asp:Panel ID="PanelErrorProveedores" runat="server" Visible="false" CssClass="alert alert-danger">
                        <div class="d-flex justify-content-between align-items-center">
                            <asp:Label ID="LabelErrorProveedores" runat="server"></asp:Label>
                            <asp:Button ID="ButtonReintentarProveedores" runat="server" Text="Reintentar" 
                                OnClick="ButtonReintentarProveedores_Click" CssClass="btn btn-outline-danger btn-sm" />
                        </div>
                    </asp:Panel>

                    <%-- Panel Sin Datos --%>
                    <asp:Panel ID="NoDataPanelProveedores" runat="server" Visible="false" CssClass="alert alert-info">
                        <i class="fas fa-info-circle me-2"></i>No hay proveedores disponibles.
                    </asp:Panel>

                    <%-- GridView de Proveedores --%>
                    <div class="table-responsive">
                        <asp:GridView ID="GridViewProveedores" runat="server" 
                            CssClass="table table-striped table-hover" 
                            AutoGenerateColumns="false"
                            EmptyDataText="No se encontraron proveedores."
                            EmptyDataCssClass="alert alert-info text-center"
                            OnRowCommand="GridViewProveedores_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="ID" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="Email" HeaderText="Email" />
                                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                                <asp:TemplateField HeaderText="Localidad">
                                    <ItemTemplate>
                                        <%# Eval("LocalidadId") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <asp:LinkButton ID="btnEditar" runat="server" CommandName="EditarProveedor" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-primary btn-sm">
                                                <i class="fas fa-edit"></i> Editar
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnEliminar" runat="server" CommandName="EliminarProveedor" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-danger btn-sm ms-1" OnClientClick="return confirm('¿Está seguro que desea eliminar este proveedor?');">
                                                <i class="fas fa-trash"></i> Eliminar
                                            </asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <%-- Formulario de Alta de Proveedor --%>
            <div class="mt-4">
                <h3>Alta de Proveedor</h3>
                <div class="form-group">
                    <label for="<%= TextBoxNombre.ClientID %>">Nombre:</label>
                    <asp:TextBox ID="TextBoxNombre" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="<%= TextBoxDireccion.ClientID %>">Dirección:</label>
                    <asp:TextBox ID="TextBoxDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="<%= TextBoxEmail.ClientID %>">Email:</label>
                    <asp:TextBox ID="TextBoxEmail" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="<%= TextBoxTelefono.ClientID %>">Teléfono:</label>
                    <asp:TextBox ID="TextBoxTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="<%= DropDownListLocalidad.ClientID %>">Localidad:</label>
                    <asp:DropDownList ID="DropDownListLocalidad" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="mt-3">
                    <asp:Button ID="ButtonGuardar" runat="server" Text="Guardar" OnClick="ButtonGuardar_Click" CssClass="btn btn-primary" />
                </div>
            </div>

            <%-- Indicador de Progreso --%>
            <asp:UpdateProgress ID="UpdateProgressProveedores" runat="server" DisplayAfter="0">
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