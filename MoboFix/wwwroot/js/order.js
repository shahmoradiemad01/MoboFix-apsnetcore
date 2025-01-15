$(document).ready(function () {
    $(".addToCartButton").click(function () {
        var productId = $(this).data("product-id");

        $.ajax({
            url: 'Customer/Order/AddToCart',
            type: 'POST',
            data: { id: productId },
            success: function (response) {
                if (response.success) {
                    alert(response.message);
                } else {
                    alert("Error: " + response.message);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert("An error occurred: " + textStatus);
            }
        });
    });
});