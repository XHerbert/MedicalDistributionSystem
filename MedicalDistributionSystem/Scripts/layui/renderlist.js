require.config({
    baseUrl: '../../Scripts',
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



require(["layer", "jquery", "common"], function (layer, $, common) {

    var layer = layer;
    var common = common;
    var upload = upld;
    var $ = $ || {};
    var app = function () { };

    //定义函数功能
    app.prototype.pageLoad = function () {
        //var that = this;
        //that.renderList();
        layer.msg("1111");
        //开始绑定事件
        
    };
  

    app.prototype.renderList = function () {
        layer.msg("loaded");
    }
});


