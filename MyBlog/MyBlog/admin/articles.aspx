<%@ Page Title="" Language="C#" MasterPageFile="~/MyBlog.Master" AutoEventWireup="true" CodeBehind="articles.aspx.cs" Inherits="MyBlog.Images.articles"  %>
<%@ Import Namespace="MyBlog" %>

<asp:Content ID="header" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <asp:Label ID="lblHeader" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="description" ContentPlaceHolderID="descHeaderPlaceHolder" runat="server">
    <asp:Label ID="lblDesc" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <asp:MultiView runat="server" ID="multiView">
        <asp:View runat="server" ID="articlesView">
            <asp:ListView runat="server" ID="lstArticles">
                <ItemTemplate>
                    <table width="100%">
                        <tr>
                            <td width="80%">
                                <a href='<%# Eval("PostLink") %>' > <%# Eval("PostTitle") %> </a>
                                <br />
                                Category: <%# Eval("Categories.CategoryName") %>
                            </td>

                            <td>
                                <a href='articles.aspx?action=change&postId=<%# Eval("PostId") %>'> Change </a>
                                <br />
                                <a href='articles.aspx?action=delete&postId=<%# Eval("PostId") %>'> Delete </a>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:ListView>
            <table width="100%">
                <tr>
                    <td align="center">
                        <asp:DataPager ID="pager" runat="server" PagedControlID="lstArticles" PageSize="12">
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
            <br />
            <br />
            <hr width="80%" color="#A5B586" />
            <br />
            <div class="navigation" align="center">
                <a href="../admin/articles.aspx?action=add">Add article</a>
            </div>
        </asp:View>

        <asp:View runat="server" ID="forms" >
            <%
                if(this.IsSet("postId"))
                {
                    ReadImages(Guid.Parse(Request.Params["postId"]));
                }
                else
                {
                    ImageList = null;
                }
                
            %>
            <%=MakeHtmlTable() %>
            <asp:PlaceHolder ID="plLoadImageForm" runat="server">
                <table cellpadding="20px" width="100%">        
                    <tr>
                        <td width="60%" align="right">
                            <asp:FileUpload ID="fileUpload" runat="server" />
                        </td>
                        <td align="left">
                            <asp:Button ID="loadBtn" runat="server" Text="Load" OnClick="LoadBtnClick" 
                                Height="20px"/>
                        </td>
                    </tr>
                </table>
            </asp:PlaceHolder>
            <hr width="80%" style="color:#A5B586" />
            <table width="100%">
                <tr align="center">
                    <td> <asp:TextBox ID="title" runat="server" Width="500px" style="text-align:justify"></asp:TextBox> </td>
                    <td>
                        <asp:RequiredFieldValidator ID="titleValidator" runat="server" Display="None" ErrorMessage="no title" ControlToValidate="title" />
                    </td>
                </tr> 
                <tr align="center">
                    <td><asp:TextBox ID="excerpt" runat="server" TextMode="MultiLine" Wrap="true" Rows="7" Width="550px" style="text-align:justify"></asp:TextBox></td>
                    <td>
                        <asp:RequiredFieldValidator ID="excerptValidator" runat="server" Display="None" ErrorMessage="no excerpt" ControlToValidate="excerpt" />
                    </td>
                </tr>
                <tr align="center">
                    <td><asp:TextBox ID="text" runat="server" TextMode="MultiLine" Wrap="true" Rows="25" Width="550px" style="text-align:justify"></asp:TextBox></td>
                    <td>
                        <asp:RequiredFieldValidator ID="textValidator" runat="server" Display="None" ErrorMessage="no text" ControlToValidate="text" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%">
                            <tr>
                                <td width="50%" align="center">
                                    <asp:DropDownList ID="ddlCategory" runat="server" />
                                </td>
                                <td align="center">
                                    <asp:CheckBox runat="server" ID="comment" Checked="true" Text="Comment Status"/>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center">  
                        <asp:Button ID="AddBtn" runat="server" Text="Add" onclick="AddBtnClick"/>
                        <asp:Button ID="SaveBtn" runat="server" Text="Save" onclick="SaveBtnClick"/>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="center">
                        
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode=BulletList>
                            
                        </asp:ValidationSummary>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
</asp:Content>
