
//往来单位点击按钮
function MemberObjectButtonClick(MemberObjectTreeID, MemberObjectTreeQueryID, IDKey, ShowMemberObjectDivID) {//树id  下拉框id  自定义id  弹出层id
    $("body>.animated ").eq(0).removeClass("animated");
    var lay;
    var index1;
    layer.open({
        type: 1 //Page层类型
            , area: ['40%', '80%']
            , title: ['往来单位选择', 'background-color:skyblue']
            , zIndex: 100000001
            , shade: 0.6 //遮罩透明度
            , maxmin: true //允许全屏最小化
            , scrollbar: false
            , anim: 1 //0-6的动画形式，-1不开启
            , content: '<div id="' +ShowMemberObjectDivID + '" style="width:100%;height:100%; overflow:auto;">'
                        + '<div class="wrapper wrapper-content animated fadeInUp">'
                            + '<div class="row">'
                                + '<div class="col-sm-12">'
                                    + '<div class="ibox">'
                                        + '<div class="ibox-content">'
                                            + '<div class="row m-b-sm m-t-sm">'
                                                + '<div class="col-md-12">'
                                                    + '<select id="'+MemberObjectTreeQueryID+'" style="width: 100%;"></select>'
                                                + '</div>'
                                            + '</div>'
                                            + '<div class="project-list">'
                                                + '<div class="ibox-content">'
                                                    + '<div id="'+MemberObjectTreeID+'"></div>'
                                                + '</div>'
                                            + '</div>'
                                        + '</div>'
                                    + '</div>'
                                + '</div>'
                            + '</div>'
                        + '</div>'
                    + '</div>'
            , success: function (layero, index) {
                $("body>.wrapper ").eq(0).addClass("animated");
                lay = $(layero);
                index1 = index;
                lay.find("#" + MemberObjectTreeQueryID).select2();
                lay.find("#" + MemberObjectTreeQueryID).select2("val", "");
                lay.find('#' + MemberObjectTreeQueryID).empty();

                lay.find('#' + MemberObjectTreeID).data('jstree', false);//清空数据，必须
                //注销事件
                lay.find('#' + MemberObjectTreeID).unbind();
                //树形加载数据
                lay.find('#' + MemberObjectTreeID).jstree({
                    "core": {
                        "animation": 0,
                        "check_callback": true,
                        "themes": { "stripes": true },
                        'data': {
                            'url': '/PublicControls/GetMemberObjectTreeData'
                            //function (node) {
                            //return node.id === '#' ?
                            //  'ajax_demo_roots.json' : 'ajax_demo_children.json';
                            //}
                            ,
                            'data': function (node) {
                                return { 'id': node.id, "types": $("#" + IDKey).attr("types") };
                            }
                        }
                    }

                });

                //下拉框加载数据
                lay.find("#" + MemberObjectTreeQueryID).select2({
                    ajax: {
                        url: "/PublicControls/GetMemberObjectSelectList",
                        dataType: 'json',
                        delay: 250,
                        data: function (params) {
                            return {
                                QueryString: params.term,//（params.term表示输入框中内容，QueryString发生到服务器的参数名）
                                types: $("#" + IDKey).attr("types")
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
                    templateSelection: formatRepoSelection //选中项时触发
                });

                //树形绑定节点单击事件
                lay.find('#' + MemberObjectTreeID).bind("activate_node.jstree", function (obj, e) {
                    var currentNode = lay.find('#' + MemberObjectTreeID).jstree().get_selected(true)[0];
                    if (!currentNode||currentNode.id.split('-').length > 1) {
                        parent.layer.msg('请选择往来单位！', { icon: 7, time: 1000 });
                    } else {
                        $("#" + IDKey).attr("memberid", currentNode.id);
                        $("#" + IDKey).val(currentNode.text);
                        $.get("/PublicControls/GetMemberObjectCompanyByID", { MemberObjectID: currentNode.id }, function (result) {
                            $("#" + IDKey).attr("companyid", result.CompanyID);
                            
                        });
                        var triggerEventName = $('#' + IDKey).attr("triggereventname")
                        //绑定自定义触发器
                        if (triggerEventName.length > 0) {
                            $('#' + IDKey).trigger(triggerEventName);//执行自定义事件

                        }
                        layer.close(index);
                        
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
        $("#" + IDKey).attr("memberid", repo.id);
        $("#" + IDKey).val(repo.text);
        $.get("/PublicControls/GetMemberObjectCompanyByID", { MemberObjectID: repo.id }, function (result) {
            $("#" + IDKey).attr("companyid", result.CompanyID);
        });
        var triggerEventName = $('#' + IDKey).attr("triggereventname")
        //绑定自定义触发器
        if (triggerEventName.length > 0) {
            $('#' + IDKey).trigger(triggerEventName);//执行自定义事件

        }
        layer.close(index1);
        //lay.find('#' + MemberObjectTreeID).jstree().deselect_all();//清空所有选中项
        //lay.find('#' + MemberObjectTreeID).jstree().close_all();//收缩所有节点
        //var parent;
        //$.ajax({
        //    type: "get",
        //    dataType: "json",
        //    url: "/PublicControls/GetSelectItemParent",
        //    data: { selID: repo.id },
        //    async:false,
        //    success: function (result) {
        //        if (result) {
        //            var parentid = result.parent;
        //            parent = lay.find('#' + MemberObjectTreeID).jstree("get_node", parentid);
                    
        //        }
        //    }
        //});
        //if (parent) {
        //    lay.find('#' + MemberObjectTreeID).jstree('open_node', parent, function () {
        //        //绑定Ajax的内容
        //        var ref = lay.find('#' + MemberObjectTreeID).jstree().get_node(repo.id);
        //        lay.find('#' + MemberObjectTreeID).jstree().select_node(ref, false, null);//设置 节点选中
        //    });
        //}
        return repo.text;
    }
}

//设置会员控件
function SetMemberObject(IDKey, model) {
    $("#" + IDKey).attr("memberid", model.MemberObjectID);
    $("#" + IDKey).attr("companyid", model.CompanyID);
    $("#"+IDKey).attr("types", model.TypeName);
    $("#"+IDKey).val(model.MemberObjectName);
}

//多选往来单位点击按钮
function ManyMemberObjectButtonClick(MemberObjectTreeID, MemberObjectTreeQueryID, IDKey, ShowMemberObjectDivID) {//树id  下拉框id  自定义id  弹出层id
    $("body>.animated ").eq(0).removeClass("animated");
    var lay;
    var index1;
    var arrcompany = [];//选中项公司id
    layer.open({
        type: 1 //Page层类型
            , area: ['40%', '80%']
            , title: ['往来单位选择', 'background-color:skyblue']
            , zIndex: 100000001
            , shade: 0.6 //遮罩透明度
            , maxmin: true //允许全屏最小化
            , scrollbar: false
            , anim: 1 //0-6的动画形式，-1不开启
            , content: '<div id="' + ShowMemberObjectDivID + '" style="width:100%;height:100%; overflow:auto;">'
                        + '<div class="wrapper wrapper-content animated fadeInUp">'
                            + '<div class="row">'
                                + '<div class="col-sm-12">'
                                    + '<div class="ibox">'
                                        + '<div class="ibox-content">'
                                            + '<div class="row m-b-sm m-t-sm">'
                                                + '<div class="col-md-12">'
                                                    + '<select id="' + MemberObjectTreeQueryID + '" style="width: 100%;" multiple class="multiSelect"></select>'
                                                + '</div>'
                                            + '</div>'
                                            + '<div class="project-list">'
                                                + '<div class="ibox-content">'
                                                    + '<div id="' + MemberObjectTreeID + '"></div>'
                                                + '</div>'
                                            + '</div>'
                                        + '</div>'
                                    + '</div>'
                                + '</div>'
                            + '</div>'
                        + '</div>'
                    + '</div>'
            , success: function (layero, index) {
                $("body>.wrapper ").eq(0).addClass("animated");
                lay = $(layero);
                index1 = index;
                lay.find("#" + MemberObjectTreeQueryID).select2();
                lay.find("#" + MemberObjectTreeQueryID).select2("val", "");
                lay.find('#' + MemberObjectTreeQueryID).empty();

                lay.find('#' + MemberObjectTreeID).data('jstree', false);//清空数据，必须
                //注销事件
                lay.find('#' + MemberObjectTreeID).unbind();
                //树形加载数据
                lay.find('#' + MemberObjectTreeID).jstree({
                    "core": {
                        "animation": 0,
                        "check_callback": true,
                        "themes": { "stripes": true },
                        'data': {
                            'url': '/PublicControls/GetMemberObjectTreeData'
                            ,
                            'data': function (node) {
                                return { 'id': node.id, "types": $("#" + IDKey).attr("types") };
                            }
                        }
                        
                    },
                    //"force_text": true,
                    //plugins: ["sort", "types", "checkbox", "themes", "html_data"],
                    //"checkbox": {
                    //    "keep_selected_style": false,//是否默认选中
                    //    "three_state": false,//父子级别级联选择
                    //    "tie_selection": false
                    //},

                }).bind("loaded.jstree", function (e, data) {
                    lay.find('#' + MemberObjectTreeID).jstree().open_all();//展开所有节点
                });

                //下拉框加载数据
                lay.find("#" + MemberObjectTreeQueryID).select2({
                    ajax: {
                        url: "/PublicControls/GetMemberObjectSelectList",
                        dataType: 'json',
                        delay: 250,
                        data: function (params) {
                            return {
                                QueryString: params.term,//（params.term表示输入框中内容，QueryString发生到服务器的参数名）
                                types: $("#" + IDKey).attr("types")
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
                    templateResult: function formatRepo(repo) {
                        return repo.text
                    },//返回结果回调函数
                    templateSelection: formatRepoSelection //选中项时触发
                });

                //下拉框绑定change事件
                lay.find("#" + MemberObjectTreeQueryID).on("change", function (e) {
                    var reslist = $(this).select2("data");
                    arrcompany = [];
                    lay.find('#' + MemberObjectTreeID).jstree().deselect_all();//清空所有选中项
                    $.each(reslist, function (i, m) {
                        $.get("/PublicControls/GetMemberObjectCompanyByID", { MemberObjectID: m.id }, function (result) {
                            arrcompany.push(result.CompanyID);
                            var ref = lay.find('#' + MemberObjectTreeID).jstree().get_node(m.id);
                            lay.find('#' + MemberObjectTreeID).jstree().select_node(ref, false, null);//设置 节点选中
                        });
                    });
                })

                //树形绑定节点单击事件
                //lay.find('#' + MemberObjectTreeID).bind("activate_node.jstree", function (obj, e) {
                //    var currentNode = e.node;
                //    var bool = lay.find('#' + MemberObjectTreeID).jstree().is_selected(currentNode);
                //    if (!currentNode || currentNode.id.split('-').length > 1) {
                //        parent.layer.msg('请选择往来单位！', { icon: 7, time: 1000 });
                //    } else {
                //        arrtxt.push(currentNode.text);
                //        arrid.push(currentNode.id);

                //        $.get("/PublicControls/GetMemberObjectCompanyByID", { MemberObjectID: currentNode.id }, function (result) {
                //            arrcompany.push(result.CompanyID);
                //        });
                //    }
                //});
            }
            , btn: ['确认',"清空"]
            , yes: function (index, layero) {
                var reslist = lay.find("#" + MemberObjectTreeQueryID).select2("data");
                var txts = [];
                var ids = [];
                $.each(reslist, function (i, m) {
                    txts.push(m.text);
                    ids.push(m.id);
                })
                $("#" + IDKey).attr("memberid",ids.join(','));
                $("#" + IDKey).val(txts.join(','));
                $("#" + IDKey).attr("companyid", arrcompany.join(','));
                var triggerEventName = $('#' + IDKey).attr("triggereventname")
                //绑定自定义触发器
                if (triggerEventName.length > 0) {
                    $('#' + IDKey).trigger(triggerEventName);//执行自定义事件

                }
                layer.close(index);
            }, btn2: function (index, layero) {
                Init(IDKey);
                layer.close(index);
            }
    });
    //select2选中项
    function formatRepoSelection(repo) {
        return repo.text;
    }
}