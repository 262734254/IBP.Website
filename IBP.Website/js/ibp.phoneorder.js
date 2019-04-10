/**
* @author taliszhou@msn.com
*/
(function ($) {
    $.fn.PhoneOrderMgr = function () {

        return this.each(function () {
            var $box = $(this);
            $(document).unbind("click", $box);
            $(document).unbind("change", $box);

            $box.change(function (event) {
                var $a = $(this);
                var opId = $a.attr("id");
                var opName = $a.attr("name");
                var action = $a.attr("action");

                if (action == "CreateAccountInfo") {
                    CreateAccountInfo($box);
                }

                switch (opName) {
                    case "ddlPayType":
                        var payType = $a.find("option:selected").val();
                        var $fdsCreditSelector = $("#fdsCreditSelector");

                        if (payType == "") {
                            alertMsg.warn("请选择一个有效的支付方式。", {
                                okCall: function () {
                                    $a[0].selectedIndex = 0;
                                    $fdsCreditSelector.removeClass("showClass").addClass("hideClass");
                                }
                            });
                        }
                        else {
                            if (payType > 8) {
                                $fdsCreditSelector.removeClass("showClass").addClass("hideClass");
                            }
                            else {
                                $fdsCreditSelector.removeClass("hideClass").addClass("showClass");
                            }
                        }
                        break;

                    default:

                }
                return false;
            });

            $box.dblclick(function (event) {
                var $a = $(this);
                var opId = $a.attr("id");
                var opName = $a.attr("name");

                switch (opName) {
                    case "btnLoadProductGuide":
                        var $selectedProductCategoryId = $('input[name=selectedProductCategoryId]');

                        var pcid = $a.attr("pcid");
                        if (pcid) {
                            $selectedProductCategoryId.attr("value", pcid);
                            AddToShoppingCart(event, $a, "");
                        }
                        break;

                    default:
                        break;
                }

                switch (opId) {
                    case "ddlProductPackageSelector":
                        var $divProductSelector = $('#divProductSelector');
                        var $btnlistMain = $('#btnlistMain');
                        var $btnlistSelectProduct = $('#btnlistSelectProduct');
                        var saleCityId = $('#ddlSaleCity').val();
                        var $selectedProductCount = $('#selectedProductCount');

                        if ($a.val() != "" && $a.find("option:selected").val() != "====================") {
                            $divProductSelector.loadUrl(
                            '/OrderCenter/PhoneSaleOrderProductSelector?itp=' + $a.find("option:selected").attr("itemprice") + '&pgid=' + $a.val() + '&city=' + saleCityId,
                            {},
                            function () {
                                $selectedProductCount.attr("value", 1);
                                $divProductSelector.find("[layoutH]").layoutH();
                            });

                            event.preventDefault();

                            $divProductSelector.attr('style', 'z-index:900; position:absolute; bottom:0px; left:0px;width:985px; height:520px; background-color:#ababab; display:block;');
                            $btnlistMain.attr('style', 'display:none;');
                            $btnlistSelectProduct.attr('style', 'display:block;');
                        }
                        break;

                    default:
                        break;
                }
                return false;
            });



            $box.click(function (event) {
                var $a = $(this);
                var opId = $a.attr("id");
                var opName = $a.attr("name");

                if (opName && opName.substring(0, 13) == "chkCollection") {
                    var index = $a.attr("packageindex");
                    var $divNotCollection = $('#divNotCollection' + index);
                    var $divCollectionInfo = $('#divCollectionInfo' + index);
                    var $hidCollection = $('[name=hidCollection' + index + ']');

                    if ($a.hasClass("chooseico02")) {
                        $a.removeClass("chooseico02").addClass("chooseico");
                        $divCollectionInfo.removeClass("hideClass").addClass("showClass");
                        $divNotCollection.removeClass("showClass").addClass("hideClass");
                        $hidCollection.attr("value", "1");
                    }
                    else {
                        $a.removeClass("chooseico").addClass("chooseico02");
                        $divNotCollection.removeClass("hideClass").addClass("showClass");
                        $divCollectionInfo.removeClass("showClass").addClass("hideClass");
                        $hidCollection.attr("value", "0");
                    }

                    CreateAccountInfo(event);
                }

                switch (opName) {

                    case "submitFollow":
                        var $followTime = $('input[name=followTime]').val() ;
                     
                        if ($followTime == "" || $followTime == null) {
                            alertMsg.error('请选择待跟进预约时间！');
                            return false;
                        }
                        alertMsg.confirm("确定保存当前为待跟进订单吗？", {
                            okCall: function () {
                                CreateAccountInfo($a);
                                var $form = $("[name='salesorderCreateForm']");
                                $form.attr("action", "/OrderCenter/DoNewSalesorderInfo?type=follow&page=" + $form.attr("page"));

                                return validateCallback($form, dialogAjaxDone);
                            }
                        });
                        break;

                    case "submitOrder":
                        alertMsg.confirm("确定要提交当前订单吗？", {
                            okCall: function () {
                                CreateAccountInfo($a);
                                var $form = $("[name='salesorderCreateForm']");
                                $form.attr("action", "/OrderCenter/DoNewSalesorderInfo?page=" + $form.attr("page"));

                                return validateCallback($form, dialogAjaxDone);
                            }
                        });
                        break;

                    case "submitUpdateOrder":
                        alertMsg.confirm("确定要提交更新当前订单吗？", {
                            okCall: function () {
                                CreateAccountInfo($a);
                                var $form = $("[name='salesorderUpdateForm']");
                                $form.attr("action", "/OrderCenter/DoUpdateSalesorderInfo?page=" + $form.attr("page"));

                                return validateCallback($form, dialogAjaxDone);
                            }
                        });
                        break;

                    case "btnLoadProductGuide":
                        var $divProductSalesGuide = $('#divProductSalesGuide');
                        var $selectedProductCategoryId = $('input[name=selectedProductCategoryId]');

                        var pcid = $a.attr("pcid");
                        if (pcid) {
                            if ($selectedProductCategoryId.attr("value") != pcid) {
                                $selectedProductCategoryId.attr("value", pcid);
                                //$selectedProductCategoryId.attr("producttype", ptid);

                                var ptid = $a.attr("ptid");

                                $divProductSalesGuide.loadUrl(
                                    '/OrderCenter/GetProductSaleGuideInfo?pcid=' + pcid + '&ptid=' + ptid,
                                    {},
                                    function () {
                                        $divProductSalesGuide.find("[layoutH]").layoutH();
                                    });

                                event.preventDefault();
                            }
                        }
                        break;

                    case "btnRemoveFromShoppingCart":
                        var itemid = $a.attr("itemid");
                        if (itemid) {
                            alertMsg.confirm("确认要移除购物车中选中的产品吗？", {
                                okCall: function () {
                                    AddToShoppingCart(event, $a, itemid);
                                }
                            });
                        }
                        break;

                    case "btnChangePayInfo":
                        var $divCustomerCreditCardSelector = $('#divCustomerCreditCardSelector');
                        var $divSelectedCreditCardInfo = $('#divSelectedCreditCardInfo');

                        $divCustomerCreditCardSelector.removeClass("hideClass").addClass("showClass");
                        $divSelectedCreditCardInfo.removeClass("showClass").addClass("hideClass");
                        break;

                    case "btnChangeDeliveryInfo":
                        var $divSelectedDeliveryInfo = $('#divSelectedDeliveryInfo');
                        var $divCustomerDeliverySelector = $('#divCustomerDeliverySelector');

                        $divCustomerDeliverySelector.removeClass("hideClass").addClass("showClass");
                        $divSelectedDeliveryInfo.removeClass("showClass").addClass("hideClass");
                        break;

                    case "btnSelectOrderCreditCard":
                        var $divCustomerCreditCardSelector = $('#divCustomerCreditCardSelector');
                        var $divSelectedCreditCardInfo = $('#divSelectedCreditCardInfo');

                        var creditId = $a.attr("creditid");
                        var $divOrderCreditInfo = $('#divOrderCreditInfo');
                        var $hidCustomerId = $('#hidCustomerId');

                        var paytype = $("input[name='radOrderPayType'][checked='checked']").val();
                        var paytype = $("#ddlPayType").find("option:selected").val();

                        $divOrderCreditInfo.loadUrl(
                        '/OrderCenter/CustomerCreditCardSelector?creditid=' + creditId + '&paytype=' + paytype + '&cid=' + $hidCustomerId.val(),
                        {},
                        function () {
                            $divOrderCreditInfo.find("[layoutH]").layoutH();
                        });

                        $divCustomerCreditCardSelector.removeClass("showClass").addClass("hideClass");
                        $divSelectedCreditCardInfo.removeClass("hideClass").addClass("showClass");
                        break;

                    case "btnSelectOrderDelivery":
                        var $divSelectedDeliveryInfo = $('#divSelectedDeliveryInfo');
                        var $divCustomerDeliverySelector = $('#divCustomerDeliverySelector');

                        var deliveryid = $a.attr("deliveryid");
                        var $divOrderDeliveryInfo = $('#divOrderDeliveryInfo');
                        var $hidCustomerId = $('#hidCustomerId');

                        $divOrderDeliveryInfo.loadUrl(
                        '/OrderCenter/CustomerDeliverySelector?delid=' + deliveryid + '&cid=' + $hidCustomerId.val(),
                        {},
                        function () {
                            $divOrderDeliveryInfo.find("[layoutH]").layoutH();
                        });

                        $divCustomerDeliverySelector.removeClass("showClass").addClass("hideClass");
                        $divSelectedDeliveryInfo.removeClass("hideClass").addClass("showClass");
                        break;

                    default:
                        break;
                }

                switch (opId) {
                    case "btnAddToShoppingCart":
                        AddToShoppingCart(event, $a, "");
                        break;

                    case "btnCancelProductSelect":
                        var $divProductSelector = $('#divProductSelector');
                        var $btnlistMain = $('#btnlistMain');
                        var $btnlistSelectProduct = $('#btnlistSelectProduct');


                        $divProductSelector.attr('style', 'display:none;z-index:900; position:absolute; bottom:0px; left:0px;width:985px; height:520px; background-color:#ababab;');
                        $btnlistMain.attr('style', 'display:block;');
                        $btnlistSelectProduct.attr('style', 'display:none;');
                        break;

                    case "btnNewPhoneSaleOrderNewCreditCard":
                        var $maskDiv = $('#maskDiv');
                        $maskDiv.attr("style", "width:980px; height:460px;z-index:900; position:absolute;filter:alpha(opacity=65);background-color:#fff; ");
                        var options = {};
                        options.width = $a.attr("width");
                        options.height = $a.attr("height");
                        options.close = NewPhoneSaleOrderSubDialogClose;
                        options.mask = true;
                        $.pdialog.open($a.attr("href"), "npso_" + $a.attr("rel"), "订单信息获取", options);
                        break;


                    case "btnNewPhoneSaleOrderNewDelivery":
                        var $maskDiv = $('#maskDiv');
                        $maskDiv.attr("style", "width:980px; height:460px;z-index:900; position:absolute;filter:alpha(opacity=65);background-color:#fff; ");
                        var options = {};
                        options.width = $a.attr("width");
                        options.height = $a.attr("height");
                        options.close = NewPhoneSaleOrderSubDialogClose;
                        options.mask = true;
                        $.pdialog.open($a.attr("href"), "npso_" + $a.attr("rel"), "新增客户配送信息", options);
                        break;

                    default:
                        break;
                }
                return false;
            });
        });
    }


})(jQuery);



function AddToShoppingCart(event, activeButton, removeId) {

    var $cartProductList = $('#cartProductList');
    var $selectedProductCategoryId = $('input[name=selectedProductCategoryId]');
    var $selectedProductCount = $('#selectedProductCount');
    var $orderSource = $('#orderSource');

    if ($selectedProductCategoryId.val() == "") {
        alertMsg.error('请从列表选择一个产品信息！');
        return false;
    }
    if ($selectedProductCount.val() == "" || $selectedProductCount.val() == "0" || $selectedProductCount.val() <= 0) {
        alertMsg.error('请填写订购产品的数量！');
        return false;
    }

    if ($selectedProductCategoryId.val() != "" && $selectedProductCount.val() != "") {
        $cartProductList.attr("value", $cartProductList.val() + $selectedProductCategoryId.val() + "|" + $selectedProductCategoryId.attr("producttype") + "|" + $selectedProductCount.val() + ",");

        var $hidOpenAccountInfo = $('#hidOpenAccountInfo');
        var $hidCustomerId = $('#hidCustomerId');
        var $divShoppingCart = $('#divShoppingCart');
        var $hidSelectCreditCardId = $('[name=hidSelectCreditCardId]');
        //var paytype = $("input[name='radOrderPayType'][checked='checked']").val();
        var paytype = $("#ddlPayType").find("option:selected").val();

        CreateAccountInfo(event);

        $divShoppingCart.loadUrl(
                        '/OrderCenter/GetPhoneSaleShoppingCartInfo?os=' + $orderSource.find("option:selected").val() + '&pl=' + $cartProductList.attr("value") + '&paytype=' + paytype + '&creditid=' + $hidSelectCreditCardId.val() + '&cid=' + $hidCustomerId.val() + '&accinfo=' + $hidOpenAccountInfo.val() + '&rmid=' + removeId,
                        {},
                        function () {
                            $divShoppingCart.find("[layoutH]").layoutH();
                        });

        

        var $divOpenAccount = $('#divOpenAccount');
        $divOpenAccount.loadUrl(
                        '/OrderCenter/CommunicationOpenAccountInfo?os=' + $orderSource.find("option:selected").val() + '&pl=' + $cartProductList.attr("value") + '&paytype=' + paytype + '&creditid=' + $hidSelectCreditCardId.val() + '&cid=' + $hidCustomerId.val() + '&accinfo=' + $hidOpenAccountInfo.val() + '&rmid=' + removeId,
                        {},
                        function () {
                            $divOpenAccount.find("[layoutH]").layoutH();
                        });

        var $divProductSelector = $('#divProductSelector');
        var $btnlistMain = $('#btnlistMain');
        var $btnlistSelectProduct = $('#btnlistSelectProduct');

        $divProductSelector.attr('style', 'display:none;z-index:900; position:absolute; bottom:0px; left:0px;width:985px; height:520px; background-color:#ababab;');
        $btnlistMain.attr('style', 'display:block;');
        $btnlistSelectProduct.attr('style', 'display:none;');
    }

    return false;
}

function CreateAccountInfo(event) {
    var $hidOpenAccountInfo = $('#hidOpenAccountInfo');
    var cpackageTotal = $hidOpenAccountInfo.attr("cpackagetotal");
    var accInfoStr = '';

    for (var i = 1; i <= cpackageTotal; i++) {
        var $hidCartId = $('[name=hidCartId' + i + ']');
        var OpeningCity = $('[name=ddlOpeningCity' + i + ']').find("option:selected").val();
        var $ddlBindNumber = $('[name=hidBindNumber' + i + ']');
        var $ddlBindPackage = $('[name=ddlBindPackage' + i + ']');
        var $ddlCreditCard = $('[name=ddlCreditCard' + i + ']');
        var $ddlCollection = $('[name=ddlCollection' + i + ']');
        var noCollection = $ddlCollection.find("option:selected").val();

        accInfoStr += $hidCartId.val() + '|' + $ddlBindNumber.val() + '|' + $ddlCreditCard.find("option:selected").val() +'|';
        if (noCollection != "" && noCollection != "undefined") {
            accInfoStr += $ddlCollection.find("option:selected").val() +'|1';
        } else {
            accInfoStr += 'none|0';
        }

        accInfoStr = accInfoStr + '|' + OpeningCity + '|' + $ddlBindPackage.find("option:selected").val() +',';
    };

    $hidOpenAccountInfo.attr("value", accInfoStr);

    return false;
}


function NewPhoneSaleOrderSubDialogClose() {
    var $maskDiv = $('#maskDiv');
    $maskDiv.attr("style", "display:none;width:0px; height:0px;z-index:900; position:absolute;filter:alpha(opacity=65);background-color:#fff; ");
    return true;
}

function AddCustomerCreditCardDone(json) {
    DWZ.ajaxDone(json);
    if (json.statusCode == DWZ.statusCode.ok) {
        var $maskDiv = $('#maskDiv');
        var $divOrderCreditInfo = $('#divOrderCreditInfo');

        //var paytype = $("input[name='radOrderPayType'][checked='checked']").val();
        var paytype = $("#ddlPayType").find("option:selected").val();

        $.ajax({
            type: 'POST',
            url: '/OrderCenter/CustomerCreditCardSelector?creditid=' + json.extPara.CreditCardId + '&paytype=' + paytype + '&cid=' + json.extPara.CustomerId,
            data: {},
            dataType: "html",
            cache: false,
            success: function (x) {
                if (x) {
                    $divOrderCreditInfo.html(x).initUI();
                    AddToShoppingCart(event, "", "");
                }
            },
            error: DWZ.ajaxError
        });

        //AddToShoppingCart(event, "", "");

        $maskDiv.attr("style", "display:none;width:0px; height:0px;z-index:900; position:absolute;filter:alpha(opacity=65);background-color:#fff; ");

        if ("closeCurrent" == json.callbackType) {
            $.pdialog.closeCurrent();
        }
    }
}

function AddCustomerDeliveryDone(json) {
    DWZ.ajaxDone(json);
    if (json.statusCode == DWZ.statusCode.ok) {
        var $maskDiv = $('#maskDiv');

        var $divOrderDeliveryInfo = $('#divOrderDeliveryInfo');

        $.ajax({
            type: 'POST',
            url: '/OrderCenter/CustomerDeliverySelector?delid=' + json.extPara.DeliveryId + '&cid=' + json.extPara.CustomerId,
            data: {},
            dataType: "html",
            cache: false,
            success: function (x) {
                if (x) {
                    $divOrderDeliveryInfo.html(x).initUI();
                }
            },
            error: DWZ.ajaxError
        });


        $maskDiv.attr("style", "display:none;width:0px; height:0px;z-index:900; position:absolute;filter:alpha(opacity=65);background-color:#fff; ");

        if ("closeCurrent" == json.callbackType) {
            $.pdialog.closeCurrent();
        }
    }
}