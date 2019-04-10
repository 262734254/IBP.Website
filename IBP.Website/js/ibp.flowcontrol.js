/**
* @author taliszhou@msn.com
*/
(function ($) {
    $.fn.flowcontrol = function () {
        return this.each(function () {
            var $box = $(this);
            var opId = $box.attr("id");
            var $customerName = $("#customer_name_out");
            var $mobilePhone = $("#mobile_phone_out");

            $(document).unbind("mousedown", $box);
            $(document).unbind("click", $box);

            $(document).unbind("blur", $customerName);
            $(document).unbind("blur", $mobilePhone);
            $(document).unbind("keydown", $customerName);
            $(document).unbind("keydown", $mobilePhone);

            $customerName.keydown(function () {
                if (event.keyCode == 13) {
                    $("[name=customer_name]").val($customerName[0].value);
                    if ($customerName[0].value != "") {
                        $("[name=sel_customer_name]").attr("value", "1");
                        $("[name=chk_customer_name]").attr("class", "chooseico02");
                    } else {
                        $("[name=sel_customer_name]").attr("value", "0");
                        $("[name=chk_customer_name]").attr("class", "chooseico");
                    }
                }
            });

            $customerName.blur(function () {
                $("[name=customer_name]").val($customerName[0].value);
                if ($customerName[0].value != "") {
                    $("[name=sel_customer_name]").attr("value", "1");
                    $("[name=chk_customer_name]").attr("class", "chooseico02");
                } else {
                    $("[name=sel_customer_name]").attr("value", "0");
                    $("[name=chk_customer_name]").attr("class", "chooseico");
                }
            });

            $mobilePhone.keydown(function () {
                $("[name=mobile_phone]").val($mobilePhone[0].value);
                if ($mobilePhone[0].value != "") {
                    $("[name=sel_mobile_phone]").attr("value", "1");
                    $("[name=chk_mobile_phone]").attr("class", "chooseico02");
                } else {
                    $("[name=sel_mobile_phone]").attr("value", "0");
                    $("[name=chk_mobile_phone]").attr("class", "chooseico");
                }
            });

            $mobilePhone.blur(function () {
                $("[name=mobile_phone]").val($mobilePhone[0].value);
                if ($mobilePhone[0].value != "") {
                    $("[name=sel_mobile_phone]").attr("value", "1");
                    $("[name=chk_mobile_phone]").attr("class", "chooseico02");
                } else {
                    $("[name=sel_mobile_phone]").attr("value", "0");
                    $("[name=chk_mobile_phone]").attr("class", "chooseico");
                }
            });


            var $locationDiv = $('div[locname=' + $box.attr("locname") + ']');


            $locationDiv.find("[chk=select]").each(function () {
                var $chk = $(this);
                if ($chk.children("input").attr("value") == "1") {
                    $chk.attr("class", "chooseico02");
                }
                else {
                    $chk.attr("class", "chooseico");
                }

                $(document).unbind("click", $chk);

                $chk.click(function () {
                    if ($chk.children("input").attr("value") == "0") {
                        $chk.attr("class", "chooseico02");
                        $chk.children("input").attr("value", "1");
                    }
                    else {
                        $chk.attr("class", "chooseico");
                        $chk.children("input").attr("value", "0");
                    }

                    return false;
                });
            });


            $box.click(function () {
                $locationDiv.click(function () {
                    _hide($locationDiv);
                    return false;
                });

                switch (opId) {
                    case "btnCustomerQueryPanel":
                        var $locationBox = $locationDiv.find('.customerQueryBox');
                        $(document).unbind("click", $locationDiv);
                        $(document).unbind("click", $locationBox);


                        if ($locationDiv.hasClass("locationShow")) {


                        } else {
                            $locationDiv.attr("style", "width:100%;height:100%;");
                            $locationBox.attr("style", "width:680px;height:385px;");

                            _show($locationDiv);

                            $locationBox.click(function () {
                                return false;
                            });
                        }
                        break;

                    case "btnLocation":
                        var $locationBox = $locationDiv.find('.locationBox'); // $('#locationBox');
                        var $chinaIdBox = $locationDiv.find('.chinaIdBox');
                        var $provinceAreaBox = $locationBox.find('.x_wrap_01');  // $('#provinceAreaBox');
                        var $provinceBox = $locationBox.find('.x_wrap_04'); // $('#provinceBox');
                        var $cityBox = $locationBox.find('.x_wrap_02'); // $('#cityBox');
                        var $countyBox = $locationBox.find('.x_wrap_03'); // $('#countyBox');
                        var levelRegion = $locationDiv.attr("levelRegion");

                        $(document).unbind("click", $locationDiv);
                        $(document).unbind("click", $locationBox);

                        if ($locationDiv.hasClass("locationShow")) {

                        } else {
                            $locationDiv.attr("style", "width:90%;height:90%;");
                            $locationBox.attr("style", "width:550px;height:200px;");
                            _show($locationDiv);


                            $locationBox.click(function () {
                                return false;
                            });

                            $provinceAreaBox.find("span").click(function () {
                                var $provinceAreaLink = $(this);

                                if (levelRegion == "provinceArea") {
                                    $box.attr('value', $provinceAreaLink.text());
                                    $chinaIdBox.attr('value', $provinceAreaLink.attr("cid"));
                                    _hide($locationDiv);
                                    return false;
                                }

                                var palist = eval('(ChinaLocation.Province' + $provinceAreaLink.attr("hid") + ')');
                                var html = '';
                                for (var i = 0; i < palist.length; i++) {
                                    html += "<span hid='" + palist[i].id + "' cid='" + palist[i].cid + "'>" + palist[i].name + "</span>";
                                }
                                $provinceBox.html(html);

                                $provinceBox.find("span").click(function () {
                                    var $provinceLink = $(this);

                                    if (levelRegion == "province") {
                                        $box.attr('value', $provinceLink.text());
                                        $chinaIdBox.attr('value', $provinceLink.attr("cid"));
                                        _hide($locationDiv);
                                        return false;
                                    }

                                    var plist = eval('(ChinaLocation.City' + $provinceLink.attr("hid") + ')');
                                    var html = '';
                                    for (var i = 0; i < plist.length; i++) {
                                        html += "<span hid='" + plist[i].id + "' cid='" + plist[i].cid + "'>" + plist[i].name + "</span>";
                                    }
                                    $cityBox.html(html);
                                    $cityBox.find("span").click(function () {
                                        var $cityLink = $(this);

                                        if (levelRegion == "city") {
                                            $box.attr('value', $cityLink.text());
                                            $chinaIdBox.attr('value', $cityLink.attr("cid"));
                                            _hide($locationDiv);
                                            return false;
                                        }

                                        var clist = eval('(ChinaLocation.County' + $cityLink.attr("hid") + ')');
                                        var html = '';
                                        for (var i = 0; i < clist.length; i++) {
                                            try {
                                                html += "<span hid='" + clist[i].id + "' cid='" + clist[i].cid + "'>" + clist[i].name + "</span>";
                                            } catch (ex) { }
                                        }
                                        $countyBox.html(html);
                                        $countyBox.find("span").click(function () {
                                            var $countyLink = $(this);

                                            if (levelRegion == "county") {
                                                $box.attr('value', $countyLink.text());
                                                $chinaIdBox.attr('value', $countyLink.attr("cid"));
                                                _hide($locationDiv);
                                                return false;
                                            }

                                        });
                                        return false;
                                    });
                                    return false;
                                });

                                return false;
                            });

                            $provinceBox.find("span").click(function () {
                                var $provinceLink = $(this);

                                if (levelRegion == "province") {
                                    $box.attr('value', $provinceLink.text());
                                    $chinaIdBox.attr('value', $provinceLink.attr("cid"));
                                    _hide($locationDiv);
                                    return false;
                                }

                                var list = eval('(ChinaLocation.City' + $provinceLink.attr("hid") + ')');
                                var html = '';
                                for (var i = 0; i < list.length; i++) {
                                    html += "<span hid='" + list[i].id + "' cid='" + list[i].cid + "'>" + list[i].name + "</span>";
                                }
                                $cityBox.html(html);
                                $cityBox.find("span").click(function () {
                                    var $cityLink = $(this);

                                    if (levelRegion == "city") {
                                        $box.attr('value', $cityLink.text());
                                        $chinaIdBox.attr('value', $cityLink.attr("cid"));
                                        _hide($locationDiv);
                                        return false;
                                    }

                                    var list = eval('(ChinaLocation.County' + $cityLink.attr("hid") + ')');
                                    var html = '';
                                    for (var i = 0; i < list.length; i++) {
                                        html += "<span hid='" + list[i].id + "' cid='" + list[i].cid + "'>" + list[i].name + "</span>";
                                    }
                                    $countyBox.html(html);
                                    $countyBox.find("span").click(function () {
                                        var $countyLink = $(this);

                                        if (levelRegion == "county") {
                                            $box.attr('value', $countyLink.text());
                                            $chinaIdBox.attr('value', $countyLink.attr("cid"));
                                            _hide($locationDiv);
                                            return false;
                                        }
                                    });
                                    return false;
                                });
                                return false;
                            });

                            $cityBox.find("span").click(function () {
                                var $cityLink = $(this);

                                if (levelRegion == "city") {
                                    $box.attr('value', $cityLink.text());
                                    $chinaIdBox.attr('value', $cityLink.attr("cid"));
                                    _hide($locationDiv);
                                    return false;
                                }

                                var list = eval('(ChinaLocation.County' + $cityLink.attr("hid") + ')');
                                var html = '';
                                for (var i = 0; i < list.length; i++) {
                                    html += "<span hid='" + list[i].id + "' cid='" + list[i].cid + "'>" + list[i].name + "</span>";
                                }
                                $countyBox.html(html);
                                $countyBox.find("span").click(function () {
                                    var $countyLink = $(this);

                                    if (levelRegion == "county") {
                                        $box.attr('value', $countyLink.text());
                                        $chinaIdBox.attr('value', $countyLink.attr("cid"));
                                        _hide($locationDiv);
                                        return false;
                                    }
                                });
                                return false;
                            });

                            $countyBox.find("span").click(function () {
                                var $countyLink = $(this);
                                if (levelRegion == "county") {
                                    $box.attr('value', $countyLink.text());
                                    $chinaIdBox.attr('value', $countyLink.attr("cid"));
                                    _hide($locationDiv);
                                    return false;
                                }
                            });
                        }
                        break;

                    default:
                        break;
                }
                return false;
            });
        });
    }

    function _show($box) {
        $box.addClass("locationShow");
        $(document).bind("click", { box: $box }, _handler);
    }
    function _hide($box) {
        $box.removeClass("locationShow");
        $(document).unbind("click", _handler);
    }

    function _handler(event) {
        _hide(event.data.box);
    }
})(jQuery);
