function resize() {
    var newHeight = $.find(".normalSlide")[0].childNodes[1].clientHeight;
    $("#top").height(newHeight);
}
function carSelected(index) {
    var numIndex = parseInt(index);
    $.cookie('detailCarType', numIndex, { path: '/' });
    window.location.href = "/Info/Car";
}
function routeDialog(id) {
    $.getJSON("/api/Routes/" + id.toString(),
        function (data) {
            $.Dialog({
                draggable: true,
                overlay: true,
                shadow: true,
                flat: true,
                icon: ' <i class="fa fa-road"></i>',
                title: '线路',
                overlayClickClose: false,
                padding: 10,
                onShow: function (_dialog) {
                    var content = '<div class="panel routeBorder">' +
                        '<div class="panel-header routeDark">' +
                        '<div class="headersContent">' + data.Name + '</div>' +
                        '</div>' +
                        '<div class="panel-content">' +
                        '<div class="dialogContent first">' +
                        '<img src="' + data.DetailImagePath1 + '">' +
                        '<p>' + data.Detail1 + '</p>' +
                        '</div>' +
                        '<div class="dialogContent second">' +
                        '<p>' + data.Detail2 + '</p>' +
                        '<img src="' + data.DetailImagePath2 + '">' +
                        '</div>' +
                        '</div>' +
                        '</div>';
                    $.Dialog.content(content);
                }
            });
        }).fail(function () {
            alert("请检查您的网络状况！");
        });
}