function deleteItem(cartID, refresh = false) {
    const cartObject = JSON.parse(localStorage.getItem(cartID));
    $.ajax({
        url: '/Cart/Delete',
        type: 'post',
        data: JSON.stringify(cartObject),
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: (json) => {
            if (json.status === 'Success') {
                fetchCart();
                if (refresh) {
                    window.location.reload();
                }
            }
            else {
                alert('Error: ' + json['message']);
            }
        },
        error: (xhr, ajaxOptions, thrownError) => {
            alert("Something went wrong. Please try again later");
        }
    });
}


async function fetchCart() {
    var ele = $("#cart > ul");
    var data = await fetch("/Cart/InCart");
    var jsonData = await data.json();
    var data = [`<a style="padding-left: 3rem;" href="/Cart">Go to cart</a>`];
    var total = 0;
    localStorage.clear();
    if (jsonData && jsonData.length > 0) {
        jsonData.map((cart, index) => {
            total += cart.product.price * cart.quantity;
            localStorage.setItem(cart.id, JSON.stringify({ProductID: cart.product.id, Quantity: cart.quantity, UserEmail: cart.user.email}));
            data.push(` 
            <li id="cart-list-item">
                <img src="${cart.product.imgPath.replace("~/", "/")}" alt="${cart.product.imgPath.replace("~","")}" height="100%" width="auto">
                <div id="cart-item-label">
                    <a href="javascript:void(0)"><strong>${cart.product.name}</strong></a>
                    <br/>
                    <p>Quantity: ${cart.quantity} - Total: $${cart.product.price * cart.quantity}</p>
                        <div class="input-group btn-block" style="max-width: 100px;">
                            <input type="text" name="quantity-${index}" value="${cart.quantity}" size="1" style="with: 60px" class="form-control" id="quantity-popup-cart-${cart.id}"/>
                            <span class="input-group-btn cartpsp" style="padding: 0 1.5rem;">
                                <button type="button" data-toggle="tooltip" title="Update" class="btn btn-danger cbt" onclick="updateCart('${cart.id}', 'quantity-popup-cart-')"><i class="fa fa-refresh"></i></button>
                            </span>
                        </div>
                    <!--<div class="homeqt">
                        <button type="button" onclick="qty.minus('30')" class="form-control pull-left btn-number btnminus dis-30" disabled="disabled">
                            <span class="glyphicon glyphicon-minus"></span>
                        </button>
                        <input name="quantity-${index}" type="number" value="1" size="2" min="1" max="999" class="form-control input-number pull-left" />
                        <button type="button" onclick="qty.plus('30')" class="form-control pull-left btn-number btnplus">
                            <span class="glyphicon glyphicon-plus"></span>
                        </button>
                    </div>-->
                </div>
                <button type="button" id="delete-cart-item" onclick="deleteItem('${cart.id}')"><ion-icon name="trash-outline"></ion-icon></button>
            </li>
        `)
        });
    } else {
        data.push(`
            <li>
                <p class="text-center">Your shopping cart is empty!</p>
            </li>
            `);
    }
    ele.html(data.join(""));
    $("span#cart-total > strong").text('$' + (Math.round(total * 100) / 100).toFixed(2));
}

function addToCart(productID, inputID = 'input-quantity-') {
    var data = {
        'ProductID': productID,
        'Quantity': +($('input#' + inputID + productID).val())
    };
    $.ajax({
        url: '/Cart/Add',
        type: 'post',
        //data: $(this).parent().parent().parent().find('input[type=\'text\'], input[type=\'hidden\'], input[type=\'radio\']:checked, input[type=\'checkbox\']:checked, select'),
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (json) {
            $('.alert-dismissible, .text-danger').remove();
            if (json['status'] === "Error") {
                alert('Error: ' + json['message'])
                // Highlight any found errors
                $('.text-danger').parent().addClass('has-error');
            }
            if (json['status'] == "Success") {
                $('#content').parent().before('<div class="a-one"><div class="alert alert-success alert-dismissible alertsuc"><svg width="20px" height="20px"> <use xlink:href="#successi"></use></svg> ' + json['message'] + ' <button type="button" class="close" data-dismiss="alert">&times;</button></div></div>');
                fetchCart();
                // Need to set timeout otherwise it wont update the total
                //setTimeout(function () {
                //    $('#cart > button').html('<svg><use xlink:href="#hcart"></use></svg><span id="cart-total">' + json['total'] + '</span>');
                //}, 100);

                //$('html, body').animate({ scrollTop: 0 }, 'slow');

                //$('#cart > ul').load('index.php?route=common/cart/info ul li');
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //alert(thrownError + "\r\n" + xhr.statusText + "\r\n" + xhr.responseText);
            alert("Something went wrong. Please try again later");
        }
    });
}

function updateCart(ID, inputID = 'input-quantity-', refresh = false) {
    let cartObject = JSON.parse(localStorage.getItem(ID));
    cartObject.Quantity = +($('input#' + inputID + ID).val());
    $.ajax({
        url: '/Cart/Update',
        type: 'patch',
        data: JSON.stringify(cartObject),
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: (json) => {
            if (json.status === 'Success') {
                if (refresh) {
                    window.location.reload();
                }
            }
            else {
                alert('Error: ' + json.message);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            //alert(thrownError + "\r\n" + xhr.statusText + "\r\n" + xhr.responseText);
            alert("Something went wrong. Please try again later");
        }
    });
}