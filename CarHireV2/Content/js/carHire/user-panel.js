var name, phone;
var nameOK = false, phoneOK = false;
var currentRating;
var Locked = false;

function informationChanged() {
    var changeName = $("#changeName")[0].value;
    var changeNameBorder = $("#changeNameBorder");
    var changePhone = $("#changePhone")[0].value;
    var changePhoneBorder = $("#changePhoneBorder");
    if (changeName == "") {
        changeNameBorder.removeClass("error-state");
        changeNameBorder.removeClass("success-state");
        nameOK = true;
        name = changeName;
    } else if (changeName.length > 30) {
        changeNameBorder.removeClass("success-state");
        changeNameBorder.addClass("error-state");
        nameOK = false;
    } else {
        changeNameBorder.removeClass("error-state");
        changeNameBorder.addClass("success-state");
        nameOK = true;
        name = changeName;
    }
    if (changePhone == "") {
        changePhoneBorder.removeClass("error-state");
        changePhoneBorder.removeClass("success-state");
        phoneOK = true;
        phone = changePhone;
    } else if (validatePhone(changePhone) == false) {
        changePhoneBorder.removeClass("success-state");
        changePhoneBorder.addClass("error-state");
        phoneOK = false;
    } else {
        changePhoneBorder.removeClass("error-state");
        changePhoneBorder.addClass("success-state");
        phoneOK = true;
        phone = changePhone;
    }
    if ((nameOK == true && phoneOK == true) &&
        !(changeName == "" && changePhone == ""))
        $("#actions").css("display", "block");
    else $("#actions").css("display", "none");
}

function changeInformation() {
    if (nameOK != true || phoneOK != true || Locked == true) return;
    Locked = true;
    $.post("https://www.zuchefw.com/User/ChangeInfo",
        {
            name: name,
            cellPhoneNumberStr: phone
        },
        function(data) {
            if (data.Succeeded == true) {
                $.Notify({
                    caption: "<i class=\"fa fa-info-circle\"></i>  修改信息",
                    content: "信息修改成功！",
                    style: { background: "#4caf50", color: "#fafafa" },
                    timeout: 3000
                });
                setTimeout(function() {
                    Locked = false;
                    window.location.reload(true);
                }, 4000);
            } else {
                Locked = false;
                $.Notify({
                    caption: "<i class=\"fa fa-info-circle\"></i>  修改信息",
                    content: "信息修改失败，" + data.Error + "。",
                    style: { background: "#f44336", color: "#fafafa" },
                    timeout: 3000
                });
                if (data.Error == "用户尚未登录或登录已过期，请登录后再试") {
                    setTimeout(function () {
                        Locked = false;
                        window.location.reload(true);
                    }, 4000);
                }
            }
        });
}

function reviewDialog(orderID) {
    $.Dialog({
        overlay: true,
        shadow: true,
        flat: true,
        icon: " <i class=\"fa fa-thumbs-o-up\"></i>",
        title: "评价",
        padding: 10,
        overlayClickClose: false,
        draggable: true,
        onShow: function(_dialog) {
            var content = "<div class=\"row reviewRow\">" +
                "<div class=\"span3 carsLight\">" +
                "<label>车型评价</label>" +
                "<div class=\"rating\" id=\"carReview\"></div>" +
                "</div></div>" +
                "<div class=\"row reviewRow\">" +
                "<div class=\"span3 suggestionsLight\">" +
                "<label>留言</label>" +
                "<textarea style=\"width:100%;height:110px\" id=\"comment\"></textarea>" +
                "</div></div>" +
                "<div class=\"row reviewRow\">" +
                "<div class=\"span3\">" +
                "<button id=\"submit\" class=\"suggestionsDark\">提交</button> <button onclick=\"$.Dialog.close()\">取消</button>" +
                "</div></div>";
            $.Dialog.content(content);
            $.Metro.initInputs();
            $.getJSON("/api/Comments/?orderID=" + orderID.toString(),
                    function(data) {
                        var carReview = $("#carReview");
                        var comment = $("#comment");
                        if (data == undefined) {
                            carReview.rating({
                                static: false,
                                score: 3,
                                stars: 5,
                                showHint: false,
                                showScore: false,
                                click: function(value, rating) {
                                    currentRating = value;
                                    rating.rate(value);
                                }
                            });
                            currentRating = 3;
                        } else {
                            carReview.rating({
                                static: false,
                                score: data.Rating,
                                stars: 5,
                                showHint: false,
                                showScore: false,
                                click: function(value, rating) {
                                    currentRating = value;
                                    rating.rate(value);
                                }
                            });
                            currentRating = data.Rating;
                            comment[0].value = data.Content;
                        }
                        $("#submit").click(function() { submitComment(orderID); });
                    })
                .fail(function() {
                    alert("请检查您的网络状况！");
                });

        }
    });
}

function submitComment(orderID) {
    if (Locked == true) return;
    Locked = true;
    var comment = $("#comment")[0].value;
    $.post("/User/Comment", {
        orderID: orderID,
        rating: currentRating,
        content: comment
    }, function(data) {
        if (data.Succeeded == true) {
            $.Notify({
                caption: "<i class=\"fa fa-thumbs-o-up\"></i>  评价",
                content: "提交评价成功！",
                style: { background: "#4caf50", color: "#fafafa" },
                timeout: 3000
            });
            setTimeout(function() {
                Locked = false;
                window.location.reload(true);
            }, 4000);
        } else {
            Locked = false;
            $.Notify({
                caption: "<i class=\"fa fa-thumbs-o-up\"></i>  评价",
                content: "提交评价失败，" + data.Error + "。",
                style: { background: "#f44336", color: "#fafafa" },
                timeout: 3000
            });
        }
    });
}