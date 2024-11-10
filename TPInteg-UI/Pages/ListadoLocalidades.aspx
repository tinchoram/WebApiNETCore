<%@ Page Title="Listado de Localidades" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoLocalidades.aspx.cs" Inherits="TPInteg_UI.Pages.ListadoLocalidades" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header">
            <h2>Listado de Localidades</h2>
        </div>
        <div class="card-body">
            <asp:UpdatePanel ID="UpdatePanelLocalidades" runat="server">
                <ContentTemplate>
                    <%-- Panel de Error --%>
                    <asp:Panel ID="PanelErrorLocalidades" runat="server" Visible="false" CssClass="alert alert-danger">
                        <div class="d-flex justify-content-between align-items-center">
                            <asp:Label ID="LabelErrorLocalidades" runat="server"></asp:Label>
                            <asp:Button ID="ButtonReintentarLocalidades" runat="server" Text="Reintentar" 
                                OnClick="ButtonReintentarLocalidades_Click" CssClass="btn btn-outline-danger btn-sm" />
                        </div>
                    </asp:Panel>

                    <%-- Panel Sin Datos --%>
                    <asp:Panel ID="NoDataPanelLocalidades" runat="server" Visible="false" CssClass="alert alert-info">
                        <i class="fas fa-info-circle me-2"></i>No hay localidades disponibles.
                    </asp:Panel>

                    <%-- GridView de Localidades --%>
                    <div class="table-responsive">
                        <asp:GridView ID="GridViewLocalidades" runat="server" 
                            CssClass="table table-striped table-hover" 
                            AutoGenerateColumns="false"
                            EmptyDataText="No se encontraron localidades."
                            EmptyDataCssClass="alert alert-info text-center"
                            OnRowCommand="GridViewLocalidades_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="ID" />
                                <asp:TemplateField HeaderText="Nombre">
                                    <ItemTemplate>
                                        <%# string.IsNullOrEmpty(Eval("Nombre").ToString()) ? "-" : Eval("Nombre") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Código Postal">
                                    <ItemTemplate>
                                        <%# Eval("CodigoPostal") != null ? Eval("CodigoPostal").ToString() : "-" %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <div class="btn-group">
                                            <asp:LinkButton ID="btnEditar" runat="server" CommandName="EditarLocalidad" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-primary btn-sm">
                                                <i class="fas fa-edit"></i> Editar
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnEliminar" runat="server" CommandName="EliminarLocalidad" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-danger btn-sm ms-1" OnClientClick="return confirm('¿Está seguro que desea eliminar esta localidad?');">
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

            <%-- Indicador de Progreso --%>
            <asp:UpdateProgress ID="UpdateProgressLocalidades" runat="server" DisplayAfter="0">
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