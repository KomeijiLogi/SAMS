/*选择备件js*/
var SelectAccessory = function () {
    //初始化页面
    var initPage = function () {
        initUniform();
        Metronic.initSlimScroll('.scroller');//滚动条

        //输入名称回车查询
        $("#accessoryCode").keypress(function (e) {
            if (e.which == 13) {
                changeQueryCondition();
            }
        });
    };

    //* END:CORE HANDLERS *//
    return {
        //main function to initiate the theme
        init: function () {
            initPage();
            changeQueryCondition();//加载数据
        }
    };

}();

///初始化列表
function initTable() {
    OperationTable.init("#tableList", "#Sort", "#Direction", changeSortCondition);
}

//改变排序条件
function changeSortCondition(sort, direction) {
    $("#Sort").val(sort);
    $("#Direction").val(direction);
    $("#PageIndex").val(1);//页索引置为1
    $("#searchByRequest").submit();
}

//改变索引页
function changePageIndex(index) {
    $("#PageIndex").val(index);
    $("#searchByRequest").submit();
}


//改变查询条件
function changeQueryCondition() {
    //组装查询条件
    var accessoryCode = $("#accessoryCode").val();
    if (accessoryCode.indexOf("输入") >= 0) {
        accessoryCode = "";
    }
    var userId = $("#userId").val();
    
    var whereData = "{'UserId':'" + userId + "','searchkey':'" + accessoryCode +"}";

    $("#Where").val(whereData);
    $("#PageIndex").val(1);//页索引置为1
    $("#searchByRequest").submit();
}




