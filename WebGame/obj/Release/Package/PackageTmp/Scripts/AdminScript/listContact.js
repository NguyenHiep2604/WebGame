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
        width: "350px",
        height: "300px",
        title: "View details contact",
        visible: false,
        modal: true
    };
    $("#detailsDialog").kendoWindow(windowOptions);
});
function onDataBound(arg) {
    $("#gridListContact > div.k-grid-content.k-auto-scrollable").css('height', (innerHeight - 240) + 'px');

    $('#gridListContact .k-grid-View').click(function (e) {
        var row = $(e.target).closest("tr");
        var grid = $('#gridListContact').data('kendoGrid');
        var dataItem = grid.dataItem(row);
        $.ajax({
            url: "/Admin/GetContactbyID",
            typr: "GET",
            data: { ID: dataItem.ID },
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (item) {
                $('#ID').val(item.ID);
                $('#FullName').text("Full name: " + item.FullName);
                $('#Email').text("Email: " + item.Email);
                $('#Subject').text("Subject: " + item.Subject);
                $('#GameName').text("Game:" + item.GameName);
                $('#Message').text("Message:" + item.Message);
                $('#detailsDialog').data('kendoWindow').center().open();
            }
        });
    });
}

jQuery("#gridListContact").kendoGrid({
    "columns": [
        { "title": "Full Name", "width": "200px", "field": "FullName", "filterable": {}, "encoded": true, "editor": null },
        { "title": "Email", "width": "200px", "field": "Email", "filterable": {}, "encoded": true, "editor": null },
        { "title": "Subject", "width": "200px", "field": "Subject", "filterable": {}, "encoded": true, "editor": null },
        { "title": "Game Name", "width": "200px", "field": "Game", "filterable": {}, "encoded": true, "editor": null, "template": "#=Game.NameGame#" },
        { "title": "Message", "width": "200px", "field": "Message", "filterable": {}, "encoded": true, "editor": null },
        { "command": ["View", "destroy"], title: "&nbsp;", width: "200px" }
    ],
    "editable": "inline",
    "selectable": "row",
    "filterable": true,
    "navigatable": true,
    "dataBound": onDataBound,
    "scrollable": true,
    "pageable": { "pageSizes": [5, 10, 20], "buttonCount": 5 },
    "sortable": true,
    "toolbar": ["search"],
    "search": {
        "fields": ["Email"]
    },
    "dataSource": {
        "type": (function () {
            if (kendo.data.transports['aspnetmvc-ajax']) {
                return 'aspnetmvc-ajax';
            } else { throw new Error('The kendo.aspnetmvc.min.js script is not included.'); }
        })(),
        "transport": {
            "read": { "url": "/Admin/ReadListContact" },
            "destroy": { "url": "/Admin/DeleteContact" },
        },
        "pageSize": 10, "page": 1,
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
                    "FullName": { "type": "string" },
                    "Email": { "type": "string" },
                    "Subject": { "type": "string" },
                    "Game": { "type": "object" },
                    "Message": { "type": "string" }
                }
            }
        }
    }
})