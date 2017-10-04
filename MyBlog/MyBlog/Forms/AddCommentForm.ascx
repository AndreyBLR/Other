<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddCommentForm.ascx.cs" Inherits="MyBlog.Forms.AddCommentForm" %>
<table>
    <tr>
        <td>
            <asp:TextBox ID="text" runat="server" Width="400px" TextMode="MultiLine" Rows="7"></asp:TextBox>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="addBtn" runat="server" Text="Button" OnClick="AddBtnClick"/>
        </td>
        <td>
        </td>
        
    </tr>
</table>
