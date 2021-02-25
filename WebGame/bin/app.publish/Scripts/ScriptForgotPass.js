function getID() {
    var full_url = document.URL;
    var url_array = full_url.split('/')
    var last_segment = url_array[url_array.length - 1];

    $.ajax({
        url: "/Account/GetbyIDAccount",
        typr: "GET",
        data: { ID: last_segment },
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (item) {
            $('#ID').val(item.ID);
            $('#Email').val(item.Email);
        },
        error: function (errormessage) {
            alert("Không tìm thấy tài khoản !");
        }
    });
}

function btnSave() {
    if ($(".PassWord").val() != $(".confirmPassword").val()) {
        return false;
    } else {
        var fd = new FormData();
        fd.append('ID', $('#ID').val());
        fd.append('Password', $(".PassWord").val());
    }
    $.ajax({
        url: "/Account/UpdatePassword",
        data: fd,
        processData: false,
        contentType: false,
        type: 'POST'
    });
}