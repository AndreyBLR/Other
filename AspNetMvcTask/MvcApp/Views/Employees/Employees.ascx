<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<MvcApp.Models.Employee>>" %>
<table id="employeeTable">
    <%int i = 0;%>
        <%if (Model != null)%>
            <%foreach (var item in Model){%>
                <tr id="<%=i%>">
                    <td>
                        <label for="txbName<%=i%>" ><%=item.Name%></label>
                        <input id="txbName<%=i%>" type="text" name="Name" style="display:none" />
                    </td>
                    <td>
                        <label for="txbBirthday<%=i%>" ><%=item.Birthday%></label>
                        <input id="txbBirthday<%=i%>" type="text" name="Birthday" style="display:none" />
                    </td>
                    <td>
                        <label for="txbDayOfEmployment<%=i%>" ><%=item.DayOfEmployment%></label>
                        <input id="txbDayOfEmployment<%=i%>" type="text" name="DayOfEmployment" style="display:none" />
                    </td>
                    <td>
                        <input type="button" title="save" style="display:none" value="save" onclick="save(this)" />
                        <input type="button" title="edit" value="edit" onclick="edit(this)" />
                        <input type="button" title="delete" value="delete" onclick="deleteRow(this)" />
                    </td>
                    <td>
                        <input type="hidden" name="Id" value="<%=item.Id %>" />
                    </td>
                </tr>
                <%i++;%>
            <%}%>
            <tr>
                <td colspan="3" align="center">
                    <span title="errors"></span>
                </td>
            </tr>
</table>


