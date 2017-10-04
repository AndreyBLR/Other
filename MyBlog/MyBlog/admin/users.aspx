<%@ Page Title="" Language="C#" MasterPageFile="~/MyBlog.Master" AutoEventWireup="true" CodeBehind="users.aspx.cs" Inherits="MyBlog.admin.users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="descHeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="categiriesPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentPlaceHolder" runat="server">  
    <asp:MultiView ID="multiView" runat="server">
        <asp:View ID="usersView" runat="server">
            <asp:ListView ID="lstUsers" runat="server" onitemcreated="LstUsersItemCreated">
                <ItemTemplate>
                    <table width="100%">
                        <tr>
                            <td width="20%">
                                <a href='../user/profile.aspx?userName=<%#Eval("UserName") %>'>
                                    <%#Eval("UserName") %>
                                </a> 
                            </td>
                            <td width="20%">
                                <%#Eval("Email") %>
                            </td>
                            <td width="20%">
                                <%#Eval("CreationDate") %>
                            </td>
                            <td width="20%">
                                <%#Eval("LastLoginDate") %>
                            </td>
                            <td width="20%">
                                <a href='../admin/users.aspx?id=<%#Eval("ProviderUserKey") %>&action=delete'>Delete</a>
                                <br />
                                <a href='../user/comments.aspx?userId=<%#Eval("ProviderUserKey") %>'>All comments</a>
                                <br />
                                <a href="../user/warnings.aspx?userId=<%#Eval("ProviderUserKey") %>">Warnings</a>&nbsp;
                                <a href="../user/warnings.aspx?userId=<%#Eval("ProviderUserKey")%>&action=add" >+</a>
                                <br />
                                <asp:HyperLink ID="lnkBanUser" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <br />
                </ItemTemplate>
            </asp:ListView>
        </asp:View>
        <asp:View ID="profileView" runat="server">
        </asp:View>
    </asp:MultiView> 
    <table width="100%">
        <tr>
            <td align="center">
                <asp:DataPager ID="pager" runat="server" PagedControlID="lstUsers" PageSize="30">
                    <Fields>
                        <asp:NextPreviousPagerField ButtonType="Link"
                            ShowFirstPageButton="true"
                            ShowNextPageButton="false"
                            ShowPreviousPageButton="false"
                            FirstPageText="<<" />
                      <asp:NumericPagerField ButtonCount="10" />
                      <asp:NextPreviousPagerField ButtonType="Link"
                            ShowLastPageButton="true"
                            ShowNextPageButton="false"
                            ShowPreviousPageButton="false"
                            LastPageText=">>" />
                    </Fields>
                </asp:DataPager>
            </td>
        </tr>
    </table> 
</asp:Content>