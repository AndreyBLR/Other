/// <reference path="../Scripts/jquery-1.4.1-vsdoc.js" />
var viewer = function (param) {
    var licenceKey = "g48ngkd58tv33aj26qbvrhgq";
    var uri = document.location.toString().replace(/\/[^\/]*$/, "/Parse.ashx");

    return {
        send: function (text, sendCallback) {
            $.ajax({
                url: uri,
                type: "POST",
                data: {
                    licenseID: licenceKey,
                    content: text
                },
                success: sendCallback
            });
        },
        termMarkup: function (text, term, highlight) {
            if (highlight == true) {
                return text.replace(term.word, "<span class=" + term.type + ">" + term.word + "</span>");
            }
            else {
                return text.replace("<span class=\"" + term.type + "\">" + term.word + "</span>", term.word);
            }
        }
    }
    $().replaceWith
} ("#container");






 