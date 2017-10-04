<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Viewer.Default" %>
<%@ Register TagPrefix="asc" TagName="Viewer" Src="~/Viewer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="Content/Site.css"  />
    <link rel="stylesheet" type="text/css" href="Content/highlight.css"  />
    <link rel="stylesheet" type="text/css" href="Content/screen.css" />
    <link rel="stylesheet" type="text/css" href="Content/jquery.treeview.css" />
    <link rel="stylesheet" type="text/css" href="Content/jquery.tooltip.css" />
    <link rel="stylesheet" type="text/css" href="Content/jquery-ui.css" />
    <link rel="stylesheet" type="text/css" href="Content/tooltips.css" />
</head>
<body>
    <form id="form" runat="server">
        <asc:Viewer runat="server"/>
    </form>
</body>
</html>
