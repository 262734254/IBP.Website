/**
* @author taliszhou@msn.com
*/
(function ($) {
    $.fn.autodialertaskOper = function () {
        return this.each(function () {
            var $box = $(this);
            $(document).unbind("click", $box);
            $box.click(function () {
                var $a = $(this);
                var opName = $a.attr("id");
                var ddlExists = $("#ddlExists");
                var taskId = $("#txtTaskId").attr("value");

                switch (opName) {
                    case "btnAddNumber":
                        var addstring = $("#txtInputBox").attr("value");
                        var addlist = addstring.split("\n");
                        var sendBox = [];
                        var opBox = [];
                        for (var i = 0; i < addlist.length; i++) {
//                            if (addlist[i].isPhone() == false || addlist[i].isMobilePhone() == false) {
//                                alertMsg.error("第 " + (i + 1) + "行号码 " + addlist[i] + " 非手机号码或固定号码，请检查输入");
//                                return false;
//                            }
//                            else {
//                                var op = $("<option value='" + addlist[i] + "'>" + addlist[i] + "</option>");
//                                opBox.push(op);
//                                sendBox.push(addlist[i]);
//                            }

                            var op = $("<option value='" + addlist[i] + "'>" + addlist[i] + "</option>");
                            opBox.push(op);
                            sendBox.push(addlist[i]);
                        }


                        $.ajax({
                            type: 'POST',
                            url: '/CallCenter/DoAddAutoDialerTaskNumbers',
                            data: { 'TaskId': taskId, 'AddNumbers': sendBox },
                            dataType: "json",
                            cache: false,
                            success: function (x) {
                                if (x) {
                                    if (x.code) {
                                        if ("ok" === x.code || "OK" === x.code) {
                                            $("#txtInputBox").attr("value", "");
                                            for (var i = 0; i < opBox.length; i++) {
                                                ddlExists.append(opBox[i]);
                                            }
                                            var total = $("#ivrNumberCount");
                                            $("#ivrNumberCount").attr("innerHTML", x.total);
                                            alertMsg.info(x.msg);
                                        }
                                        else {
                                            alertMsg.error(x.msg);
                                        }
                                    }
                                }
                            },
                            error: DWZ.ajaxError
                        });
                        break;

                    case "btnDelNumber":
                        var otherList = [];
                        var delList = [];
                        var numList = ddlExists[0];
                        var str = "";
                        for (var i = 0; i < numList.length; i++) {
                            var op = $("<option value='" + numList[i].value + "'>" + numList[i].text + "</option>");

                            if (numList[i].selected == false) {
                                otherList.push(op);
                            }
                            else {
                                delList.push(numList[i].value);
                                str = str + numList[i].text + "\n";
                            }
                        }

                        alertMsg.confirm('确定要删除选中外呼号码条目吗？', {
                            okCall: function () {
                                $.ajax({
                                    type: 'POST',
                                    url: '/CallCenter/DoDeleteAutoDialerTaskNumbers',
                                    data: { 'TaskId': taskId, 'DelNumbers': delList },
                                    dataType: "json",
                                    cache: false,
                                    success: function (x) {
                                        if (x) {
                                            if (x.code) {
                                                if ("ok" === x.code || "OK" === x.code) {
                                                    ddlExists.empty();

                                                    for (var i = 0; i < otherList.length; i++) {
                                                        ddlExists.append(otherList[i]);
                                                    }

                                                    $("#txtInputBox").attr("value", str);

                                                    var total = $("#ivrNumberCount");
                                                    $("#ivrNumberCount").attr("innerHTML", x.total);
                                                    alertMsg.info(x.msg);
                                                }
                                                else {
                                                    alertMsg.error(x.msg);
                                                }
                                            }
                                        }
                                    },
                                    error: DWZ.ajaxError
                                });
                            }
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