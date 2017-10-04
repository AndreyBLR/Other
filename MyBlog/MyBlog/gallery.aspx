<%@ Page Title="" Language="C#" MasterPageFile="~/MyBlog.Master" AutoEventWireup="true" CodeBehind="gallery.aspx.cs" Inherits="MyBlog.gallery" EnableViewState="false"%>
<%@ Register TagPrefix="asc" TagName="TagsCloud" Src="~/Controls/TagsCloud.ascx" %>

<asp:Content ID="js" ContentPlaceHolderID="jsPlaceHolder" runat="server">
     <script type="text/javascript">
         hs.graphicsDir = '../highslide/graphics/';
         hs.align = 'center';
         hs.transitions = ['expand', 'crossfade'];
         hs.outlineType = 'glossy-dark';
         hs.wrapperClassName = 'dark';
         hs.fadeInOut = true;
         //hs.dimmingOpacity = 0.75;

         // Add the controlbar
         if (hs.addSlideshow) hs.addSlideshow({
             //slideshowGroup: 'group1',
             interval: 5000,
             repeat: false,
             useControls: true,
             fixedControls: 'fit',
             overlayOptions: {
                 opacity: .6,
                 position: 'bottom center',
                 hideOnMouseOut: true
             }
         });
    </script>
</asp:Content>
<asp:Content ID="leftBlock" ContentPlaceHolderID="leftBlockPlaceHolder" runat="server">
    <asc:TagsCloud ID="tagsCloud" runat="server"/>   
</asp:Content>

<asp:Content ID="header" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    The Gallery
</asp:Content>
<asp:Content ID="description" ContentPlaceHolderID="descHeaderPlaceHolder" runat="server">
    Gallery page
</asp:Content>

<asp:Content ID="mainContent" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <table width="100%" cellspacing="20px">
        <tr>
            <td>
                <%=MakeHtmlTable() %>
            </td>
        </tr>
    </table>
</asp:Content>
