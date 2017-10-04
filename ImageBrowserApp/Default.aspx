<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ImageBrowserApp.Default" %>
<%@ Register Src="~/User Controls/ImageBrowserControl.ascx" TagName="ImageBrowser" TagPrefix="abc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="Scripts/jquery-lightbox-0.5/js/jquery.js"></script>
    <script type="text/javascript" src="Scripts/jquery-lightbox-0.5/js/jquery.lightbox-0.5.js"></script>
    <link rel="stylesheet" type="text/css" href="Scripts/jquery-lightbox-0.5/css/jquery.lightbox-0.5.css" media="screen" />
    <link rel="Stylesheet" type="text/css" href="CSS/StyleSheet.css"/>
</head>
<body>
    <form id="form1" runat="server">
<div>
   <h2> Image browser </h2>
   <table>
   <tr>
      <td>
         <h4>Image:</h4>
      </td>
      <td>
         <asp:FileUpload runat="server" ID="imageFileUpload" />
      </td>
   </tr>
   <tr>
      <td>
          <h4>Image name: </h4>
      </td>
      <td>
          <asp:TextBox ID="ImageNameTextBox" runat="server" />
      </td>
   </tr>
   <tr>
      <td>
         <h4>Image description: </h4> 
      </td>
      <td>
         <asp:TextBox ID="ImageDescriptionTextBox" runat="server" />
      </td>
   </tr>
   <tr>
      <td colspan="2">
         <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Text="Save image" ValidationGroup="UploadFileValidationGroup" />
            <br />
            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Only *.jpg, *.jpeg, *.png and *.bmp files are supported."
                Display="Dynamic" ControlToValidate="imageFileUpload" ClientValidationFunction="validateFileExtension"  ValidationGroup="UploadFileValidationGroup">*</asp:CustomValidator>
                <br />

                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="imageFileUpload"
                Display="Dynamic" ErrorMessage="You must choose the picture!" ValidationGroup="UploadFileValidationGroup">*</asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ImageNameTextBox"
                Display="Dynamic" ErrorMessage="You must specify picture name!" ValidationGroup="UploadFileValidationGroup">*</asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ImageDescriptionTextBox"
                Display="Dynamic" ErrorMessage="You must specify picture description!" ValidationGroup="UploadFileValidationGroup">*</asp:RequiredFieldValidator>
          <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="UploadFileValidationGroup" runat="server" />
      </td>
   </tr>
   </table>
</div>
<asp:Panel id="ImageBrowsersPanel" runat="server">
<fieldset>
<div> 
    <abc:ImageBrowser ID="Browser1" runat="server" RowCount="2" ColumnCount="2" />
</div>
</fieldset>
<fieldset>
<div>
    <abc:ImageBrowser ID="Browser2" runat="server" RowCount="2" ColumnCount="2" />
</div>
</fieldset>
</asp:Panel>
    </form>
</body>
</html>
