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
        actions: ["Minimize", "Maximize", "Close"],
        draggable: true,
        resizable: true,
        width: "500px",
        height: "500px",
        title: "Create/Edit",
        visible: false,
        modal: true
    };
    $("#detailsDialog").kendoWindow(windowOptions);
});
//--Function save--\\
function btnSave() {
    var formData = new FormData();
    formData.append('Name', $('#Name').val());
    formData.append('LinkIOS', $('#LinkIOS').val());
    formData.append('LinkAndroid', $('#LinkAndroid').val());
    formData.append('Caption', $('#Caption').val());
    formData.append('Featured', $('#Featured').val());
    formData.append('Image', $('#Image')[0].files[0]);
    $.ajax({
        url: '/Admin/AddNewGame',
        data: formData,
        processData: false,
        contentType: false,
        type: 'POST',
        success: function (data) {
            $('#gridListGame').data('kendoGrid').dataSource.read();
            $('#detailsDialog').data('kendoWindow').close();
            Swal.fire({
                position: 'top-end',
                icon: 'success',
                title: 'Add game successfully !',
                showConfirmButton: false,
                timer: 1500
            })
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//--Function update--\\
function btnUpdate() {
    var formData = new FormData();
    formData.append('ID', $('#ID').val());
    formData.append('Name', $('#Name').val());
    formData.append('LinkIOS', $('#LinkIOS').val());
    formData.append('LinkAndroid', $('#LinkAndroid').val());
    formData.append('Caption', $('#Caption').val());
    formData.append('Featured', $('#Featured').val());
    formData.append('Image', $('#Image')[0].files[0]);
    $.ajax({
        url: "/Admin/UpdateGame",
        data: formData,
        processData: false,
        contentType: false,
        type: 'POST',
        success: function (data) {
            $('#gridListGame').data('kendoGrid').dataSource.read();
            $('#detailsDialog').data('kendoWindow').close();
            Swal.fire({
                position: 'top-end',
                icon: 'success',
                title: 'Update game successfully !',
                showConfirmButton: false,
                timer: 1500
            })
        }
    });
}
//-------------\\
//--DataonBound--\\
function onDataBound(arg) {
    $("#gridListGame > div.k-grid-content.k-auto-scrollable").css('height', (innerHeight - 240) + 'px');

    $('#gridListGame .k-grid-edit').click(function (e) {
        var row = $(e.target).closest("tr");
        var grid = $('#gridListGame').data('kendoGrid');
        var dataItem = grid.dataItem(row);
        $.ajax({
            url: "/Admin/GetbyIDGame",
            typr: "GET",
            data: { ID: dataItem.ID },
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (item) {
                $('#ID').val(item.ID);
                $('#Name').val(item.Name);
                $('#LinkIOS').val(item.LinkIOS);
                $('#LinkAndroid').val(item.LinkAndroid);
                $('#Caption').val(item.Caption);
                $('#Featured').val(item.Featured_Games);
                $('#detailsDialog').data('kendoWindow').center().open();
                $('#btnSave').hide();
            }
        });
    });

    $('#gridListGame .k-grid-add').click(function (e) {
        $('#detailsDialog').data('kendoWindow').center().open();
        $('#ID').val('');
        $('#Name').val('');
        $('#LinkIOS').val('');
        $('#LinkAndroid').val('');
        $('#Caption').val('');
        $('#Image').val('');
        $('#btnUpdate').hide();
        $('#btnSave').show();
    });

    $('#gridListGame .k-grid-delete').click(function (e) {
        var row = $(e.target).closest("tr");
        var grid = $('#gridListGame').data('kendoGrid');
        var dataItem = grid.dataItem(row);
        var ans = confirm("Are you sure you want to delete this Record?");
        if (ans) {
            $.ajax({
                url: "/Admin/DeleteGame/" + dataItem.ID,
                type: "POST",
                contentType: "application/json;charset=UTF-8",
                dataType: "json",
                success: function (result) {
                    $('#gridListGame').data('kendoGrid').dataSource.read();
                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: 'Delete game successfully !',
                        showConfirmButton: false,
                        timer: 1500
                    })
                },
                error: function (errormessage) {
                    alert("Can not delete item! Please try again!");
                }
            });
        }
    });
}
//--------------\\
//--Grid Teacher--\\
jQuery("#gridListGame").kendoGrid({
    "columns": [
        { "title": "Tên Game", "width": "200px", "field": "Name", "filterable": {}, "encoded": true, "editor": null },
        {
            "title": "Hình ảnh",
            "width": "200px",
            "template": function (dataItem) {
                return "<img class='image-game' src='/Admin/ViewImage?id=" + (dataItem.ID) + "&time=" + new Date().getTime() +"'/>";
            }
        },
        { "title": "Đường dẫn AppStore", "width": "200px", "field": "LinkIOS", "filterable": {}, "encoded": true, "editor": null },
        { "title": "Đường dẫn GooglePlay", "width": "200px", "field": "LinkAndroid", "filterable": {}, "encoded": true, "editor": null },
        { "title": "Chú thích", "width": "200px", "field": "Caption", "filterable": {}, "encoded": true, "editor": null },
        { "command": ["edit", "destroy"], title: "&nbsp;", width: "250px" }
    ],
    "selectable": "row",
    "filterable": true,
    "navigatable": true,
    "dataBound": onDataBound,
    "scrollable": true,
    "pageable": { "pageSizes": [5, 10, 20], "buttonCount": 5 },
    "sortable": true,
    "toolbar": ["create", "search"],
    "search": {
        "fields": ["Name"]
    },
    "dataSource": {
        "type": (function () {
            if (kendo.data.transports['aspnetmvc-ajax']) {
                return 'aspnetmvc-ajax';
            } else { throw new Error('The kendo.aspnetmvc.min.js script is not included.'); }
        })(),
        "transport": {
            "read": { "url": "/Admin/ListGame/" },
            "prefix": ""
        },
        "pageSize": 5, "page": 1,
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
                    "ID": { "type": "number", "editable": false },
                    "Name": { "type": "string" },
                    "Image": { "type": "string" },
                    "LinkIOS": { "type": "string" },
                    "LinkAndroid": { "type": "string" },
                    "Caption": { "type": "string" }
                }
            }
        }
    }
})