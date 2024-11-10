<%@ Page Title="Administración de Localidades" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ABMLocalidades.aspx.cs" Inherits="TPInteg_UI.Pages.ABMLocalidades" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header">
            <div class="d-flex justify-content-between align-items-center">
                <h2>Administración de Localidades</h2>
                <asp:Button ID="ButtonNuevo" runat="server" Text="Nueva Localidad" 
                    CssClass="btn btn-primary" OnClick="ButtonNuevo_Click" />
            </div>
        </div>
        <div class="card-body">
            <asp:UpdatePanel ID="UpdatePanelLocalidad" runat="server">
                <ContentTemplate>
                    <%-- Mensajes de Feedback --%>
                    <asp:Panel ID="PanelError" runat="server" Visible="false" CssClass="alert alert-danger mb-4">
                        <asp:Label ID="LabelError" runat="server"></asp:Label>
                    </asp:Panel>

                    <asp:Panel ID="PanelSuccess" runat="server" Visible="false" CssClass="alert alert-success mb-4">
                        <asp:Label ID="LabelSuccess" runat="server"></asp:Label>
                    </asp:Panel>

                    <%-- Panel de Formulario --%>
                    <asp:Panel ID="PanelForm" runat="server" Visible="false" CssClass="card mb-4">
                        <div class="card-header">
                            <h4><asp:Label ID="LabelFormTitle" runat="server"></asp:Label></h4>
                        </div>
                        <div class="card-body">
                            <asp:HiddenField ID="HiddenLocalidadId" runat="server" />
                            
                            <div class="row">
                                <div class="col-md-6 mb-3">
                                    <label for="<%= TextBoxNombre.ClientID %>" class="form-label">Nombre <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBoxNombre" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorNombre" runat="server"
                                        ControlToValidate="TextBoxNombre"
                                        Display="Dynamic"
                                        CssClass="text-danger"
                                        ValidationGroup="LocalidadForm"
                                        ErrorMessage="El nombre es requerido.">
                                    </asp:RequiredFieldValidator>
                                </div>

                                <div class="col-md-6 mb-3">
                                    <label for="<%= TextBoxCodigoPostal.ClientID %>" class="form-label">Código Postal <span class="text-danger">*</span></label>
                                    <asp:TextBox ID="TextBoxCodigoPostal" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorCP" runat="server"
                                        ControlToValidate="TextBoxCodigoPostal"
                                        Display="Dynamic"
                                        CssClass="text-danger"
                                        ValidationGroup="LocalidadForm"
                                        ErrorMessage="El código postal es requerido.">
                                    </asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="RangeValidatorCP" runat="server"
                                        ControlToValidate="TextBoxCodigoPostal"
                                        Type="Integer"
                                        MinimumValue="0"
                                        MaximumValue="99999"
                                        Display="Dynamic"
                                        CssClass="text-danger"
                                        ValidationGroup="LocalidadForm"
                                        ErrorMessage="El código postal debe ser un número entre 0 y 99999.">
                                    </asp:RangeValidator>
                                </div>
                            </div>

                            <div class="row mb-3">
                                <div class="col-md-6">
                                    <label for="<%= DropDownListEstado.ClientID %>" class="form-label">Estado</label>
                                    <asp:DropDownList ID="DropDownListEstado" runat="server" CssClass="form-select">
                                        <asp:ListItem Text="Activo" Value="1" />
                                        <asp:ListItem Text="Inactivo" Value="0" />
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="mt-3">
                                <asp:Button ID="ButtonGuardar" runat="server" Text="Guardar" 
                                    CssClass="btn btn-primary me-2" 
                                    OnClick="ButtonGuardar_Click"
                                    ValidationGroup="LocalidadForm" />
                                <asp:Button ID="ButtonCancelar" runat="server" Text="Cancelar" 
                                    CssClass="btn btn-secondary" 
                                    OnClick="ButtonCancelar_Click"
                                    CausesValidation="false" />
                            </div>
                        </div>
                    </asp:Panel>

                    <%-- Listado de Localidades --%>
                    <asp:GridView ID="GridViewLocalidades" runat="server" 
                        CssClass="table table-striped table-hover" 
                        AutoGenerateColumns="false"
                        DataKeyNames="id"
                        OnRowCommand="GridViewLocalidades_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="ID" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                            <asp:BoundField DataField="codigoPostal" HeaderText="Código Postal" />
                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <span class='<%# Convert.ToDateTime(Eval("fechaBaja")) == null ? "badge bg-success" : "badge bg-danger" %>'>
                                        <%# Convert.ToDateTime(Eval("fechaBaja")) == null ? "Activo" : "Inactivo" %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:LinkButton ID="ButtonEdit" runat="server" 
                                        CommandName="EditarLocalidad" 
                                        CommandArgument='<%# Eval("id") %>'
                                        CssClass="btn btn-sm btn-primary me-2">
                                        <i class="fas fa-edit"></i> Editar
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="ButtonDelete" runat="server" 
                                        CommandName="EliminarLocalidad" 
                                        CommandArgument='<%# Eval("id") %>'
                                        CssClass="btn btn-sm btn-danger"
                                        OnClientClick="return confirm('¿Está seguro que desea eliminar esta localidad?');">
                                        <i class="fas fa-trash"></i> Eliminar
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="alert alert-info">
                                No se encontraron localidades.
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>

            <%-- Indicador de Progreso --%>
            <asp:UpdateProgress ID="UpdateProgressLocalidad" runat="server" DisplayAfter="0">
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