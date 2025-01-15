jQuery(document).ready(function ($) {
    $(document).ready(function () {
        $(document).on("click", ".addToCartButton", function (e) {
            console.log("Button clicked");
            var productId = $(this).data("product-id");
            console.log("Product ID:", productId);

            $.ajax({
                url: 'Customer/Order/AddToCart',
                type: 'POST',
                data: { id: productId },
                success: function (response) {
                    console.log("Response:", response);
                    if (response.success) {
                        alert(response.message);
                    } else {
                        alert("Error: " + response.message);
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.error("AJAX Error:", textStatus, errorThrown);
                    alert("An error occurred: " + textStatus);
                }
            });
        });
    });
});



$(document).ready(function () {
    $("#Checkout").click(function (e) {
        var CartId = $(this).data("cart-id");

        $.ajax({
            url: '/Customer/Order/Checkout',
            type: 'POST',
            data: { id: CartId },
            success: function (response) {
                if (response.success) {
                    alert(response.message);
                    setTimeout(1000);
                    window.location.replace("/Customer/Manage/Index");
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




$(document).ready(function () {
    $(".OrderControl").click(function (e) {
        var orederId = $(this).data("order-id");
        var status = $(this).data("order-status");

        $.ajax({
            url: '/Seller/Order/SetOrder',
            type: 'POST',
            data: {
                id: orederId,
                status: status
            },
            success: function (response) {
                if (response.success) {
                    alert(response.message);
                    setTimeout(1000);
                    window.location.reload();
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



    $(document).ready(function () {
        $("#ConfirmUser").click(function (e) {
            var userId = $(this).data("user-id");

            $.ajax({
                url: '/Admin/User/ConfirmUser/',
                type: 'POST',
                data: {
                    userId: userId
                },
                success: function (response) {
                    if (response.success) {
                        alert(response.message);
                        setTimeout(500);
                        window.location.replace("/Admin/User/Index");
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
    $(document).ready(function () {
        $("#DeleteUser").click(function (e) {
            var userId = $(this).data("user-id");

            $.ajax({
                url: '/Admin/User/DeleteUser/',
                type: 'POST',
                data: {
                    userId: userId
                },
                success: function (response) {
                    if (response.success) {
                        alert(response.message);
                        setTimeout(500);
                        window.location.replace("/Admin/User/Index");
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