

define(["jquery", "layui" ,"common"], function ($, lay ,common) {
    var module = {};
    var $ = $;
    var layui = lay;
    var co = common;

    var func = function ($, layui, co) {
        layui.msg($.core_version);
    };

    module.jqueryVer = func;
    module.commonVer = co.version;
    return module;
});