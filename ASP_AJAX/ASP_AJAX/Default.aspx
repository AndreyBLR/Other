<%@ Page Title="Home Page" Language="C#"  AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ASP_AJAX._Default" %>
<%@ Register TagPrefix="asc" TagName="MyControl" Src="~/MyControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="~/Scripts/jquery.validate.js" type="text/javascript"></script>
    <script src="~/Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="~/Scripts/jquery.js" type="text/javascript"></script>
</head>
<body>
    <form id="Form1" runat="server">
    <div class="page">
        <div class="header">
            <div class="title">
                <h1>
                    My ASP.NET Application
                </h1>
            </div>
        </div>
        <div class="main">
        <table width="100%">
            <tr>
                <td align="center">
                    <asc:MyControl ID="myControl" runat="server" />
                </td>
            </tr>
        </table>
        </div>
        <div class="clear">
        </div>
    </div>
    <div class="footer">
    </div>
    </form>
</body>
</html>