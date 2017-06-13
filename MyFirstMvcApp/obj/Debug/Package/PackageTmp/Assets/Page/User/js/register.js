$(document).ready(function () {

});

function InsertUser() {
    $("#form-register").submit(function (e) {
        e.preventDefault();
    }).validate({
        rules: {
            firstname: {
                required: true
            },
            lastname: {
                required: true
            },
            email: {
                required: true,
                email: true
            },
            username: {
                required: true
            },
            password: {
                required: true
            },
            confirm: {
                required: true,
                equalTo: "#password"
            }
        },
        messages: {
            firstname: {
                required: "Bu alan boş geçilemez!"
            },
            lastname: {
                required: "Bu alan boş geçilemez!"
            },
            email: {
                required: "Bu alan boş geçilemez!",
                email: "Bu mail adresi geçerli değil!"
            },
            username: {
                required: "Bu alan boş geçilemez!"
            },
            password: {
                required: "Bu alan boş geçilemez!"
            },
            confirm: {
                required: "Bu alan boş geçilemez!",
                equalTo: "Şifreler aynı değil!"
            }
        },
        submitHandler: function (form) {
            var data = $(form).serialize();
            $.ajax({
                type: "POST",
                url: "/User/Register",
                dataType: "JSON",
                data: data,
                success: function (response) {
                    if (response == "Success") {
                        $('#insertUserSuccessModal').modal();
                    }
                    else {
                        console.log("Ajax successfuly but response is not Success: " + response);
                    }
                },
                error: function (err) {
                    console.log("Ajax failed: " + err);
                }
            });
        }
    });
}