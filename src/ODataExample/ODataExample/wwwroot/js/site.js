// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

(function ($) {
	$(function () {
		$('a.jw').click(function (event) {
			var $t = $(this);
			if ($t.next().length === 0) {
				var h = $t.attr('href');
				$t.after($('<div class="card">' +
					'  <div class="card-body">' +
					'    <h5 class="card-title"><a href="' + h + '" target="_blank">' + h + '</a></h5>' +
					'    <p class="card-text json-viewer"></p>' +
					'  </div>' +
					'</div>'));

				$.getJSON(h, function (d) {
					//var w = $('.card-text', $t.next()).get(0);
					//jsonTree.create(d, w);
					$('.card-text', $t.next()).jsonView(d);
				});
			}
			else {
				$t.next().remove();
			}
			event.preventDefault();
			return false;
		});

		$('a.xw').click(function (event) {
			var $t = $(this);
			if ($t.next().length === 0) {
				var h = $t.attr('href');
				$t.after($('<div class="card">' +
					'  <div class="card-body">' +
					'    <h5 class="card-title"><a href="' + h + '" target="_blank">' + h + '</a></h5>' +
					'    <p class="card-text xml-viewer"></p>' +
					'  </div>' +
					'</div>'));

				$.get(h, {}, function (d) {
					//var w = $('.card-text', $t.next()).get(0);
					//jsonTree.create(d, w);
					$('.card-text', $t.next()).simpleXML({ xmlString: d });
				}, 'text');
			}
			else {
				$t.next().remove();
			}
			event.preventDefault();
			return false;
		});

	});
})(jQuery);