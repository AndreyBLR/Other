<%@ Page Title="" Language="C#" MasterPageFile="~/MyBlog.Master" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="MyBlog.search" %>
<asp:Content ID="header" ContentPlaceHolderID="headerPlaceHolder" runat="server">
Search
</asp:Content>
<asp:Content ID="description" ContentPlaceHolderID="descHeaderPlaceHolder" runat="server">
Search result
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="contentPlaceHolder" runat="server">
        <asp:ListView ID="lstSearchResult" runat="server" 
        onitemcreated="LstSearchResultItemCreated">
            <ItemTemplate>
            <div class="item">
		        <div class="title"> 
                    <a href='<%# Eval("PostLink") %>'> <%# Eval("PostTitle") %> <a/>
                </div>
		        <div class="metadata"><%# Eval("PostDate") %></div>
		        <div class="body">
		    	    <p><%# Eval("PostExcerpt") %></p>
		        </div>
                <div>
                    <table width="100%">
                        <tr>
                            <td align="left">
                                Category: <a href='journal.aspx?category=<%# Eval("Categories.CategoryName") %>'> <%# Eval("Categories.CategoryName")%> <a/>
                            </td>
                            <td align="right">
                                Comments: <%# Eval("CommentCount") %>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="divider"><span></span></div>
        </ItemTemplate>
        </asp:ListView>
    
    <table width="100%">
        <tr>
            <td align="center">
                <asp:DataPager ID="pager" runat="server" PagedControlID="lstSearchResult" PageSize="50">
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
