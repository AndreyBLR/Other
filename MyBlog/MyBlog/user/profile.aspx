<%@ Page Title="" Language="C#" MasterPageFile="~/MyBlog.Master" AutoEventWireup="true" CodeBehind="profile.aspx.cs" Inherits="MyBlog.user.profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headerPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="descHeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="categiriesPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <asp:MultiView ID="multiView" runat="server">
        <asp:View ID="viewUserProfile" runat="server">
        <asp:ListView ID="lstProfile" runat="server">
        <ItemTemplate>
        
            <table width="85%">
                <tr>
                    <td>NickName</td>
                    <td> 
                        <%# Eval("aspnet_Users.UserName") %>
                    </td>
                </tr>
                <tr>
                    <td>Role</td>
                    <td>
                        <%# Eval("aspnet_Users.aspnet_Roles[0].RoleName")%>
                    </td>
                </tr>
                <tr>
                    <td>IsBanned</td>
                    <td>
                        <%# Eval("IsBanned") %>
                    </td>
                </tr>
                <tr>
                    <td>Gender</td>
                    <td>
                        <%# Eval("Gender") %>
                    </td>
                </tr>
                <tr>
                    <td>Age</td>
                    <td>
                        <%# Eval("Age") %>
                    </td>
                </tr>
                <tr>
                    <td>Location</td>
                    <td>
                        <%# Eval("Location") %>
                    </td>
                </tr>
                <tr>
                    <td>Comments</td>
                    <td>
                        <%# Eval("Comments") %>
                    </td>
                </tr>
                <tr>
                    <td>Warnings</td>
                    <td>
                        <%# Eval("Warnings") %>
                    </td>
                </tr>
                <tr>
                    <td>Authograph&nbsp&nbsp&nbsp</td>
                    <td>
                        <%# Eval("Autograph") %>
                    </td>
                </tr>
                <tr>
                    <td>Avatar</td>
                    <td>
                        <img src='../Handlers/image.ashx?id=<%# Eval("Avatar") %>&mode=full' />
                    </td>
                </tr>
            </table>
            </ItemTemplate>
        </asp:ListView>
        </asp:View>
        <asp:View ID="viewChangeUserProfile" runat="server">
        <asp:ListView ID="lstUserProfile" runat="server">
            <ItemTemplate>
            <table width="85%">
                <tr>
                    <td>NickName</td>
                    <td> 
                        <%# Eval("aspnet_Users.UserName") %>
                    </td>
                </tr>
                <tr>
                    <td>Role</td>
                    <td>
                        <%# Eval("aspnet_Users.aspnet_Roles[0].RoleName")%>
                    </td>
                </tr>
                <tr>
                    <td>IsBanned</td>
                    <td>
                        <%# Eval("IsBanned") %>
                    </td>
                </tr>
                <tr>
                    <td>Gender</td>
                    <td>
                        <asp:TextBox runat="server" ID="gender" Text=<%# Eval("Gender") %> > </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Age</td>
                    <td>
                        <asp:TextBox runat="server" ID="age" Text=<%# Eval("Age") %>> </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Location</td>
                    <td>
                        <asp:TextBox runat="server" ID="location" Text=<%# Eval("Location") %>></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Comments</td>
                    <td>
                        <%# Eval("Comments") %>
                    </td>
                </tr>
                <tr>
                    <td>Warnings</td>
                    <td>
                        <%# Eval("Warnings") %>
                    </td>
                </tr>
                <tr>
                    <td>Authograph&nbsp&nbsp&nbsp</td>
                    <td>
                        <asp:TextBox runat="server" ID="autograph" Columns="75" Text=<%# Eval("Autograph") %>></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Avatar</td>
                    <td>
                        <img src='../Handlers/image.ashx?id=<%# Eval("Avatar") %>&mode=full' />
                        <table>
                            <tr>
                                <td>
                                    <asp:FileUpload ID="fileUpload" runat="server" />
                                </td>
                                <td>
                                    <asp:Button ID="uploadBtn" runat="server" Text="Upload" onclick="uploadBtnClick" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="Button3" runat="server" Text="Save" OnClick="SaveBtnClick" />
                    </td>
                </tr>
            </table>
            </ItemTemplate>
        </asp:ListView>
        </asp:View>
    </asp:MultiView>
</asp:Content>
