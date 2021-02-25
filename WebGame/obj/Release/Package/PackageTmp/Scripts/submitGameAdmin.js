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
        height: "720px",
        title: "View Details Game Submit",
        visible: false,
        modal: true
    };
    $("#detailsDialog").kendoWindow(windowOptions);
});
function onDataBound(arg) {
    $("#gridSubmitGame > div.k-grid-content.k-auto-scrollable").css('height', (innerHeight - 240) + 'px');

    $('#gridSubmitGame .k-grid-view').click(function (e) {
        var row = $(e.target).closest("tr");
        var grid = $('#gridSubmitGame').data('kendoGrid');
        var dataItem = grid.dataItem(row);
        $.ajax({
            url: "/Admin/GetbyIDGameSubmit",
            typr: "GET",
            data: { ID: dataItem.ID },
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (item) {
                $('#ID').val(item.ID);
                $('#FullName').val(item.FullName);
                $('#Email').val(item.Email);
                $('#GameTitle').val(item.GameTitle);
                $('#VideoFootageLink').val(item.VideoFootageLink);
                $('#CompanyName').val(item.CompanyName);
                $('#Country').val(item.Country);
                $('#LinkAppStore').val(item.LinkAppStore);
                $('#MoreAbout').val(item.MoreAbout);
                $('#DateSubmit').val(item.DateSubmit);
                $('#detailsDialog').data('kendoWindow').center().open();
            }
        });
    });
}

jQuery("#gridSubmitGame").kendoGrid({
    "columns": [
        { "title": "FullName", "width": "200px", "field": "FullName", "filterable": {}, "encoded": true, "editor": null },
        { "title": "Email", "width": "200px", "field": "Email", "filterable": {}, "encoded": true, "editor": null },
        { "title": "Game Title", "width": "200px", "field": "GameTitle", "filterable": {}, "encoded": true, "editor": null },
        { "title": "Company Name", "width": "200px", "field": "CompanyName", "filterable": {}, "encoded": true, "editor": null },
        { "title": "Country", "width": "200px", "field": "Country", "filterable": {}, "encoded": true, "editor": null },
        { "title": "Date Submit", "format": "{0:dd/MM/yyyy}", "width": "150px", "field": "DateSubmit", "filterable": {}, "encoded": true, "editor": null },
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
            "read": { "url": "/Admin/ListSubmitGame" },
            "destroy": { "url": "/Admin/DeleteSubmitGame" },
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
                    "GameTitle": { "type": "string" },
                    "VideoFootageLink": { "type": "string" },
                    "CompanyName": { "type": "string" },
                    "Country": { "type": "string" },
                    "LinkAppStore": { "type": "string" },
                    "MoreAbout": { "type": "string" },
                    "DateSubmit": { "type": "date" }
                }
            }
        }
    }
})