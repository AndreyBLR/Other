<%@ Page Title="" Language="C#" MasterPageFile="~/MyBlog.Master" AutoEventWireup="true" CodeBehind="categories.aspx.cs" Inherits="MyBlog.admin.Categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <asp:Literal ID="Literal1" runat="server" Text="Categories" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="descHeaderPlaceHolder" runat="server">
    <asp:Literal ID="Literal2" runat="server" Text="&#1059;&#1087;&#1088;&#1072;&#1074;&#1083;&#1077;&#1085;&#1080;&#1077; &#1082;&#1072;&#1090;&#1077;&#1075;&#1086;&#1088;&#1080;&#1103;&#1084;&#1080;" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="categiriesPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <asp:MultiView runat="server" ID="multiView">
        <asp:View runat="server" ID="categoriesView">
            <asp:ListView runat="server" ID="lstCategories">
                <ItemTemplate>
                    <table border="0px" style="padding: 5px;" width="100%">
                        <tr>
                            <td width="30%" align="left">
                                Name: <%# Eval("CategoryName") %>
                                <br />
                                Content Type: <%# Eval("ContentType") %>
                            </td>
                            <td align="left">
                                Description: <%# Eval("CategoryDesc") %>
                            </td>
                            <td align="right">
                                <a href='../admin/categories.aspx?action=change&categoryId=<%# Eval("CategoryId") %>' >Change</a>
                                <br />
                                <a href='../admin/categories.aspx?action=delete&categoryId=<%# Eval("CategoryId") %>'>Delete</a>
                            </td>
                        </tr>
                    </table>
                    <br />
                </ItemTemplate>
            </asp:ListView>
            <table width="100%">
                <tr>
                    <td align="center">
                        <asp:DataPager ID="pager" runat="server" PagedControlID="lstCategories" PageSize="40">
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
                <a href="../admin/categories.aspx?action=add">Add category</a>
            </div>
        </asp:View>

        <asp:View runat="server" ID="formView">
            <table width="100%">
                <tr>
                    <td align="center">
                        <asp:TextBox ID="txbName" runat="server" Width="375px"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:TextBox ID="txbDescription" runat="server" Height="56px" Width="373px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="center">
                        <table>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="rbtn1" runat="server" Text="post" GroupName="ContentType" Checked="true"/>   
                                </td>
                                <td>
                                    <asp:RadioButton ID="rbtn2" runat="server" Text="image" GroupName="ContentType"/>   
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="AddBtn" runat="server" Text="Add" onclick="AddBtnClick" />
                        <asp:Button ID="SaveBtn" runat="server" Text="Save" onclick="SaveBtnClick" />
                    </td>
                    <td></td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>    
</asp:Content>
