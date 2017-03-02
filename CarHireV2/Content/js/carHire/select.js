var allDone;
var Locked = false;
var selectedCarType, selectedStartDate, startTime, selectedPlace, needDriver, manualConfirm;

function getNowDate() {
    var now = new Date();
    return formatDate(now);
}

function formatDate(date) {
    var separator = "-";
    var month = date.getMonth() + 1;
    var day = date.getDate();
    if (month >= 1 && month <= 9) {
        month = "0" + month;
    }
    if (day >= 0 && day <= 9) {
        day = "0" + day;
    }
    return date.getFullYear() + separator + month + separator + day;
}

function resize() {
    var newHeight = $.find(".normalSlide")[0].childNodes[1].clientHeight;
    $("#recommends").height(newHeight);
}

function recommendSelected(index) {
    var re = /^[A-Za-z]+/g;
    var reResult = re.exec(index);
    var numStr = index.substr(reResult[0].length);
    var numIndex = parseInt(numStr);
    $.cookie("selectedCarType", numIndex, { path: "/" });
    window.location.href = "/Order/Select";
}

function toggleDriver() {
    var driverToggle = $("#driverToggle")[0];
    if (driverToggle.checked) {
        $.cookie("needDriver", 1, { path: "/" });
    } else {
        $.cookie("needDriver", 0, { path: "/" });
    }
}

function readCookies() {
    var selectedCarType = $.cookie("selectedCarType", Number);
    var selectedPlace = $.cookie("selectedPlace", Number);
    var needDriver = $.cookie("needDriver", Number);
    var startTime = $.cookie("startTime");
    if (selectedCarType != undefined) {
        $("#carType" + selectedCarType).addClass("selected");
        $("#carImageSelected")[0].src = $("#carImage" + selectedCarType)[0].src;
        $("#carNameSelected")[0].innerText = $("#carName" + selectedCarType)[0].innerText;
        $("#carTypeNone").css("opacity", 0);
        $("#carTypeOK").css("opacity", 1.0);
    }
    if (selectedPlace != undefined) {
        $("#place" + selectedPlace).addClass("selected");
        $("#placeImageSelected")[0].src = $("#placeImage" + selectedPlace)[0].src;
        $("#placeNameSelected")[0].innerText = $("#placeName" + selectedPlace)[0].innerText;
        $("#placeNone").css("opacity", 0);
        $("#placeOK").css("opacity", 1.0);
    }
    if (startTime != undefined) {
        $("#startTimeInput")[0].value = startTime;
        $("#startTime").addClass("success-state");
        $("#timeNone").animate({ opacity: "0" }, "fast");
        $("#timeOK").animate({ opacity: "1.0" }, "fast");
    } else {
        $("#timeNone").animate({ opacity: "1.0" }, "fast");
        $("#timeOK").animate({ opacity: "0" }, "fast");
    }
    if (needDriver == 1) {
        var driverToggle = $("#driverToggle")[0].checked = "checked";
    }
    checkDone();
}

function timeChanged() {
    var timeInput = $("#startTimeInput")[0].value;
    var timeBorder = $("#startTime");
    if (timeInput == "") {
        timeBorder.removeClass("error-state");
        timeBorder.removeClass("success-state");
        $.removeCookie("startTime", { path: "/" });
    } else if (validateTime(timeInput) == false) {
        timeBorder.removeClass("success-state");
        timeBorder.addClass("error-state");
        $.removeCookie("startTime", { path: "/" });
    } else {
        timeBorder.removeClass("error-state");
        timeBorder.addClass("success-state");
        $.cookie("startTime", timeInput, { path: "/" });
    }
    if ($.cookie("startTime") != undefined) {
        $("#timeNone").animate({ opacity: "0" }, "fast");
        $("#timeOK").animate({ opacity: "1.0" }, "fast");
    } else {
        $("#timeNone").animate({ opacity: "1.0" }, "fast");
        $("#timeOK").animate({ opacity: "0" }, "fast");
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
    var numStr = index.substr(reResult[0].length);
    var numIndex = parseInt(numStr);
    var selectedTile = $("#" + index);
    var hasSelected = mustSelect ? false : selectedTile.hasClass("selected");
    var selectedClass = selectedTile.attr("class").split(" ")[1];
    if (hasSelected) {
        switch (selectedClass) {
            case "selectCarTypeTile":
                $.removeCookie("selectedCarType", { path: "/" });
                $("#carImageSelected")[0].src = "";
                $("#carNameSelected")[0].innerText = "";
                break;
            case "selectPlaceTile":
                $.removeCookie("selectedPlace", { path: "/" });
                $("#placeImageSelected")[0].src = "";
                $("#placeNameSelected")[0].innerText = "";
                break;
            default:
                break;
        }
    } else {
        switch (selectedClass) {
            case "selectCarTypeTile":
                $.cookie("selectedCarType", numIndex, { path: "/" });
                $("#carImageSelected")[0].src = $("#carImage" + numStr)[0].src;
                $("#carNameSelected")[0].innerText = $("#carName" + numStr)[0].innerText;
                break;
            case "selectPlaceTile":
                $.cookie("selectedPlace", numIndex, { path: "/" });
                $("#placeImageSelected")[0].src = $("#placeImage" + numStr)[0].src;
                $("#placeNameSelected")[0].innerText = $("#placeName" + numStr)[0].innerText;
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
    allDone = !($.cookie("selectedCarType") == undefined ||
        $.cookie("selectedPlace") == undefined ||
        $.cookie("startTime") == undefined);
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
            icon: "<i class=\"fa fa-pencil\"></i>",
            title: "在线预约",
            width: 400,
            height: 135,
            padding: 10,
            overlayClickClose: false,
            draggable: true,
            onShow: function (_dialog) {
                var content = "<p>在预约租车前，您需要注册一个用户或登录您的用户。</p>" +
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
    selectedCarType = $.cookie("selectedCarType", Number);
    selectedStartDate = $("#startDateInput")[0].value;
    startTime = $.cookie("startTime");
    selectedPlace = $.cookie("selectedPlace", Number);
    var needDriverNumber = $.cookie("needDriver", Number);
    if (needDriverNumber == 0) {
        needDriver = false;
        var driverIcon = "<i class=\"fa fa-times\"></i>";
    }
    else {
        needDriver = true;
        var driverIcon = "<i class=\"fa fa-check\"></i>";
    }
    manualConfirm = $("#manualConfirmToggle")[0].checked;
    if (manualConfirm == true) {
        var confirmIcon = "<i class=\"fa fa-check\"></i>";
    }
    else {
        var confirmIcon = "<i class=\"fa fa-times\"></i>";
    }
    $.getJSON("/api/Cars/" + selectedCarType.toString(),
        function (dataCar) {
            var carName = dataCar.Car.Name;
            var carPrice = dataCar.Car.Price;
            var carDeposit = dataCar.Car.Deposit;
            $.getJSON("/api/Stores/" + selectedPlace.toString(),
                function (dataStore) {
                    var storeName = dataStore.Name;
                    $.Dialog({
                        overlay: true,
                        shadow: true,
                        flat: true,
                        icon: "<i class=\"fa fa-pencil\"></i>",
                        title: "在线预约",
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
                                "<tr><td>车型</td>" + "<td>" + carName + "</td></tr>" +
                                "<tr class=\"info\"><td>租金</td>" + "<td>" + carPrice + " 元/日</td></tr>" +
                                "<tr class=\"success\"><td>订金</td>" + "<td>" + carDeposit + " 元</td></tr>" +
                                "<tr><td>店面</td>" + "<td>" + storeName + "</td></tr>" +
                                "<tr><td>取车日期</td>" + "<td>" + selectedStartDate + "</td></tr>" +
                                "<tr><td>预约取车时间</td>" + "<td>" + startTime + "</td></tr>" +
                                "<tr><td>需要司机</td>" + "<td>" + driverIcon + "</td></tr>" +
                                "<tr><td>需要人工确认</td>" + "<td>" + confirmIcon + "</td></tr>" +
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
        }).fail(
        function () {
            alert("请检查您的网络状况！");
        });
}
function submitOrder() {
    if (Locked == true) return;
    Locked = true;
    var note = $("#noteInput")[0].value;
    $.post("https://www.zuchefw.com/Order/SelectOrder",
    {
        carID: selectedCarType,
        dateStart: selectedStartDate,
        timeStart: startTime,
        storeID: selectedPlace,
        needDriver: needDriver,
        manualConfirm: manualConfirm,
        note: note
    },
    function (data) {
        if (data.Succeeded == true) {
            $.Notify({
                caption: "<i class=\"fa fa-pencil\"></i>  在线预约",
                content: "预约成功，您的订单号为" + padLeft(data.OrderID, 8) + "。您需支付订金 " + data.Deposit + " 元，稍后将跳转至支付宝以支付订金。",
                style: { background: "#4caf50", color: "#fafafa" },
                timeout: 3000
            });
            setTimeout(function () {
                Locked = false;
                window.location.href = "https://www.zuchefw.com/Order/PayOrder?orderID=" + data.OrderID;
            }, 4000);
        } else {
            $.Notify({
                caption: "<i class=\"fa fa-pencil\"></i>  在线预约",
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