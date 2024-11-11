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
                            EmptyDataCssClass="alert alert-info text-center"
                            OnRowCommand="GridViewProductos_RowCommand">
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
                                <asp:TemplateField HeaderText="Fecha Alta">
                                    <ItemTemplate>
                                        <%# Convert.ToDateTime(Eval("fechaAlta")).ToString("dd/MM/yyyy") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                        <span class='<%# Eval("fechaBaja") == null ? "estado-badge estado-activo" : "estado-badge estado-inactivo" %>'>
                                            <%# Eval("fechaBaja") == null ? "Activo" : "Inactivo" %>
                                        </span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditar" runat="server" CommandName="EditarProducto" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-primary btn-sm">
                                            <i class="fas fa-edit"></i> Editar
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnEliminar" runat="server" CommandName="EliminarProducto" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('¿Está seguro que desea eliminar este producto?');">
                                            <i class="fas fa-trash"></i> Eliminar
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <%-- Formulario de Alta de Producto --%>
            <div class="mt-4">
                <h3>Alta de Producto</h3>
                <div class="form-group">
                    <label for="<%= TextBoxCodigo.ClientID %>">Código:</label>
                    <asp:TextBox ID="TextBoxCodigo" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="<%= TextBoxDescripcion.ClientID %>">Descripción:</label>
                    <asp:TextBox ID="TextBoxDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="<%= TextBoxPrecioUnitario.ClientID %>">Precio Unitario:</label>
                    <asp:TextBox ID="TextBoxPrecioUnitario" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="<%= TextBoxStock.ClientID %>">Stock:</label>
                    <asp:TextBox ID="TextBoxStock" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="<%= DropDownListProveedor.ClientID %>">Proveedor:</label>
                    <asp:DropDownList ID="DropDownListProveedor" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="mt-3">
                    <asp:Button ID="ButtonGuardarProducto" runat="server" Text="Guardar" OnClick="ButtonGuardarProducto_Click" CssClass="btn btn-primary" />
                </div>
            </div>

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
