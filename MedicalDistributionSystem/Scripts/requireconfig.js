///////
require.config({
    baseUrl: 'http://localhost:810/Scripts',
    paths: {
        "jquery": "http://localhost:810/Scripts/jquery-1.10.2",
        "layer": "http://localhost:810/Scripts/layui/lay/modules/layer",
        "common": "http://localhost:810/Scripts/lib/common",
        "layui": "http://localhost:810/Scripts/layui/layui",
    }, shim: {
        "layui": {
            exports: "layui"
        }
    }
});