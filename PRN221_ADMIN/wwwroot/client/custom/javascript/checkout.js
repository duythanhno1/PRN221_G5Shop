function checkout(orderDetailsJson, total) {
    var ods = orderDetailsJson.replace(/&quot;/g, '\"');
    var checkoutModel = {
        "Account": {
            "Name": $('#input-payment-name').val(),
            "Email": $('#input-payment-email').val(),
            "Address": $('#input-payment-address').val(),
        },
        "OrderDetails": JSON.parse(ods),
        "Total": total,
    };
    if ($('#input-payment-address').val()) {
        $.ajax({
            url: '/Transaction/Checkout',
            type: 'post',
            data: JSON.stringify(checkoutModel),
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            beforeSend: () => {
                $('button#button-confirm').prop('disabled', true);
                $('button#button-confirm').text("Loading...");
            },
            success: (json) => {
                if (json.status === 'Success') {
                    $(".infobg").html(`
                        <h1>Success</h1>
                        <h4>${json.message}</h4>
                        <div class="buttons clearfix">
                            <div class="pull-left"><a href="/" class="btn btn-primary">Continue Shopping</a></div>
                        </div>
                    `);
                    $('html, body').animate({ scrollTop: 0 }, 'slow');
                }
                else {
                    alert('Error: ' + json['message']);
                    $('button#button-confirm').prop('disabled', false);
                    $('button#button-confirm').text("Confirm order");
                }
            },
            error: (xhr, ajaxOptions, thrownError) => {
                alert("Something went wrong. Please try again later");
                $('button#button-confirm').prop('disabled', false);
                $('button#button-confirm').text("Confirm order");
            }
        });
    } else {
        alert("Address is required");
    }
}