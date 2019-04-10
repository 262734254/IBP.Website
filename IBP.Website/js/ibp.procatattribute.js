/**
* @author taliszhou@msn.com
*/
(function ($) {
    $.fn.procatattributeOper = function () {
        return this.each(function () {
            var $box = $(this);
            $(document).unbind("click", $box);
            $(document).unbind("change", $box);

            $box.change(function () {
                var $a = $(this);
                var opName = $a.attr("id");

                switch (opName) {
                    case "ddlFieldType":
                        var selectedFiled = $a.attr("value");

                        if (selectedFiled == 'datetime' || selectedFiled == 'text') {
                            $("#divFieldBox1").attr("style", "display:none");
                            $("#divFieldBox2").attr("style", "display:none");
                        }
                        else if (selectedFiled == 'string' || selectedFiled == 'decimal') {
                            $("#divFieldBox1").attr("style", "display:block");
                            $("#divFieldBox2").attr("style", "display:none");
                        }
                        else if (selectedFiled == 'custom') {
                            $("#divFieldBox1").attr("style", "display:none");
                            $("#divFieldBox2").attr("style", "display:block");
                        }
                        break;

                    default:
                        break;
                }
                return false;
            });

        });

    }


})(jQuery);