var Login = function () {
    this.loginName = null;
    this.loginPwd = null;
    this.validCode = null;
    this.btnLogin = null;
    this.hrefNewPic = null;
}
//$("body").keydown(function (e) {
//    var _this = this;

//    if (e.keyCode == 13) {
//        $('input[name="username"]').focus();
//    }

//});

$('input[name="username"]').keydown(function (e) {
    if (e.keyCode == 13) {
        $('input[name="password"]').focus();
    }
});

$('input[name="password"]').keydown(function (e) {
    if (e.keyCode == 13) {
        $('input[name="validCode"]').focus();
    }
});

$('input[name="validCode"]').keydown(function (e) {
    if (e.keyCode == 13) {
        $('input[name="btnLogin"]').focus();
    }
});




//初始化方法
Login.prototype.init = function () {
    var _this = this;
    $('[name="username"]').focus();
    this.loginName = $('[name="username"]');
    this.loginPwd = $('[name="password"]');
    this.validCode = $('[name="validCode"]');
    this.btnLogin = $('[name="btnLogin"]');
    this.hrefNewPic = $('[name="codepic"]');

    this.bindEvent();
}

Login.prototype.bindEvent = function () {
    var _this = this;

    _this.btnLogin.click(function (e) {
        _this.Login(e);
    });
    _this.hrefNewPic.click(function (e) {
        _this.GetValidCode(e);
    });

    $('input[name="username"]').focus();

}
Login.prototype.GetValidCode = function (e) {
    $('[name="codepic"]')[0].src = "/Home/GetValidCode?time=" + (new Date()).getTime();
}

Login.prototype.Login = function (e) {
    var _this = this;
    var aj = 1;
    var postData = {};
    var LoginAccount = this.loginName.attr('value');
    if (LoginAccount == "") {
        $("input[name='username']").select();
        alert("请输入账户名！");
        return;
    }
    var password = this.loginPwd.attr('value');
    if (password == "") {
        $("input[name='password']").select();
        alert("请输入密码！");
        return;
    }
    postData["LoginPwd"] = password;
    postData["CodeNum"] = $('[name="validCode"]').attr('value');
    postData["LoginInput"] = LoginAccount;
    if (aj == 1) {
        aj = 0;
        $.ajax({
            type: "post",
            url: "/Home/DoLogin",
            dataType: 'json',
            data: postData,
            success: function (x) {
                if (x) {
                    if (x.code) {
                        if ("ok" === x.code || "OK" === x.code) {
                            //alert("logined");
                            window.location.replace("/");
                            //window.location.href = "/home/index";
                            return false;
                        }
                        else {
                            //验证码错误
                            if ("NUMCODEERROR" === x.code) {
                                $('[name="validCode"]').select();
                                alert(x.msg);
                                return;
                            }
                            //账户已存在或账户不能为空 
                            if ("ACCOUNTNOTEXISTS" === x.code || "Forbidden" === x.code || "FAILED" === x.code || "ACCOUNTNULL" == x.code) {
                                $("input[name='username']").select();
                                //重新生成验证码
                                _this.GetValidCode();
                                alert(x.msg);
                                return;
                            }
                            //密码
                            if ("PASSWORDNULL" === x.code) {
                                $("input[name='txtLoginInput']").select();
                                alert(x.msg);
                                return;
                            }

                        }
                    }
                }
            },
            complete: function () { aj = 1; }
        });
        return false;
    }
}

var login = new Login();
login.init();