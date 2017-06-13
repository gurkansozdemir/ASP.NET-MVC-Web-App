function showInsertModal() {
    $('#insertModal').modal({
        backdrop: 'static',
        keyboard: false
    });
}

function productInsert() {
    $('#formInsert').submit(function (e) {
        e.preventDefault();
    }).validate({
        rules: {
            Name: {
                required: true,
            },
            Brand: {
                required: true
            }
        },
        messages: {
            Name: {
                required: "Bu alan zorunludur."
            },
            Brand: {
                required: "Bu alan zorunludur.",
            }
        },
        submitHandler: function (form) {
            var data = $(form).serialize();
            $.ajax({
                type: "POST",
                url: "/Product/productInsert",
                dataType: 'json',
                data: data,
                success: function (response) {
                    if (response == "Success") {
                        $('#insertModal').modal({
                            backdrop: 'static',
                            keyboard: false
                        });
                    }
                    else {
                        console.log("Ajax Successful but there was an error on the server side");
                    }
                },
                error: function (response) {
                    console.log("Ajax failed: " + response);
                }
            });
        }
    });
}