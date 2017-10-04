
var rowListId = "rowList",
    columnListId = "columnList";

function script2() {
    return confirm('Hello');
}
 
function validateFileExtension(oSrc, args) {

    args.IsValid = (args.Value.indexOf(".jpg") != -1 ||
                    args.Value.indexOf(".jpeg") != -1 ||
                    args.Value.indexOf(".bmp") != -1 ||
                    args.Value.indexOf(".png") != -1);
}

//Receive and set cookie
function setCookie(name, value, expires) {
    if (!expires) {
        expires = new Date();
    }
    document.cookie = name + "=" + escape(value) + "; expires=" + expires.toGMTString() + "; path=/";
}

//get the value that is stored in cookie
function getCookie(name) {
    cookie_name = name + "=";
    cookie_length = document.cookie.length;
    cookie_begin = 0;
    while (cookie_begin < cookie_length) {
        value_begin = cookie_begin + cookie_name.length;
        if (document.cookie.substring(cookie_begin, value_begin) == cookie_name) {
            var value_end = document.cookie.indexOf(";", value_begin);
            if (value_end == -1) {
                value_end = cookie_length;
            }
            return unescape(document.cookie.substring(value_begin, value_end));
        }
        cookie_begin = document.cookie.indexOf(" ", cookie_begin) + 1;
        if (cookie_begin == 0) {
            break;
        }
    }
    return null;
}

function saveArrayInCookie(myArray, name) {
    var tmp = "";
    if (myArray != null) {

        //walk through the array, collect values into the variable and divide them by comma
        for (i in myArray) {
            if (myArray[i] != "") {
                tmp = tmp + myArray[i];
                if (i != myArray.length - 1) {
                    tmp = tmp + ",";
                }
            }
        }
    }
    expires = new Date(); // get current date
    expires.setTime(expires.getTime() + (1000 * 86400 * 365)); //calculate cookie storage time
    setCookie(name, tmp, expires); // set cookie
}


function setupDimensionsOfAllImageBrowsers(names) {
    if (Page_ClientValidate() == true) {
        for (var i = 0; i < names.length; i++) {
            var array = getCookie(names[i]); // read cookie value
            if (array != null) {
                var tmp = '';
                array = array.split(",");  // split the string and put it into array
                setupImageBrowserDimensions(array[0], array[1], names[i]);
            }
        }
    }
}

//setup the dimensions (rowCount and columnCount) of image browser
function setupImageBrowserDimensions(rowCount, columnCount, userControlId) {
    $('#' + userControlId + rowListId).val(rowCount);
    $('#' + userControlId + columnListId).val(columnCount);
}

function createImageBrowser(wrappingContainerId,
                            userControlId,
                            rowCount,
                            columnCount,
                            totalImagesCount,
                            imageHandlerName,
                            imageInfoHandlerName) {
    this.invalidArgumentsError = 'Invalid arguments have been passed!';
    this.invalidWrappingContainerIdError = 'Invalid wrapping container identifier!';
      
    
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
    this.maxPageNum=0;
    this.divElement = $('#' + this.wrappingContainerId)[0];
    this.userControlId = userControlId;
    this.anchorClass = "anchorClass";
    this.dropDownListClass = "dropDownListClass";
    this.spanClass1 = "spanClass1";
    this.spanClass2 = "spanClass2";
    
    saveArrayInCookie([this.rowCount, this.columnCount], this.userControlId);
    this.createCustomImage = function (srcValue) {
        var img = document.createElement('img');
        img.alt = 'Image';
        img.title = 'Click to enlarge';
        img.src = srcValue;

        return img;
    }

    //Create specified anchor
    this.createCustomAnchor = function (hrefValue, className, contentText, titleText, onClickEventHandler) {
        var a = document.createElement('a');

        a.href = hrefValue;
        a.title = titleText;
        if (typeof onClickEventHandler != 'undefined') {
            a.onclick = onClickEventHandler;
        }
        this.setClassForControl(a, className);
        if (typeof contentText != 'undefined') {
            a.innerHTML = contentText;
        }
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
        $(select).change(onChangeEventHandler);
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

    this.createTableCellContents = function (item, itemsLength) {
        if (this.columnId == 1) {
            this.currentRow = document.createElement("tr");
        }

        var mycell = document.createElement("td");
        var mydiv = document.createElement("div");

        var mylink = this.createCustomAnchor(this.imageHandlerName + "?isThumb=false&ID=" + item['Id'], 'anchorClass', undefined ,item['Name']);

        $(mylink).lightBox({ fixedNavigation: true });
        var myimg = this.createCustomImage(this.imageHandlerName + '?isThumb=true&ID=' + item['Id'], 'anchorClass');

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
            this.calcMaxPageNumber();
            var curPageText = this.createCustomSpan("Page " + this.pageNum + " of " + this.maxPageNum, "spanClass1");
            mydiv.appendChild(curPageText);
            mydiv.appendChild(document.createElement("br"));
            var myrowlist = this.createCustomDropDownList("Choose amount of rows",
            ["1", "2", "3", "4", "5"], ["1", "2", "3", "4", "5"],
            (function (obj) {
                return function () {
                    obj.rowSelectionChanged(myrowlist);
                }
            })(this), this.dropDownListClass);

            $(myrowlist).attr("id", this.userControlId + rowListId);

            $(myrowlist).val(this.rowCount.toString());
            mydiv.appendChild(myrowlist);

            mydiv.appendChild(document.createElement("br"));
            mydiv.appendChild(document.createElement("br"));
            var mycolumnlist = this.createCustomDropDownList("Choose amount of columns",
            ["1", "2", "3", "4", "5"], ["1", "2", "3", "4", "5"],
            (function (obj) {
                return function () {
                    obj.columnSelectionChanged(mycolumnlist);
                }
            })(this), this.dropDownListClass);

            $(mycolumnlist).attr("id", this.userControlId + columnListId);
            $(mycolumnlist).val(this.columnCount.toString());

            mydiv.appendChild(mycolumnlist);
            var mydiv2 = document.createElement("div");
            mydiv2.appendChild(this.mytable);
            this.mycontainer.appendChild(mydiv2);
            this.mycontainer.appendChild(mydiv);

            mydiv = document.createElement("div");
            var linkTable = document.createElement("table");
            linkTable.setAttribute("width", "100px");
            var linkRow = document.createElement("tr");
            var linkCell = document.createElement("td");
            linkCell.setAttribute("align", "left");
            var mylink = this.createCustomAnchor("javascript:void(0)", "anchorClass", "Previous page", undefined,
            (function (obj) {
                return function () {
                    obj.goToPreviousPage();
                }
            })(this));
            linkCell.appendChild(mylink);
            linkRow.appendChild(linkCell);

            linkCell = document.createElement("td");
            linkCell.setAttribute("align", "right");
            var mylink = this.createCustomAnchor("javascript:void(0)", "anchorClass", "Next page", undefined,
             (function (obj) {
                return function () {
                    obj.goToNextPage();
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
    }


    this.iterator = function (items) {
        this.mycontainer = document.createElement("div");
        this.mytable = document.createElement("table");

        this.mytable.setAttribute("cellspacing", "10px");

        $(this.divElement).html("");
        this.columnId = 1;
        this.currentImageId = 1;

        for (var i = 0; i < items.length; i++) {
            this.createTableCellContents(items[i], items.length);
        }
    }

    this.getImageBrowser = function () {
        $.getJSON(this.imageInfoHandlerName + "?RowCount=" + this.rowCount +
          "&ColumnCount=" + this.columnCount + "&PageNumber=" + this.pageNum,
          (function (obj) {
              return function (items) {
                  obj.iterator(items);
              }
          })(this));
    }


      this.generateBrowser = function () {
        $(document).ready((function (obj) {
            return function () {
                obj.getImageBrowser();
            }
        })(this));
    }

    this.goToPreviousPage = function () {
        if (this.pageNum > 1) {
            this.pageNum--;
            this.getImageBrowser();
        }
    }

    this.calcMaxPageNumber = function () {
        this.maxPageNum = Math.ceil(this.totalImagesCount / (this.rowCount * this.columnCount));
    }

    this.goToNextPage = function () {
        if (this.pageNum < this.maxPageNum) {
            this.pageNum++;
            this.getImageBrowser();
        }
    }

    this.rowSelectionChanged = function (selectionList) {
        var selectedRowValue = selectionList.options[selectionList.selectedIndex].value;
        if (selectedRowValue == "1") {
            this.rowCount = 1;
        }
        if (selectedRowValue == "2") {
            this.rowCount = 2;
        }
        if (selectedRowValue == "3") {
            this.rowCount = 3;
        }
        if (selectedRowValue == "4") {
            this.rowCount = 4;
        }
        if (selectedRowValue == "5") {
            this.rowCount = 5;
        }
        saveArrayInCookie([this.rowCount, this.columnCount], this.userControlId);
        this.calcMaxPageNumber();
        this.getImageBrowser();
    }

    this.columnSelectionChanged = function (selectionList) {
        var selectedColumnValue = selectionList.options[selectionList.selectedIndex].value;
        if (selectedColumnValue == "1") {
            this.columnCount = 1;
        }
        if (selectedColumnValue == "2") {
            this.columnCount = 2;
        }
        if (selectedColumnValue == "3") {
            this.columnCount = 3;
        }
        if (selectedColumnValue == "4") {
            this.columnCount = 4;
        }
        if (selectedColumnValue == "5") {
            this.columnCount = 5;
        }
        saveArrayInCookie([this.rowCount, this.columnCount], this.userControlId);
        this.calcMaxPageNumber();
        this.getImageBrowser();
    }
}


