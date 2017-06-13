function showUpdateModal() {
    $('#updateModal').modal({
        backdrop: 'static',
        keyboard: false
    });
}

function productUpdate() {
    var data = $('#formUpdate').serialize();
    $.ajax({
        type: "POST",
        url: "/Product/productUpdate",
        data: data,
        dataType: "json",
        success: function (response) {
            if (response == "Success") {
                $('#updateModal').modal('hide');
                $('#updateProductSuccessModal').modal({
                    backdrop: 'static',
                    keyboard: false
                });
            }
            else {
                console.log("Ajax Successful but there was an error on the server side");
            }
        },
        error: function (response) {
            console.log("Ajax failed: " + response)
        }
    });
}