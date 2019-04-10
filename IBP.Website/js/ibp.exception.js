/**
* @author taliszhou@msn.com
*/
(function ($) {
    $.fn.exceptionMgr = function () {
        $(function () {
            $('input[name=status]').each(function () {

            })
            $('input[name=status]').click(function () {
                var status = $(this).val();
                var $Divexception = $("#Divexception")

                if (status == "1") {
                    $Divexception.removeClass("hideClass").addClass("showClass");
                }
                else {
                    $Divexception.removeClass("showClass").addClass("hideClass");
                }
            })


            $("#needBill option").click(

	            function () {

	                $('#cat').val();
	                var needBill = $('#needBill option:selected').val();
	                var $DiveTitle = $("#DiveTitle")
	                if (needBill == "1") {
	                  
	                    $DiveTitle.removeClass("hideClass").addClass("showClass");
	                   
	                }
	                else {
	                   
	                    $DiveTitle.removeClass("showClass").addClass("hideClass");
	                 
	                }

	            }

            );

            // 订单扣款操作。
            $('input[name=chargeType]').click(function () {
                var typeid = $(this).val();
                var $dlChargeSuccessed = $('#dlChargeSuccessed');
                var $dlChargeException = $('#dlChargeException');

                switch (typeid) {
                    case "0":
                        $dlChargeSuccessed.removeClass("hideClass").addClass("showClass");
                        $dlChargeException.removeClass("showClass").addClass("hideClass");
                        break;

                    case "1":
                        $dlChargeSuccessed.removeClass("showClass").addClass("hideClass");
                        $dlChargeException.removeClass("hideClass").addClass("showClass");
                        break;

                    default:
                        break;
                }
            })

            // 订单质检操作。
            $('input[name=opType]').click(function () {
                var orderCheckedType = $(this).val();
                var $Divexception = $("#Divexception")
                var $DivLogisticscompany = $("#DivLogisticscompany")

                if (orderCheckedType == "1") {
                    $Divexception.removeClass("hideClass").addClass("showClass");
                    $DivLogisticscompany.removeClass("showClass").addClass("hideClass")
                }
                else {
                    $Divexception.removeClass("showClass").addClass("hideClass");
                    $DivLogisticscompany.removeClass("hideClass").addClass("showClass");
                }
            })
        })
    }




})(jQuery);