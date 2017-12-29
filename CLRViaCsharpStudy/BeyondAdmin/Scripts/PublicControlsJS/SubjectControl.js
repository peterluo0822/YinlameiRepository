
//科目管理点击按钮
function SubjectButtonClick(SubjectTreeID, SubjectTreeQueryID, IDKey, ShowSubjectDivID) {//树id  下拉框id  自定义id  弹出层id
    $("body>.animated ").eq(0).removeClass("animated");
    var lay;
    layer.open({
        type: 1 //Page层类型
           , area: ['40%', '80%']
           , title: ['科目选择', 'background-color:skyblue']
           , zIndex: 100000001
           , shade: 0.6 //遮罩透明度
           , maxmin: true //允许全屏最小化
           , scrollbar: false
           , anim: 1 //0-6的动画形式，-1不开启
           , content: '<div id="'+ShowSubjectDivID+'" style="width:100%;height:100%;overflow:auto;">'+
                            '<div class="wrapper wrapper-content animated fadeInUp">'+
                                '<div class="row">'+
                                    '<div class="col-sm-12">'+
                                        '<div class="ibox">'+
                                            '<div class="ibox-content">'+
                                                '<div class="row m-b-sm m-t-sm">'+
                                                    '<div class="col-md-12">'+
                                                        '<select id="'+SubjectTreeQueryID+'" style="width: 100%;">'+
                                                        '</select>'+
                                                    '</div>'+
                                                '</div>'+
                                                '<div class="project-list">'+
                                                    '<div class="ibox-content">'+
                                                        '<div id="'+SubjectTreeID+'"></div>'+
                                                    '</div>'+
                                                '</div>'+
                                            '</div>'+
                                        '</div>'+
                                    '</div>'+
                                '</div>'+
                            '</div>'+
                        '</div>'
           , success: function (layero, index) {
               $("body>.wrapper ").eq(0).addClass("animated");
               lay = $(layero);
               lay.find("#" + SubjectTreeQueryID).select2();
               lay.find("#" + SubjectTreeQueryID).select2("val", "");
               lay.find('#' + SubjectTreeQueryID).empty();

               lay.find('#' + SubjectTreeID).data('jstree', false);//清空数据，必须
               //注销事件
               lay.find('#' + SubjectTreeID).unbind();
               //树形加载数据
               $.get("/PublicControls/GetSubjectTreeData?math="+Math.random(), "", function (result) {
                   //var data = eval('(' + result.data + ')');
                   lay.find('#' + SubjectTreeID).jstree({
                       'core': {
                           'data': result.data
                       }
                   });
               });

               //下拉框加载数据
               lay.find("#" + SubjectTreeQueryID).select2({
                   ajax: {
                       url: "/PublicControls/GetSubjectSelectList",
                       dataType: 'json',
                       delay: 250,
                       data: function (params) {
                           return {
                               QueryString: params.term,//（params.term表示输入框中内容，QueryString发生到服务器的参数名）
                           };
                       },
                       processResults: function (data) {
                           return {
                               results: data.data//绑定数据   data为返回json数据
                           };
                       },
                       cache: true
                   },
                   escapeMarkup: function (markup) { return markup; },//字符转义处理
                   minimumInputLength: 1,//最小需要输入多少个字符才进行查询，与之相关的maximumSelectionLength表示最大输入限制。
                   templateResult: function formatRepo(repo) { return repo.text },//返回结果回调函数
                   templateSelection: formatRepoSelection//选中项时触发
               });

               //树形绑定节点单击事件
               lay.find('#' + SubjectTreeID).bind("activate_node.jstree", function (obj, e) {
                    var currentNode = e.node;
                    $("#" + IDKey).attr("memberid", currentNode.id);
                    $("#" + IDKey).val(currentNode.text);
                    layer.close(index);
                    var triggerEventName = $('#' + IDKey).attr("triggereventname")
                    //绑定自定义触发器
                    if (triggerEventName.length > 0) {
                        $('#' + IDKey).trigger(triggerEventName);//执行自定义事件

                    }
                });
           }
           , btn: ["清空"]
           , yes: function (index, layero) {
               Init(IDKey);
               layer.close(index);
           }
    });
    //select2选中项
    function formatRepoSelection(repo) {
        lay.find('#' + SubjectTreeID).jstree().deselect_all();//清空所有选中项
        lay.find('#' + SubjectTreeID).jstree().close_all();//收缩所有节点
        //绑定Ajax的内容
        var ref = lay.find('#' + SubjectTreeID).jstree().get_node(repo.id);
        lay.find('#' + SubjectTreeID).jstree().select_node(ref, false, null);//设置 节点选中
        return repo.text;
    }
}





