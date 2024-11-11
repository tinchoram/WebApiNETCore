<%@ Page Title="Editar Proveedor" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarProveedor.aspx.cs" Inherits="TPInteg_UI.Pages.EditarProveedor" Async="true" %>
<%@ Register Src="~/Controls/CuitNumberFormatter.ascx" TagPrefix="uc" TagName="NumberFormatter" %>

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
                    <label for="<%= TextBoxNombre.ClientID %>">Nombre:</label>
                    <asp:TextBox ID="TextBoxNombre" runat="server" CssClass="form-control"></asp:TextBox>                    
                    <asp:RequiredFieldValidator ID="NombreRequiredField" runat="server" ControlToValidate="TextBoxNombre"
                        ErrorMessage="El Nombre del Proveedor es requerido." Display="None"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label for="<%= TextBoxApellido.ClientID %>">Apellido:</label>
                    <asp:TextBox ID="TextBoxApellido" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ApellidoRequiredField" runat="server" ControlToValidate="TextBoxApellido"
                        ErrorMessage="El Apellido del Proveedor es requerido." Display="None"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label for="<%= TextBoxNombreComercial.ClientID %>">NombreComercial:</label>
                    <asp:TextBox ID="TextBoxNombreComercial" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="NombreComercialRequiredField" runat="server" ControlToValidate="TextBoxNombreComercial"
                        ErrorMessage="El Nombre Comercial del Proveedor es requerido." Display="None"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label for="<%= TextBoxDireccion.ClientID %>">Dirección:</label>
                    <asp:TextBox ID="TextBoxDireccion" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="DireccionRequiredField" runat="server" ControlToValidate="TextBoxDireccion"
                        ErrorMessage="La Dirección del Proveedor es requerida." Display="None"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <uc:NumberFormatter ID="ucNumberFormatter" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label for="<%= TextBoxEmail.ClientID %>">Email:</label>
                    <asp:TextBox ID="TextBoxEmail" runat="server" CssClass="form-control" PlaceHolder="email@dominio.com"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="EmailRequiredField" runat="server" ControlToValidate="TextBoxEmail"
                        ErrorMessage="El Email del Proveedor es requerido." Display="None"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="EmailRequiredRegularExpressionValidator" runat="server" ControlToValidate="TextBoxEmail"
                        ValidationExpression="^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$"
                        ErrorMessage="El formato del email es incorrecto." Display="None"></asp:RegularExpressionValidator>
                </div>
                <div class="form-group">
                    <label for="<%= TextBoxSitioWebUrl.ClientID %>">Sitio Web:</label>
                    <asp:TextBox ID="TextBoxSitioWebUrl" runat="server" CssClass="form-control" PlaceHolder="www.dominio.com"></asp:TextBox>                    
                    <asp:RequiredFieldValidator ID="WebSiteRequiredField" runat="server" ErrorMessage="El Sitio Web del Proveedor es requerido." ControlToValidate="TextBoxSitioWebUrl" Display="None"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="WebSiteRegularExpressionValidator" runat="server" ControlToValidate="TextBoxSitioWebUrl"
                        ValidationExpression="^(http|http(s)?://)?([\w-]+\.)+[\w-]+[.com|.in|.org]+(\[\?%&=]*)?" 
                        ErrorMessage="El formato de la URL es incorrecto." Display="None"></asp:RegularExpressionValidator>
                </div>
                <div class="form-group">
                    <label for="<%= TextBoxTelefono.ClientID %>">Teléfono:</label>
                    <asp:TextBox ID="TextBoxTelefono" runat="server" CssClass="form-control" PlaceHolder="1122223333"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="TelefonoRequiredField" runat="server" ErrorMessage="El Teléfono del Proveedor es requerido." ControlToValidate="TextBoxTelefono" Display="None"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBoxTelefono"
                        ValidationExpression="^(?:\+54)?\s?\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$" 
                        ErrorMessage="El formato del número es incorrecto o contiene letras." Display="None"></asp:RegularExpressionValidator>
                </div>
                <div class="form-group">
                    <label for="<%= TextBoxDate.ClientID %>">Fecha de Nacimiento:</label>
                    <asp:TextBox runat="server" id="TextBoxDate" PlaceHolder="dd/mm/yyyy" />
                    <asp:RangeValidator runat="server" ID="rngDate" ControlToValidate="TextBoxDate" 
                        type="Date" 
                        minimumvalue="01-01-1930" 
                        maximumvalue="31-12-2006" 
                        ErrorMessage="Por favor ingrese una fecha entre 1930 y 2006, con formato: dd/mm/yyyy, dd-mm-yyyy o dd.mm.yyyy"  Display="None"/>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"  ControlToValidate="TextBoxDate" 
                        ValidationExpression="^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[13-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"                         
                        ErrorMessage="El formato de fecha es incorrecto o contiene letras" Display="None"></asp:RegularExpressionValidator>
                </div>
                <div class="form-group">
                    <label for="<%= DropDownListLocalidad.ClientID %>">Localidad:</label>
                    <asp:DropDownList ID="DropDownListLocalidad" runat="server" CssClass="form-control"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="LocalidadRequiredFieldValidator" runat="server" ControlToValidate="DropDownListLocalidad" 
                        ErrorMessage="Debe seleccionar una localidad." Display="None"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <label for="<%=CheckEsActivo.ClientID %>">Activo:</label>
                    <asp:CheckBox ID="CheckEsActivo" runat="server" CssClass="form-control" Checked="true"/>
                </div>
            <asp:HiddenField ID="HiddenFieldProveedorId" runat="server" />
            <%--Muestra el listado de Validaciones--%>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
            <%-- Botones de Acción --%>
            <asp:Button ID="ButtonActualizar" runat="server" CssClass="btn btn-primary" Text="Actualizar" OnClick="ButtonActualizar_Click" />
            <asp:Button ID="ButtonCancelar" runat="server" CssClass="btn btn-secondary" Text="Cancelar" OnClick="ButtonCancelar_Click" />
        </div>
    </div>
</asp:Content>
