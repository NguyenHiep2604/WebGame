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
function onDataBound(arg) {
    $("#gridListAccount > div.k-grid-content.k-auto-scrollable").css('height', (innerHeight - 240) + 'px');
}

jQuery("#gridListAccount").kendoGrid({
    "columns": [
        { "title": "UserName", "width": "200px", "field": "UserName", "filterable": {}, "encoded": true, "editor": null },
        { "title": "Email", "width": "200px", "field": "Email", "filterable": {}, "encoded": true, "editor": null },
        { "title": "PassWord", "width": "200px", "field": "PassWord", "filterable": {}, "encoded": true, "editor": null },
        { "command": ["edit", "destroy"], title: "&nbsp;", width: "250px" }
    ],
    "editable": "inline",
    "selectable": "row",
    "filterable": true,
    "navigatable": true,
    "dataBound": onDataBound,
    "scrollable": true,
    "pageable": { "pageSizes": [5, 10, 20], "buttonCount": 5 },
    "sortable": true,
    "toolbar": ["create", "search"],
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
            "read": { "url": "/Admin/ReadAccountList" },
            "update": { "url": "/Admin/UpdateAccount" },
            "destroy": { "url": "/Admin/DeleteAccount" },
            "create": { "url": "/Admin/CreateAccount" },
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
                    "UserName": { "type": "string" },
                    "Email": { "type": "string" },
                    "PassWord": { "type": "string" }
                }
            }
        }
    }
})