<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MvcApp.Models.Employee>" %>
<form method="post" action="FormCreatingEmp/CreateEmployee">
    <table id="formCreatingEmp">
        <tr>
            <td> <%=Html.Label("Name: ")%> </td>
            <td> <%=Html.TextBox("Name")%> </td>
            <td rowspan="3" align="center">
                <span title="errors"></span>
            </td>
        </tr>
        <tr>
            <td> <%=Html.Label("Birthday: ")%> </td>
            <td> <%=Html.TextBox("Birthday")%> </td>
        </tr>
        <tr>
            <td> <%=Html.Label("Day of employment: ")%> </td>
            <td> <%=Html.TextBox("DayOfEmployment")%> </td>
        </tr>
        <tr>
            <td colspan="2" align="center"> 
                <input title="create" type="submit" value="Create employee" style="width:100px;"/>
            </td>
        </tr>
    </table>
</form>




