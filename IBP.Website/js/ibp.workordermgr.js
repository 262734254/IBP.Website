/**
* @author taliszhou@msn.com
*/
(function ($) {
    $.fn.WorkOrderMgr = function () {
        return this.each(function () {
            var $box = $(this);
            $(document).unbind("click", $box);
            $(document).unbind("change", $box);

            $box.change(function (event) {
                var $a = $(this);
                var opId = $a.attr("id");

                switch (opId) {
                    // 在/WorkOrderCenter/WorkOrderMgr页面，点击工单类型，获取工单状态及结果                                  
                    case "ddlWorkorderType":
                        var typeSelectorPanelId = $a.attr('joincontrol');
                        //                        var $TypeSelectorPanel = $('#' + typeSelectorPanelId);
                        //                        if ($a.val() == "OpenSelector") {
                        //                            $TypeSelectorPanel.removeClass("hideClass");
                        //                        }
                        //                        return;

                        $.ajax({
                            type: 'POST',
                            url: "/WorkOrderCenter/GetWorkTypeDomainModelJson?wotid=" + $a.val(),
                            dataType: "json",
                            cache: false,
                            success: function (json) {
                                if (json) {
                                    var ddlWorkTypeStatus = $a.parent().parent().parent().find(".workorder_nowstatus");
                                    var ddlWorkTypeResult = $a.parent().parent().parent().find(".workorder_nowresult");

                                    ddlWorkTypeStatus.empty();
                                    ddlWorkTypeResult.empty();

                                    var all = $("<option value='All'>所有</option>");
                                    var all2 = $("<option value='All'>所有</option>");
                                    ddlWorkTypeStatus.append(all);
                                    ddlWorkTypeResult.append(all2);

                                    if (json.StatusList && json.StatusList.length > 0) {
                                        for (var i = 0; i < json.StatusList.length; i++) {
                                            var op = $("<option value='" + json.StatusList[i].Value.WorkorderStatusId + "'>" + json.StatusList[i].Value.StatusName + "</option>");

                                            ddlWorkTypeStatus.append(op);
                                        }
                                    }

                                    if (json.ResultList && json.ResultList.length > 0) {
                                        for (var i = 0; i < json.ResultList.length; i++) {
                                            var op = $("<option value='" + json.ResultList[i].Value.WorkorderResultId + "'>" + json.ResultList[i].Value.ResultName + "</option>");

                                            ddlWorkTypeResult.append(op);
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

            $box.click(function () {
                var $a = $(this);
                var opId = $a.attr("id");
                var opName = $a.attr("name");

                switch (opName) {
                    case "btnWorkOrderTypeSelector":
                        var typeId = $a.attr("typeId");

                        var $TypeSelectorBox = $a.parent().parent();
                        $TypeSelectorBox.addClass("hideClass");

                        var $joinController = $('[joincontrol=' + $TypeSelectorBox.attr('id') + ']');
                        $joinController.empty();

                        $joinController.append($("<option value='All'>所有</option>"));
                        $joinController.append($("<option value=''>---------------------------------</option>"));
                        $joinController.append($("<option value='" + typeId + "'>" + $a.text() + "</option>"));
                        $joinController.append($("<option value=''>---------------------------------</option>"));
                        $joinController.append($("<option value='OpenSelector'>打开选择器</option>"));
                        $joinController.get(0).selectedIndex = 2;

                        $.ajax({
                            type: 'POST',
                            url: "/WorkOrderCenter/GetWorkTypeDomainModelJson?wotid=" + typeId,
                            dataType: "json",
                            cache: false,
                            success: function (json) {
                                if (json) {
                                    var ddlWorkTypeStatus = $box.parent().parent().parent().find(".workorder_nowstatus");
                                    var ddlWorkTypeResult = $box.parent().parent().parent().find(".workorder_nowresult");

                                    ddlWorkTypeStatus.empty();
                                    ddlWorkTypeResult.empty();

                                    var all = $("<option value='All'>所有</option>");
                                    var all2 = $("<option value='All'>所有</option>");
                                    ddlWorkTypeStatus.append(all);
                                    ddlWorkTypeResult.append(all2);

                                    if (json.StatusList && json.StatusList.length > 0) {
                                        for (var i = 0; i < json.StatusList.length; i++) {
                                            var op = $("<option value='" + json.StatusList[i].Value.WorkorderStatusId + "'>" + json.StatusList[i].Value.StatusName + "</option>");

                                            ddlWorkTypeStatus.append(op);
                                        }
                                    }

                                    if (json.ResultList && json.ResultList.length > 0) {
                                        for (var i = 0; i < json.ResultList.length; i++) {
                                            var op = $("<option value='" + json.ResultList[i].Value.WorkorderResultId + "'>" + json.ResultList[i].Value.ResultName + "</option>");

                                            ddlWorkTypeResult.append(op);
                                        }
                                    }
                                }
                            },
                            error: DWZ.ajaxError
                        });

                        break;

                    case "fldSetWorkorderTypeSelector":
                        var $TypeSelectorBox = $a.parent();
                        $TypeSelectorBox.addClass("hideClass");

                        var $joinController = $('[joincontrol=' + $TypeSelectorBox.attr('id') + ']');
                        $joinController.get(0).selectedIndex = 0;
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
                                    for (var i = 0; i < json.length; i++) {
                                        var op = $("<option value='" + json[i].UserId + "'>" + json[i].CnName + "【" + json[i].WorkId.replace("WORKID_", "") + "】" + "</option>");

                                        listUserList.append(op);
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