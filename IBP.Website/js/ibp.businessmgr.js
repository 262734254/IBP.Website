/**
* @author taliszhou@msn.com
*/
(function ($) {
    $.fn.BusinessMgr = function () {
        return this.each(function () {
            var $box = $(this);
            $(document).unbind("change", $box);

            $box.change(function (event) {
                var $a = $(this);
                var opName = $a.attr("id");

                switch (opName) {
                    case "ddlSaleCity":
                        var $div = $("#packageProductSelector");
                        var $form = $("#searchForm");

                        $.ajax({
                            type: 'POST',
                            url: '/BusinessCenter/SalePackageProductSelector?city=' + $a.val(),
                            data: $form.serializeArray(),
                            dataType: "html",
                            cache: false,
                            success: function (x) {
                                if (x) {
                                    $div.html(x).initUI();
                                }
                            },
                            error: DWZ.ajaxError
                        });

                        break;



                    default:
                        break;
                }
                return false;
            });

        });

    }

})(jQuery);