$(document).ready(function () {
    $('#exampleModal').on('shown.bs.modal', function () {
        $('.FullName').focus();
    });

    $('#gameSubmit').click(function (e) {
        var formData = new FormData();
        formData.append('FullName', $('.FullName').val());
        formData.append('Email', $('.Email').val());
        formData.append('Title', $('.Title').val());
        formData.append('FootageLink', $('.FootageLink').val());
        formData.append('Featured', $('#Featured').val());
        formData.append('CompanyName', $('.CompanyName').val());
        formData.append('Country', $('.Country').val());
        formData.append('LinkStore', $('.LinkStore').val());
        formData.append('MoreAbout', $('.MoreAbout').val());
        formData.append('g-Recaptcha-Response', reCaptchaResponse);
        $.ajax({
            url: '/Home/SubmitGame',
            data: formData,
            processData: false,
            contentType: false,
            type: 'POST',
            success: function (data) {
                $("#exampleModal .close").click();
                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Submitted successfully !',
                    showConfirmButton: false,
                    timer: 1500
                })
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    })


    $('.btnSubmit').click(function (e) {
        var formData = new FormData();
        formData.append('FullName', $('.FullName').val());
        formData.append('Email', $('.Email').val());
        formData.append('Title', $('.GameTitle').val());
        formData.append('FootageLink', $('.FootageLink').val());
        //formData.append('Featured', $('#Featured').val());
        formData.append('CompanyName', $('.CompanyName').val());
        formData.append('Country', $('.Country').val());
        formData.append('LinkStore', $('.LinkStore').val());
        formData.append('MoreAbout', $('.MoreAbout').val());
        formData.append('g-Recaptcha-Response', reCaptchaResponse);
        $.ajax({
            url: '/Home/SubmitGame',
            data: formData,
            processData: false,
            contentType: false,
            type: 'POST',
            success: function (data) {
                grecaptcha.reset();
                document.getElementsByClassName('formSubmitGame')[0].reset();
                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Submitted successfully !',
                    showConfirmButton: false,
                    timer: 1500
                })
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    })

});