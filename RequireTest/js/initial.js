require.config({
    //baseUrl: "js/lib",  整体目录是
    paths: {
        'jquery': 'http://apps.bdimg.com/libs/jquery/2.1.4/jquery',
        'vue': 'http://apps.bdimg.com/libs/vue/1.0.8/vue',
        //'goodsinfodetailPage': 'page/goods/infodetailPage',对外infodetailPage的名称是goodsinfodetailPage
        //譬如在HTML页面上  data-page="themePage" 
        'index':'indexPage'
    }
})

require(['vue', 'jquery','index'],
    function (Vue, jQuery,index) {
        //alert("hello require");
    })

//测试用
window.ajaxServer = "http://xingdian.fineex.cn";
if (location.href.indexOf("localhost") >= 0 || location.href.indexOf("192.168.") >= 0) {
    window.ajaxServer = "http://103.6.223.203:8008";
    //window.ajaxServer = "http://vshopapi.fuqiantonglu.com";
}