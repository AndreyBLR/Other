<%@ Page Title="" Language="C#" MasterPageFile="~/MyBlog.Master" AutoEventWireup="true" CodeBehind="gallery.aspx.cs" Inherits="MyBlog.admin.gallery" %>
<%@ Import Namespace="MBPresentation.ServiceReference" %>

<asp:Content ID="header" ContentPlaceHolderID="headerPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="description" ContentPlaceHolderID="descHeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <asp:MultiView ID="multiView" runat="server">
        <asp:View ID="imagesView" runat="server">
            <%=MakeHtmlTable() %>
            <br />
            <br />
            <hr width="80%" color="#A5B586" />
            <br />
            <div class="navigation" align="center">
                <a href="../admin/gallery.aspx?action=add">Add image</a>
            </div>
        </asp:View>
        <asp:View ID="formView" runat="server">
            <asp:PlaceHolder ID="plLoadForm" runat="server">
                <table cellpadding="20px" width="100%">        
                    <tr align="center">
                        <td>
                            <asp:FileUpload ID="fileUpload" runat="server" />
                            <asp:Button ID="btnLoad" runat="server" Text="Load" OnClick="LoadBtnClick" Height="20px"/>
                        </td>
                    </tr>
                </table>
            </asp:PlaceHolder>
            <table width="55%" style="position:relative; left:120px" >  
                <tr>
                    <td align="center">
                        <br />
                        Description:
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:TextBox ID="txbDesc" runat="server" TextMode="MultiLine" Wrap="true" Rows="5" Width="278px" /> 
                    </td>
                </tr>  
                <tr>
                    <td align="center">
                        <asp:DropDownList ID="ddlCategoryList" runat="server" /> 
                    </td>
                </tr>    
                <tr>
                    <td align="center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" onclick="BtnSaveClick"/>
                    </td>
                </tr> 
            </table>          
        </asp:View>
    </asp:MultiView>
</asp:Content>

