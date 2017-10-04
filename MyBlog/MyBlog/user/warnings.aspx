<%@ Page Title="" Language="C#" MasterPageFile="~/MyBlog.Master" AutoEventWireup="true" CodeBehind="warnings.aspx.cs" Inherits="MyBlog.user.warnings" EnableViewState="false"%>
<asp:Content ID="loginStatus" ContentPlaceHolderID="loginStatusPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="header" ContentPlaceHolderID="headerPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="description" ContentPlaceHolderID="descHeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="contentPlaceHolder" runat="server">
<asp:MultiView ID="multiView" runat="server" >
    <asp:View ID="rptWarningsView" runat="server">
        <table width="100%">
            <tr>
                <td>
                    <asp:Repeater ID="rptWarnings" runat="server">
                        <ItemTemplate>
                            <table width="100%">
                                <tr>
                                    <td align="left" style="width:70%">
                                        <%#Eval("WarningText") %>
                                    </td>
                                    <td>
                                        <%#Eval("Date") %>
                                        <br />
                                        <a href="warnings.aspx?warningId=<%#Eval("WarningId") %>&action=delete">Delete</a>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="formView" runat="server">
        <table width="100%">
            <tr>
                <td align="center">
                    <asp:TextBox ID="warningText" runat="server" TextMode="MultiLine" MaxLength="490" Width="270px" Rows="4"/>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="addBtn" runat="server" Text="Add warning" 
                        onclick="AddBtnClick"/>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="viewUserBanned" runat="server">
        User is banned;
    </asp:View>
</asp:MultiView>

    
</asp:Content>
