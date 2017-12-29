//$(function () {
//    //菜单点击
//    $(".J_menuItem").on('click', function () {
//        var url = $(this).attr('href');
//        $("#J_iframe").attr('src', url);
//        return false;
//    });
//});


//注销按钮
function CancelLogin() {
    $.post("/Home/emptySession", "", function (result) {
        if (result.status) {
            window.location.href = "/Account/Login";
        }
        else {
            parent.layer.msg("注销失败！", { icon: 7, time: 1500 });
        }
    });
}