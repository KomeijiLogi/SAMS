(function () {
    $(function () {
        var _$workOrdersTable = $('#WorkOrderTable');
        var _workOrderService=abp.services.app.workOrder;
        var _createOrEditModal = new app.ModalManager({
            viewUrl: abp.appPath + 'Admin/WorkOrder/CreateOrEditModal',
            scriptUrl: abp.appPath + 'Areas/Admin/Views/WorkOrder/_CreateOrEditModal.js',
            modalClass: 'CreateOrEditWorkOrderModal',
            lg:true

        });
        
        $('#CreateNewOrderButton').click(function () {
            _createOrEditModal.open();
        });
        //显示或隐藏筛选功能
        $('body').on('click', '#btnFilter.collapse, #btnFilter.expand', function (e) {
            if ($(this).hasClass("collapse")) {
                $(this).removeClass("collapse").addClass("expand").text('取消筛选');
                $('#filterContent').slideDown(300);
            } else {
                $(this).removeClass("expand").addClass("collapse").text('筛选');
                $('#filterContent').slideUp(300);
            }
        });

        //取消筛选功能
        $("#btnFilter").click(function () {
            if ($(this).hasClass("expand")) {
                $("#select-customer").select2("val", "")
                $("#select-serviceOrg,#select-product,#state_hide,#select-serviceCategory,#startDate,#endDate").val("");
                $("ul.filter-option input[type=checkbox]").attr("checked", false);
                $("ul.filter-option input[type=checkbox]").parent().removeClass("checked");
                // changeQueryCondition();
                
            }
        });
        
        
        initTable();//初始化列表

       
        abp.event.on('app.createOrEditWorkOrderModalSaved', function () {
           // getWorkOrders(true);
        });
        //getWorkOrders();
       
    })
})();
//改变索引页
var changePageIndex = function (index) {
    $("#PageIndex").val(index);
    $("#searchByRequest").submit();
}
//改变排序条件
function changeSortCondition(sort, direction) {
    $("#Sort").val(sort);
    $("#Direction").val(direction);
    $("#PageIndex").val(1);//页索引置为1
    $("#searchByRequest").submit();
}
//显示加载中
function showLoading() {
   // Metronic.blockUI({ target: '.page-content', boxed: true });
}
//隐藏加载中
function hideLoading() {
    //Metronic.unblockUI('.page-content');
}

//显示加载中
function showPopLoading() {
    //Metronic.blockUI({ target: '.modal-content', boxed: true });
}

//隐藏加载中
function hidePopLoading() {
    //Metronic.unblockUI('.modal-content');
}
///初始化列表
function initTable() {
    OperationTable.init("#WorkOrderTable", "#Sort", "#Direction", changeSortCondition);
    //单击行跳转到明细页
    $('td:not(:lt(1))', $('#WorkOrderTable tr')).click(function () {
        var id = $(this).parent().find("input[type=checkbox]").val();//id值
        window.location = "/admin/workorder/details/" + id;
    });
}