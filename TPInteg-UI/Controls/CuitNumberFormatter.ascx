<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CuitNumberFormatter.ascx.cs" Inherits="TPInteg_UI.Controls.CuitNumberFormatter" %>
<script type="text/javascript"> 
    function formatNumber(input)
    {
        // Eliminar caracteres no numéricos
        let value = input.value.replace(/\D/g, '');
        //Da el formato
        if (value.length == 11) {
            value = value.substring(0, 2) + '-' + value.substring(2, 4) + '.' + value.substring(4, 7) + '.' + value.substring(7, 10) + '-' + value.substring(10,11);
        }
        input.value = value;
    } 
</script>

<label for="<%= txtCuitNumber.ClientID %>">CUIT:</label>
<asp:TextBox ID="txtCuitNumber" runat="server" onkeyup="formatNumber(this)"></asp:TextBox>
<asp:Label ID="lblFormattedNumber" runat="server"></asp:Label>
<asp:RequiredFieldValidator ID="CuitRequiredFieldValidator" runat="server" ErrorMessage="El CUIT es requerido." ControlToValidate="txtCuitNumber" Display="None"></asp:RequiredFieldValidator>