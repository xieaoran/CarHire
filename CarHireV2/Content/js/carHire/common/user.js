var regEmail, regPassword, regConfirmPassword, regName, regPhone;
var regEmailOK, regPasswordOK, regConfirmPasswordOK, regNameOK, regPhoneOK;
var loginEmail, loginPassword;
var loginEmailOK, loginPasswordOK;
var changePassword, changeConfirmPassword;
var changePasswordOK, changeConfirmPasswordOK;
var Locked = false;

function loginDialog(isOrder) {
    $.Dialog({
        overlay: true,
        shadow: true,
        flat: true,
        icon: "<i class=\"fa fa-sign-in\"></i>",
        title: "登录",
        width: 300,
        height: 200,
        padding: 10,
        overlayClickClose: false,
        draggable: true,
        onShow: function (_dialog) {
            if (isOrder == true) {
                var content = "<label>电子邮箱</label>" +
                    "<div id=\"loginEmailBorder\" class=\"input-control email info-state\">" +
                    "<input type=\"email\" id=\"loginEmail\" onkeyup=\"loginCheckEmail();\">" +
                    "</div> " +
                    "<label>密码</label>" +
                    "<div id=\"loginPasswordBorder\" class=\"input-control password info-state\">" +
                    "<input type=\"password\" id=\"loginPassword\" onkeyup=\"loginCheckPassword();\">" +
                    "<button class=\"btn-reveal\"></button></div> " +
                    "<div class=\"form-actions\">" +
                    "<button class=\"button\" id=\"login\" style=\"background-color: #eaeaea; color:#bebebe\" onclick=\"login(true);\">登录</button> " +
                    "<button class=\"button\" type=\"button\" onclick=\"$.Dialog.close();\">取消</button> " +
                    "</div>";
            }
            else {
                var content = "<label>电子邮箱</label>" +
                    "<div id=\"loginEmailBorder\" class=\"input-control email info-state\">" +
                    "<input type=\"email\" id=\"loginEmail\" onkeyup=\"loginCheckEmail();\">" +
                    "</div> " +
                    "<label>密码</label>" +
                    "<div id=\"loginPasswordBorder\" class=\"input-control password info-state\">" +
                    "<input type=\"password\" id=\"loginPassword\" onkeyup=\"loginCheckPassword();\">" +
                    "<button class=\"btn-reveal\"></button></div> " +
                    "<div class=\"form-actions\">" +
                    "<button class=\"button\" id=\"login\" style=\"background-color: #eaeaea; color:#bebebe\" onclick=\"login(false);\">登录</button> " +
                    "<button class=\"button\" type=\"button\" onclick=\"$.Dialog.close();\">取消</button> " +
                    "</div>";
            }
            loginEmailOK = false;
            loginPasswordOK = false;
            $.Dialog.content(content);
            $.Metro.initInputs();
        }
    });
}

function loginCheckEmail() {
    var email = $("#loginEmail")[0].value.toLowerCase();
    var emailBorder = $("#loginEmailBorder");
    if (email == "") {
        emailBorder.removeClass("error-state");
        emailBorder.removeClass("success-state");
        emailBorder.addClass("info-state");
        loginEmailOK = false;
    } else if (validateEmail(email) == false) {
        emailBorder.removeClass("success-state");
        emailBorder.removeClass("info-state");
        emailBorder.addClass("error-state");
        loginEmailOK = false;
    } else {
        emailBorder.removeClass("error-state");
        emailBorder.removeClass("info-state");
        emailBorder.addClass("success-state");
        loginEmailOK = true;
        loginEmail = email;
    }
    loginCheckDone();
}

function loginCheckPassword() {
    var password = $("#loginPassword")[0].value;
    var passwordBorder = $("#loginPasswordBorder");
    if (password == "") {
        passwordBorder.removeClass("error-state");
        passwordBorder.removeClass("success-state");
        passwordBorder.addClass("info-state");
        loginPasswordOK = false;
    } else if (password.length < 6) {
        passwordBorder.removeClass("success-state");
        passwordBorder.removeClass("info-state");
        passwordBorder.addClass("error-state");
        loginPasswordOK = false;
    } else {
        passwordBorder.removeClass("error-state");
        passwordBorder.removeClass("info-state");
        passwordBorder.addClass("success-state");
        loginPasswordOK = true;
        loginPassword = password;
    }
    loginCheckDone();
}

function loginCheckDone() {
    if (loginEmailOK == true && loginPasswordOK == true)
        $("#login").animate({ "background-color": "#03a9f4", "color": "#222222" }, "fast");
    else $("#login").css({ "background-color": "#eaeaea", "color": "#bebebe" });
}
function login(isOrder) {
    if (loginEmailOK != true || loginPasswordOK != true || Locked == true) return;
    Locked = true;
    $.post("https://www.zuchefw.com/User/Login",
        {
            email: loginEmail,
            password: loginPassword
        },
        function (data) {
            if (data.Succeeded == true) {
                $.Notify({
                    caption: "<i class=\"fa fa-sign-in\"></i>  登录",
                    content: "登录成功，欢迎 " + data.LoggedInUserName + " 顾客的到来，我们将竭诚为您服务！",
                    style: { background: "#4caf50", color: "#fafafa" },
                    timeout: 3000
                });
                setTimeout(function () {
                    Locked = false;
                    if (isOrder == true) {
                        $.Dialog.close();
                        lastCheck();
                    }
                    else window.location.reload(true);
                }, 4000);
            } else {
                switch (data.Error) {
                    case "该电子邮箱尚未注册":
                        var emailBorder = $("#loginEmailBorder");
                        emailBorder.removeClass("success-state");
                        emailBorder.removeClass("info-state");
                        emailBorder.addClass("error-state");
                        loginEmailOK = false;
                        break;
                    case "密码不正确，请核对您的密码":
                        var loginPasswordBorder = $("#loginPasswordBorder");
                        loginPasswordBorder.removeClass("success-state");
                        loginPasswordBorder.removeClass("info-state");
                        loginPasswordBorder.addClass("error-state");
                        loginPasswordOK = false;
                        break;
                    default:
                        break;
                }
                loginCheckDone();
                Locked = false;
                $.Notify({
                    caption: "<i class=\"fa fa-sign-in\"></i>  登录",
                    content: "登录失败，" + data.Error + "。",
                    style: { background: "#f44336", color: "#fafafa" },
                    timeout: 3000
                });
            }
        });
}
function logOut() {
    if (Locked == true) return;
    Locked = true;
    $.post("/User/LogOut", function (data) {
        if (data.Succeeded == true) {
            $.Notify({
                caption: "<i class=\"fa fa-sign-out\"></i>  注销",
                content: "注销成功，我们期待着您的下次到来！",
                style: { background: "#4caf50", color: "#fafafa" },
                timeout: 3000
            });
            setTimeout(function () {
                Locked = false;
                window.location.href = "/Home/Index";
            }, 4000);
        }
    });
}
function regDialog(isOrder) {
    $.Dialog({
        overlay: true,
        shadow: true,
        flat: true,
        icon: "<i class=\"fa fa-user-plus\"></i>",
        title: "注册",
        width: 300,
        height: 200,
        padding: 10,
        overlayClickClose: false,
        draggable: true,
        onShow: function (_dialog) {
            if (isOrder == true) {
                var content = "<label>电子邮箱</label>" +
                    "<div id=\"regEmailBorder\" class=\"input-control email info-state\"><input type=\"email\" id=\"regEmail\" onkeyup=\"regCheckEmail();\">" +
                    "</div>" +
                    "<label>密码</label>" +
                    "<div id=\"regPasswordBorder\" class=\"input-control password info-state\">" +
                    "<input type=\"password\" id=\"regPassword\" onkeyup=\"regCheckPassword();\">" +
                    "<button class=\"btn-reveal\"></button></div> " +
                    "<label>确认密码</label>" +
                    "<div id=\"regConfirmPasswordBorder\" class=\"input-control password info-state\">" +
                    "<input type=\"password\" id=\"regConfirmPassword\" onkeyup=\"regCheckConfirmPassword();\">" +
                    "<button class=\"btn-reveal\"></button></div>" +
                    "<label>姓名</label>" +
                    "<div id=\"regNameBorder\" class=\"input-control text info-state\"><input type=\"text\" id=\"regName\" onkeyup=\"regCheckName();\">" +
                    "</div> " +
                    "<label>手机</label>" +
                    "<div id=\"regPhoneBorder\" class=\"input-control tel info-state\"><input type=\"tel\" id=\"regPhone\" onkeyup=\"regCheckPhone();\">" +
                    "</div> " +
                    "<div class=\"form-actions\">" +
                    "<button id=\"register\" class=\"button\" style=\"background-color:#eaeaea; color:#bebebe\" onclick=\"register(true);\">注册</button> " +
                    "<button class=\"button\" type=\"button\" onclick=\"$.Dialog.close();\">取消</button> " +
                    "</div>";
            }
            else {
                var content = "<label>电子邮箱</label>" +
                    "<div id=\"regEmailBorder\" class=\"input-control email info-state\"><input type=\"email\" id=\"regEmail\" onkeyup=\"regCheckEmail();\">" +
                    "</div>" +
                    "<label>密码</label>" +
                    "<div id=\"regPasswordBorder\" class=\"input-control password info-state\">" +
                    "<input type=\"password\" id=\"regPassword\" onkeyup=\"regCheckPassword();\">" +
                    "<button class=\"btn-reveal\"></button></div> " +
                    "<label>确认密码</label>" +
                    "<div id=\"regConfirmPasswordBorder\" class=\"input-control password info-state\">" +
                    "<input type=\"password\" id=\"regConfirmPassword\" onkeyup=\"regCheckConfirmPassword();\">" +
                    "<button class=\"btn-reveal\"></button></div>" +
                    "<label>姓名</label>" +
                    "<div id=\"regNameBorder\" class=\"input-control text info-state\"><input type=\"text\" id=\"regName\" onkeyup=\"regCheckName();\">" +
                    "</div> " +
                    "<label>手机</label>" +
                    "<div id=\"regPhoneBorder\" class=\"input-control tel info-state\"><input type=\"tel\" id=\"regPhone\" onkeyup=\"regCheckPhone();\">" +
                    "</div> " +
                    "<div class=\"form-actions\">" +
                    "<button id=\"register\" class=\"button\" style=\"background-color:#eaeaea; color:#bebebe\" onclick=\"register(false);\">注册</button> " +
                    "<button class=\"button\" type=\"button\" onclick=\"$.Dialog.close();\">取消</button> " +
                    "</div>";
            }
            regEmailOK = false;
            regPasswordOK = false;
            regConfirmPasswordOK = false;
            regNameOK = false;
            regPhoneOK = false;
            $.Dialog.content(content);
            $.Metro.initInputs();
        }
    });
}

function regCheckEmail() {
    var email = $("#regEmail")[0].value.toLowerCase();
    var emailBorder = $("#regEmailBorder");
    if (email == "") {
        emailBorder.removeClass("error-state");
        emailBorder.removeClass("success-state");
        emailBorder.addClass("info-state");
        regEmailOK = false;
    } else if (validateEmail(email) == false) {
        emailBorder.removeClass("success-state");
        emailBorder.removeClass("info-state");
        emailBorder.addClass("error-state");
        regEmailOK = false;
    } else {
        emailBorder.removeClass("error-state");
        emailBorder.removeClass("info-state");
        emailBorder.addClass("success-state");
        regEmailOK = true;
        regEmail = email;
    }
    regCheckDone();
}

function regCheckPassword() {
    var password = $("#regPassword")[0].value;
    var passwordBorder = $("#regPasswordBorder");
    if (password == "") {
        passwordBorder.removeClass("error-state");
        passwordBorder.removeClass("success-state");
        passwordBorder.addClass("info-state");
        regPasswordOK = false;
    } else if (password.length < 6) {
        passwordBorder.removeClass("success-state");
        passwordBorder.removeClass("info-state");
        passwordBorder.addClass("error-state");
        regPasswordOK = false;
    } else {
        passwordBorder.removeClass("error-state");
        passwordBorder.removeClass("info-state");
        passwordBorder.addClass("success-state");
        regPasswordOK = true;
        regPassword = password;
    }
    regCheckConfirmPassword();
    regCheckDone();
}

function regCheckConfirmPassword() {
    var password = $("#regPassword")[0].value;
    var confirmPassword = $("#regConfirmPassword")[0].value;
    var confirmPasswordBorder = $("#regConfirmPasswordBorder");
    if (confirmPassword == "") {
        confirmPasswordBorder.removeClass("error-state");
        confirmPasswordBorder.removeClass("success-state");
        confirmPasswordBorder.addClass("info-state");
        regConfirmPasswordOK = false;
    } else if (confirmPassword != password) {
        confirmPasswordBorder.removeClass("success-state");
        confirmPasswordBorder.removeClass("info-state");
        confirmPasswordBorder.addClass("error-state");
        regConfirmPasswordOK = false;
    } else {
        confirmPasswordBorder.removeClass("error-state");
        confirmPasswordBorder.removeClass("info-state");
        confirmPasswordBorder.addClass("success-state");
        regConfirmPasswordOK = true;
        regConfirmPassword = confirmPassword;
    }
    regCheckDone();
}

function regCheckName() {
    var name = $("#regName")[0].value;
    var nameBorder = $("#regNameBorder");
    if (name == "") {
        nameBorder.removeClass("error-state");
        nameBorder.removeClass("success-state");
        nameBorder.addClass("info-state");
        regNameOK = false;
    } else if (name.length > 30) {
        nameBorder.removeClass("success-state");
        nameBorder.removeClass("info-state");
        nameBorder.addClass("error-state");
        regNameOK = false;
    } else {
        nameBorder.removeClass("error-state");
        nameBorder.removeClass("info-state");
        nameBorder.addClass("success-state");
        regNameOK = true;
        regName = name;
    }
    regCheckDone();
}

function regCheckPhone() {
    var phone = $("#regPhone")[0].value;
    var phoneBorder = $("#regPhoneBorder");
    if (phone == "") {
        phoneBorder.removeClass("error-state");
        phoneBorder.removeClass("success-state");
        phoneBorder.addClass("info-state");
        regPhoneOK = false;
    } else if (validatePhone(phone) == false) {
        phoneBorder.removeClass("success-state");
        phoneBorder.removeClass("info-state");
        phoneBorder.addClass("error-state");
        regPhoneOK = false;
    } else {
        phoneBorder.removeClass("error-state");
        phoneBorder.removeClass("info-state");
        phoneBorder.addClass("success-state");
        regPhoneOK = true;
        regPhone = phone;
    }
    regCheckDone();
}

function regCheckDone() {
    if (regEmailOK == true && regPasswordOK == true &&
        regConfirmPasswordOK == true && regNameOK == true &&
        regPhoneOK == true) $("#register").animate({ "background-color": "#4caf50", "color": "#222222" }, "fast");
    else $("#register").css({ "background-color": "#eaeaea", "color": "#bebebe" });
}

function register(isOrder) {
    if (regEmailOK != true || regPasswordOK != true ||
        regConfirmPasswordOK != true || regNameOK != true ||
        regPhoneOK != true || Locked == true) return;
    Locked = true;
    $.post("https://www.zuchefw.com/User/Register",
        {
            email: regEmail,
            name: regName,
            password: regPassword,
            confirmPassword: regConfirmPassword,
            cellPhoneNumberStr: regPhone
        },
        function (data) {
            if (data.Succeeded == true) {
                $.Notify({
                    caption: "<i class=\"fa fa-user-plus\"></i>  注册",
                    content: "注册成功，欢迎 " + data.NewUserName + " 顾客加入我们，我们将竭诚为您服务！",
                    style: { background: "#4caf50", color: "#fafafa" },
                    timeout: 3000
                });
                setTimeout(function () {
                    Locked = false;
                    if (isOrder == true) {
                        $.Dialog.close();
                        lastCheck();
                    }
                    else window.location.reload(true);
                }, 4000);
            } else {
                switch (data.Error) {
                    case "该邮箱已被注册":
                        var emailBorder = $("#regEmailBorder");
                        emailBorder.removeClass("success-state");
                        emailBorder.removeClass("info-state");
                        emailBorder.addClass("error-state");
                        regEmailOK = false;
                        break;
                    case "两次输入的密码不一致":
                        var confirmPasswordBorder = $("#regConfirmPasswordBorder");
                        confirmPasswordBorder.removeClass("success-state");
                        confirmPasswordBorder.removeClass("info-state");
                        confirmPasswordBorder.addClass("error-state");
                        regConfirmPasswordOK = false;
                        break;
                    default:
                        break;
                }
                regCheckDone();
                Locked = false;
                $.Notify({
                    caption: "<i class=\"fa fa-user-plus\"></i>  注册",
                    content: "注册失败，" + data.Error + "。",
                    style: { background: "#f44336", color: "#fafafa" },
                    timeout: 3000
                });
            }
        });
}

function changePasswordDialog() {
    $.Dialog({
        overlay: true,
        shadow: true,
        flat: true,
        icon: "<i class=\"fa fa-key\"></i>",
        title: "修改密码",
        width: 300,
        height: 200,
        padding: 10,
        overlayClickClose: false,
        draggable: true,
        onShow: function (_dialog) {
            var content = "<label>新密码</label>" +
                "<div id=\"changePasswordBorder\" class=\"input-control password info-state\">" +
                "<input type=\"password\" id=\"changePassword\" onkeyup=\"changeCheckPassword();\">" +
                "<button class=\"btn-reveal\"></button></div> " +
                "<label>确认密码</label>" +
                "<div id=\"changeConfirmPasswordBorder\" class=\"input-control password info-state\">" +
                "<input type=\"password\" id=\"changeConfirmPassword\" onkeyup=\"changeCheckConfirmPassword();\">" +
                "<button class=\"btn-reveal\"></button></div>" +
                "<div class=\"form-actions\">" +
                "<button id=\"change\" class=\"button\" style=\"background-color: #eaeaea; color: #bebebe\" onclick=\"change();\">确认</button> " +
                "<button class=\"button\" type=\"button\" onclick=\"$.Dialog.close();\">取消</button> " +
                "</div>";
            changePasswordOK = false;
            changeConfirmPasswordOK = false;
            $.Dialog.content(content);
            $.Metro.initInputs();
        }
    });
}

function changeCheckPassword() {
    var password = $("#changePassword")[0].value;
    var passwordBorder = $("#changePasswordBorder");
    if (password == "") {
        passwordBorder.removeClass("error-state");
        passwordBorder.removeClass("success-state");
        passwordBorder.addClass("info-state");
        changePasswordOK = false;
    } else if (password.length < 6) {
        passwordBorder.removeClass("success-state");
        passwordBorder.removeClass("info-state");
        passwordBorder.addClass("error-state");
        changePasswordOK = false;
    } else {
        passwordBorder.removeClass("error-state");
        passwordBorder.removeClass("info-state");
        passwordBorder.addClass("success-state");
        changePasswordOK = true;
        changePassword = password;
    }
    changeCheckDone();
}

function changeCheckConfirmPassword() {
    var password = $("#changePassword")[0].value;
    var confirmPassword = $("#changeConfirmPassword")[0].value;
    var confirmPasswordBorder = $("#changeConfirmPasswordBorder");
    if (confirmPassword == "") {
        confirmPasswordBorder.removeClass("error-state");
        confirmPasswordBorder.removeClass("success-state");
        confirmPasswordBorder.addClass("info-state");
        changeConfirmPasswordOK = false;
    } else if (confirmPassword != password) {
        confirmPasswordBorder.removeClass("success-state");
        confirmPasswordBorder.removeClass("info-state");
        confirmPasswordBorder.addClass("error-state");
        changeConfirmPasswordOK = false;
    } else {
        confirmPasswordBorder.removeClass("error-state");
        confirmPasswordBorder.removeClass("info-state");
        confirmPasswordBorder.addClass("success-state");
        changeConfirmPasswordOK = true;
        changeConfirmPassword = confirmPassword;
    }
    changeCheckDone();
}

function changeCheckDone() {
    if (changePasswordOK == true && changeConfirmPasswordOK == true)
        $("#change").animate({ "background-color": "#f44336", "color": "#222222" }, "fast");
    else $("#change").css({ "background-color": "#eaeaea", "color": "#bebebe" });
}

function change() {
    if (changePasswordOK != true || changeConfirmPasswordOK != true || Locked == true) return;
    Locked = true;
    $.post("https://www.zuchefw.com/User/ChangePassword",
        {
            changePassword: changePassword,
            changeConfirmPassword: changeConfirmPassword
        },
        function (data) {
            if (data.Succeeded == true) {
                $.Notify({
                    caption: "<i class=\"fa fa-key\"></i>  修改密码",
                    content: "密码修改成功，请牢记您的新密码！",
                    style: { background: "#4caf50", color: "#fafafa" },
                    timeout: 3000
                });
                setTimeout(function () {
                    Locked = false;
                    window.location.reload(true);
                }, 4000);
            } else {
                switch (data.Error) {
                    case "两次输入的密码不一致":
                        var confirmPasswordBorder = $("#changeConfirmPasswordBorder");
                        confirmPasswordBorder.removeClass("success-state");
                        confirmPasswordBorder.removeClass("info-state");
                        confirmPasswordBorder.addClass("error-state");
                        changeConfirmPasswordOK = false;
                        break;
                    case "用户尚未登录或登录已过期，请登录后再试":
                        setTimeout(function () {
                            Locked = false;
                            window.location.reload(true);
                        }, 4000);
                    default:
                        break;
                }
                changeCheckDone();
                Locked = false;
                $.Notify({
                    caption: "<i class=\"fa fa-sign-in\"></i>  修改密码",
                    content: "密码修改失败，" + data.Error + "。",
                    style: { background: "#f44336", color: "#fafafa" },
                    timeout: 3000
                });
            }
        });
}