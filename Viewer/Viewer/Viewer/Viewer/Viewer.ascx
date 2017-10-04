<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Viewer.ascx.cs" Inherits="Viewer.Viewer" %>
<script type="text/javascript" src="Scripts/viewer.js"></script>
<script type="text/javascript" src="Scripts/jquery-1.4.1.min.js"></script>
<script type="text/javascript" src="Scripts/jquery.treeview.js"></script>
<script type="text/javascript" src="Scripts/jquery.tooltip.min.js"></script>
<script type="text/javascript" src="Scripts/jquery-ui.min.js"></script>
<script type="text/javascript">
    function requestSuccessCallback(response) {
        /// <reference path="../Scripts/jquery-1.4.1-vsdoc.js" />
        /// <reference path="../Scripts/jquery.treeview" />
        $("#outputContainer").html(response);
        $("#termsBrowser input[type='checkbox']").click(checkEventHandler);
        $("#termsBrowser").treeview({
            animated: "fast"
        });
        $("#outputContainer").dialog({
            resize: "auto",
            width: 700,
            maxWidth: 1000,
            heigth: 500,
            maxHeigth: 700,
            position: "center",
            modal: true,
            hide: "slide"
        });    
    }

    function checkEventHandler() {
        var text = $("#markupTextContainer").html();
        var type = $($(this).parent().parent().parent().find("span")[0]).text();
        var word = $(this).siblings("span").text();
        $("#markupTextContainer").html(viewer.termMarkup(text, {type: type, word: word}, this.checked));
    }

    $(function () {
        $("#outputContainer").hide();
        $("#btnSend").click(function () { viewer.send($(".inputContent").attr("value"), requestSuccessCallback); });
    });
</script>

<div id="mainContainer" class="MainContainer" style="width:70%;">
    <div id="outputContainer" >
        
    </div>
            
    <div id="inputContainer">
        <table>
            <tr>
                <td>
                    <asp:TextBox runat="server" ID="inputContent" TextMode="MultiLine" CssClass="inputContent" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:FileUpload ID="fileUpload" runat="server" />
                    <asp:Button ID="btnFileOpen" runat="server" Text="Open" OnClick="BtnFileOpenClick" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <input type="button" id="btnSend" value="Send" />
                </td>
            </tr>
        </table>
    </div>
 </div>


