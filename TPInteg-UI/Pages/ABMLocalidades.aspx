<%@ Page Title="ABM de Localidades" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ABMLocalidades.aspx.cs" Inherits="TPInteg_UI.ABMLocalidades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card">
        <div class="card-header">
            <h2>ABM de Localidades</h2>
        </div>
        <div class="card-body">
            <!-- Formulario para ABM de localidades -->
            <asp:UpdatePanel ID="UpdatePanelLocalidades" runat="server">
                <ContentTemplate>
                    <asp:Label ID="LabelMessage" runat="server" CssClass="alert alert-success" Visible="false"></asp:Label>

                    <asp:TextBox ID="txtLocalidadNombre" runat="server" CssClass="form-control" Placeholder="Nombre de la localidad"></asp:TextBox>
                    <asp:Button ID="btnAgregarLocalidad" runat="server" Text="Agregar" CssClass="btn btn-primary mt-2" OnClick="btnAgregarLocalidad_Click" />
                    <asp:Button ID="btnActualizarLocalidad" runat="server" Text="Actualizar" CssClass="btn btn-secondary mt-2" OnClick="btnActualizarLocalidad_Click" Visible="false" />

                    <asp:GridView ID="GridViewLocalidades" runat="server" CssClass="table table-striped table-hover mt-3" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="ID" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="btn btn-sm btn-info" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' />
                                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-sm btn-danger" CommandName="Eliminar" CommandArgument='<%# Eval("Id") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

