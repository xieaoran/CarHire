var allDone;
var Locked = false;
var selectedAirport, planeTime, selectedDate;

function getNowDate() {
    var now = new Date();
    return formatDate(now);
}

function formatDate(date) {
    var seperator = "-";
    var month = date.getMonth() + 1;
    var day = date.getDate();
    if (month >= 1 && month <= 9) {
        month = "0" + month;
    }
    if (day >= 0 && day <= 9) {
        day = "0" + day;
    }
    return date.getFullYear() + seperator + month + seperator + day;
}

function resize() {
    var newHeight = $.find(".normalSlide")[0].childNodes[1].clientHeight;
    $("#recommends").height(newHeight);
}

function readCookies() {
    var selectedAirport = $.cookie("selectedAirport", Number);
    var planeTime = $.cookie("planeTime");
    if (selectedAirport != undefined) {
        $("#airport" + selectedAirport).addClass("selected");
        $("#airportNone").css("opacity", 0);
        $("#airportOK").css("opacity", 1.0);
    }
    if (planeTime != undefined) {
        $("#timeInput")[0].value = planeTime;
        $("#timeBorder").addClass("success-state");
        $("#timeNone").css("opacity", 0);
        $("#timeOK").css("opacity", 1.0);
    }
    checkDone();
}

function timeChanged() {
    var timeInput = $("#timeInput")[0].value;
    var timeBorder = $("#timeBorder");
    if (timeInput == "") {
        timeBorder.removeClass("error-state");
        timeBorder.removeClass("success-state");
        $("#timeNone").animate({ opacity: "1.0" }, "fast");
        $("#timeOK").animate({ opacity: "0" }, "fast");
        $.removeCookie("planeTime", { path: "/" });
    } else if (validateTime(timeInput) == false) {
        timeBorder.removeClass("success-state");
        timeBorder.addClass("error-state");
        $("#timeNone").animate({ opacity: "1.0" }, "fast");
        $("#timeOK").animate({ opacity: "0" }, "fast");
        $.removeCookie("planeTime", { path: "/" });
    } else {
        timeBorder.removeClass("error-state");
        timeBorder.addClass("success-state");
        $("#timeNone").animate({ opacity: "0" }, "fast");
        $("#timeOK").animate({ opacity: "1.0" }, "fast");
        $.cookie("planeTime", timeInput, { path: "/" });
    }
    checkDone();
}

function noteChanged() {
    var noteInput = $("#noteInput")[0].value;
    var noteBorder = $("#noteBorder");
    if (noteInput == "") {
        noteBorder.removeClass("error-state");
        noteBorder.removeClass("success-state");
        $("#noteNone").animate({ opacity: "1.0" }, "fast");
        $("#noteOK").animate({ opacity: "0" }, "fast");
    } else if (noteInput.length > 50) {
        noteBorder.removeClass("success-state");
        noteBorder.addClass("error-state");
        $("#noteNone").animate({ opacity: "1.0" }, "fast");
        $("#noteOK").animate({ opacity: "0" }, "fast");
    } else {
        noteBorder.removeClass("error-state");
        noteBorder.addClass("success-state");
        $("#noteNone").animate({ opacity: "0" }, "fast");
        $("#noteOK").animate({ opacity: "1.0" }, "fast");
    }
}

function tileSelected(index, mustSelect) {
    var re = /^[A-Za-z]+/g;
    var reResult = re.exec(index);
    var indicator = $("#" + reResult[0] + "OK");
    var icon = $("#" + reResult[0] + "None");
    var numIndex = parseInt(index.substr(reResult[0].length));
    var selectedTile = $("#" + index);
    var hasSelected = mustSelect ? false : selectedTile.hasClass("selected");
    var selectedClass = selectedTile.attr("class").split(" ")[1];
    if (hasSelected) {
        switch (selectedClass) {
            case "selectAirportTile":
                $.removeCookie("selectedAirport", { path: "/" });
                break;
            default:
                break;
        }
    } else {
        switch (selectedClass) {
            case "selectAirportTile":
                $.cookie("selectedAirport", numIndex, { path: "/" });
                break;
            default:
                break;
        }
    }
    $("." + selectedClass).removeClass("selected");
    if (hasSelected) {
        selectedTile.removeClass("selected");
        indicator.animate({ opacity: "0" }, "fast");
        icon.animate({ opacity: "1.0" }, "fast");
    } else {
        selectedTile.addClass("selected");
        indicator.animate({ opacity: "1.0" }, "fast");
        icon.animate({ opacity: "0" }, "fast");
    }
    checkDone();
}

function checkDone() {
    var rentTile = $("#rent");
    allDone = !($.cookie("planeTime") == undefined ||
        $.cookie("selectedAirport") == undefined);
    var rentImageCar = $("#rentImageCar");
    var rentMask = $("#rentMask");
    if (allDone) {
        var animateRight = rentTile[0].clientWidth;
        var animateRightStr = animateRight.toString() + "px";
        rentTile.addClass("enabled");
        rentImageCar.css("visibility", "visible");
        rentMask.css("visibility", "visible");
        rentTile.css({ "background-color": "#ef6c00", "color": "#ffffff" });
        rentImageCar.animate({ left: animateRightStr }, 1000);
        rentMask.animate({ left: animateRightStr }, 1000);
    } else {
        if (rentTile.hasClass("enabled")) {
            rentTile.removeClass("enabled");
            rentTile.css({ "background-color": "#eaeaea", "color": "#bebebe" });
            rentImageCar.stop();
            rentMask.stop();
            rentImageCar.css("visibility", "hidden");
            rentMask.css("visibility", "hidden");
            rentImageCar.css({ "left": 0 });
            rentMask.css({ "left": 0 });
        }
    }
}
function loginOrRegAndSubmitOrder() {
    if (allDone) {
        $.Dialog({
            overlay: true,
            shadow: true,
            flat: true,
            icon: "<i class=\"fa fa-plane\"></i>",
            title: "接机服务",
            width: 400,
            height: 135,
            padding: 10,
            overlayClickClose: false,
            draggable: true,
            onShow: function (_dialog) {
                var content = "<p>在预约接机前，您需要注册一个用户或登录您的用户。</p>" +
                    "<p>请从下面选择一项，预约流程将在您注册或登录后继续。</p>" +
                    "<div class=\"form-actions\">" +
                    "<button class=\"button\" style=\"background-color:#4caf50; color:#222222\" onclick=\"$.Dialog.close();regDialog(true);\">注册</button> " +
                    "<button class=\"button\" style=\"background-color: #03a9f4; color:#222222\" onclick=\"$.Dialog.close();loginDialog(true);\">登录</button> " +
                    "</div>";
                $.Dialog.content(content);
                $.Metro.initInputs();
            }
        });
    }
}
function lastCheck() {
    selectedAirport = $.cookie("selectedAirport", Number);
    planeTime = $.cookie("planeTime");
    selectedDate = $("#dateInput")[0].value;
    $.getJSON("/api/Airports/" + selectedAirport.toString(),
        function (dataAirport) {
            var airportName = dataAirport.Name;
            $.Dialog({
                overlay: true,
                shadow: true,
                flat: true,
                icon: "<i class=\"fa fa-plane\"></i>",
                title: "接机服务",
                width: 400,
                height: 135,
                padding: 10,
                overlayClickClose: false,
                draggable: true,
                sysBtnCloseClick: function (e) {
                    window.location.reload(true);
                },
                onShow: function (_dialog) {
                    var content = "<p>以下是您本次订单的详细信息。</p>" +
                        "<p>在正式下单之前，这是您进行确认的最后一次机会。</p>" +
                        "<p>请认真核对您的各项选择。</p>" +
                        "<table class=\"table bordered\" style=\"margin-top: 14pt\">" +
                        "<tr><td>机场</td>" + "<td>" + airportName + "</td></tr>" +
                        "<tr><td>接机日期</td>" + "<td>" + selectedDate + "</td></tr>" +
                        "<tr><td>接机时间</td>" + "<td>" + planeTime + "</td></tr>" +
                        "</table>" +
                        "<div class=\"form-actions\">" +
                        "<button class=\"button\" style=\"background-color:#4caf50; color:#222222\" onclick=\"submitOrder();\">确认</button> " +
                        "<button class=\"button\" type=\"button\" onclick=\"window.location.reload(true);\">取消</button> " +
                        "</div>";
                    $.Dialog.content(content);
                    $.Metro.initInputs();
                }
            });
        }).fail(
        function () {
            alert("请检查您的网络状况！");
        });
}
function submitOrder() {
    if (Locked == true) return;
    Locked = true;
    var note = $("#noteInput")[0].value;
    $.post("https://www.zuchefw.com/Order/PlaneOrder",
    {
        airportID: selectedAirport,
        datePlane: selectedDate,
        timePlane: planeTime,
        note: note
    },
    function (data) {
        if (data.Succeeded == true) {
            $.Notify({
                caption: "<i class=\"fa fa-plane\"></i>  接机服务",
                content: "预约成功，您的订单号为" + padLeft(data.OrderID, 8) + "，我们将会在您的航班到达时前去迎接您。",
                style: { background: "#4caf50", color: "#fafafa" },
                timeout: 3000
            });
            setTimeout(function () {
                Locked = false;
                window.location.href = "/User/UserPanel";
            }, 4000);
        } else {
            $.Notify({
                caption: "<i class=\"fa fa-plane\"></i>  接机服务",
                content: "预约失败，" + data.Error + "。",
                style: { background: "#f44336", color: "#fafafa" },
                timeout: 3000
            });
            setTimeout(function () {
                Locked = false;
                window.location.reload(true);
            }, 4000);
        }
    });
}