/**
* @author taliszhou@msn.com
*/
(function ($) {
    $.fn.productmgrOper = function () {
        return this.each(function () {
            var $box = $(this);
            $(document).unbind("change", $box);

            $box.change(function (event) {
                var $a = $(this);
                var opId = $a.attr("id");

                if ($a.attr("name").substr(0, 4) == 'chk_' && $a.val() == "0") {
                    $a.parent().parent().attr("style", "display:none");
                    $a.attr("value", "1");
                }


                if ($a.attr("ftype") === "datetime" && $a.get(0).selectedIndex != 0) {
                    $a.parent().next().attr("style", "display:none");
                }
                else if ($a.attr("ftype") === "datetime" && $a.get(0).selectedIndex == 0) {
                    $a.parent().next().attr("style", "display:block");
                }

                switch (opId) {
                    case "ddlProCatGroupName":
                        var $ddlProCatGroupName = $('#ddlProCatGroupName');
                        var $ddlProCat2 = $('#ddlProCat2');
                        $.ajax({
                            type: 'POST',
                            url: '/ProductCenter/GetProductCategoryListByGroupId',
                            data: { 'ProductCategoryGroupId': $ddlProCatGroupName.find("option:selected").val() },
                            dataType: "json",
                            cache: false,
                            success: function (x) {
                                if (x) {
                                    $ddlSearchField.empty();
                                    //var op = $("<option value=''>添加选择条件</option>");
                                    $ddlSearchField.append("<option value=''>==添加选择条件==</option>");
                                    $ddlSearchField.append("<option value=''>---------------</option>");
                                    for (var i = 0; i < x.length; i++) {
                                        var op = $("<option value='" + x[i].CategoryAttributeId + "'>" + x[i].AttributeName + "</option>");
                                        $ddlSearchField.append(op);
                                    }

                                    event.preventDefault();
                                }
                            },
                            error: DWZ.ajaxError
                        });
                        break;

                    case "ddlChangeProductGroup":

                        var $form = $("[name='" + $a.attr("formname") + "']");
                        return navTabSearch($form);
                        break;

                    case "ddlProCat2":
                        var ddl = document.getElementById("ddlProCat2");
                        var $ddlSearchField = $("#ddlSearchField");

                        $.ajax({
                            type: 'POST',
                            url: '/ProductCenter/GetProductCategoryAttributeListByCategoryId',
                            data: { 'ProductCategoryId': ddl.options[ddl.selectedIndex].value },
                            dataType: "json",
                            cache: false,
                            success: function (x) {
                                if (x) {
                                    $ddlSearchField.empty();
                                    //var op = $("<option value=''>添加选择条件</option>");
                                    $ddlSearchField.append("<option value=''>==添加选择条件==</option>");
                                    $ddlSearchField.append("<option value=''>---------------</option>");
                                    for (var i = 0; i < x.length; i++) {
                                        var op = $("<option value='" + x[i].CategoryAttributeId + "'>" + x[i].AttributeName + "</option>");
                                        $ddlSearchField.append(op);
                                    }

                                    event.preventDefault();
                                }
                            },
                            error: DWZ.ajaxError
                        });
                        break;

                    case "ddlSearchField":
                        var $div = $("#divSearchGroup");
                        var $form = $("#searchForm");

                        $.ajax({
                            type: 'POST',
                            url: '/ProductCenter/ProductSearchPanel',
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

                        var ddlSearchField = document.getElementById("ddlSearchField");
                        ddlSearchField.selectedIndex = 0;
                        break;

                    case "chkAutoProId":
                        var $txtPorCode = $("#txtPorCode");
                        if ($a.attr('value') == '0') {
                            $a.attr('value', '1');
                            $a.attr('checked', 'checked');
                            $txtPorCode.attr('value', 'AUTO');
                            $txtPorCode.attr('disabled', 'disabled');
                        }
                        else {
                            $a.attr('value', '0');
                            $a.attr('checked', '');
                            $txtPorCode.attr('value', '');
                            $txtPorCode.attr('disabled', '');
                        }
                        break;

                    case "ddlProCat":
                        var $ddlSaleStatus = $("#ddlSaleStatus");
                        var ddl = document.getElementById("ddlProCat");

                        $.ajax({
                            type: 'POST',
                            url: '/ProductCenter/GetProductSaleStatusListByCategoryId',
                            data: { 'ProductCategoryId': ddl.options[ddl.selectedIndex].value },
                            dataType: "json",
                            cache: false,
                            success: function (x) {
                                if (x) {
                                    $ddlSaleStatus.empty();
                                    for (var i = 0; i < x.length; i++) {
                                        var op = $("<option value='" + x[i].SalesStatusId + "'>" + x[i].SalestatusName + "</option>");
                                        $ddlSaleStatus.append(op);
                                    }

                                    var $rel = $("#divExtAttributeBox");
                                    $rel.loadUrl("/ProductCenter/ProductExtAttributeList?pcid=" + ddl.options[ddl.selectedIndex].value, {}, function () {
                                        $rel.find("[layoutH]").layoutH();
                                    });

                                    event.preventDefault();
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

    // 产品类型属性JS控制
    $.fn.procatattributeOper = function () {
        return this.each(function () {
            var $box = $(this);
            $(document).unbind("click", $box);
            $(document).unbind("change", $box);

            $box.change(function () {
                var $a = $(this);
                var opId = $a.attr("id");

                switch (opId) {
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