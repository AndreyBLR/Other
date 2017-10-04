<%@ Page Title="" Language="C#" MasterPageFile="~/MyBlog.Master" AutoEventWireup="true" CodeBehind="article.aspx.cs" Inherits="MyBlog.article" EnableViewState="false"%>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="asc" %>  
<%@ Register TagPrefix="asc" TagName="RegistrationForm" Src="~/Forms/RegistrationForm.ascx" %>

<asp:Content ID="header" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    <% Response.Write(Post != null ? Post.PostTitle : "Post not found"); %>
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="contentPlaceHolder" runat="server">
            <table width="95%">
                <tr>
                    <td align="center">
                        <asp:Label ID="titleArticle" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="metaArticle" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="justify">
                        <asp:Label ID="textArticle" runat="server"></asp:Label>
                    </td>
                </tr>
            <tr>
                <td>
                    <br />
                    <hr width="80%" style="color:#A5B586;" />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:ListView ID="lstComments" runat="server" onitemcreated="CommentsItemCreated" >
                        <ItemTemplate>
                            <table width="100%" border="0px" style="background:#F5F3E3">
                                <tr>
                                    <td>
                                        <a href='../user/profile.aspx?userName=<%#Eval("aspnet_Users.UserName") %>'> <%#Eval("aspnet_Users.UserName")%> </a> 
                                    </td>
                                    <td align="right">
                                        Date: <%# Eval("CommentDate")%>
                                    </td>
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
                                    <td>
                                        Warnings: <%# Eval("aspnet_Users.Profiles.Warnings")%>&nbsp;
                                        <asp:PlaceHolder ID="addWarningPh" runat="server" />
                                        <asp:PlaceHolder ID="delWarningPh" runat="server" />
                                    </td>
                               </tr>
                               <tr>
                                    <td>
                                        <asp:PlaceHolder ID="banUserPh" runat="server" />
                                    </td>
                               </tr>
                               <tr>
                                   <td align="right" colspan="3">
                                       <table width="70%">
                                        <tr>
                                            <td>
                                                <asp:PlaceHolder ID="deleteCommentPh" runat="server" />
                                            </td>
                                            <td>
                                                <asp:PlaceHolder ID="changeCommentPh" runat="server" />
                                            </td>
                                            <td>
                                                <asp:PlaceHolder ID="allCommentsPh" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                   </td>
                               </tr>
                           </table>
                           <br />
                           <br />
                           <br />
                        </ItemTemplate>
                    </asp:ListView>
                </td>
            </tr>   
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <asp:DataPager ID="pager" runat="server" PagedControlID="lstComments" PageSize="40">
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
                </td>
            </tr>
            <%
                if(User.Identity.IsAuthenticated)
                {
                    ReadUserStatus((Guid)Membership.GetUser(User.Identity.Name).ProviderUserKey);
                    if (IsBanned)
                    {
                        viewForm.Visible = false;
                    }
                }
            %>
            <tr>
                <td align="center" style="padding:70px 0px 0px 0px;">
                    <asp:LoginView runat="server" ClientIDMode="Predictable" ID="viewForm">
                    <LoggedInTemplate>
                    
                        <table>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txbComment" runat="server" Width="400px" TextMode="MultiLine" Rows="7" ViewStateMode="Enabled"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RegularExpressionValidator ID="txbCommentValidator1" runat="server"                                         
                                        ErrorMessage="&#1053;&#1077;&#1082;&#1086;&#1088;&#1088;&#1077;&#1082;&#1090;&#1085;&#1086;&#1077; &#1089;&#1086;&#1086;&#1073;&#1097;&#1077;&#1085;&#1080;&#1077;" Display="None" 
                                        ControlToValidate="txbComment" 
                                        ValidationExpression="^[A-ZA-&#1071;a-z&#1072;-&#1103;0-9 _,./!?]{5,30}$" />
                                        <asp:RequiredFieldValidator ID="txbCommentValidator2" runat="server" Display="None" ErrorMessage="no text comment" ControlToValidate="txbComment" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="addBtn" runat="server" Text="Add comment" OnClick="AddBtnClick"/>
                                </td>
                                <td>
                                    
                                </td>
                                
                            </tr>
                            <tr>
                                <td align="center">
                                        <asc:CaptchaControl ID="captcha" runat="server" 
                                            CaptchaBackgroundNoise="Low" CaptchaLength="5"    
                                            CaptchaHeight="40" CaptchaWidth="140"    
                                            CaptchaLineNoise="Medium" CaptchaMinTimeout="0"    
                                            CaptchaMaxTimeout="240" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:TextBox CssClass="TextBox" ID="txbCaptcha" runat="server"></asp:TextBox>  
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="txbCaptchaValidator1" runat="server" Display="None" ErrorMessage="enter text captcha" ControlToValidate="txbCaptcha" /> 
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:ValidationSummary ID="ValidationSummary" runat="server" DisplayMode="BulletList" />
                                </td>
                            </tr>
                        </table>
                    </LoggedInTemplate>
                    </asp:LoginView>
                </td>
                
            </tr>
          </table>  
</asp:Content>
