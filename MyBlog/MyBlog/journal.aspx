<%@ Page Title="" Language="C#" MasterPageFile="~/MyBlog.Master" AutoEventWireup="true" CodeBehind="journal.aspx.cs" Inherits="MyBlog.journal" EnableViewState="false"%>
<%@ Register TagPrefix="asc" TagName="TagsCloud" Src="~/Controls/TagsCloud.ascx" %>

<asp:Content ID="loginStatus" ContentPlaceHolderID="loginStatusPlaceHolder" runat="server">
<div style="position:absolute; left:1050px;"> 
    <asp:LoginView ID="loginView" runat="server">
        <AnonymousTemplate>
            <asp:Login ID="Login1" runat="server"  BackColor="#A5B586" BorderColor="#CCCC99" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="10pt" >
                <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
            </asp:Login>
            <asp:HyperLink ID="HyperLink1" runat="server" Text="Registration" NavigateUrl="~/registration.aspx" ForeColor="#A5B586" Font-Size="Small" Font-Bold="true"/>
        </AnonymousTemplate>
        <LoggedInTemplate>
            <h3 style="position:relative; top:100px; left:70px;">
                <a href="user/profile.aspx"> <asp:Label ID="lblUserName" runat="server" ForeColor="#F5F3A3" Font-Size="Large"/> </a>
                <br />
                <asp:LoginStatus ID="LoginStatus1" runat="server"/>
            </h3>
        </LoggedInTemplate>
    </asp:LoginView>
</div>
</asp:Content>
<asp:Content ID="leftBlock" ContentPlaceHolderID="leftBlockPlaceHolder" runat="server">
<asc:TagsCloud ID="tagsCloud" runat="server"/>   
</asp:Content>
<asp:Content ID="header" ContentPlaceHolderID="headerPlaceHolder" runat="server">
The Journal
</asp:Content>
<asp:Content ID="description" ContentPlaceHolderID="descHeaderPlaceHolder" runat="server">
Journal page
</asp:Content>

<asp:Content ID="mainContent" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <asp:ListView runat="server" ID="lstArticles">
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
                <asp:DataPager ID="pager" runat="server" PagedControlID="lstArticles" PageSize="15">
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
