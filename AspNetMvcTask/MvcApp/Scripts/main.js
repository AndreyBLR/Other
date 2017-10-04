var el_with_validate = [ { el: "formCreatingEmp" }, {el: "employeeTable" }];
$(function () {
    $("#formCreatingEmp input[title='create']").click(create);
});

function create() {
    for (var i = 0; i < el_with_validate.length; i++) {
        clearSpanError("#" + el_with_validate[i].el);
    }
    reset("#" + el_with_validate[1].el);

    var isSend = true;
    var table = $("#" + el_with_validate[0].el);

    var name = table.find("input[name='Name']").attr("value");
    var birthday = table.find("input[name='Birthday']").attr("value");
    var dayOfEmployment = table.find("input[name='DayOfEmployment']").attr("value");
    var spanForError = table.find("span[title='errors']");

    if (!requiredValidate(name, spanForError, "Field 'Name' must not be empty")) {
        table.find("input[name='Name']").attr("value", "");
        isSend = false;
    }
    if (!requiredValidate(birthday, spanForError, "Field 'Birthday' must not be empty") || !regExpValidate(birthday, spanForError, "Date birthday must match the pattern mm/dd/yyyy") || !rangeValidator(birthday, "01/01/1900", spanForError, "Birthday should be more 01/01/1900") || !compareValidate(birthday, dayOfEmployment, compareDate1, spanForError, "Introduced date birthday more than a day of employment")) {
        table.find("input[name='Birthday']").attr("value", "");
        isSend = false;
    }
    if (!requiredValidate(dayOfEmployment, spanForError, "Field 'Day of employment' must not be empty") || !regExpValidate(dayOfEmployment, spanForError, "Date employment must match the pattern mm/dd/yyyy") || !rangeValidator(dayOfEmployment, "01/01/1900", spanForError, "Day of employment should be more 01/01/1900") || !compareValidate(birthday, dayOfEmployment, compareDate2, spanForError, "Employee age should be more than 18 years")) {
        table.find("input[name='DayOfEmployment']").attr("value", "");
        isSend = false;
    }

    if (isSend) {
        $.post("/FormCreatingEmp/CreateEmployee", {
                                                    Name: name,
                                                    Birthday: birthday,
                                                    DayOfEmployment: dayOfEmployment
                                                  },
                                                  function (data) {
                                                      var input_el = table.find("input[type='text']");
                                                      for (var i = 0; i < input_el.length; i++) {
                                                          input_el[i].value = "";
                                                      }
                                                      $("#employees_div").load("/Employees/ShowEmployeeTable");
                                                  }
        );
    }
    return false;
}

function edit(source) {
    for (var i = 0; i < el_with_validate.length; i++) {
        clearSpanError("#" + el_with_validate[i].el);
    }
    reset("#" + el_with_validate[1].el);

    var row = source.parentNode.parentNode;
    editMode(row, true);
}

function deleteRow(source) {
    for (var i = 0; i < el_with_validate.length; i++) {
        clearSpanError("#" + el_with_validate[i].el);
    }
    reset("#" + el_with_validate[1].el);

    var row = source.parentNode.parentNode;
    var columns = row.children;
    $.post("/Employees/DeleteEmployee", {
                                            Id: columns[4].children[0].value
                                        },
                                        function (data) {
                                            $("#employees_div").load("/Employees/ShowEmployeeTable");
                                        }
    );
}

function save(source) {
    for (var i = 0; i < el_with_validate.length; i++) {
        clearSpanError("#" + el_with_validate[i].el);
    }
    reset("#" + el_with_validate[1].el);

    var table = $("#" + el_with_validate[1].el);
    var isSend = true;
    var row = source.parentNode.parentNode;
    var columns = row.children;

    var id = columns[4].children[0].value;
    var name = columns[0].children[1].value;
    var birthday = columns[1].children[1].value;
    var dayOfEmployment = columns[2].children[1].value;
    var spanForError = table.find("span[title='errors']");

    if (!requiredValidate(name, spanForError, "Field 'Name' must not be empty")) {
        columns[0].className = "input-validation-error";
        isSend = false;
    }
    if (!requiredValidate(birthday, spanForError, "Field 'Birthday' must not be empty") || !regExpValidate(birthday, spanForError, "Date birthday must match the pattern mm/dd/yyyy") || !rangeValidator(birthday, "01/01/1900", spanForError, "Birthday should be more 01/01/1900") || !compareValidate(birthday, dayOfEmployment, compareDate1, spanForError, "Introduced date birthday more than a day of employment")) {
        columns[1].className = "input-validation-error";
        isSend = false;
    }
    if (!requiredValidate(dayOfEmployment, spanForError, "Field 'Day of employment' must not be empty") || !regExpValidate(dayOfEmployment, spanForError, "Date employment must match the pattern mm/dd/yyyy") || !rangeValidator(dayOfEmployment, "01/01/1900", spanForError, "Day of employment should be more 01/01/1900") || !compareValidate(birthday, dayOfEmployment, compareDate2, spanForError, "Employee age should be more than 18 years")) {
        columns[2].className = "input-validation-error";
        isSend = false;
    }
    if(isSend){
        $.post("/Employees/UpdateEmployee", {
                                                Id: id,
                                                Name: name,
                                                Birthday: birthday,
                                                DayOfEmployment: dayOfEmployment
                                            },
                                            function (data) {
                                                $("#employees_div").load("/Employees/ShowEmployeeTable");
                                            }
        );
    }    
    editMode(row, false);
}

function editMode(row, bool) {
    var columns = row.children;
    if (bool == true) {
        for (var i = 0; i <= 2; i++) {
            columns[i].children[0].style.display = "none";
            columns[i].children[1].style.display = "inline";
            columns[i].children[1].value = columns[i].children[0].innerHTML;
        }
        $(row).find("input[title='save']").attr("style", "display:inline");
        $(row).find("input[title='edit']").attr("style", "display:none");
    }
    else {
        for (var i = 0; i <= 2; i++) {
            columns[i].children[0].style.display = "inline";
            columns[i].children[1].style.display = "none";
        }
        $(row).find("input[title='save']").attr("style", "display:none");
        $(row).find("input[title='edit']").attr("style", "display:inline");
    }
}

function requiredValidate(data, span, textError) {
    if (data.length == 0) {
        if (span.attr("innerHTML") != "") {
            textError = "<br />" + textError;
        }
        span.attr("innerHTML", span.attr("innerHTML") + textError);
        return false;
    }
    return true;
}

function regExpValidate(data, span, textError) {
    var reg = /^\d{2}\/\d{2}\/\d{4}/;
    if (!reg.test(data)) {
        if (span.attr("innerHTML") != "") {
            textError = "<br />" + textError;
        }
        span.attr("innerHTML", span.attr("innerHTML") + textError);
        return false;
    }
    return true;
}

function compareValidate(value1, value2, func_compare, span, textError) {
    if (!func_compare(value1, value2)) {
        if (span.attr("innerHTML") != "") {
            textError = "<br />" + textError;
        }
        span.attr("innerHTML", span.attr("innerHTML") + textError);
        return false;
    }
    return true;
}

function rangeValidator(value, min, span, textError) {
    if (compareDate1(value, min)) {
        if (span.attr("innerHTML") != "") {
            textError = "<br />" + textError;
        }
        span.attr("innerHTML", span.attr("innerHTML") + textError);
        return false;
    }
    return true;
}

function compareDate1(arg_1, arg_2) {
    var date_1 = new Date(arg_1);
    var date_2 = new Date(arg_2);
    if (date_1 < date_2) {
        return true;
    }
    return false;
}

function compareDate2(arg_1, arg_2) {
    var date_1 = new Date(arg_1);
    var date_2 = new Date(arg_2);

    if (date_2.getFullYear() - date_1.getFullYear() >= 18) {
        return true;
    }
    return false;
}

function clearSpanError(el_id) {
    $(el_id + " span[title='errors']").attr("innerHTML", "");
}

function reset(table_id) {
    var table = $(table_id);
    var cells = table.find("tr td[className='input-validation-error']");
    for (var i = 0; i < cells.length; i++) {
        cells[i].className = "";
    }
}

