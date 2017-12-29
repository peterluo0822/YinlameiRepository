

var $InitFeeType = function (IDKey, url) {
    if (!url||url=="") {
        url="/PublicControls/GetFeeType";
    }
    var MyCtl = $("#" + IDKey);
    MyCtl.select2();
    MyCtl.empty();
    $.get(url, "", function (result) {
        MyCtl.select2({
            data: result,
            placeholder: '请选择'
        });
        MyCtl.find("option").length > 0 ? MyCtl.find("option:first").before("<option value='0' selected='selected'>--请选择--</option>") : MyCtl.append("<option value='0' selected='selected'>--请选择--</option>");
        MyCtl.select2({ width: 180 });
    });
    MyCtl.on("change", function (result) {
        var triggerEventName = MyCtl.attr("triggereventname")
        //绑定自定义触发器
        if (triggerEventName.length > 0) {
            MyCtl.trigger(triggerEventName);//执行自定义事件

        }
    });
}