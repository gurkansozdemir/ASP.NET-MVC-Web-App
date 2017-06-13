/// <reference path="../../../Dist/Bootstrap/js/jquery-1.9.1.js" />

//$(document).ready(function () {
//    $.ajax({
//        type: "POST",
//        url: "/User/ResetPassword?newPassword=123&resetTime=22/11/2009&email=gurkansozdemir@gmail.com",
//        dataType: "JSON",
//        success: function (response) {
//            if (response == "Can Reset") {
//                window.location("/User/ResetPassword");
//            }
//            else if (response == "User Can't Registered") {
//                alert("E-Posta Kayıtlı Değil.");
//            }
//            else if (response == "Time Out") {
//                alert("Başlantı süresi dolmuştur, lütfen yeniden parola sıfırlama isteği gönderin.");
//            }
//            else if (response == "Not Authorization") {
//                alert("Bu sayfaya giriş yetkiniz bulunmamaktadır.");
//            }
//            else {
//                console.log("Ajax Successfuly but an error occourred: " + response);
//            }
//        },
//        error: function (err) {
//            console.log("Ajax Error: " + err);
//        }
//    });
//});

function PasswordReset() {
    $("#form-resetPassword").submit(function (e) {
        e.preventDefault();
        var data = $("#form-resetPassword").serialize();
        $.ajax({
            type: "POST",
            url: "/User/ResetPassword",
            dataType: "JSON",
            data: data,
            success: function (response) {
                if (response == "Success") {
                    alert("Şifreniz güncellendi");
                }
                else {
                    console.log("Ajax successfuly but an error occourred: " + response);
                }
            },
            error: function (err) {
                console.log("Ajax Error: " + err);
            }
        });
    });
}