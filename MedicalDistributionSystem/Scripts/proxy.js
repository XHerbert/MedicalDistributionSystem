//////加载JS，记得修改端口
require.config({
    //baseUrl: 'http://localhost:26076/Scripts',
    paths: {
        "jquery": "http://localhost:26076/Scripts/jquery-1.10.2",
        "layui": "http://localhost:26076/Scripts/layui/layui",
        "layer": "http://localhost:26076/Scripts/layui/lay/modules/layer",
        "common": "http://localhost:26076/Scripts/lib/common",
        "deleterecord": "http://localhost:26076/Scripts/lib/deleterecord"
    }, shim: {
        "layui": {
            exports: "layui"
        },
        "layer": {
            exports: "layer"  //之前一直报undefined，是因为这个lib 不是标准的AMD规范
        },
        "deleterecord": {
            deps: ["jquery", "common"]
        }
    }
});

//////核心类库
require(["jquery", "layui", "layer", "common", "deleterecord"], function ($, layui, layer, common, _) {
    var layui = layui;
    var layer = layer;
    var common = common;
    var $ = $ || {};
    var del = _;
    var tmp = {};
    var proxy = function () { };
    //定义函数功能
    proxy.prototype.pageLoad = function () {
        var $this = this;
        //common.ajax();
        //开始绑定事件
        
        $("#submitCreate").bind("click", function () {
            var data = {};
            data.data = $("form").serialize();
            data.url = "/ProxyAjax/Create";
            data.type = "POST",
            data.successCallBack = $this.successCallBack;
            data.errorCallBack = $this.errorCallBack;
            //发送请求
            common.ajax(data.url, data.data, data.type, data.successCallBack, data.errorCallBack);

        });

        $("#submitUpdate").bind("click", function () {
            var data = {};
            data.data = $("#targetForm").serialize();
            data.url = "/ProxyAjax/Update";
            data.type = "POST",
            data.successCallBack = $this.successUpdateCallBack;
            data.errorCallBack = $this.errorCallBack;
            //发送请求
            common.ajax(data.url, data.data, data.type, data.successCallBack, data.errorCallBack);
        });

        //弹出新建窗口
        $("#create").bind("click", function () {
            var idx = layer.open({
                type: 2,
                title: "创建代理人信息",
                skin: 'layui-layer-rim',
                area: ['1240px', '643px'],
                shade: 0.5,
                fixed: false,
                maxmin: true,
                content: ['/Proxy/Create', 'no']
            });
        });
        
        $("#search").bind("click", function () {
            layer.msg("search");
        });



        //加载列表数据并绑定操作事件
        layui.use('table', function () {
            var table = layui.table;
            table.render({
                elem: '#dataTable'
              , height: 'full-100'
              , skin: 'nob'
              , even: true
              , url: '/ProxyAjax/GetList'
              , loading: true
              , page: true
              , cols: [[
                  { field: 'Id', title: 'Id', sort: true, width: 80, fixed: 'left' }
                , { field: 'ProxyName', title: '代理姓名', width: 180 }
                , { field: 'Mobile', title: '手机号', width: 150 }
                , { field: 'Province', title: '省份', width:150 }
                , { field: 'City', title: '城市',width:150 }
                , { field: 'CurrentMoney', title: '当前余额', minWidth: 150,width:150 }
                , {
                    field: 'BackMoneyPercent', title: '提点', width: 80
                    //templet: function (d) {
                    //    var stamp = d.CreateTime.replace(/[^0-9]/ig, "");
                    //    var date = new Date();
                    //    date.setTime(stamp);
                    //    return date.Format("yyyy-MM-dd hh:mm:ss");
                    //}
                }
                , {
                    field: 'ProxyLevel', title: '代理级别', width: 100,
                    templet: function (d) {
                        var proxyLevel = "";
                        switch (d.ProxyLevel) {
                            
                            case 1:
                                proxyLevel = "超级管理员";
                                break;
                            case 2:
                                proxyLevel = "一级代理";
                                break;
                            case 3:
                                proxyLevel = "二级代理";
                                break;
                            case 4:
                                proxyLevel = "三级代理";
                                break;
                            default:
                                break;
                        }
                        return proxyLevel;
                    }
                }
                , {
                    field: 'CreatorUserId', title: '上级Id', width: 80,
                    templet: function (d) {
                        if (d.CreatorUserId == 0) {
                            return '--'
                        } else {
                            return d.CreatorUserId
                        }
                    }, style: "color:#777 ;font-weight:bold"
                },
                {
                    field: 'CreatorTime', title: '创建时间', width: 170,
                    templet: function (d) {
                        var stamp = d.CreatorTime.replace(/[^0-9]/ig, "");
                        var date = new Date();
                        date.setTime(stamp);
                        return date.Format("yyyy-MM-dd hh:mm:ss");
                    }
                },
                {
                    field: 'DeleteMark', title: '有效', width: 100,
                    templet: function (d) {
                        if (d.DeleteMark == true) {
                            return '无效'
                        } else {
                            return '有效'
                        }
                    }, style: "color:#5FB878 ;"
                },
                { fixed: 'right', title: "操作", align: 'center', toolbar: '#toolbar' }
              ]]
            , response: {
                    statusName: 'Code' //数据状态的字段名称，默认：code
                  , statusCode: 200 //成功的状态码，默认：0
                  , msgName: 'Msg' //状态信息的字段名称，默认：msg
                  , countName: 'Count' //数据总数的字段名称，默认：count
                  , dataName: 'Data' //数据列表的字段名称，默认：data
                }
            });
            table.on('tool(dataTb)', function (obj) { //注：tool是工具条事件名，test是table原始容器的属性 lay-filter="对应的值"
                var data = obj.data; //获得当前行数据
                var layEvent = obj.event; //获得 lay-event 对应的值（也可以是表头的 event 参数对应的值）
                var tr = obj.tr; //获得当前行 tr 的DOM对象

                if (layEvent === 'detail') { //查看
                    //layer.msg("detail");
                    layer.open({
                        type: 2,
                        title: false,
                        skin: 'layui-layer-rim',
                        area: ['1240px', '693px'],
                        shade: 0.5,
                        closeBtn: 1,
                        content: ['/Proxy/ViewData?id=' + data.Id,'no']
                    });

                } else if (layEvent === 'del') { //删除
                    layer.confirm('确定删除当前记录么？', function (index) {
                        
                        layer.close(index);
                        //向服务端发送删除指令
                        $this.delete(data.Id,obj);
                        //obj.del(); //删除对应行（tr）的DOM结构，并更新缓存
                        table.reload("dataTable");
                    });
                } else if (layEvent === 'edit') { //编辑
                    var idx = layer.open({
                        type: 2,
                        title: "修改代理人信息",
                        skin: 'layui-layer-rim',
                        area: ['1240px', '693px'],
                        shade: 0.5,
                        fixed:false,
                        maxmin:true,
                        content: ['/Proxy/Edit?id=' + data.Id, 'no']
                    });
                    
                    //同步更新缓存对应的值
                    //obj.update({
                    //    TitleYear:  '2222',
                    //    TitleMonth:  tmp.TitleMonth,
                    //    TitleDay:  tmp.TitleDay,
                    //    Copy:  tmp.Copy,
                    //    Images:  tmp.Images,
                    //    CreateTime: tmp.CreateTime,
                    //    UpdateTime: tmp.UpdateTime
                    //});
                    //还原对象
                    tmp = {};
                }
            });
        });

        //上传文件
        layui.use('upload', function () {
            var upld = layui.upload;
            var uploadInst = upld.render({
                elem: '#upld', //绑定元素
                loading: true,
                url: '/Admin/UploadImage',//上传接口
                done: function (res, index, upload) {
                    //上传完毕回调
                    $("#img").val(res.Data);
                },
                accept: 'jpg|png|gif|bmp|jpeg',
                size: 500,
                error: function (err) {
                    //请求异常回调
                }
            });
        });

        //日期选择
        layui.use('laydate', function () {
            var laydate = layui.laydate;
            laydate.render({
                elem: "#date",
                theme: 'grid',
                done: function (value, date) {
                    $("input[name='TitleYear']").val(date.year);
                    $("input[name='TitleMonth']").val(date.month);
                    $("input[name='TitleDay']").val(date.date);
                }
            });
        });


    };

    //发布成功回调
    proxy.prototype.successCallBack = function (result) {
        var $this = this;
        $this.layer.msg("创建成功", { time: 3000, icon: 6 });
        setTimeout(function () {
            parent.window.location = "/Proxy/Index";
        }, 3000);
    },

    //删除成功回调
    proxy.prototype.successDeleteCallBack = function (result) {
        if (result.Code == 200) {
            layer.msg("删除成功", { time: 3000, icon: 6 });
        } else {
            layer.msg(result.Msg, {time:5000,icon:6});
        }
    }

    //更新成功回调
    proxy.prototype.successUpdateCallBack = function (result) {
        var $this = this;
        if (result.Code == 200) {
            $this.layer.msg("修改成功", { time: 3000, icon: 6 });
            setTimeout(function () {
                var idx = parent.layer.getFrameIndex(window.name);
                parent.layer.close(idx);
            }, 3000);
            setTimeout(function () {
                parent.window.location = "/Proxy/Index";
            },400);
        }
        else {
            $this.layer.msg("修改失败", { time: 3000, icon: 5 });
        }
    },

    //发生错误回调
    proxy.prototype.errorCallBack = function (err) {

    },

    //删除记录
    proxy.prototype.delete = function (id, obj) {
        var $this = this;
        var data = {};
        data.data = { id: id };
        data.url = "/ProxyAjax/Delete";
        data.type = "POST",
        data.successCallBack = $this.successDeleteCallBack;
        //发送请求
        common.ajax(data.url, data.data, data.type, data.successCallBack, null);
    }

    //载入页面
    proxy.prototype.pageLoad();
});