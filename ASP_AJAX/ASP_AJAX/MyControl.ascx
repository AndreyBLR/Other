<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyControl.ascx.cs" Inherits="ASP_AJAX.MyControl" %>

<script language = "javascript" type="text/javascript">
        function getAjaxObject() {
            var xmlhttp;
            try {
                xmlhttp = new ActiveXObject("Msxml2.XMLHTTP");
            } catch (e) {
                try {
                    xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
                } catch (E) {
                    xmlhttp = false;
                }
            }
            if (!xmlhttp && typeof XMLHttpRequest != 'undefined') {
                xmlhttp = new XMLHttpRequest();
            }
            return xmlhttp;
        }

        function getDataFromServer(targetControl)
        {
            var ajaxObject = getAjaxObject();
            if (ajaxObject) {
                var message = document.getElementById("myControl_txbTextToSend").value;
                
                ajaxObject.open("GET", "default.aspx?message="+message, true);
                ajaxObject.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded')
                ajaxObject.onreadystatechange =
                                          function () {
                                              if (ajaxObject.readyState == 4 && ajaxObject.status == 200) {
                                                  var targetObj = document.getElementById(targetControl);
                                                  targetObj.innerText = ajaxObject.responseText;
                                                  delete myAjaxObject;
                                                  ajaxObject = null;
                                              }
                                          }
                ajaxObject.send(null);
            }
        };

        $().ready(function () {
            $("#myForm").validate({
                rules: {
                    mycontrol_txbTextToSend: { required: true, minlength: 2 }
                },
                messages: {
                    mycontrol_txbTextToSend: {
                        required: "Введите ваше имя",
                        minlength: "Введите не менее, чем 2 символа."
                    }
                }
            });
        });
</script> 
<table>
    <tr>
        <td>
            <asp:TextBox ID="txbTextToSend" runat="server" TextMode="MultiLine" />
        </td>
    </tr>
    <tr>
        <td align="center">
            <input id="btnFind" type="button" value="Search" onclick="getDataFromServer('myControl_lblResponceText')" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblResponceText" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:ValidationSummary ID="ValidationSummary" runat="server" />
        </td>
    </tr>
</table>
