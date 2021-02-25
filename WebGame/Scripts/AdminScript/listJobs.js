function error_handler(e) {
    if (e.errors) {
        var message = "Errors:\n";
        $.each(e.errors, function (key, value) {
            if ('errors' in value) {
                $.each(value.errors, function () {
                    message += this + "\n";
                });
            }
        });
        alert(message);
    }
}

$(document).ready(function () {
    var windowOptions = {
        actions: ["Custom", "Minimize", "Maximize", "Close"],
        draggable: true,
        resizable: true,
        width: "800px",
        height: "800px",
        title: "Create/Edit News",
        visible: false,
    };
    $("#detailsDialog").kendoWindow(windowOptions);
});
//Create
function createItem() {
    $("#Team").val();
    $("#Location").val();
    $("#Vacancies").val();
    CKEDITOR.instances.Description.setData(null);
    $("#btnUpdate").hide();
    $('#detailsDialog').data('kendoWindow').center().open();
}
//Edit
function editItem(btnEdit) {
    var dataID = $('#gridJobs').data('kendoGrid').dataItem(btnEdit.closest('tr')).ID;
    $.ajax({
        url: "/Admin/GetbyIDJob",
        typr: "GET",
        data: { ID: dataID },
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (item) {
            $('#ID').val(item.ID);
            $('#Team').val(item.Team);
            $('#Location').val(item.Location);
            $('#Vacancies').val(item.Vacancies);
            CKEDITOR.instances.Description.setData(decodeURIComponent(item.Description));
            $('#detailsDialog').data('kendoWindow').center().open();
            $("#btnSave").hide();
            $("#btnUpdate").show();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}
//Delete
function deleteItem(btnDelete) {
    var dataID = $('#gridJobs').data('kendoGrid').dataItem(btnDelete.closest('tr')).ID;
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Admin/DeleteJob/" + dataID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                $('#gridJobs').data('kendoGrid').dataSource.read();
            },
            error: function (errormessage) {
                alert("Can not delete item! Please try again!");
            }
        });
    }
}
//Save
function btnSave() {
    //var res = validate();
    //if (res == false) {
    //    return false;
    //}
    var fd = new FormData();
    fd.append('Team', $("#Team").val());
    fd.append('Location', $("#Location").val());
    fd.append('Vacancies', $("#Vacancies").val());
    fd.append('Description', encodeURIComponent(CKEDITOR.instances.Description.getData()));
    $.ajax({
        url: '/Admin/AddJob',
        data: fd,
        processData: false,
        contentType: false,
        type: 'POST',
        success: function (data) {
            $('#gridJobs').data('kendoGrid').dataSource.read();
            $('#detailsDialog').data('kendoWindow').close();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//Update
function btnUpdate() {
    //var res = validate();
    //if (res == false) {
    //    return false;
    //}
    var fd = new FormData();
    fd.append('ID', $("#ID").val());
    fd.append('Team', $("#Team").val());
    fd.append('Location', $("#Location").val());
    fd.append('Vacancies', $("#Vacancies").val());
    fd.append('Description', encodeURIComponent(CKEDITOR.instances.Description.getData()));
    $.ajax({
        url: "/Admin/UpdateJob",
        data: fd,
        processData: false,
        contentType: false,
        type: 'POST',
        success: function (data) {
            $('#gridJobs').data('kendoGrid').dataSource.read();
            $('#detailsDialog').data('kendoWindow').close();
            $('#ID').val("");
            $('#Team').val("");
            $('#Location').val("");
            $('#Vacancies').val("");
            $('#Description').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
//Validation
function validate() {
    var isValid = true;
    if ($('#TieuDe').val().trim() == "") {
        $('#TieuDe').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#TieuDe').css('border-color', 'lightgrey');
    }
    if ($('#MoTa').val().trim() == "") {
        $('#MoTa').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#MoTa').css('border-color', 'lightgrey');
    }
    if ($('#NgayTao').val().trim() == "") {
        $('#NgayTao').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#NgayTao').css('border-color', 'lightgrey');
    }
    if ($('#HinhAnh').val().trim() == "") {
        $('#HinhAnh').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#HinhAnh').css('border-color', 'lightgrey');
    }
    return isValid;
}

//Format Date
function formatDate(NgayTao) {
    var d = new Date(NgayTao),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [day, month, year].join('-');
}
//DataonBound
function onDataBound(arg) {
    $("#gridJobs > div.k-grid-content.k-auto-scrollable").css('height', (innerHeight - 245) + 'px');
}
jQuery("#gridJobs").kendoGrid({
    "columns": [
        { "title": "Nhóm", "width": "120px", "field": "Team", "filterable": {}, "encoded": true, "editor": null },
        { "title": "Khu vực", "width": "120px", "field": "Location", "filterable": {}, "encoded": true, "editor": null },
        { "title": "Vị trí", "width": "120px", "field": "Vacancies", "filterable": {}, "encoded": true, "editor": null },
        {
            "width": "80px",
            "template": "<div> <a role='button' class='k-button k-button-icontext k-grid-edit' onclick='editItem(this)' > <span class='k-icon k-i-edit'></span>Edit</a> <a role='button' class='k-button k-button-icontext k-grid-delete' onclick='deleteItem(this)' > <span class='k-icon k-i-close'></span>Delete</a></div> ",
        }],
    "selectable": "row",
    "dataBound": onDataBound,
    "scrollable": true,
    "pageable":
        { "pageSizes": [5, 10, 20], "buttonCount": 5 },
    "sortable": true,
    toolbar: kendo.template($("#template").html()),
    "dataSource": {
        "type": (function () {
            if (kendo.data.transports['aspnetmvc-ajax']) {
                return 'aspnetmvc-ajax';
            } else { throw new Error('The kendo.aspnetmvc.min.js script is not included.'); }
        })(),
        "transport": {
            "read": { "url": "/Admin/ReadListJobs" }, "prefix": ""
        }, "pageSize": 10, "page": 1,
        "total": 0,
        "serverScrollable": true,
        "serverPaging": true,
        "serverSorting": true,
        "serverFiltering": true,
        "serverGrouping": true, "serverAggregates": true, "filter": [], "error": error_handler,
        "schema": {
            "data": "Data", "total": "Total", "errors": "Errors",
            "model": {
                "id": "ID",
                "fields": {
                    "ID": { "type": "number" },
                    "Team": { "type": "string" },
                    "Location": { "type": "string" },
                    "Description": { "type": "string" },
                    "Vacancies": { "type": "string" }
                }
            }
        }
    }
});