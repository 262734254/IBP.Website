/**
* @author taliszhou@msn.com
*/
(function ($) {
    $.fn.CustomerMgr = function () {
        return this.each(function () {
            var $box = $(this);
            $(document).unbind("click", $box);
            $(document).unbind("change", $box);

            $box.change(function (event) {
                var $a = $(this);
                var opId = $a.attr("id");

                switch (opId) {
                    // 在/CallCenter/NewWorkOrder页面中，点击工单类型，获取工单状态及结果                                                                                  
                    case "ddlWorkorderType":
                        $.ajax({
                            type: 'POST',
                            url: "/WorkOrderCenter/GetWorkTypeDomainModelJson?wotid=" + $a.val(),
                            dataType: "json",
                            cache: false,
                            success: function (json) {
                                if (json) {
                                    var ddlWorkTypeStatus = $('#ddlWorkTypeStatus');
                                    var ddlWorkTypeResult = $('#ddlWorkTypeResult');

                                    ddlWorkTypeStatus.empty();
                                    ddlWorkTypeResult.empty();
                                    for (var i = 0; i < json.StatusList.length; i++) {
                                        var op = $("<option value='" + json.StatusList[i].Value.WorkorderStatusId + "'>" + json.StatusList[i].Value.StatusName + "</option>");

                                        ddlWorkTypeStatus.append(op);
                                    }
                                    for (var i = 0; i < json.ResultList.length; i++) {
                                        var op = $("<option value='" + json.ResultList[i].Value.WorkorderResultId + "'>" + json.ResultList[i].Value.ResultName + "</option>");

                                        ddlWorkTypeResult.append(op);
                                    }
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



            $box.click(function (event) {
                var $a = $(this);
                var opId = $a.attr("id");
                var opName = $a.attr("name");

                switch (opName) {
                    case "btnOutCall":
                        var callNumber = $a.attr("callnumber");
                        var customerId = $a.attr("customerid");

                        if (callNumber) {
                            alertMsg.confirm("确认要对号码【" + callNumber + "】发起发呼吗？", {
                                okCall: function () {
                                    aOCX.DialOut(callNumber);
                                    alertMsg.info('系统已启动外呼操作，请按下话机摘机键！');
                                    //aOCX.DoSetAssociatedData("SensitiveCustomerId", "" + customerId + "");

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
                        break;
                }

                switch (opId) {
                    case "btnUpdateCustomerBasic":
                        alertMsg.confirm($a.attr("title"), {
                            okCall: function () {
                                var $form = $a.parent().parent().parent().parent();

                                return validateCallback($form, dialogAjaxDone);
                            }
                        });

                        break;

                    // 在/CallCenter/NewWorkOrder页面中，点击用户组，获取用户组用户并赋值                                                                                    
                    case "lstUserGroupList":
                        $.ajax({
                            type: 'POST',
                            url: "/UserMgr/GetGroupUserListJson?gid=" + $a.val(),
                            dataType: "json",
                            cache: false,
                            success: function (json) {
                                if (json) {
                                    var listUserList = $('#lstGroupUserList');
                                    listUserList.empty();
                                    if (json.length > 0) {
                                        for (var i = 0; i < json.length; i++) {
                                            var op = $("<option value='" + json[i].UserId + "'>" + json[i].CnName + "【" + json[i].WorkId.replace("WORKID_", "") + "】" + "</option>");

                                            listUserList.append(op);
                                        }
                                    }
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

