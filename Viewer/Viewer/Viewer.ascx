<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Viewer.ascx.cs" Inherits="Viewer.Viewer" %>
<script type="text/javascript" src="Scripts/viewer.js"></script>
<script type="text/javascript" src="Scripts/jquery-1.4.1.min.js"></script>
<script type="text/javascript" src="Scripts/jquery.treeview.js"></script>
<script type="text/javascript" src="Scripts/jquery.cookie.js"></script>
<script type="text/javascript" src="Scripts/jquery.tooltip.min.js"></script>
<script type="text/javascript" src="Scripts/jquery-ui.min.js"></script>
<script type="text/javascript" src="Scripts/jquery.tooltip.min.js"></script>
<script type="text/javascript">
    /// <reference path="../Scripts/jquery-1.4.1-vsdoc.js" />
    /// <reference path="../Scripts/jquery.treeview" />
    var leftSidebar, markupTextContainer, outputContainer;

    function requestSuccessCallback(xmlDoc) {
        var ulEm = viewer.createMetadataHtmlList("em", xmlDoc);
        ulEm.treeview({ animated: "fast", collapsed: true });
        ulEm.find("input[type='checkbox']").click(checkEventHandler);
        ulEm.addClass("ulEm");

        var ulEr = viewer.createMetadataHtmlList("er", xmlDoc);
        ulEr.treeview({ animated: "fast", collapsed: true });
        ulEr.addClass("ulEr");

        leftSidebar.append(ulEm)
                   .append(ulEr)
                   .find("span[title]").tooltip({ track: true, fade: 250 });

        markupTextContainer.text($("document", xmlDoc).text());
        outputContainer.dialog({
            resize: "auto",
            width: 1000,
            heigth: 200,
            position: "center",
            modal: true,
            hide: "slide",
            close: function (event, ui) {
                $("#sidebar").html("");
            }
        });
    }

    function checkEventHandler() {
        var text = markupTextContainer.html();
        var type = $($(this).parent().parent().parent().find("span:first")).text();
        var word = $(this).siblings("span").text();
        $("#markupTextContainer").html(viewer.termMarkup(text, { type: type, word: word }, this.checked));
    }

    $(function () {
        leftSidebar = $("#sidebar");
        markupTextContainer = $("#markupTextContainer");
        outputContainer = $("#outputContainer");
        outputContainer.hide();
        $("#btnSend").click(function () { parser.parse($(".inputContent").attr("value"), requestSuccessCallback); });
    });
</script>

<div id="mainContainer">
    <div id="outputContainer" >
        <div id="markupTextContainer" class="markupTextContainer"></div> 
        <div id="sidebar" class="sidebar"></div>      
    </div>
            
    <div id="inputContainer" class="inputContainer">
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


