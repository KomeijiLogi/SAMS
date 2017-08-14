/*客户js*/
var Customer = function () {
    //初始化页面
    var initPage = function () {
        initcustomer();
    };

    //获取省市区域的值
    var getAreaVal = function getAreaVal() {
        //获取省市区的值
        var area = $("#sel_pro").select2('data').text + "-" + $("#sel_city").select2('data').text + "-" + $("#sel_distr").select2('data').text;
        if ($("#sel_pro").val() && $("#sel_city").val() && $("#sel_distr").val()) {
            $("#Area").val(area).focus().blur();
        }
        else { $("#Area").val(""); }
    }

    //初始化弹出的对话框
    var initcustomer = function initDialog() {
        initArea("#sel_pro", "#sel_city", "#sel_distr", $("#Area").val());//初始化省市区域
        initSelect();//初始化select2控件
        Metronic.initSlimScroll('.scroller');//滚动条
        $("#editForm").removeData("validator");//删除验证器
        $.validator.unobtrusive.parse("#editForm");//添加验证器
        $("#s2id_sel_distr input").blur(function () { getAreaVal(); });
        initUniform();
    }

    //* END:CORE HANDLERS *//
    return {
        //main function to initiate the theme
        init: function () {
            initPage();
        }
    };

}();



