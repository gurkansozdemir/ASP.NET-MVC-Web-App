/// <reference path="../../../Dist/Bootstrap/js/jquery-1.9.1.js" />
function ForgotPassword() {
    $("#form-forgotPassword").submit(function (e) {
        e.preventDefault();
        var data = $("#form-forgotPassword").serialize();
        $.ajax({
            type: "POST",
            url: "/User/ForgotPassword",
            dataType: "JSON",
            data: data,
            success: function (response) {
                if (response == "Mail Sent") {
                    $("#mailSentSuccess").modal({});
                }
                else if (response == "Email Adress Not Registered") {
                    $("#emailAdressNotRegistered").modal({});
                }
                else {
                    console.log("Ajax successfuly but an error occurred: " + response);
                }
            },
            error: function (err) {
                console.log("Ajax error: " + err);
            }
        });
    });
}