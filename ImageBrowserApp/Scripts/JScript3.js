
/*  rowCount * columnCount = amount of images per page*/
/*var pageNum = 1, rowCount, columnCount, divId, totalImagesCount, drawImageHandler,
getImageHandler, getThumbnailHandler, pagePath,
selectedValue;*/

function CreateImageBrowser(wrappingContainerId,
                            rowCount,
                            columnCount,
                            totalImagesCount,
                            imageHandlerName,
                            imageInfoHandlerName) {
    this.invalidArgumentsError = 'Invalid arguments have been passed!';
    this.invalidWrappingContainerIdError = 'Invalid wrapping container identifier!';
    //     alert("AAA");

    //Check parameters before image browser generation
    if ((typeof wrappingContainerId == 'undefined')
    || (typeof rowCount == 'undefined')
    || (typeof columnCount == 'undefined')
    || (typeof totalImagesCount == 'undefined')) {
        alert(this.invalidArgumentsError);
        return;
    }

    this.wrappingContainerId = wrappingContainerId;
    this.rowCount = rowCount;
    this.columnCount = columnCount;
    this.totalImagesCount = totalImagesCount;
    this.imageHandlerName = imageHandlerName;
    this.imageInfoHandlerName = imageInfoHandlerName;
    this.pageNum = 1;
    this.currentRow;
    this.divElement = $('#' + this.wrappingContainerId)[0];
    this.selectedValue;
    this.anchorClass = "anchorClass";
    this.dropDownListClass = "dropDownListClass";
    this.spanClass1 = "spanClass1";
    this.spanClass2 = "spanClass2";

    //   alert("hello");
    //Create specified image.
    //  this.GenerateBrowser = function(){
    this.CreateCustomImage = function (srcValue) {
        var img = document.createElement('img');
        img.alt = 'Image';
        img.title = 'Click to enlarge';
        img.src = srcValue;

        return img;
    }

    //Create specified anchor
    this.createCustomAnchor = function (hrefValue, className, contentText, onClickEventHandler) {
        var a = document.createElement('a');

        a.href = hrefValue;
       // this.setClassForControl(a, 'highslide');
        if (onClickEventHandler != 'undefined') {
            a.onclick = onClickEventHandler;
        }
        this.setClassForControl(a, className);
        a.innerHTML = contentText;
        return a;
    }

    this.createCustomDropDownList = function (caption, optionContentArray, optionValueArray,
     onChangeEventHandler, className) {
        var select = document.createElement('select');
        var optionLength = optionContentArray.length;
        var captionOption = document.createElement('option');
        captionOption.innerHTML = caption;
        captionOption.disabled = "disabled";
        select.appendChild(captionOption);
        for (var i = 0; i < optionLength; i++) {
            var option = document.createElement('option');
            option.innerHTML = optionContentArray[i];
            option.value = optionValueArray[i];
            select.appendChild(option);
        }
        select.onchange = onChangeEventHandler;
        this.setClassForControl(select, className);
        return select;
    }

    this.createCustomSpan = function (contentText, className) {
        var span = document.createElement('span');
        span.innerHTML = contentText;
        this.setClassForControl(span, className);
        return span;
    }

    //Sets class for the specified control
    this.setClassForControl = function (control, className) {
        var classAttr = document.createAttribute('class');
        classAttr.nodeValue = className;

        control.setAttributeNode(classAttr);
    }

    this.createTableCellContents = function (items) {
        //     alert(items);
        for (var i = 0; i < items.length; i++) {
            //       alert(this.currentImageId + " " + itemsLength);
            // var myrow;
            if (this.columnId == 1) {
                this.currentRow = document.createElement("tr");
            }

            var mycell = document.createElement("td");
            var mydiv = document.createElement("div");

            var mylink = this.createCustomAnchor(this.imageHandlerName + "?isThumb=false&ID=" + item['Id'], this.anchorClass);
            $(mylink).lightBox({ fixedNavigation: true });
            var myimg = this.CreateCustomImage(this.imageHandlerName + '?isThumb=true&ID=' + item['Id'], this.anchorClass);
            /*      myimg.setAttribute('src',
            this.thumbnailHandlerName + '?ID=' + item['Id']);
            myimg.setAttribute('alt', '');
            myimg.setAttribute('align', 'middle');
            myimg.setAttribute('width', '100');
            myimg.setAttribute('height', '100');*/
            mylink.appendChild(myimg);
            mydiv.appendChild(mylink);
            mycell.appendChild(mydiv);


            mydiv = document.createElement("div");
            var myspan = this.createCustomSpan(item['Name'], "spanClass1");
            mydiv.appendChild(myspan);
            mycell.appendChild(mydiv);

            mydiv = document.createElement("div");
            var myspan = this.createCustomSpan(item['Description'], "spanClass2");
            mydiv.appendChild(myspan);
            mycell.appendChild(mydiv);
            this.currentRow.appendChild(mycell);

            if (this.currentImageId == itemsLength) {
                mydiv = document.createElement("div");
                var myrowlist = this.createCustomDropDownList("Choose amount of rows",
            ["1", "2", "3", "4", "5"], ["val1", "val2", "val3", "val4", "val5"],
            (function (obj) {
                return function () {
                    obj.RowSelectionChanged();
                }
            })(this), "dropDownListClass");

                mydiv.appendChild(myrowlist);
                mydiv.appendChild(document.createElement("br"));
                var mycolumnlist = this.createCustomDropDownList("Choose amount of columns",
            ["1", "2", "3", "4", "5"], ["val1", "val2", "val3", "val4", "val5"],
            (function (obj) {
                return function () {
                    obj.ColumnSelectionChanged();
                }
            })(this), "dropDownListClass");
                var mydiv2 = document.createElement("div")
                mydiv2.appendChild(this.mytable);
                this.mycontainer.appendChild(mydiv2);
                this.mycontainer.appendChild(mydiv);

                mydiv = document.createElement("div");
                var linkTable = document.createElement("table");
                linkTable.setAttribute("width", "100px");
                var linkRow = document.createElement("tr");
                var linkCell = document.createElement("td");
                linkCell.setAttribute("align", "left");
                var mylink = this.createCustomAnchor("javascript:void(0)", "anchorClass", "Previous page",
            (function (obj) {
                return function () {
                    obj.GoToPreviousPage();
                }
            })(this));
                linkCell.appendChild(mylink);
                linkRow.appendChild(linkCell);

                linkCell = document.createElement("td");
                linkCell.setAttribute("align", "right");
                var mylink = this.createCustomAnchor("javascript:void(0)", "anchorClass", "Next page", (function (obj) {
                    return function () {
                        obj.GoToNextPage();
                    }
                })(this));
                linkCell.appendChild(mylink);
                linkRow.appendChild(linkCell);

                linkTable.appendChild(linkRow);
                mydiv.appendChild(linkTable);

                this.mycontainer.appendChild(mydiv);
                this.divElement.appendChild(this.mycontainer);
            }

            if (this.columnId == this.columnCount || this.currentImageId == itemsLength) {
                this.mytable.appendChild(this.currentRow);
                this.columnId = 1;
            }
            else {
                this.columnId++;
            }
            this.currentImageId++;
            //    alert("abcd");
        }
    }


    this.Iterator = function (items) {
        //   alert(this.mycontainer);

        this.mycontainer = document.createElement("div");
        this.mytable = document.createElement("table");

        this.mytable.setAttribute("cellspacing", "10px");

        this.divElement = $('#' + this.wrappingContainerId)[0];
        $(this.divElement).html("");
        this.columnId = 1;
        this.currentImageId = 1;

        //      alert(items);
        /*
        $.each(items, (function (obj) {
        return function () {
        obj.InnerIterator(this, items.length);
        }
        })(this));*/
        //   for (var i = 0; i < items.length; i++) {
        //       alert(i);
        this.createTableCellContents(items);
        //innerIterator(items[i], items.length);
        //   }
        //alert("hello");
        // this.divElement.appendChild(this.mycontainer);
        //$(this.divElement).append(this.mycontainer);
    }
    //  alert(this.imageInfoHandlerName);

    // alert(this.jsonHandlerName);
    this.GetImageBrowser = function (/*createContents*/) {
        //   alert("hello");


        $.getJSON(this.imageInfoHandlerName + "?RowCount=" + this.rowCount +
          "&ColumnCount=" + this.columnCount + "&PageNumber=" + this.pageNum,
          (function (obj) {
              return function (items) {
                  obj.Iterator(items);
              }
          })(this));
        /*  (function (obj) {
        return function (items) {
        obj.Iterator(items, createTableCellContents);
        }
        })(this));*/
    }


    this.GenerateBrowser = function () {
        //     alert("hello");
        //     GetImageBrowser();
        //  debugger;
        //  alert("hello");
        //  debugger;
        $(document).ready((function (obj) {
            return function () {
                obj.GetImageBrowser(/*this.Iterator, this.createTableCellContents*/);
            }
        })(this));
    }
    this.GoToPreviousPage = function () {
        if (this.pageNum > 1) {
            this.pageNum--;
            this.GetImageBrowser();
        }
    }

    this.GoToNextPage = function () {
        var maxPageNum = this.totalImagesCount / (this.rowCount * this.columnCount);
        if (this.totalImagesCount - maxPageNum * this.rowCount * this.columnCount > 0) {
            maxPageNum++;
        }
        if (this.pageNum < maxPageNum) {
            this.pageNum++;
            this.GetImageBrowser();
        }
    }

    this.RowSelectionChanged = function (selectionList) {

        //   var selectionList = $('#' + listId)[0];
        //   alert(selectionList);
        this.selectedValue = selectionList.options[selectionList.selectedIndex].value;
        if (this.selectedValue == "val1") {
            this.rowCount = 1;
        }
        if (this.selectedValue == "val2") {
            this.rowCount = 2;
        }
        if (this.selectedValue == "val3") {
            this.rowCount = 3;
        }
        if (this.selectedValue == "val4") {
            this.rowCount = 4;
        }
        if (this.selectedValue == "val5") {
            this.rowCount = 5;
        }
        this.GetImageBrowser();
    }

    this.ColumnSelectionChanged = function (selectionList) {

        //   var selectionList = $('#' + listId)[0];
        //   alert(selectionList);
        this.selectedValue = selectionList.options[selectionList.selectedIndex].value;
        if (this.selectedValue == "val1") {
            this.columnCount = 1;
        }
        if (this.selectedValue == "val2") {
            this.columnCount = 2;
        }
        if (this.selectedValue == "val3") {
            this.columnCount = 3;
        }
        if (this.selectedValue == "val4") {
            this.columnCount = 4;
        }
        if (this.selectedValue == "val5") {
            this.columnCount = 5;
        }
        this.GetImageBrowser();
    }
}


/*function GoToNextPage() {
maxPageNum = totalImagesCount / (rowCount * columnCount);
if (totalImagesCount - maxPageNum * rowCount * columnCount > 0) {
maxPageNum++;
}
if (pageNum < maxPageNum) {
pageNum++;
GetImageBrowser();
}
}*/



/*function GetImageBrowser() {
$.getJSON("DrawImagesHandler.ashx?RowCount=" + rowCount +
"&ColumnCount=" + columnCount + "&PageNumber=" + pageNum,
function (items) {
              
var mycontainer = document.createElement("div");
var mytable = document.createElement("table");
mytable.setAttribute("cellspacing", "10px");
var divElement = $('#' + divId)[0];
$(divElement).html("");
var columnId = 1, currentImageId = 1;
$.each(items,
function () {
if (columnId == 1) {
myrow = document.createElement("tr");
}
mycell = document.createElement("td");
mydiv = document.createElement("div");
mylink = document.createElement("a");
mylink.setAttribute("href", "GetImageHandler.ashx?ID=" + this['Id']);
$(mylink).lightBox({ fixedNavigation: true });
myimg = document.createElement("img");
myimg.setAttribute('src',
'GetThumbnailHandler.ashx?ID=' + this['Id']);
myimg.setAttribute('alt', '');
myimg.setAttribute('align', 'middle');
myimg.setAttribute('width', '100');
myimg.setAttribute('height', '100');
mylink.appendChild(myimg);
mydiv.appendChild(mylink);
mycell.appendChild(mydiv);

mydiv = document.createElement("div");
myspan = document.createElement("span");
myspan.style.color = "red";
myspan.style.fontSize = "20px";
myspan.style.fontWeight = "bold";

myspan.innerHTML = this['Name'];
mydiv.appendChild(myspan);
mycell.appendChild(mydiv);

mydiv = document.createElement("div");
myspan = document.createElement("span");
myspan.style.color = "green";
myspan.style.fontSize = "15px";
myspan.innerHTML = this['Description'];
mydiv.appendChild(myspan);
mycell.appendChild(mydiv);
myrow.appendChild(mycell);
mytable.appendChild(myrow);

if (currentImageId == items.length) {
mydiv = document.createElement("div");
mylist = document.createElement("select");
mylist.setAttribute("id", "DimensionList");
myoption = document.createElement("option");
myoption.setAttribute("disabled", "disabled");
myoption.innerHTML = "Choose the dimensions";
mylist.appendChild(myoption);

myoption = document.createElement("option");
myoption.setAttribute("value", "val1");
if (selectedValue == "val1") {
myoption.setAttribute("selected", "selected");
}
myoption.innerHTML = "2 x 2";
mylist.appendChild(myoption);

myoption = document.createElement("option");
myoption.setAttribute("value", "val2");
if (selectedValue == "val2") {
myoption.setAttribute("selected", "selected");
}
myoption.innerHTML = "3 x 3";
mylist.appendChild(myoption);

myoption = document.createElement("option");
myoption.setAttribute("value", "val3");
if (selectedValue == "val3") {
myoption.setAttribute("selected", "selected");
}
myoption.innerHTML = "4 x 4";
mylist.appendChild(myoption);

myoption = document.createElement("option");
myoption.setAttribute("value", "val4");
if (selectedValue == "val4") {
myoption.setAttribute("selected", "selected");
}
myoption.innerHTML = "5 x 5";
mylist.appendChild(myoption);
mylist.setAttribute("onChange", "SelectionChanged('" + mylist.getAttribute("id") + "');");
mydiv.appendChild(mylist);
mydiv2 = document.createElement("div");
mydiv2.appendChild(mytable);
mycontainer.appendChild(mydiv2);
mycontainer.appendChild(mydiv);

mydiv = document.createElement("div");
linkTable = document.createElement("table");
linkTable.setAttribute("width", "100px");
linkRow = document.createElement("tr");
linkCell = document.createElement("td");
linkCell.setAttribute("align", "left");
mylink = document.createElement("a");
mylink.style.fontWeight = "bold";
mylink.setAttribute('href', "javascript:void(0)");
mylink.setAttribute('onclick', 'GoToPreviousPage();');
mylink.innerHTML = 'Previous';
linkCell.appendChild(mylink);
linkRow.appendChild(linkCell);

linkCell = document.createElement("td");
linkCell.setAttribute("align", "right");
mylink = document.createElement("a");
mylink.style.fontWeight = "bold";
mylink.setAttribute('href', 'javascript:void(0)');
mylink.setAttribute('onclick', 'GoToNextPage();');
mylink.innerHTML = 'Next';
linkCell.appendChild(mylink);
linkRow.appendChild(linkCell);

linkTable.appendChild(linkRow);
mydiv.appendChild(linkTable);
mycontainer.appendChild(mydiv);
}
if (columnId == columnCount || currentImageId == items.length) {
mytable.appendChild(myrow);
columnId = 1;
}
else {
columnId++;
}
currentImageId++;
});
$('#' + divId)[0].appendChild(mycontainer);
});
}*/