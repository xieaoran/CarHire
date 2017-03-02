function aboutDialog() {
    $.Dialog({
        overlay: true,
        shadow: true,
        flat: true,
        icon: "<i class=\"fa fa-info-circle\"></i>",
        title: "关于泓驰租车",
        width: 450,
        height: 135,
        padding: 10,
        overlayClickClose: false,
        draggable: true,
        onShow: function (_dialog) {
            var content = "<p>泓驰汽车租赁有限公司，自有轿车、越野、商务车辆共计118辆、专职司机47人。</p>" +
                "<p>面向阿尔山市及呼伦贝尔市提供自驾用车和代驾用车服务。</p>" +
                "<p>我们以给您最放心、最安全的用车为己任，为您提供最贴心，最周到、最细致的服务！</p>" +
                "<p><a class='button routeDark' style='margin-top: 10px' href='/Home/About'>显示为完整网页</a></p>";
            $.Dialog.content(content);
            $.Metro.initInputs();
        }
    });
}

function contactDialog() {
    $.Dialog({
        overlay: true,
        shadow: true,
        flat: true,
        icon: "<i class=\"fa fa-envelope\"></i>",
        title: "联系我们",
        width: 300,
        height: 100,
        padding: 10,
        overlayClickClose: false,
        draggable: true,
        onShow: function (_dialog) {
            var content = "<p>您可以通过下面的方式联系我们：</p>" +
                "<p>固定电话：0470-8219977</p>" +
                "<p>移动电话：15547000007</p>";
            $.Dialog.content(content);
            $.Metro.initInputs();
        }
    });
}