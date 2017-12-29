
var $FeeInit = function (IDKey) {
    $("#" + IDKey).select2();
    $("#" + IDKey).select2("val", "");
    $('#' + IDKey).empty();
    $.get("/PublicControls/GetFee", "", function (result) {
        $('#' + IDKey).select2({
            data: result,
            placeholder: '请选择'
        });

        $('#' + IDKey).append("<option value='0' selected='selected'>--请选择--</option>");
        $('#' + IDKey).select2();
    })
}

function FeeButtonClick(IDKey, ShowFeeDiv, ShowFeeText) {
    $("body>.animated ").eq(0).removeClass("animated");
    var lay;
    layer.open({
        type: 1 //Page层类型
        , area: ['35%', '30%']
        , title: ['新增核算项目', 'background-color:skyblue']
        , zIndex: 100000001
        , shade: 0.6 //遮罩透明度
        , maxmin: true //允许全屏最小化
        , scrollbar: false
        , anim: 1 //0-6的动画形式，-1不开启
        , content: '<div id="'+ShowFeeDiv+'" style="width:100%;height:100%; overflow:auto;">' +
                        '<div class="wrapper wrapper-content animated fadeInUp">' +
                            '<div class="row">' +
                                '<div class="col-sm-12">' +
                                    '<div class="ibox">' +
                                        '<div class="ibox-content">' +
                                            '<form class="form-horizontal m-t">' +
                                                '<div class="form-group" style="width:200px;">' +
                                                    '<label style="width:80px;">核算项目：</label>' +
                                                    '<div class="col-sm-8">' +
                                                        '<input id="'+ShowFeeText+'"  type="text" class="form-control" required="" ariarequired="true">' +
                                                    '</div>' +
                                                '</div>' +
                                            '</form>' +
                                        '</div>' +
                                    '</div>' +
                                '</div>' +
                            '</div>' +
                        '</div>' +
                    '</div>'
        , success: function (layero, index) {
            $("body>.wrapper ").eq(0).addClass("animated");
            lay = $(layero);//弹出框对象
            lay.find("#" + ShowFeeText).val("");
            lay.find("#" + ShowFeeText).focus();
        }
        , btn: ['保存', '关闭'],
        yes: function (index, layero) {
            if (lay.find("#" + ShowFeeText).val() == "") {
                parent.layer.msg('请输入添加核算项目类型名称！', { icon: 7, time: 1000 });
            }
            else {
                $.post("/PublicControls/AddFee", { "FeeName": lay.find("#" + ShowFeeText).val() }, function (result) {
                    if (result.state < 0) {
                        parent.layer.msg('该核算项目已存在！', { icon: 2, time: 1000 });
                    }
                    else {
                        parent.layer.msg('添加成功！', { icon: 1, time: 1000 });
                        $("#" + IDKey).append("<option value='" + result.data.FeeID + "' selected='selected'>" + result.data.FeeName + "</option>");
                        $("#" + IDKey).select2();
                        var triggerEventName = $('#' + IDKey).attr("triggereventname")
                        //绑定自定义触发器
                        if (triggerEventName.length > 0) {
                            $('#' + IDKey).trigger(triggerEventName);//执行自定义事件

                        }
                        layer.close(index);
                    }
                });
            }
        }, btn2: function (index, layero) {
            layer.close(index);
        }
    });
}