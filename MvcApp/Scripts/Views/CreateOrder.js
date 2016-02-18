function onAjaxBegin() {
	var $body = $("body");
	$body.addClass("loading");
}

function onAjaxComplete() {
	var $body = $("body");
	$body.removeClass("loading");
}