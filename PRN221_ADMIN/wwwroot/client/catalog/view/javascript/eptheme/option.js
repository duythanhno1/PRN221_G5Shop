// Winter Infotech
// Custom Function
var webiOption = function(json) {
	//console.log(json);
	var product_id = json['product_id'];
	if(json['price']) {
		$('[data-update=price-'+ product_id +']').text(json['price']);
	}
	if(json['special']) {
		$('[data-update=price-new-'+ product_id +']').text(json['special']);
	}
	if(json['without_tax']) {
		$('[data-update=price-tax-'+ product_id +']').text(json['without_tax']);
	}
	if(json['discount']) {
		$('[data-update=discount-'+ product_id +']').text(json['discount']);
	}
}
var webiOptionAjex = function() {
	$.ajax({
		url: 'index.php?route=/extension/winter/product_data/option_price',
		type: 'post',
		data: $(this).closest('.webi-main').find('input[type=\'hidden\'], input[type=\'checkbox\']:checked, input[type=\'radio\']:checked, select'),
		success: webiOption,
		error: function(xhr, ajaxOptions, thrownError) {
			alert(thrownError + "\r\n" + xhr.statusText + "\r\n" + xhr.responseText);
		}
	});
}
// Custom Function End

$(document).ready(function() {
	$('.webi-option-click').on('click', webiOptionAjex);
	$('.webi-option-select').on('change', webiOptionAjex);
	
	$('.webi-cart').on('click', function() {
		$.ajax({
			url: '/Cart/Add',
			type: 'post',
			data: $(this).parent().parent().parent().find('input[type=\'text\'], input[type=\'hidden\'], input[type=\'radio\']:checked, input[type=\'checkbox\']:checked, select'),
			dataType: 'json',
			beforeSend: function() {
				$('#cart > button').button('loading');
			},
			complete: function() {
				$('#cart > button').button('reset');
			},
			success: function(json) {
				$('.alert-dismissible, .text-danger').remove();
				if (json['error']) {
					if (json['error']['option']) {
						for (i in json['error']['option']) {
							var element = $('#input-option' + i.replace('_', '-'));
	
							if (element.parent().hasClass('input-group')) {
								element.parent().after('<div class="text-danger">' + json['error']['option'][i] + '</div>');
							} else {
								element.after('<div class="text-danger">' + json['error']['option'][i] + '</div>');
							}
						}
					}
					if (json['error']['recurring']) {
						$('select[name=\'recurring_id\']').after('<div class="text-danger">' + json['error']['recurring'] + '</div>');
					}
					// Highlight any found errors
					$('.text-danger').parent().addClass('has-error');
				}
				if (json['success']) {
					$('#content').parent().before('<div class="a-one"><div class="alert alert-success alert-dismissible alertsuc"><svg width="20px" height="20px"> <use xlink:href="#successi"></use></svg> ' + json['success'] + ' <button type="button" class="close" data-dismiss="alert">&times;</button></div></div>');

					// Need to set timeout otherwise it wont update the total
					setTimeout(function () {
						$('#cart > button').html('<svg><use xlink:href="#hcart"></use></svg><span id="cart-total">' + json['total'] + '</span>');
					}, 100);

					$('html, body').animate({ scrollTop: 0 }, 'slow');

					$('#cart > ul').load('index.php?route=common/cart/info ul li');
				}
			},
			error: function(xhr, ajaxOptions, thrownError) {
				//alert(thrownError + "\r\n" + xhr.statusText + "\r\n" + xhr.responseText);
				alert("Unimplemented method. option.js");
			}
		});
	});
});