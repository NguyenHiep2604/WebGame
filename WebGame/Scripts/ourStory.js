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
//--Load Image--\\
var loadFile1 = function (event) {
    var reader = new FileReader();
    reader.onload = function () {
        var ourput = document.getElementById('output1');
        ourput.src = reader.result;
    };
    $('#output1').show();
    reader.readAsDataURL(event.target.files[0]);
};
var loadFile2 = function (event) {
    var reader = new FileReader();
    reader.onload = function () {
        var ourput = document.getElementById('output2');
        ourput.src = reader.result;
    };
    $('#output2').show();
    reader.readAsDataURL(event.target.files[0]);
};


$(document).ready(function () {
    var windowOptions = {
        actions: ["Minimize", "Maximize", "Close"],
        draggable: true,
        resizable: true,
        width: "500px",
        height: "800px",
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
    formData.append('Picture1', $('#PictureMaxWidth')[0].files[0]);
    formData.append('Picture2', $('#Picture640Width')[0].files[0]);
    formData.append('Title', encodeURIComponent(CKEDITOR.instances.Title.getData()))
    $.ajax({
        url: '/Admin/AddStory',
        data: formData,
        processData: false,
        contentType: false,
        type: 'POST',
        success: function (data) {
            $('#gridOurStory').data('kendoGrid').dataSource.read();
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
    formData.append('Picture1', $('#PictureMaxWidth')[0].files[0]);
    formData.append('Picture2', $('#Picture640Width')[0].files[0]);
    formData.append('Title', encodeURIComponent(CKEDITOR.instances.Title.getData()))
    $.ajax({
        url: "/Admin/UpdateStory",
        data: formData,
        processData: false,
        contentType: false,
        type: 'POST',
        success: function (data) {
            $('#gridOurStory').data('kendoGrid').dataSource.read();
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
    $("#gridOurStory > div.k-grid-content.k-auto-scrollable").css('height', (innerHeight - 240) + 'px');

    $('#gridOurStory .k-grid-edit').click(function (e) {
        var row = $(e.target).closest("tr");
        var grid = $('#gridOurStory').data('kendoGrid');
        var dataItem = grid.dataItem(row);
        $.ajax({
            url: "/Admin/GetStorybyID",
            typr: "GET",
            data: { ID: dataItem.ID },
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (item) {
                $('#ID').val(item.ID);
                $('#Name').val(item.OurStoryName);
                CKEDITOR.instances.Title.setData(decodeURIComponent(item.Title));
                $('#detailsDialog').data('kendoWindow').center().open();
                $('#btnSave').hide();
                $('#btnUpdate').show();
            }
        });
    });

    $('#gridOurStory .k-grid-add').click(function (e) {
        $('#detailsDialog').data('kendoWindow').center().open();
        $('#ID').val('');
        $('#Name').val('');
        $('#Title').val('');
        $('#PictureMaxWidth').val('');
        $('#Picture460Width').val('');
        $('#output1').hide();
        $('#output2').hide();
        $('#btnUpdate').hide();
        $('#btnSave').show();
    });

    $('#gridOurStory .k-grid-delete').click(function (e) {
        var row = $(e.target).closest("tr");
        var grid = $('#gridOurStory').data('kendoGrid');
        var dataItem = grid.dataItem(row);
        var ans = confirm("Are you sure you want to delete this Record?");
        if (ans) {
            $.ajax({
                url: "/Admin/DeleteStory/" + dataItem.ID,
                type: "POST",
                contentType: "application/json;charset=UTF-8",
                dataType: "json",
                success: function (result) {
                    $('#gridOurStory').data('kendoGrid').dataSource.read();
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
jQuery("#gridOurStory").kendoGrid({
    "columns": [
        { "title": "Name", "width": "200px", "field": "OurStoryName", "filterable": {}, "encoded": true, "editor": null },
        {
            "title": "Picture Max Width",
            "width": "400px",
            "template": function (dataItem) {
                return "<img class='picture1' src='/Admin/ViewImageStory1?id=" + (dataItem.ID) + "&time=" + new Date().getTime() + "'/>";
            }
        },
        {
            "title": "Picture 640 Width",
            "width": "400px",
            "template": function (dataItem) {
                return "<img class='picture2' src='/Admin/ViewImageStory2?id=" + (dataItem.ID) + "&time=" + new Date().getTime() + "'/>";
            }
        },
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
            "read": { "url": "/Admin/ReadListStory/" },
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
                    "OurStoryName": { "type": "string" },
                    "Title": { "type": "string" },
                    "PictureMaxWidth": { "type": "string" },
                    "Picture640Width": { "type": "string" }
                }
            }
        }
    }
})