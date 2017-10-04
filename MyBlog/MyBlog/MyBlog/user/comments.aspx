<%@ Page Title="" Language="C#" MasterPageFile="~/MyBlog.Master" AutoEventWireup="true" CodeBehind="comments.aspx.cs" Inherits="MyBlog.user.comments" EnableViewState="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="jsPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headerPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="descHeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="categiriesPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <asp:MultiView ID="multiView" runat = "server">
        <asp:View ID="commentsView" runat="server">
            <asp:ListView ID="lstComments" runat="server" onitemcreated="LstCommentsItemCreated">
                <ItemTemplate>
                    <table width="100%" border="0px" style="background:#F5F3E3">
                        <tr>
                            <td>
                                <a href='../user/profile.aspx?userName=<%#Eval("aspnet_Users.UserName") %>'>
                                    <%#Eval("aspnet_Users.UserName")%>
                                </a> 
                            </td>
                            <td align="right">Date: <%# Eval("CommentDate") %></td>
                        </tr>
                        <tr>
                            <td>
                                <img src='../Handlers/image.ashx?id=<%# Eval("aspnet_Users.Profiles.Avatar") %>&mode=full' alt="No image" />
                            </td>
                            <td rowspan="6" width="70%" align="justify" style="border:3px double Black">
                                <%# Eval("CommentText") %>
                            </td>
                       </tr>
                       <tr>
                            <td>Location: <%# Eval("aspnet_Users.Profiles.Location")%></td>
                       </tr>
                       <tr>
                            <td>Gender: <%# Eval("aspnet_Users.Profiles.Gender")%></td>
                       </tr>
                       <tr>
                            <td>Age: <%# Eval("aspnet_Users.Profiles.Age")%></td>
                       </tr>
                       <tr>
                            <td>Comments: <%# Eval("aspnet_Users.Profiles.Comments")%></td>
                       </tr>
                       <tr>
                            <td>Warnings: <%# Eval("aspnet_Users.Profiles.Warnings")%></td>
                       </tr>
                       <tr>
                            <td colspan="2">
                                <a href='../article.aspx?id=<%# Eval("CommentPostId") %>'>Article</a>
                                <table width="70%">
                                    <tr>
                                        <td>
                                            <asp:PlaceHolder ID="deleteCommentPh" runat="server" />
                                            <asp:PlaceHolder ID="changeCommentPh" runat="server" />      
                                        </td>
                                    </tr>
                                </table>
                           </td>
                       </tr>
                   </table>
                </ItemTemplate>
                <ItemSeparatorTemplate>
                    <br />
                    <br />
                    <br />
                </ItemSeparatorTemplate>
            </asp:ListView>
        </asp:View>
        <asp:View ID="changeCommentView" runat="server">
            <table>
                <tr>
                    <td>
                        <asp:TextBox ID="text" runat="server" TextMode="MultiLine" Height="91px" 
                            Width="270px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="changeBtn" runat="server" Text="Change" 
                            onclick="ChangeBtnClick" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="noCommentsView" runat="server">
        <table width="100">
            <tr align="center">
                <td>No comments</td>
            </tr>
        </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
