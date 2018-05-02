///////
require.config({
    baseUrl: './Scripts',
    paths: {
        "jquery": "jquery-1.10.2",
        "layer": "layui/lay/modules/layer",
        "common": "lib/common",
        "layui": "layui/layui",
    }, shim: {
        "layui": {
            exports: "layui"
        }
    }
});

//require(["lib/alert"]);

//require(["common"], function (common) {
//    var c = common;
//    alert(c.version);
//});
//require(["layui/lay/modules/upload"], ["layui"],function (upld) {
//    var l = upld;
//});
//require(["layui"],function (upld)  {
//    //执行实例
//    var upld = upload;
//    $("#upld").bind("click", function () {
//        var uploadInst = upld.render({
//            elem: '#upld', //绑定元素
//            url: '/Admin/UploadImage',//上传接口
//            done: function (res) {
//                //上传完毕回调
//                $("#img").val("1234");
//            },
//            error: function () {
//                //请求异常回调
//            }
//        });
//    });
//})


require(["layui","layer", "jquery", "common"], function (layui,layer, $, common) {

    var layer = layer;
    var common = common;
    var upload = upld;
    var $ = $ || {};
    var app = function () { };

    //定义函数功能
    app.prototype.pageLoad = function () {
        var $this = this;
        //common.ajax();
        //开始绑定事件
        alert(layui);

        $("#submitBtn").bind("click", function () {
            var data = {};
            data.data = $("form").serialize();
            data.url = "/Admin/Create";
            data.type = "POST",
            data.successCallBack = $this.successCallBack;
            data.errorCallBack = $this.errorCallBack;
            //发送请求
            common.ajax(data.url, data.data, data.type, data.successCallBack, data.errorCallBack);

        });
    };
    //app.prototype.upload = function () {
    //    //执行实例
    //    var uploadInst = upload.render({
    //        elem: '#upld', //绑定元素
    //        url: '/Admin/UploadImage',//上传接口
    //        done: function (res) {
    //            //上传完毕回调
    //            $("#img").val("1234");
    //        },
    //        error: function () {
    //            //请求异常回调
    //        }
    //    });
    //};
    app.prototype.successCallBack = function () {
        layer.msg("发布成功", { time: 1, icon: 6 });
        window.location = "/Admin/LineList";
    },

    app.prototype.successDeleteCallBack = function () {
        layer.msg("删除成功", { time: 1, icon: 6 });
    }

    app.prototype.errorCallBack = function (err) {

    },

    app.prototype.delete = function (id) {
        var $this = this;
        var data = {};
        data.data = {id:id};
        data.url = "/Admin/Delete";
        data.type = "POST",
        data.successCallBack = $this.successDeleteCallBack;
        //发送请求
        common.ajax(data.url, data.data, data.type, dara.successCallBack, null);
    }
    app.prototype.pageLoad();
});


