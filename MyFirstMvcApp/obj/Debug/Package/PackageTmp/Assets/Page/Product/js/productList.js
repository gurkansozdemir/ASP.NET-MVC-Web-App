$(document).ready(function () {
    getAllProduct();
});

//Document'de class'ı delete olan elemente tıklandığında tıklanan butonun ait olduğu ürün tespit edildi
$(document).on("click", ".delete", function () {
    var proId = $(this).attr("data-id");
    // deleteModal'da sil butonunun href'ine ürünün id'si queryString ile gönderildi
    $('#btn-delete').attr("href", "/ürün/sil/" + proId);
});

//deleteModal'ı açar
function showDeleteModal() {
    $('#deleteModal').modal({
        backdrop: 'static',
        keyboard: false
    });
}

function getAllProduct() {
    $.ajax({
        type: "GET",
        url: "/Product/dataTable",
        dataType: "json",
        contentType: "application/json",
        success: function (data) {
            var dataSet = [];
            $.each(data, function (key, val) {
                var deleteButton = '<button class="btn btn-danger delete" data-id="' + val.Id + '" onclick="showDeleteModal()">Sil</button>';
                var editButton = '<a href="/ürün/güncelle/' + val.Id + '" class="btn btn-warning">Düzenle</a>';
                var row = [];
                row.push(val.Id);
                row.push(val.ProductName);
                row.push(val.Brand);
                row.push(val.CategoryName);
                row.push(deleteButton + "&nbsp" + editButton);
                dataSet.push(row);
            });

            $('#table-productList').DataTable({
                data: dataSet,
                "language": {
                    "url": "//cdn.datatables.net/plug-ins/1.10.13/i18n/Turkish.json"
                },
                "lengthMenu": [[5, 10, 15, -1], [5, 10, 15, "All"]],
                responsive: true
            });
        },
        error: function (response) {
            console.log("Ajax failed: " + response);
        }
    });
}