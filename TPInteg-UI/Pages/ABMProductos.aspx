<%@ Page Title="Administración de Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ABMProductos.aspx.cs" Inherits="TPInteg_UI.Pages.ABMProductos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header bg-primary text-white">
            <h2 class="mb-0">Administración de Productos</h2>
        </div>
        <div class="card-body">
            <p class="text-muted mb-4">Aquí puedes añadir, modificar o eliminar productos.</p>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <%-- Panel de Error --%>
                    <asp:Panel ID="PanelError" runat="server" Visible="false" CssClass="alert alert-danger mb-4">
                        <div class="d-flex justify-content-between align-items-center">
                            <asp:Label ID="LabelError" runat="server"></asp:Label>
                            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                        </div>
                    </asp:Panel>

                    <%-- Formulario de Producto --%>
                    <div class="row g-3 mb-4">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtCodigo" class="form-label">Código</label>
                                <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvCodigo" runat="server"
                                    ControlToValidate="txtCodigo"
                                    ErrorMessage="El código es requerido"
                                    Display="Dynamic"
                                    CssClass="text-danger" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtDescripcion" class="form-label">Descripción</label>
                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvDescripcion" runat="server"
                                    ControlToValidate="txtDescripcion"
                                    ErrorMessage="La descripción es requerida"
                                    Display="Dynamic"
                                    CssClass="text-danger" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="txtPrecioUnitario" class="form-label">Precio Unitario</label>
                                <asp:TextBox ID="txtPrecioUnitario" runat="server" CssClass="form-control" Type="number" step="0.01" />
                                <asp:RangeValidator ID="rvPrecioUnitario" runat="server"
                                    ControlToValidate="txtPrecioUnitario"
                                    MinimumValue="1" MaximumValue="1000"
                                    Type="Currency"
                                    ErrorMessage="El precio debe estar entre 1 y 1000"
                                    Display="Dynamic"
                                    CssClass="text-danger" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="txtStock" class="form-label">Stock</label>
                                <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" Type="number" />
                                <asp:RequiredFieldValidator ID="rfvStock" runat="server"
                                    ControlToValidate="txtStock"
                                    ErrorMessage="El stock es requerido"
                                    Display="Dynamic"
                                    CssClass="text-danger" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="ddlProveedor" class="form-label">Proveedor</label>
                                <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="form-select">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvProveedor" runat="server"
                                    ControlToValidate="ddlProveedor"
                                    ErrorMessage="Debe seleccionar un proveedor"
                                    InitialValue=""
                                    Display="Dynamic"
                                    CssClass="text-danger" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="ddlEstado" class="form-label">Estado</label>
                                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select">
                                    <asp:ListItem Text="Seleccione un estado" Value="" />
                                    <asp:ListItem Text="Disponible" Value="Disponible" />
                                    <asp:ListItem Text="No Disponible" Value="No Disponible" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvEstado" runat="server"
                                    ControlToValidate="ddlEstado"
                                    ErrorMessage="Debe seleccionar un estado"
                                    InitialValue=""
                                    Display="Dynamic"
                                    CssClass="text-danger" />
                            </div>
                        </div>
                    </div>

                    <%-- Botones de Acción --%>
                    <div class="row mb-4">
                        <div class="col">
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" 
                                OnClick="btnGuardar_Click" 
                                CssClass="btn btn-primary me-2" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                                OnClick="btnCancelar_Click"
                                CssClass="btn btn-secondary"
                                CausesValidation="false" />
                            <asp:HiddenField ID="hdnProductoId" runat="server" />
                        </div>
                    </div>

                    <%-- GridView de Productos --%>
                    <div class="table-responsive">
                        <asp:GridView ID="gvProductos" runat="server"
                            CssClass="table table-striped table-hover"
                            AutoGenerateColumns="false"
                            OnRowCommand="gvProductos_RowCommand"
                            DataKeyNames="Id">
                            <Columns>
                                <asp:BoundField DataField="Codigo" HeaderText="Código" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unit." DataFormatString="{0:C}" />
                                <asp:BoundField DataField="Stock" HeaderText="Stock" />
                                <asp:BoundField DataField="ProveedorNombre" HeaderText="Proveedor" />
                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                        <span class='<%# Eval("Estado").ToString() == "Disponible" ? "estado-badge estado-activo" : "estado-badge estado-inactivo" %>'>
                                            <%# Eval("Estado") %>
                                        </span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEditar" runat="server"
                                            CommandName="Editar"
                                            CommandArgument='<%# Container.DataItemIndex %>'
                                            CssClass="btn btn-sm btn-primary me-2">
                                            <i class="fas fa-edit"></i> Editar
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lnkEliminar" runat="server"
                                            CommandName="Eliminar"
                                            CommandArgument='<%# Container.DataItemIndex %>'
                                            CssClass="btn btn-sm btn-danger"
                                            OnClientClick="return confirm('¿Está seguro que desea eliminar este producto?');">
                                            <i class="fas fa-trash-alt"></i> Eliminar
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="alert alert-info">No hay productos registrados.</div>
                            </EmptyDataTemplate>
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

