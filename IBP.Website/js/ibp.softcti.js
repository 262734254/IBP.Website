/**
* @author taliszhou@msn.com
*/
(function ($) {
    $.fn.softctiOper = function () {
        return this.each(function () {
            var $box = $(this);
            $(document).unbind("click", $box);
            $box.click(function () {
                var $a = $(this);
                var opId = $a.attr("id");
                var opName = $a.attr("name");

                switch (opName) {
                    case "btnOutCallSelector":
                        var customerId = $a.attr("customerid");
                        var callNumber = $a.parent().parent().find('.customPhone').val();
                        if (callNumber == "") {
                            callNumber = $a.parent().parent().find('.contactPhone').find('option:selected').attr("callvalue");
                        }
                        if (callNumber == "") {
                            alertMsg.info('请选择一个外呼的号码！');
                            return false;
                        }

                        if (callNumber) {
                            alertMsg.confirm("确认要对号码【" + callNumber + "】发起发呼吗？", {
                                okCall: function () {
                                    aOCX.DialOut(callNumber);
                                    alertMsg.info('系统已启动外呼操作，请按下话机摘机键！');

                                    var options = {};
                                    options.rel = customerId;
                                    options.title = "外呼号码【" + callNumber + "】处理";
                                    options.mask = "true";
                                    options.width = "780";
                                    options.height = "490";

                                    var url = unescape("/CallCenter/ProcessOutCallInfo?call=" + callNumber + "&cid=" + customerId);
                                    $.pdialog.open(url, options.rel, options.title, options);
                                    return false;
                                }
                            });

                        }
                        break;

                    case "btnOutCall":
                        var callNumber = $a.attr("callnumber");
                        var customerId = $a.attr("customerid");

                        if (callNumber) {
                            alertMsg.confirm("确认要对号码【" + callNumber + "】发起发呼吗？", {
                                okCall: function () {
                                    aOCX.DialOut(callNumber);
                                    alertMsg.info('系统已启动外呼操作，请按下话机摘机键！');

                                    var options = {};
                                    options.rel = customerId;
                                    options.title = "外呼号码【" + callNumber + "】处理";
                                    options.mask = "true";
                                    options.width = "780";
                                    options.height = "490";

                                    var url = unescape("/CallCenter/ProcessOutCallInfo?call=" + callNumber + "&cid=" + customerId);
                                    $.pdialog.open(url, options.rel, options.title, options);
                                    return false;
                                }
                            });

                        }
                        break;

                    default:

                }
                
                return false;
            });
        });

    }

    function _openDialog(alink, urlParam) {
        var $this = $(alink);
        var title = $this.attr("title") || $this.text();
        var rel = $this.attr("rel") || "_blank";
        var options = {};
        var w = $this.attr("width");
        var h = $this.attr("height");
        if (w) options.width = w;
        if (h) options.height = h;
        options.max = eval($this.attr("max") || "false");
        options.mask = eval($this.attr("mask") || "false");
        options.maxable = eval($this.attr("maxable") || "true");
        options.minable = eval($this.attr("minable") || "true");
        options.fresh = eval($this.attr("fresh") || "true");
        options.resizable = eval($this.attr("resizable") || "true");
        options.drawable = eval($this.attr("drawable") || "true");
        options.close = eval($this.attr("close") || "");
        options.param = $this.attr("param") || "";

        var url = unescape($this.attr("href") + urlParam);

        $.pdialog.open(url, rel, title, options);

        return false;
    }
})(jQuery);



function openDialog(url, urlParam, option) {
    var url = unescape(url + urlParam);
    $.pdialog.open(url, option.rel, option.title, option);
    return false;
}

function ProcIncomeCall(inComeNumber, callNumber, aOCX) {
    $.ajax({
        type: 'POST',
        url: "/CallCenter/DoCreateFirstIncomeCall?income=" + inComeNumber + "&call=" + callNumber,
        dataType: "json",
        cache: false,
        success: function (response) {
            var json = DWZ.jsonEval(response);
            if (json.statusCode == DWZ.statusCode.timeout) {
                alertMsg.error(json.message || DWZ.msg("sessionTimout"), { okCall: function () {
                    if ($.pdialog) $.pdialog.checkTimeout();
                    if (navTab) navTab.checkTimeout();

                    DWZ.loadLogin();
                }
                });
            }

            if (json.statusCode == DWZ.statusCode.ok) {

                if (json.extPara.ProjectGroupName == "40077项目组") {
                    var options = {};
                    options.rel = json.extPara.CustomerId;
                    options.title = "40077项目组客户来电";
                    options.mask = "true";
                    options.width = "780";
                    options.height = "400";
                    navTab.openTab("incomecall_" + json.extPara.InComeNumber, "/CallCenter/IncomeCallForGroup77?cid=" + json.extPara.CustomerId + "&icnum=" + json.extPara.InComeNumber, { title: "77项目客户【" + json.extPara.InComeNumber + "】" });
                    //aOCX.DoSetAssociatedData("SensitiveCustomerId", "" + json.extPara.CustomerId + "");
                    return;
                }

                if (json.extPara.IsFirstIncomeCall == true) {
                    var options = {};
                    options.rel = json.extPara.CustomerId;
                    options.title = "新建来电客户档案";
                    options.mask = "true";
                    options.width = "880";
                    options.height = "460";

                    customerId = json.extPara.CustomerId;

                    openDialog("/CallCenter/FirstInComeCall", "?cid=" + json.extPara.CustomerId + "&icnum=" + json.extPara.InComeNumber, options);
                }
                else {
                    customerId = json.extPara.CustomerId;
                    navTab.openTab(json.navTabId, "/CallCenter/CustomerInfo?cid=" + json.extPara.CustomerId, { title: json.extPara.Title });
                }

            } else {
                alertMsg.error('来电【' + inComeNumber + '】未能正确处理，请与管理员联系！');
            }
        },
        error: DWZ.ajaxError
    });

}