var currentID, currentData, mouseOver, mouseOverHTML;

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
    $("#carPictures").height(newHeight);
}
function loadCar(id) {
    $("#carType" + id.toString()).addClass("active");
    $.getJSON("/api/Cars/" + id.toString(),
        function (data) {
            currentData = data;
            expand(id, true);
            showDetail();
        });
}
function expand(id, dataFetched) {
    if (currentID == id) return;
    if (mouseOver != undefined) {
        if (mouseOver.id == "#carType" + id.toString()) return;
        mouseOver.innerHTML = mouseOverHTML;
    }
    mouseOver = $("#carType" + id.toString())[0];
    mouseOverHTML = mouseOver.innerHTML;
    if (dataFetched == true) {
        mouseOver.innerHTML = "<div class=\"list-content\">" +
                    "<img src=\"" + currentData.Car.ImagePath + "\"/>" +
                    "<div class=\"title\">" + currentData.Car.Name + "</div>" +
                    "</div>" +
                    "</div>";
    } else {
        $.getJSON("/api/Cars/" + id.toString(),
            function (data) {
                currentData = data;
                mouseOver.innerHTML = "<div class=\"list-content\">" +
                    "<img src=\"" + data.Car.ImagePath + "\"/>" +
                    "<div class=\"title\">" + data.Car.Name + "</div>" +
                    "</div>" +
                    "</div>";
            }).fail(
            function () {
                alert("请检查您的网络状况！");
            });
    }
    currentID = id;
}

function onChecked(id) {
    var commentIndex = 1;
    $(".radioButton").prop("checked", false);
    $("#" + id).prop("checked", true);
    var comment = $(".comment");
    for (var noneIndex = 1; noneIndex <= 3; noneIndex++) {
        $("#commentContainer" + noneIndex).css("display", "none");
    }
    var commentType = 0;
    switch (id) {
        case "goodComment":
            comment.each(function () {
                $(this).animate({ "background-color": "#81c784" }, "fast");
            });
            commentType = 0;
            break;
        case "mediumComment":
            comment.each(function () {
                $(this).animate({ "background-color": "#ffd54f" }, "fast");
            });
            commentType = 1;
            break;
        case "badComment":
            comment.each(function () {
                $(this).animate({ "background-color": "#e57373" }, "fast");
            });
            commentType = 2;
            break;
        default:
            break;
    }
    $.getJSON("/api/Cars/" + currentID.toString() + "?commentType=" + commentType.toString(),
            function (data) {
                $.each(data, function (i, commentItem) {
                    var indexStr = (i + 1).toString();
                    $("#commentContainer" + indexStr).css("display", "block");
                    $("#user" + indexStr)[0].innerHTML = commentItem.User.Name.charAt(0) + "顾客";
                    $("#rating" + indexStr).rating({
                        static: true,
                        score: commentItem.Rating,
                        stars: 5,
                        showHint: false,
                        showScore: false
                    });
                    $("#comment" + indexStr)[0].innerHTML = commentItem.Content;
                    $("#date" + indexStr)[0].innerHTML = formatDate(new Date(commentItem.DateCreated));
                    commentIndex++;
                });
            }).fail(
            function () {
                alert("请检查您的网络状况！");
            });
}

function showDetail() {
    $("#detailContainer").css("display", "block");
    $("#detailImage1")[0].src = currentData.Car.DetailImagePath1;
    $("#detail1")[0].innerHTML = currentData.Car.Detail1;
    $("#detailImage2")[0].src = currentData.Car.DetailImagePath2;
    $("#detail2")[0].innerHTML = currentData.Car.Detail2;
    $(".select").click(function () { carSelected(currentID); });
    $("#speedPercentage").progressbar().progressbar("value", currentData.SpeedPercentage);
    $("#speed")[0].innerHTML = currentData.Car.Speed + " 公里/小时";
    $("#oilCostPercentage").progressbar().progressbar("value", currentData.OilCostPercentage);
    $("#oilCost")[0].innerHTML = currentData.Car.OilCost + " 升/百公里";
    $("#pricePercentage").progressbar().progressbar("value", currentData.PricePercentage);
    $("#price")[0].innerHTML = currentData.Car.Price + " 元/日";
    onChecked("goodComment");
    setTimeout(resize, 500);
}

function carSelected(index) {
    $.cookie("selectedCarType", index, { path: "/" });
    window.location.href = "/Order/Select";
}