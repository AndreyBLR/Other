<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddArticleForm.ascx.cs" Inherits="MyBlog.Controls.AddArticleForm" %>
<table>
    <tr>
        <td> <asp:TextBox ID="title" runat="server" Width="500px"></asp:TextBox> </td>
        <td></td>
    </tr> 
    <tr>
        <td><asp:TextBox ID="excerpt" runat="server" TextMode="MultiLine" Wrap="true" Rows="7" Width="600px"></asp:TextBox></td>
        <td></td>
    </tr>
    <tr>
        <td><asp:TextBox ID="text" runat="server" TextMode="MultiLine" Wrap="true" Rows="15" Width="600px"></asp:TextBox></td>
        <td></td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="AddBtn" runat="server" Text="Button" onclick="AddBtnClick"/>
        </td>
    </tr>
</table>
