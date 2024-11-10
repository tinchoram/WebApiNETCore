<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchControl.ascx.cs" Inherits="TPInteg_UI.Controls.SearchControl" %>
<div class="input-group mb-3">
    <asp:TextBox ID="searchInput" CssClass="form-control" placeholder="Buscar..." runat="server" />
    <asp:Button ID="searchButton" CssClass="btn btn-primary" Text="Buscar" OnClick="SearchButton_Click" runat="server" />
</div>
