
$(document).ready(function () {
    $("#desDown,#desUp").click(function () {
        $(this).parent().addClass("hidden").siblings().removeClass("hidden");
    });//工单描述展开收起
    initEditSide();//初始化工单侧栏
    $('[data-toggle="popover"]').popover({ html: true });//回单信息鼠标弹出

    if ($(".btn-group ul li").length == 0)//更多操作
    {
        $(".btn-group").addClass("hidden");
    }
});

//启动分配派工对话框
function startDialog(title, height) {
    modalLoadding("#editDailog", title, height)
    $("#editDailog").modal();

}

//显示分配派工对话框
function showDialog(data) {

    $("#editDailog .modal-content").html(data);//填充对话框
    initUniform();
    Metronic.initSlimScroll('.scroller');//滚动条
    $("#editForm").removeData("validator");//删除验证器
    $.validator.unobtrusive.parse("#editForm");//添加验证器

    //派工弹出层
    if ($("#select-staff")) {
        $("#select-staff").change(function () {
            $("#nextHandlerID").val($(this).val());
            $("#nextHandlerName").val($("#select-staff option:selected").text());

        });
    }
    //分配弹出层
    if ($("#serviceOrg")) {
        $("#serviceOrg").change(function () {
            $("#nextHandlerID").val($(this).val());
            $("#nextHandlerName").val($("#serviceOrg option:selected").text());
        });
    }
}

//编辑记录成功后的处理
function editSucess(data) {
    if (data == "ok") {
        $("#editDailog").modal("hide");
        growlAlert.success("操作成功");
        window.location.reload();
    } else if (data == "error") { growlAlert.danger("操作失败"); } else { growlAlert.info(data); }
}

//改变tab内容
function changetab(id) {
    $("#content" + id).siblings().addClass("hidden");
    $("#content" + id).removeClass("hidden");
}

//初始化编辑区侧栏
function initEditSide() {
    EditSideView();
    Metronic.addResizeHandler(function () {
        EditSideView();
    });

    function EditSideView() {
        if (Metronic.getViewPort().width <= 992) {
            $(".esh-side-edit-wrap").css("height", "auto");
        } else {
            var contentH = $(window).height() - $(".esh-detail-content").offset().top - parseInt($(".portlet.light .portlet-body").css("padding-bottom"));
            if ($(".esh-detail-content").height() < contentH) {
                $(".esh-side-edit-wrap").css("height", ($(window).height() - parseInt($(".esh-side-edit-wrap").offset().top)) + "px");
            } else {
                $(".esh-side-edit-wrap").css("height", ($(document).height() - parseInt($(".esh-side-edit-wrap").offset().top)) + "px");
            }
        }

    }
}

//关闭
function close(id) {
    botbox.confirm("提示", "您确认关闭工单吗?", function (result) {
        if (result) {
            //调用action
            showLoading();
            $.ajax({
                url: "/manufacturer/workorder/close",
                type: "POST",
                data: { "id": id },
                success: function (data) {
                    hideLoading();
                    editSucess(data);
                },
                error: function (data) { hideLoading(); showException(data); }
            });
        }
    });
}

//取消
function cancel(id) {
    botbox.confirm("提示", "您确认取消工单吗?", function (result) {
        if (result) {
            //调用action
            showLoading();
            $.ajax({
                url: "/admin/workorder/cancel",
                type: "POST",
                data: { "id": id },
                success: function (data) {
                    hideLoading();
                    editSucess(data);
                },
                error: function (data) { hideLoading(); showException(data); }
            });
        }
    });
}

//启动结算单窗体
function startStatementDialog(title) {
    modalLoadding("#statementDailog", title, 500)
    $("#statementDailog").modal();
}

//显示结算单对话框
function showStatementDialog(data) {
    $("#statementDailog .modal-content").html(data);//填充对话框
    initUniform();
    Metronic.initSlimScroll('.scroller');//滚动条
    if ($("#filePortrait")) {
        initFileInput("filePortrait", "/manufacturer/workorder/uploadimg?workOrderId=" + $("#workOrderId").val(), $("#current_urlImg").val());
    }

    calAmount();
}

//启动选择对话框
function startBigDailog(title) {
    modalLoadding("#editBigDailog", title, 450)
    $("#editBigDailog").modal();
}

//显示选择备件对话框
function showAccessoryDialog(data) {
    $("#editBigDailog .modal-content").html(data);//填充对话框
    SelectAccessory.init();
}

//显示选择服务项目对话框
function showFeeItemDialog(data) {
    $("#editBigDailog .modal-content").html(data);//填充对话框
    initUniform();
    Metronic.initSlimScroll('.scroller');
}

//弹出选择其他收费项目对话框
function showOtherFeeItemDialog(data) {
    $("#editBigDailog .modal-content").html(data);//填充对话框
    initUniform();
    Metronic.initSlimScroll('.scroller');
}

//结算单添加备件
function add_statmentorder_accessory(isReported) {
    startBigDailog('选择备件');
    $.ajax({
        url: "/admin/workorder/selectaccessory",
        type: "Get",
        data: { 'isReported': isReported, 'id': $('#workOrderId').val() },
        success: function (data) {
            showAccessoryDialog(data);
        },
        error: function (data) { showException(data); }

    });
}

//结算单选择备件
function select_statmentorder_accessory(val) {
    var isGuaranteed = $("#guaranteedstate").val() == "In";//在保状态
    var accessory = eval("(" + val + ")");
    var hasExist = false;//是否存在
    $("#div-statementOrder table[name=tb_accessory] tbody tr:not(:first)").each(function () {
        if ($(this).find("span[name=accessoryID]").html() == accessory.ID) {
            hasExist = true;
            $(this).find("input[name=count]").val(Number($(this).find("input[name=count]").val()) + 1);
            $(this).find("span[name=accessoryCount]").html($(this).find("input[name=count]").val());
            return;
        }
    });
    if (!hasExist) {
        var no = $("#div-statementOrder table[name=tb_accessory] tbody tr").length;
        var tr_accessory = $("#div-statementOrder table[name=tb_accessory] tbody tr:first");
        tr_accessory.find("span[name=accessoryNo]").html(no);
        tr_accessory.find("span[name=accessoryID]").html(accessory.ID);
        tr_accessory.find("span[name=accessoryName]").html(accessory.Name);
        tr_accessory.find("span[name=accessoryCode]").html(accessory.Coding);
        tr_accessory.find("span[name=accessoryType]").html(accessory.Type);
        tr_accessory.find("span[name=accessoryPrice]").html(accessory.Price);
        if (!accessory.IsCharged && isGuaranteed) {
            tr_accessory.find("span[name=accessoryAmount]").html("0.00");
        } else {
            tr_accessory.find("span[name=accessoryAmount]").html(accessory.Price);
        }

        tr_accessory.find("span[name=IsCharged]").html(String(accessory.IsCharged));
        $("#div-statementOrder table[name=tb_accessory] tbody").append("<tr>" + tr_accessory.html() + "</tr>");

    }

    calAmount();//计算总金额
}

//改变备件数量
function change_accessory_count(input) {
    $(input).parent().find("span[name=accessoryCount]").html($(input).val());
    calAmount();
}

//编辑结算单备件
function edit_statement_accessory(btn) {
    $(btn).parent().find("button[name=edit]").addClass("hidden");
    $(btn).parent().find("button[name=deleted]").addClass("hidden");
    $(btn).parent().find("button[name=save]").removeClass("hidden");
    $(btn).parent().find("button[name=cancel]").removeClass("hidden");
    $(btn).parent().parent().find("span[name=accessoryCount]").addClass("hidden");
    $(btn).parent().parent().find("input[name=count]").removeClass("hidden");
}

//删除结算单备件
function delete_statement_accessory(btn) {
    $(btn).parent().parent().remove();
    calAmount();
}

//查询服务项目
function queryFeeItems() {
    showPopLoading();
    $.ajax({
        url: "/manufacturer/workorder/getfeeitem",
        type: "Get",
        data: { "type": $("#select-type").val() },
        success: function (data) {
            hidePopLoading();
            $("#dataTable").html(data);
        },
        error: function (data) { hidePopLoading(); showException(data); }
    });
}

//结算单添加服务项目
function add_statmentorder_service() {
    startBigDailog('选择服务项目');
    $.ajax({
        url: "/manufacturer/workorder/selectfeeitem",
        type: "Get",
        success: function (data) {
            showFeeItemDialog(data);
        },
        error: function (data) { showException(data); }
    });
}

//结算单选择服务项目
function select_statmentorder_feeItem(val) {
    var isGuaranteed = $("#guaranteedstate").val() == "In";//在保状态
    var feeItem = eval("(" + val + ")");
    var hasExist = false;//是否存在

    $("#div-statementOrder table[name=tb_service] tbody tr:not(:first)").each(function () {
        if ($(this).find("span[name=serviceID]").html() == feeItem.ID) {
            hasExist = true;
            return;
        }
    });
    if (!hasExist) {
        var no = $("#div-statementOrder table[name=tb_service] tbody tr").length;
        var tr_service = $("#div-statementOrder table[name=tb_service] tbody tr:first");
        tr_service.find("span[name=serviceNo]").html(no);
        tr_service.find("span[name=serviceID]").html(feeItem.ID);
        tr_service.find("span[name=serviceName]").html(feeItem.Name);
        tr_service.find("span[name=servicePrice]").html(feeItem.CustomerPrice);

        if (!feeItem.IsCharged && isGuaranteed) {
            tr_service.find("span[name=serviceAmount]").html("0.00");
        }
        else {
            tr_service.find("span[name=serviceAmount]").html(feeItem.CustomerPrice);
        }

        tr_service.find("span[name=IsCharged]").html(String(feeItem.IsCharged));
        $("#div-statementOrder table[name=tb_service] tbody").append("<tr>" + tr_service.html() + "</tr>");

    }

    calAmount();//计算总金额
}

//结算单删除服务项目
function delete_statmentorder_service(btn) {
    $(btn).parent().parent().remove();
    calAmount();
}

//结算单添加其他收费项目
function add_statmentorder_other() {
    startBigDailog('选择其他收费项目');
    $.ajax({
        url: "/manufacturer/workorder/selectotherfeeitem",
        type: "Get",
        success: function (data) {
            showOtherFeeItemDialog(data);
        },
        error: function (data) { showException(data); }
    });
}

//结算单选择其他收费项目
function select_statmentorder_otherfeeItem(val) {
    var isGuaranteed = $("#guaranteedstate").val() == "In";//在保状态
    var feeItem = eval("(" + val + ")");
    var hasExist = false;//是否存在

    $("#div-statementOrder table[name=tb_other] tbody tr:not(:first)").each(function () {
        if ($(this).find("span[name=otherID]").html() == feeItem.ID) {
            hasExist = true;
            return;
        }
    });
    if (!hasExist) {
        var no = $("#div-statementOrder table[name=tb_other] tbody tr").length;
        var tr_other = $("#div-statementOrder table[name=tb_other] tbody tr:first");
        tr_other.find("span[name=otherNo]").html(no);
        tr_other.find("span[name=otherID]").html(feeItem.ID);
        tr_other.find("span[name=otherName]").html(feeItem.Name);
        tr_other.find("span[name=otherPrice]").html(feeItem.CustomerPrice);

        if (!feeItem.IsCharged && isGuaranteed) {
            tr_other.find("span[name=otherAmount]").html("0.00");
        }
        else {
            tr_other.find("span[name=otherAmount]").html(feeItem.CustomerPrice);
        }

        tr_other.find("span[name=IsCharged]").html(String(feeItem.IsCharged));
        $("#div-statementOrder table[name=tb_other] tbody").append("<tr>" + tr_other.html() + "</tr>");

    }

    calAmount();//计算总金额
}

//结算单删除服务项目
function delete_statmentorder_other(btn) {
    $(btn).parent().parent().remove();
    calAmount();
}

//计算结算单金额
function calAmount() {
    var isGuaranteed = $("#guaranteedstate").val() == "In";//在保状态

    //计算备件
    var accessoryfee = 0;
    $("#div-statementOrder table[name=tb_accessory] tbody tr:not(:first) ").each(function () {
        var accessoryAmount = 0;
        var price = $(this).find("span[name=accessoryPrice]").html();
        var count = $(this).find("span[name=accessoryCount]").html();
        var isCharged = $(this).find("span[name=IsCharged]").html();
        if (isCharged == 'false' && isGuaranteed) {
        } else {
            accessoryAmount = Number(price) * count;
        }

        $(this).find("span[name=accessoryAmount]").html((Number(accessoryAmount)).toFixed(2));
        accessoryfee += accessoryAmount;
    });
    $("#div-statementOrder strong[name=accessoryfee]").html((Number(accessoryfee)).toFixed(2));

    //计算服务费
    var servicefee = 0;
    $("#div-statementOrder table[name=tb_service] tbody tr:not(:first)").each(function () {
        var serviceAmount = 0;
        var serviceID = $(this).find("span[name=serviceID]").html();
        var isCharged = $(this).find("span[name=IsCharged]").html();
        serviceAmount = $(this).find("span[name=servicePrice]").html();

        if (isCharged == 'false' && isGuaranteed) {
            serviceAmount = 0;
        }
        $(this).find("span[name=serviceAmount]").html((Number(serviceAmount)).toFixed(2));

        servicefee += Number(serviceAmount);
    });

    $("#div-statementOrder strong[name=servicefee]").html((Number(servicefee)).toFixed(2));

    //计算其他
    var otherfee = 0;
    $("#div-statementOrder table[name=tb_other] tbody tr:not(:first)").each(function () {
        var otherAmount = 0;
        var otherID = $(this).find("span[name=otherID]").html();
        var isCharged = $(this).find("span[name=IsCharged]").html();
        otherAmount = $(this).find("span[name=otherPrice]").html();
        if (isCharged == 'false' && isGuaranteed) {
            otherAmount = 0;
        }
        $(this).find("span[name=otherAmount]").html((Number(otherAmount)).toFixed(2));
        otherfee += Number(otherAmount);
    });
    $("#div-statementOrder strong[name=otherfee]").html((Number(otherfee)).toFixed(2));
    $("#div-statementOrder em[name=stamentorderAmount]").html((Number(accessoryfee + servicefee + otherfee)).toFixed(2));

}

//保存结算单
function statmentorder_save() {
    var CustomerAmount = $("#div-statementOrder em[name=stamentorderAmount]").html();
    var FacilitatorAmount = $("#div-statementOrder span[name=FacilitatorAmount]").html();
    var accessorys = new Array();

    $("#div-statementOrder table[name=tb_accessory] tbody tr:not(:first)").each(function () {

        var accessoryID = $(this).find("span[name=accessoryID]").html();

        if (accessoryID) {
            var count = $(this).find("span[name=accessoryCount]").html();
            accessorys.push("{'AccessoryID':'" + accessoryID + "','Count':'" + count + "'}");
        }

    });
    var feeItems = new Array();
    $("#div-statementOrder table[name=tb_service] tbody tr:not(:first)").each(function () {
        var serviceID = $(this).find("span[name=serviceID]").html();
        if (serviceID) {
            feeItems.push(serviceID);
        }
    });

    var otherfeeItems = new Array();
    $("#div-statementOrder table[name=tb_other] tbody tr:not(:first)").each(function () {
        var otherID = $(this).find("span[name=otherID]").html();
        if (otherID) {
            otherfeeItems.push(otherID);
        }
    });
    showPopLoading();
    $.ajax({
        url: "/manufacturer/workorder/savestatementorder",
        type: "POST",
        data: { "ID": $("#statementorderid").val(), "GuaranteedState": $("#guaranteedstate").val(), "StatementState": $("#statementstate").val(), "WorkOrderID": $("#workorderid").val(), "CustomerAmount": CustomerAmount, "FacilitatorAmount": FacilitatorAmount, "feeItems": JSON.stringify(feeItems), "otherfeeItems": JSON.stringify(otherfeeItems), "accessorys": JSON.stringify(accessorys) },
        success: function (data) {
            hidePopLoading();
            if (data == "ok") {
                $("#statementDailog").modal("hide");
                window.location.reload();
            } else { growlAlert.danger("保存失败"); }
        },
        error: function (data) { hidePopLoading(); showException(data); }
    });
}

//发送结算单
function statmentorder_send() {
    showPopLoading();
    $.ajax({
        url: "/manufacturer/workorder/sendstatementorder",
        type: "POST",
        data: { "workOrderId": $("#workorderid").val() },
        success: function (data) {
            hidePopLoading();
            if (data == "ok") {
                growlAlert.success("发送成功");
            }
            else {
                growlAlert.info(data);
            }
        },
        error: function (data) { hidePopLoading(); showException(data); }
    });
}

//保存回单
function report_save() {
    if ($("#hasStatement").val() == "true") {
        //验证员工备件库存
        var isvalid = true;//是有效
        var errormsg = "";
        var staffstocks = eval($("#staffStocks").val());
        $("#div-statementOrder table[name=tb_accessory] tbody tr:not(:first)").each(function () {
            var accessoryID = $(this).find("span[name=accessoryID]").html();
            var count = $(this).find("span[name=accessoryCount]").html();
            var name = $(this).find("span[name=accessoryName]").html();
            isvalid = false;
            if (staffstocks) {
                $.each(staffstocks, function (i, o) {
                    if (o.Accessory.ID == accessoryID && o.NewCount >= count) {
                        isvalid = true; return false;
                    }
                });
            }

            if (!isvalid) {
                errormsg = "您持有的备件:" + name + "数量不足！";
                return false;
            }
        });

        if (!isvalid) {
            growlAlert.danger(errormsg);
            return;
        }
    }

    process_save();
}

//保存处理信息
function process_save() {
    showPopLoading();
    $.ajax({
        url: "/manufacturer/workorder/report",
        type: "POST",
        data: { "serviceModeId": $("#serviceModeId").val(), "processDesc": $("#processDesc").val(), "workOrderId": $("#workorderid").val() },
        success: function (data) {
            hidePopLoading();
            if (data == "ok") {
                if ($("#hasStatement").val() == "true") {
                    recycle_save();//保存回收记录
                }
                else {
                    window.location.reload();
                }
            }
        },
        error: function (data) { hidePopLoading(); showException(data); }
    });
}

//保存回收记录
function recycle_save() {
    var accessorys = new Array();
    $("#div-statementOrder table[name=tb_accessory] tbody tr:not(:first)").each(function () {
        var accessoryID = $(this).find("span[name=accessoryID]").html();
        if (accessoryID) {
            var count = $(this).find("input[name=backcount]").val();
            accessorys.push("{'AccessoryID':'" + accessoryID + "','Count':'" + count + "'}");
        }
    });
    showPopLoading();
    $.ajax({
        url: "/manufacturer/workorder/saverecoverylog",
        type: "POST",
        data: { "workOrderId": $("#workorderid").val(), "accessorys": JSON.stringify(accessorys), "workOrdernumber": $("#workordernumber").val() },
        success: function (data) {
            hidePopLoading();
            statmentorder_save();//保存结算单
        },
        error: function (data) { hidePopLoading(); showException(data); }
    });
}

//改变回单tab
function changepanel(id, li) {
    $("#panel_" + id).siblings().addClass("hidden");
    $("#panel_" + id).removeClass("hidden");
    $(li).parent().siblings().removeClass("active");
    $(li).parent().addClass("active");
}

//初始化fileinput
function initFileInput(ctrlName, uploadUrl, imageurl) {
    var control = $('#' + ctrlName);
    control.fileinput({
        language: 'zh', //设置语言
        uploadUrl: uploadUrl, //上传的地址
        allowedPreviewTypes: ['image'],
        allowedFileExtensions: ['jpg', 'png'],//接收的文件后缀
        dropZoneEnabled: false,//是否显示拖拽区域
        showPreview: true, //是否显示预览
        uploadAsync: true,
        showUpload: false, //是否显示上传按钮
        showCaption: false,//是否显示标题
        showRemove: false,
        showCancel: false,
        showClose: false,
        showUploadedThumbs: false,
        autoReplace: true,
        browseClass: "btn default", //按钮样式
        enctype: 'multipart/form-data',
        validateInitialCount: true,
        initialPreview: [ //预览图片的设置
             "<img src='" + imageurl + "' class='file-preview-image' alt='' title='' style='width:auto;height:160px;'/>",
        ],
        maxFileCount: 2, //表示允许同时上传的最大文件个数
        browseIcon: "<i class='glyphicon glyphicon-picture'></i>",
        maxFileSize: 2000
    }).on("filebatchselected", function (event, files) {
        control.fileinput('upload');
    }).on("fileuploaded", function (event, data) {
        $("#UrlImg").val(data.response);
        $(".esh-file-view .esh-loading").addClass("hidden");
    }).on('filepreupload', function (event, data, previewId, index) {
        $(".esh-file-view .esh-loading").removeClass("hidden").css("left", (parseInt($(".esh-file-view .kv-file-content").width() / 2) - parseInt($(".esh-loading .loading-message").innerWidth() / 2)) + "px");
    });
}

//启动编辑工单对话框
function startWorkOrderDialog(title) {
    modalLoadding("#editworkDailog", title, 460)
    $("#editworkDailog").modal();
}

//弹出编辑工单对话框
function showWorkOrderDialog(data) {
    $("#editworkDailog .modal-content").html(data);//填充对话框
    WorkOrder.init("/manufacturer/customer/getcustomersbyname");//初始化对话框
}

//创建工单成功
function editWorkOrderSucess(data) {
    if (data == "ok") {
        $("#editworkDailog").modal("hide"); growlAlert.success("编辑工单成功"); setTimeout('window.location.reload()', 1500);
    } else { growlAlert.danger("编辑工单失败。"); }
}
