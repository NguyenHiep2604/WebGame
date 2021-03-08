$('#submitApplyJob').click(function (e) {
    var formData = new FormData();
    formData.append('FullName', $('#FullName').val());
    formData.append('Email', $('#Email').val());
    formData.append('Phone', $('#Title').val());
    formData.append('CurrentCompany', $('#CurrentCompany').val());
    formData.append('CV', $('#CV')[0].files[0]);
    formData.append('LinkedInURL', $('#LinkedInURL').val());
    formData.append('TwitterURL', $('#TwitterURL').val());
    formData.append('GithubURL', $('#GithubURL').val());
    formData.append('PortfolioURL', $('#PortfolioURL').val());
    formData.append('OtherWebsite', $('#OtherWebsite').val());
    formData.append('More', $('.more').val());
    $.ajax({
        url: '/Home/SubmitApplyJob',
        data: formData,
        processData: false,
        contentType: false,
        type: 'POST',
        success: function (data) {
            Swal.fire({
                position: 'top-end',
                icon: 'success',
                title: 'Update game successfully !',
                showConfirmButton: false,
                timer: 1500
            })
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
})