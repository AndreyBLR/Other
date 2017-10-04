<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegistrationForm.ascx.cs" Inherits="MyBlog.Forms.RegistrationForm" %>
<table>
    <tr>
        <td>
            <asp:TextBox ID="login" runat="server" width="200"></asp:TextBox>
        </td>
        <td></td>
    </tr>
    <tr>
        <td></td>
    </tr>
    <tr>
        <td>
            <asp:TextBox ID="password" runat="server" width="200"></asp:TextBox>
        </td>
        <td></td>
    </tr>
    <tr>
        <td>    
            <asp:Literal runat="server" ID="regCompleteText"/>
        </td>
    </tr>
    <tr>
        <td>
            <asp:textbox id="e_mail" runat="server" width="200"></asp:textbox>
        </td>
        <td></td>
    </tr>
    <tr>
        <td></td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="registrationBtn" runat="server" Text="Button" onclick="RegistrationBtnClick" />
        </td>
    </tr>
</table>
