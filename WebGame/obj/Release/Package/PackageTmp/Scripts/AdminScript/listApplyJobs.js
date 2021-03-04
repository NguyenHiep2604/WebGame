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
        title: "View details apply job",
        visible: false,
        modal: true
    };
    $("#detailsDialog").kendoWindow(windowOptions);
});
function onDataBound(arg) {
    $("#gridApplyJobs > div.k-grid-content.k-auto-scrollable").css('height', (innerHeight - 240) + 'px');

    $('#gridApplyJobs .k-grid-View').click(function (e) {
        var row = $(e.target).closest("tr");
        var grid = $('#gridApplyJobs').data('kendoGrid');
        var dataItem = grid.dataItem(row);
        $.ajax({
            url: "/Admin/GetbyIDApplyJobs",
            typr: "GET",
            data: { ID: dataItem.ID },
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (item) {
                $('#ID').val(item.ID);
                $('#FullName').text("Full name: " + item.FullName);
                $('#Email').text("Email: " + item.Email);
                $('#Phone').text("Phone: " + item.Phone);
                $('#CurrentCompany').text("Current company: " + item.CurrentCompany);
                $('#LinkedInURL').text("Linked URL: " + item.LinkedInURL);
                $('#TwitterURL').text("Twitter URL: " + item.TwitterURL);
                $('#GithubURL').text("Github URL: " + item.GithubURL);
                $('#PortfolioURL').text("Portfolio URL: " + item.PortfolioURL);
                $('#Otherwebsite').text("Other website: " + item.OtherWebsite);
                $('#More').text(item.More);
                $('#detailsDialog').data('kendoWindow').center().open();
            }
        });
    });
}

jQuery("#gridApplyJobs").kendoGrid({
    "columns": [
        { "title": "FullName", "width": "200px", "field": "FullName", "filterable": {}, "encoded": true, "editor": null },
        { "title": "Email", "width": "200px", "field": "Email", "filterable": {}, "encoded": true, "editor": null },
        { "title": "Phone", "width": "200px", "field": "GameTitle", "filterable": {}, "encoded": true, "editor": null },
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
            "read": { "url": "/Admin/ReadListApplyJobs" },
            "destroy": { "url": "/Admin/DeleteApplyJobs" },
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
                    "Phone": { "type": "string" },
                    "CurrentCompany": { "type": "string" },
                    "LinkedInURL": { "type": "string" },
                    "TwitterURL": { "type": "string" },
                    "GithubURL": { "type": "string" },
                    "PortfolioURL": { "type": "string" },
                    "OtherWebsite": { "type": "string" },
                    "More": { "type": "string" }
                }
            }
        }
    }
})