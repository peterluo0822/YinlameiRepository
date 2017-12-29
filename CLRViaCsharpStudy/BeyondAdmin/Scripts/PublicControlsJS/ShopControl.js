
$(function () {
    $(".ctl_shopsel").select2({
        placeholder: '请选择',
        allowClear: false,
        width: 220
    });
    $(".ctl_shopsel").empty();
});

var $GetShopDataByMemberCtl = function (IDKey,MemberID) {
    $("#" + IDKey).empty();
    $.get("/PublicControls/GetShopDataByMember", { MemberID: MemberID }, function (result) {
        $("#" + IDKey).select2({
            data: result,
            placeholder: '请选择',
            allowClear: false,
            width:220
        });
    });
}