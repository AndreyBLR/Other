/// <reference path="../Scripts/jquery-1.4.1-vsdoc.js" />

var viewer = function () {

    function cr(name) {
        return $(document.createElement(name));
    }

    function createHtmlList(nodeName, xmlRoot) {
        var ul = cr("ul");
        var value, checkbox, span, type;
        $(nodeName, xmlRoot.children()).each(function (i, node) {
            value = $(node).text();
            type = xmlRoot.attr("type");
            checkbox = cr("input").attr("type", "checkbox");
            span = cr("span").attr("title", type + ": " + value).html(value);
            ul.append(cr("li").append(checkbox)
                              .append(span)
                     );
        });
        return ul;
    }
    return {
        termMarkup: function (text, term, highlight) {
            var title = term.type + ": " + term.word;
            if (highlight == true) {
                return text.replace(term.word, "<b><span class=" + term.type + " title='" + title + "'>" + term.word + "</span></b>");
            }
            else {
                return text.replace("<b><span class=\"" + term.type + "\" title=\"" + title + "\">" + term.word + "</span></b>", term.word);
            }
        },
        createMetadataHtmlList: function (prefix, xmlDoc) {
            var ul = cr("ul");
            var span, type;
            $("metadata " + prefix, xmlDoc).find('entity', xmlDoc).each(function (i, node) {
                type = $(node).attr("type");
                span = cr("span").addClass(type)
                                  .html(type);
                ul.append(cr("li").append(span)
                                  .append(createHtmlList((prefix == "em-e") ? "term" : "fact", $(node))));
            });
            span = cr("span").text((prefix == "em-e") ? "Entities" : "Events/Facts");
            return cr("ul").append(cr("li").append(span)
                                           .append(ul));
        }
    }
}()

var parser = function () {
    var _licenceKey = "g48ngkd58tv33aj26qbvrhgq";
    var _uri = document.location.toString().replace(/\/[^\/]*$/, "/Parse.ashx");
    var _metadata = null;
    return {
        parse: function (text, successCallback) {
            $.ajax({
                url: _uri,
                type: "POST",
                dataType: "xml",
                data: {
                    licenseID: _licenceKey,
                    content: text
                },
                success: function (response) {
                    _metadata = response;
                    successCallback(_metadata);
                }
            });
        },
        getData: function () {
            return _metadata
        }
    }
} ();





 