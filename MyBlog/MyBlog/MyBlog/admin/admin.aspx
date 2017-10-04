<%@ Page Title="" Language="C#" MasterPageFile="~/MyBlog.Master" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="MyBlog.admin.admin" %>
<asp:Content ID="dsa" ContentPlaceHolderID=jsPlaceHolder runat="server">

</asp:Content>
<asp:Content ID="header" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    Admin
</asp:Content>
<asp:Content ID="description" ContentPlaceHolderID="descHeaderPlaceHolder" runat="server">
    Admin page
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="categiriesPlaceHolder" runat="server">
    <asp:Literal runat="server" Text="<br />" />
    <asp:HyperLink runat="server" Text="Articles" NavigateUrl="~/admin/articles.aspx"/>
    <asp:Literal runat="server" Text="&nbsp&nbsp&nbsp&nbsp&nbsp" />
    <asp:HyperLink runat="server" Text="Categories" NavigateUrl="~/admin/categories.aspx"/>
    <asp:Literal runat="server" Text="&nbsp&nbsp&nbsp&nbsp&nbsp" />
    <asp:HyperLink runat="server" Text="Users" NavigateUrl="~/admin/users.aspx"/>
    <asp:Literal runat="server" Text="&nbsp&nbsp&nbsp&nbsp&nbsp" />
    <asp:HyperLink runat="server" Text="Gallery" NavigateUrl="~/admin/gallery.aspx"/>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="contentPlaceHolder" runat="server">
</asp:Content>
