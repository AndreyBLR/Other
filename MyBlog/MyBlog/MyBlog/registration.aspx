<%@ Page Title="" Language="C#" MasterPageFile="~/MyBlog.Master" AutoEventWireup="true" CodeBehind="registration.aspx.cs" Inherits="MyBlog.registration" %>
<%@ Register TagPrefix="asc" TagName="RegistrationForm" Src="~/Forms/RegistrationForm.ascx" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="asc" %>  

<asp:Content ID="header" ContentPlaceHolderID="headerPlaceHolder" runat="server">
    Registration
</asp:Content>
<asp:Content ID="description" ContentPlaceHolderID="descHeaderPlaceHolder" runat="server">
    Registration page
</asp:Content>
<asp:Content ID="mainContent" ContentPlaceHolderID="contentPlaceHolder" runat="server">
    <table width="100%">
    <tr>
        <td align="center">
            <asp:CreateUserWizard ID="regUserWizard" runat="server" 
                ContinueDestinationPageUrl="~/default.aspx" 
                oncreateduser="RegUserWizardCreatedUser" >
        <WizardSteps>
            <asp:CreateUserWizardStep ID="regUserWizardStep" runat="server">
            </asp:CreateUserWizardStep>
            <asp:CompleteWizardStep ID="regCompleteWizardStep" runat="server">
                <ContentTemplate>
                    Registration is completed. You <a href="user/profile.aspx">profile</a>
                </ContentTemplate>
            </asp:CompleteWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
        </td>
    </tr>
</table>
</asp:Content>
