$('.btnSubmit').click(function (e) {
    var formData = new FormData();
    formData.append('FullName', $('.FullName').val());
    formData.append('Email', $('.Email').val());
    formData.append('Subject', $('.Subject').val());
    formData.append('Optional', $('.Optional').val());
    formData.append('Message', $('.Message').val());
    $.ajax({
        url: '/Home/SubmitContact',
        data: formData,
        processData: false,
        contentType: false,
        type: 'POST',
        success: function (data) {
            grecaptcha.reset();
            document.getElementsByClassName('formContact')[0].reset();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
})