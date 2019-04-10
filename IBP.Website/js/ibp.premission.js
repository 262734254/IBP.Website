﻿/**
* @author taliszhou@msn.com
*/
(function ($) {
    $.fn.premissionOper = function () {
        return this.each(function () {
            var $box = $(this);
            $(document).unbind("click", $box);
            $box.click(function () {
                var $a = $(this);
                var opName = $a.attr("id");
                var roleId = $("#hidSelectedRoleId").attr("value");

                switch (opName) {
                    case "moveAllRight_p":
                        var ddlUserList = $("#ddlUnDefineUserList");
                        var ddlSelected = $("#ddlRoleUserSelected");

                        var userList = ddlUserList[0];
                        var otherList = [];
                        for (var i = 0; i < userList.length; i++) {
                            var op = $("<option value='" + userList[i].value + "'>" + userList[i].text + "</option>");

                            if (userList[i].selected) {
                                ddlSelected.append(op);
                            }
                            else {
                                otherList.push(op);
                            }
                        }
                        ddlUserList.empty();
                        for (var i = 0; i < otherList.length; i++) {
                            ddlUserList.append(otherList[i]);
                        }

                        

                        _setSelectedValue();
                        break;

                    case "moveAllLeft_p":
                        var ddlUserList = $("#ddlUnDefineUserList");
                        var ddlSelected = $("#ddlRoleUserSelected");

                        var userList = ddlSelected[0];
                        var otherList = [];
                        for (var i = 0; i < userList.length; i++) {
                            var op = $("<option value='" + userList[i].value + "'>" + userList[i].text + "</option>");

                            if (userList[i].selected) {
                                ddlUserList.append(op);
                            }
                            else {
                                otherList.push(op);
                            }
                        }
                        ddlSelected.empty();
                        for (var i = 0; i < otherList.length; i++) {
                            ddlSelected.append(otherList[i]);
                        }

     

                        _setSelectedValue();
                        break;

                    default:
                        break;
                }
                return false;
            });
        });

    }

    function _setSelectedValue() {
        var ddlSelectedBox = $("#ddlRoleUserSelected");
        var hidSelectedBox = $("#hidSelectedRoleUser");
        var idstring = "";

        var userList = ddlSelectedBox[0];
        for (var i = 0; i < userList.length; i++) {
            idstring += userList[i].value + "|";
        }
        hidSelectedBox.attr("value", idstring);
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