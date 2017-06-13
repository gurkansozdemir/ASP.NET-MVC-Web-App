/// <reference path="../../../Dist/Bootstrap/js/jquery-1.9.1.js" />
$(document).on('change', '#rememberMe', function () {
    if (this.checked) {
        $("#rememberMe").val("on");
    } else {
        $("#rememberMe").val("off");
    }
});

function login() {
    $("#form-login").submit(function (e) {
        e.preventDefault();
        var data = $("#form-login").serialize();
        $.ajax({
            type: "POST",
            url: "/user/login",
            data: data,
            dataType: "JSON",
            success: function (response) {
                if (response == "Login Successfuly") {
                    window.location = "/ürün/listele";
                }
                else if (response == "Faulty Username") {
                    alert("Hatalı Kullanıcı Adı");
                }
                else if (response == "Faulty Password") {
                    alert("Hatalı Parola");
                }
                else {
                    console.log("Success Error: " + response);
                }
            },
            error: function (err) {
                console.log("Ajax Error: " + err);
            }
        });
    });
}