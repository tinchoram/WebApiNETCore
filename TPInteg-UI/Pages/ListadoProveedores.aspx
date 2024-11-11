<%@ Page Title="Listado de Proveedores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoProveedores.aspx.cs" Inherits="TPInteg_UI.Pages.ListadoProveedores" Async="true" %>
<%@ Register Src="~/Controls/CuitNumberFormatter.ascx" TagPrefix="uc" TagName="NumberFormatter" %>
<%@ Register Src="~/Controls/SearchControl.ascx" TagPrefix="uc" TagName="SearchControl" %>

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

                    <%-- Filtros de Proveedores --%>                    
                    <div class="mt-3">
                        <asp:Panel ID="FiltrosPanel" runat="server" BorderWidth="1" BackColor="LightBlue" ForeColor="White">
                            <div class="input-group mb-3">
                                <label for="<%= txtSearchByNameInput.ClientID %>">Bucar Por Nombre:</label>
                                <asp:TextBox ID="txtSearchByNameInput" CssClass="form-control" placeholder="Buscar..." runat="server"/>
                            </div>
                            <div class="input-group mb-3">
                                <label for="<%= txtSearchByLastNameInput.ClientID %>">Bucar Por Apellido:</label>
                                <asp:TextBox ID="txtSearchByLastNameInput" CssClass="form-control" placeholder="Buscar..." runat="server"/>
                            </div>
                            <div class="input-group mb-3">
                                <label for="<%= txtSearchByComercialNameInput.ClientID %>">Bucar Por Nombre Comercial:</label>
                                <asp:TextBox ID="txtSearchByComercialNameInput" CssClass="form-control" placeholder="Buscar..." runat="server"/>
                            </div>
                            <asp:Button ID="btnSearchButton" CssClass="btn btn-primary" Text="Buscar" OnClick="SearchButton_Click" runat="server" />
                            <asp:Button ID="btnLimpiarFiltros" CssClass="btn btn-primary" Text="Limpiar" OnClick="LimpiarFiltros_Click" runat="server" />
                        </asp:Panel>
                        </div>        
                    <br />

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
                                <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                                <asp:BoundField DataField="NombreComercial" HeaderText="Nombre Comercial" />
                                <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
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
            <br />
            <%-- Formulario de Alta de Proveedor --%>
            <div class="container">
                <fieldset class="form-group">
                    <!-- Form Name -->
                    <legend>Alta de Proveedor</legend>
                    <div class="form-group p-md-2">                        
                        <div class="col-md-4">
                            <div class="input-group">
                                <label for="<%= TextBoxNombre.ClientID %>" class="col-md-4 control-label">Nombre:</label>
                                <asp:TextBox ID="TextBoxNombre" runat="server" CssClass="form-control"></asp:TextBox>                    
                                <asp:RequiredFieldValidator ID="NombreRequiredField" runat="server" ControlToValidate="TextBoxNombre"
                                    ErrorMessage="El Nombre del Proveedor es requerido." Display="None"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="form-group p-md-2">
                        <div class="col-md-4">
                            <div class="input-group">
                                <label for="<%= TextBoxApellido.ClientID %>" class="col-md-4 control-label">Apellido:</label>
                                <asp:TextBox ID="TextBoxApellido" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="ApellidoRequiredField" runat="server" ControlToValidate="TextBoxApellido"
                                    ErrorMessage="El Apellido del Proveedor es requerido." Display="None"></asp:RequiredFieldValidator>
                             </div>
                        </div>
                    </div>
                    <div class="form-group p-md-2">
                        <div class="col-md-4">
                            <div class="input-group">
                                <label for="<%= TextBoxNombreComercial.ClientID %>" class="col-md-4 control-label">NombreComercial:</label>
                                <asp:TextBox ID="TextBoxNombreComercial" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="NombreComercialRequiredField" runat="server" ControlToValidate="TextBoxNombreComercial"
                                    ErrorMessage="El Nombre Comercial del Proveedor es requerido." Display="None"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="form-group p-md-2">
                        <div class="col-md-4">
                            <div class="input-group">
                                <label for="<%= TextBoxDireccion.ClientID %>" class="col-md-4 control-label">Dirección:</label>
                                <asp:TextBox ID="TextBoxDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="DireccionRequiredField" runat="server" ControlToValidate="TextBoxDireccion"
                                    ErrorMessage="La Dirección del Proveedor es requerida." Display="None"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="form-group p-md-2">
                        <div class="col-md-4">
                            <div class="input-group">
                                <uc:NumberFormatter ID="ucNumberFormatter" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group p-md-2">
                        <div class="col-md-4">
                            <div class="input-group">
                                <label for="<%= TextBoxEmail.ClientID %>" class="col-md-4 control-label">Email:</label>
                                <asp:TextBox ID="TextBoxEmail" runat="server" CssClass="form-control" PlaceHolder="email@dominio.com"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequiredField" runat="server" ControlToValidate="TextBoxEmail"
                                    ErrorMessage="El Email del Proveedor es requerido." Display="None"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="EmailRequiredRegularExpressionValidator" runat="server" ControlToValidate="TextBoxEmail"
                                    ValidationExpression="^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$"
                                    ErrorMessage="El formato del email es incorrecto." Display="None"></asp:RegularExpressionValidator>
                                </div>
                        </div>
                    </div>
                    <div class="form-group p-md-2">
                        <div class="col-md-4">
                            <div class="input-group">
                                <label for="<%= TextBoxSitioWebUrl.ClientID %>" class="col-md-4 control-label">Sitio Web:</label>
                                <asp:TextBox ID="TextBoxSitioWebUrl" runat="server" CssClass="form-control" PlaceHolder="www.dominio.com"></asp:TextBox>                    
                                <asp:RequiredFieldValidator ID="WebSiteRequiredField" runat="server" ErrorMessage="El Sitio Web del Proveedor es requerido." ControlToValidate="TextBoxSitioWebUrl" Display="None"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="WebSiteRegularExpressionValidator" runat="server" ControlToValidate="TextBoxSitioWebUrl"
                                    ValidationExpression="^(http|http(s)?://)?([\w-]+\.)+[\w-]+[.com|.in|.org]+(\[\?%&=]*)?" 
                                    ErrorMessage="El formato de la URL es incorrecto." Display="None"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                    <div class="form-group p-md-2">
                        <div class="col-md-4">
                            <div class="input-group">
                                <label for="<%= TextBoxTelefono.ClientID %>" class="col-md-4 control-label">Teléfono:</label>
                                <asp:TextBox ID="TextBoxTelefono" runat="server" CssClass="form-control" PlaceHolder="1122223333"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="TelefonoRequiredField" runat="server" ErrorMessage="El Teléfono del Proveedor es requerido." ControlToValidate="TextBoxTelefono" Display="None"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBoxTelefono"
                                    ValidationExpression="^(?:\+54)?\s?\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$" 
                                    ErrorMessage="El formato del número es incorrecto o contiene letras." Display="None"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                    <div class="form-group p-md-2">
                        <div class="col-md-4">
                            <div class="input-group">
                                <label for="<%= TextBoxDate.ClientID %>" class="col-md-4 control-label">Fecha de Nacimiento:</label>
                                <asp:TextBox runat="server" id="TextBoxDate" PlaceHolder="dd/mm/yyyy"/>
                                <asp:RangeValidator runat="server" ID="rngDate" ControlToValidate="TextBoxDate" 
                                    type="Date" 
                                    minimumvalue="01-01-1930" 
                                    maximumvalue="31-12-2006" 
                                    ErrorMessage="Por favor ingrese una fecha entre 1930 y 2006, con formato: dd/mm/yyyy, dd-mm-yyyy o dd.mm.yyyy" Display="None"/>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"  ControlToValidate="TextBoxDate" 
                                    ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[13-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"                         
                                    ErrorMessage="El formato de fecha es incorrecto o contiene letras" Display="None"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                    <div class="form-group p-md-2">
                        <div class="col-md-4">
                            <div class="input-group">
                                <label for="<%= DropDownListLocalidad.ClientID %>" class="col-md-4 control-label">Localidad:</label>
                                <asp:DropDownList ID="DropDownListLocalidad" runat="server" CssClass="form-control"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="LocalidadRequiredFieldValidator" runat="server" ControlToValidate="DropDownListLocalidad" 
                                    ErrorMessage="Debe seleccionar una localidad." Display="None"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="form-group p-md-2">
                        <div class="col-md-4">
                            <div class="input-group">
                                <label for="<%=CheckEsActivo.ClientID %>" class="col-md-4 control-label">Activo:</label>
                                <asp:CheckBox ID="CheckEsActivo" runat="server" CssClass="form-control" Checked="true"/>
                            </div>
                        </div>
                    </div>

                    <%--Muestra el listado de Validaciones--%>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
                    
                    <%-- Botones de Acción --%>
                    <div class="mt-3">
                        <asp:Button ID="ButtonGuardar" runat="server" Text="Guardar" OnClick="ButtonGuardar_Click" CssClass="btn btn-primary" CausesValidation="true"/>
                    </div>
                </fieldset>
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