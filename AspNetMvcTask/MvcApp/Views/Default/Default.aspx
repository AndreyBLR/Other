<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<MvcApp.Models.Employee>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>default</title>
    <script type="text/javascript" src="../../Scripts/jquery-1.4.1.js"></script>
    <script type="text/javascript" src="../../Scripts/main.js"></script>
    <link rel="stylesheet" type="text/css" href="../../Content/Site.css"/>
    <link rel="stylesheet" type="text/css" href="../../Content/Validators.css"/>
</head>
<body>  
    <%Html.RenderAction("ShowForm", "FormCreatingEmp");%>
    <div id="employees_div">
    <%Html.RenderAction("ShowEmployeeTable", "Employees");%>
    </div>
</body>
</html>
