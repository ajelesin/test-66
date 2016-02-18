
$(function () {

	var service = $.connection.exchangeHub;
	service.client.refresh = function () {
		$.ajax({
			url: '/Home/GetSellOrders',
			dataType: 'html',
			success: function (data) {
				$('#SellOrders').html(data);
			}
		});
		$.ajax({
			url: '/Home/GetBuyOrders',
			dataType: 'html',
			success: function (data) {
				$('#BuyOrders').html(data);
			}
		});
		$.ajax({
			url: '/Home/GetTradeHistory',
			dataType: 'html',
			success: function (data) {
				$('#TradeHistory').html(data);
			}
		});
	}

	$.connection.hub.start();

});