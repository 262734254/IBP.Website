$(function () {

    $('body').everyTime('10s', function () {
        $.ajax({
            type: 'POST',
            url: "/OrderCenter/SalesOrderTotalJson",
            dataType: "json",
            cache: false,
            success: function (json) {

                if (json.SalesOrderTotal.WaitCharge == null) {
                    json.SalesOrderTotal.WaitCharge = "0";
                }
                if (json.SalesOrderTotal.WaitCheck == null) {
                    json.SalesOrderTotal.WaitCheck = "0";
                }
                if (json.SalesOrderTotal.WaitApproval == null) {
                    json.SalesOrderTotal.WaitApproval = "0";
                }
                if (json.SalesOrderTotal.WaitOpening == null) {
                    json.SalesOrderTotal.WaitOpening = "0";
                }
                if (json.SalesOrderTotal.WaitStocking == null) {
                    json.SalesOrderTotal.WaitStocking = "0";
                }
                if (json.SalesOrderTotal.WaitDelivery == null) {
                    json.SalesOrderTotal.WaitDelivery = "0";
                }
                if (json.SalesOrderTotal.WaitSign == null) {
                    json.SalesOrderTotal.WaitSign = "0";
                }
                if (json.SalesOrderTotal.WaitRecover == null) {
                    json.SalesOrderTotal.WaitRecover = "0";
                }
                if (json.SalesOrderTotal.WaitRefund == null) {
                    json.SalesOrderTotal.WaitRefund = "0";
                }
                if (json.SalesOrderTotal.WaitReturns == null) {
                    json.SalesOrderTotal.WaitReturns = "0";
                }
                if (json.SalesOrderTotal.WaitCancelOpening == null) {
                    json.SalesOrderTotal.WaitCancelOpening = "0";
                }
                $("#OrderCenter_WaitChargeOrder").text("待扣款单（" + json.SalesOrderTotal.WaitCharge + "）");

                $("#OrderCenter_WaitCheckOrder").text("待质检单（" + json.SalesOrderTotal.WaitCheck + "）");

                $("#OrderCenter_WaitApprovalOrder").text("待审批单（" + json.SalesOrderTotal.WaitApproval + "）");

                $("#OrderCenter_WaitOpeningOrder").text("待开户单（" + json.SalesOrderTotal.WaitOpening + "）");

                $("#OrderCenter_WaitStockingOrder").text("待备货单（" + json.SalesOrderTotal.WaitStocking + "）");
                $("#OrderCenter_WaitDeliveryOrder").text("待发货单（" + json.SalesOrderTotal.WaitDelivery + "）");
                $("#OrderCenter_WaitSignOrder").text("待签收单（" + json.SalesOrderTotal.WaitSign + "）");
                $("#OrderCenter_WaitRecoverOrder").text("待回收单（" + json.SalesOrderTotal.WaitRecover + "）");
                $("#OrderCenter_WaitRefundOrder").text("待退款单（" + json.SalesOrderTotal.WaitRefund + "）");
                $("#OrderCenter_WaitReturnsOrder").text("待退货单（" + json.SalesOrderTotal.WaitReturns + "）");
                $("#OrderCenter_WaitCancelOpeningOrder").text("待销户单（" + json.SalesOrderTotal.WaitCancelOpening + "）");


            },

            error: DWZ.ajaxError
        });
    });

});        