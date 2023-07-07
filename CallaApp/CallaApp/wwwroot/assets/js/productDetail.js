$(function(){
	
	$('ul.tabs li').click(function(){
		var tab_id = $(this).attr('data-tab');

		$('ul.tabs li').removeClass('current');
		$('.tab-content').removeClass('current');

		$(this).addClass('current');
		$("#"+tab_id).addClass('current');
	})

})

$(function () {
    $(document).on("click", ".detail-add", function (e) {
        let id = $(this).attr("data-id");
        let data = { id: id };
        let count = (".basket-count");
        $.ajax({
            type: "Post",
            url: "/Shop/AddToCart",
            data: data,
            success: function (res) {
                $(count).text(res);
            }
        })
        return false;
    });
})