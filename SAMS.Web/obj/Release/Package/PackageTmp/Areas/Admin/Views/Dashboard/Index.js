(function () {
    $(function () {

        var _$workOrderTable = $('#WorkOrderTable');
        var _workOrderService = abp.services.app.workOrder;

        _$workOrderTable.jtable({
            paging: true,
            sorting: true,
            multiSorting: true,

            actions: {
                listAction: {
                    method:_workOrderService.getList
                }
            },
            field: {
                id: {
                    key: true,
                    list:false
                },
                number:{
                    title: '单号',
                    width: '10%'
                },
                customerName: {
                    title: '客户',
                    width: '10%'
                },
                equipmentName: {
                    title: '产品名称',
                    width: '10%'
                },
                serviceTypeName: {
                    title: '服务名称',
                    width:'10%'
                },
                saleMan: {
                    title: '销售员',
                    width:'10%'
                },
                assignedPersonName: {
                    title: '接单人',
                    width:'10%'
                },
                billStatusName: {
                    title: '状态',
                    width: '10%',
                    display: function (data) {
                        var $span = $('<span>' + data + '</span>');
                        if (data == '已派工')
                            $span.addClass('label label-primary');
                        else if(data=='已完工')
                            $span.addClass('label label-dark');
                        else if(data=='已关闭')
                            $span.addClass('label label-success');
                        else if (data == '已取消')
                            $span.addClass('label label-default');
                        else if (data == '新建')
                            $span.addClass('label label-newly');
                        else if (data == '已受理')
                            $span.addClass('label label-warning');


                    }
                },
                releaseTime: {
                    title:'派工时间',
                }


            }
        })
    });
})