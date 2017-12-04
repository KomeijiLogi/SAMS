/*工单js*/
var WorkOrder = function () {
    var productdata = "";//产品数据

    //初始化工单页面事件
    var initPage = function () {
        initDataPicker();//初始化自定义字段中日期类型字段

        //初始化产品数据
        productdata = eval($("#productdata").val());

        //选择产品
        $("#ProductName").change(function () {
            var productName = $(this).val();
            if (productdata) {
                $("#ProductType option:not(:first)").remove();
                $.each(productdata, function (i, item) {
                    if (item.Name == productName) {
                        $("#ProductType").append("<option value=" + item.Model + ">" + item.Model + "</option>");//产品类型赋值
                    }
                });
            }

            var productType = $("#ProductType").val();
            $("#ProductID,#WorkOrder_ProductID").val("");
            $.each(productdata, function (i, item) {
                if (item.Name == productName && item.Model == productType) {
                    $("#ProductID,#WorkOrder_ProductID").val(item.Id);
                }
            });
        });

        //选择型号
        $("#ProductType").change(function () {
            var productName = $("#ProductName").val();
            var productType = $("#ProductType").val();
            $("#ProductID,#WorkOrder_ProductID").val("");
            $.each(productdata, function (i, item) {
                if (item.Name == productName && item.Model == productType) {
                    $("#ProductID,#WorkOrder_ProductID").val(item.Id);
                }
            });
        });

        //选择工单类型
        $("#ServiceType").change(function () {
            $("#ServiceTypeID,#WorkOrder_ServiceTypeID").val($(this).val());
        });

        //initSelect();//初始化select2控件
        if ($("#Customer_Area")) {
            initArea("#sel_pro", "#sel_city", "#sel_distr", $("#Customer_Area").val());//初始化省市区域
            $("#sel_distr").blur(function () { getAreaVal(); });
        }
        Metronic.initSlimScroll('.scroller');//滚动条
        $("#editWorkOrderForm").removeData("validator");//删除验证器
        $.validator.unobtrusive.parse("#editWorkOrderForm");//添加验证器
        initUniform();
    };

    //初始化选择客户控件
    var initCustomer = function selectCustomer(id, url) {
        var v = $(id).val();
        $(id).select2({
            minimumInputLength: 1,
            formatInputTooShort: "请输入客户名称",
            allowClear: true,
            id: function (item) {
                return item.Name
            },
            ajax: {
                url: url,
                type: "get",
                dataType: 'json',
                data: function (term) {
                    return {
                        name: term
                    };
                },
                results: function (data) { // parse the results into the format expected by Select2.
                    return {
                        results: data
                    };
                }
            },
            formatResult: function (item) {

                return item.Name;
            }, // omitted for brevity, see the source of this page
            formatSelection: function (item) {
                $("#WorkOrder_CustomerID,#Customer_ID").val(item.Id);
                $("#Customer_Mobile").val(item.Mobile);//,#WorkOrder_ContactNumber
                //$("#WorkOrder_Contact").val(item.Name);
                if (item.Area) { initArea2("#sel_pro", "#sel_city", "#sel_distr", item.Area); }
                else { $("#sel_pro").val(""); $("#sel_pro").change(); }


                $("#Customer_Area").val(item.Area);
                $("#Customer_Address").val(item.Address);
                return item.Name;
            }, // omitted for brevity, see the source of this page
            escapeMarkup: function (m) {
                return m;
            },
            initSelection: function (element, callback) {
                //初始化时设置默认值

                var data = {
                    Id: $("#WorkOrder_CustomerID").val(),
                    Mobile: $("#Customer_Mobile").val(),
                    Area: $("#Customer_Area").val(),
                    Address: $("#Customer_Address").val(),
                    Name: $(element).val()
                };
                callback(data);
                //$.ajax(url, 
                //    { 
                //        data: { 
                //            name: $(element).val(),
                //            dataType: "json" 
                //        } 
                //    }
                //    )
                //.done(function(data){
                //    callback(data[0]);
                //});

            }
        });


    }

    //获取省市区域的值
    var getAreaVal = function getAreaVal() {
        //获取省市区的值
        var area = $("#sel_pro").find("option:selected").text() + "-" + $("#sel_city").find("option:selected").text() + "-" + $("#sel_distr").find("option:selected").text();
        if ($("#sel_pro").val() && $("#sel_city").val() && $("#sel_distr").val()) {
            $("#Customer_Area").val(area).focus().blur();
        }
        else { $("#Customer_Area").val(""); }
    }

    return {
        //传入查询客户数据的url
        init: function () {
            //选择客户控件初始化
            //if (!$("#CustomerName").val()) { initCustomer("#CustomerID", url); }
            //if (!$("#Customer_Name").val()) { initCustomer("#Customer_Name", url); }
            //initCustomer("#Customer_Name", url);
            initPage();
        }
    };
}();

//提交工单表单
function submitWorkOrderForm() {

    debugger;
    var isvalid = true;
    if (!$("#editWorkOrderForm").valid()) {
        isvalid = false;
    }
   
    var inputs = $("#editWorkOrderForm").find("input[data-bind='required'],select[data-bind='required'],textarea[data-bind='required']");
    if (inputs) {
        $.each(inputs, function () {
            if (!$(this).val()) {
                $(this).parent().find("span.esh-error-lable").html("输入项不能为空！");
                isvalid = false;
            }
            else {
                $(this).parent().next().html("");
            }
        });
    }

    if (isvalid) {
        $("#editWorkOrderForm").submit();
        //window.location.reload();
    }
}
var xhr;
function searchCus(o, url) {
    var a = $(o).attr("data-bind");//控制区域
    var b = $(o).attr("data-id");//文本id

    var name = $("#" + b).val();

    if (name.length < 3) return;

    if (xhr) { xhr.abort(); }
    xhr = $.ajax({
        url: url,
        type: "Get",
        data: { "name": name },
        success: function (data) {
            $("#" + a).empty();
            $("#" + a).append(data);
            if ($("#" + a).find("li").length > 0) {
                $("#" + a).find("li:eq(0)").addClass("active").siblings().removeClass("active");
            }
        },
        error: function (data) {
            if (data.readyState == 4)
                showException(data)
        }
    });

}

function searchBtnCus(o, url, input) {
    searchCus(o, url);
    $("#" + input).focus();
}

function bindCusData(o) {
    var cusVal = $(o).attr("data-val");
    var cusId = $(o).attr("data-id");

    var a = $(o).attr("data-bind");//获取当前客户id
    var b = $(o).attr("data-content");//获取当前客户姓名
    var c = $(o).attr("data-area");//省市
    var d = $(o).attr("data-address");//详细地址
    var e = $(o).attr("data-code");//客户编码
    $("#" + cusVal).val(b).focus().blur();
    $("#" + cusVal + "_hidden").val(b);
    $("#" + cusId).val(a);

    $("#Customer_Code").val(e);
    $("#customer_area").val(c).focus().blur();
    $("#Customer_Address").val(d).focus().blur();

}

//自定义字段值改变
function changeCustomFieldValue(id, type, field) {
    var value = "";
    $("#customField_" + id).html("");
    //数据格式检验
    if (type == 1) {//单行文本
        value = $(field).val();
    }
    else if (type == 2) {//多行文本
        value = $(field).val();
    }
    else if (type == 3) {//纯数字
        value = $(field).val();
        if (value.length > 8) {
            $("#customField_" + id).html("请输入小于10000000的数值"); value = ""; $(field).val("");
        }
    }
    else if (type == 4) {//邮箱
        value = $(field).val();
        if (value && !validEmail(value)) {
            $("#customField_" + id).html("请输入正确的邮箱"); value = ""; $(field).val("");
        }
    }
    else if (type == 5) {//勾选项
        value = $(field).attr('checked') ? true : false;
    }
    else if (type == 6) {//下拉框
        value = $(field).val();
    }
    else if (type == 7) {//日期
        value = $(field).val();
    }
    else if (type == 8) {//小数
        value = $(field).val();
        var p = /^[1-9](\d+(\.\d{1,9})?)?$/;
        var p1 = /^[0-9](\.\d{1,9})?$/;
        if (value && !p.test(value) && !p1.test(value)) {
            $("#customField_" + id).html("请输入正确的小数"); value = ""; $(field).val("");
        }

        if (value.length > 9) {
            $("#customField_" + id).html("请输入长度小于10位的小数"); value = ""; $(field).val("");
        }
    }
    else if (type == 9) {//radio list
        $("#customField_" + id + "_hide").val($(field).val());
        value = $("#customField_" + id + "_hide").val();
    }
    else if (type == 10) {//checkk list
        var checks = $("#customField_" + id + "_hide").parent().find("input[type=checkbox]");
        var subData = "";
        $.each(checks, function (i, o) {
            if ($(o).attr('checked')) {
                subData += $(o).val() + ",";
            }
        });

        subData = subData.substring(0, subData.length - 1)
        $("#customField_" + id + "_hide").val(subData);
        value = $("#customField_" + id + "_hide").val();
    }

    //json数组存放自定义字段id value
    var jsonarrayNewCustomFields = new Array();
    var arr =
         {
             "id": id,
             "value": value
         }
    jsonarrayNewCustomFields.push(arr);
    var jsonstr;
    var jsonarrayOldCustomFields;
    if ($("#CustomFields")) {
        jsonstr = $("#CustomFields").val();
        if (jsonstr) {
            jsonarrayOldCustomFields = eval('(' + jsonstr + ')');
            for (i = 0; i < jsonarrayOldCustomFields.length; i++) {
                if (jsonarrayOldCustomFields[i].id != id) {
                    jsonarrayNewCustomFields.push(jsonarrayOldCustomFields[i]);
                }
            }
        }
        $("#CustomFields").val(JSON.stringify(jsonarrayNewCustomFields));
    }

    if ($("#WorkOrder_CustomFields")) {
        jsonstr = $("#WorkOrder_CustomFields").val();
        if (jsonstr) {
            jsonarrayOldCustomFields = eval('(' + jsonstr + ')');
            for (i = 0; i < jsonarrayOldCustomFields.length; i++) {
                if (jsonarrayOldCustomFields[i].id != id) {
                    jsonarrayNewCustomFields.push(jsonarrayOldCustomFields[i]);
                }
            }
        }
        $("#WorkOrder_CustomFields").val(JSON.stringify(jsonarrayNewCustomFields));
    }

}
