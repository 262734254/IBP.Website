/**
 * Theme Plugins
 * @author ZhangHuihua@msn.com
 */
(function ($) {
    $.fn.extend({
        cssTable: function (options) {

            return this.each(function () {
                var $this = $(this);
                var $trs = $this.find('tbody>tr');
                var $grid = $this.parent(); // table
                var nowrap = $this.hasClass("nowrap");
                var noclickedClass = $this.hasClass("noclickedclass");

                $trs.hoverClass("hover").each(function (index) {
                    var $tr = $(this);
                    if (!nowrap && index % 2 == 1) $tr.addClass("trbg");

                    $tr.click(function () {
                        $trs.filter(".selected").removeClass("selected");

                        if (noclickedClass) {
                            $tr.addClass("selected");
                        }
                        var sTarget = $tr.attr("target");
                        if (sTarget) {
                            if ($("#" + sTarget, $grid).size() == 0) {
                                $grid.prepend('<input id="' + sTarget + '" type="hidden" />');
                            }
                            $("#" + sTarget, $grid).val($tr.attr("rel"));
                        }
                    });

                });

                $this.find("thead [orderField]").orderBy({
                    targetType: $this.attr("targetType"),
                    rel: $this.attr("rel"),
                    asc: $this.attr("asc") || "asc",
                    desc: $this.attr("desc") || "desc"
                });
            });
        }
    });
})(jQuery);
