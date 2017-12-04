(function ($) {
    app.modals.CreateOrEditWorkOrderModal = function() {

        var _workOrderService = abp.services.app.workOrder;
        var _customerService = abp.services.app.customer;
        //用户名查找用户
        var url_getcustomersbyname = "/api/services/app/customer/GetCustomerByName";
        //产品数据
        var productdata = "";

        var _modalManager;
        var _$userInformationForm = null;
        //初始化选择客户控件
        var initCustomer = function selectCustomer(id, url) {

            //var customerInput = _modalManager.getModal().find('input[id=WorkOrder_CustomerName]');
            $(id).select2({
                minimumInputLength: 1,
                formatInputTooShort: "请输入客户名称",
                allowClear: true,
                id: function (item) {
                    return item.name
                },
                
                ajax: {
                    url: url,
                    input:"",
                    type: "post",
                    dataType: 'json',
                    data: function (term) {
                        input=term;
                        return {
                            name: term
                        };
                    },
                    results: function (data) { // parse the results into the format expected by Select2.
                        var result;
                        if (data.success === true && data.result.items.length > 0)
                            result = data.result.items;
                        else
                            result = [{ name: input,id:null,mobile:null,area:null,email:null,address:null }];
                        return {
                            results: result
                        };
                    }
                },
                formatResult: function (item) {

                    return item.name;
                }, // omitted for brevity, see the source of this page
                formatSelection: function (item) {
                    $("#WorkOrder_CustomerId").val(item.id);
                    $("#WorkOrder_CustomerPhone").val(item.mobile);
                    
                    if (item.area) { initArea2("#sel_pro", "#sel_city", "#sel_distr", item.area); }
                    else { $("#sel_pro").val(""); $("#sel_pro").change(); }

                    $("#WorkOrder_CustomerEmail").val(item.email);
                    $("#WorkOrder_Area").val(item.area);
                    $("#WorkOrder_Address").val(item.address);
                    return item.name;
                }, // omitted for brevity, see the source of this page
                escapeMarkup: function (m) {
                    return m;
                }
            });
        }
        //获取省市区域的值
        var getAreaVal = function getAreaVal() {
            //获取省市区的值
            var area = $("#sel_pro").find("option:selected").text() + "-" + $("#sel_city").find("option:selected").text() + "-" + $("#sel_distr").find("option:selected").text();
            if ($("#sel_pro").val() && $("#sel_city").val() && $("#sel_distr").val()) {
                $("#WorkOrder_Area").val(area).focus().blur();
            }
            else { $("#WorkOrder_Area").val(""); }
        }

        
      

     
        this.init = function (modalManager) {
            _modalManager = modalManager;
            //选择客户控件初始化
            if (!$("#WorkOrder_CustomerName").val()) {
                initCustomer("#WorkOrder_CustomerName", url_getcustomersbyname);
            }
            //初始化产品数据
            productdata = eval($("#productdata").val());

            //选择产品
            $("#ProductName").change(function () {
                var productName = $(this).val();
                if (productdata) {
                    $("#ProductModel option:not(:first)").remove();
                    $.each(productdata, function (i, item) {
                        if (item.Name == productName) {
                            $("#ProductModel").append("<option value=" + item.Model + ">" + item.Model + "</option>");//产品类型赋值
                        }
                    });
                }
                var productModel = $("#ProductModel").val();
                $("#ProductID").val("");
                $.each(productdata, function (i, item) {
                    if (item.Name == productName && item.Model == productModel) {
                        $("#WorkOrder_ProductId").val(item.Id);
                    }
                });
            });

            //选择型号
            $("#ProductModel").change(function () {
                var productName = $("#ProductName").val();
                var productModel = $("#ProductModel").val();
                $("#WorkOrder_ProductId").val("");
                $.each(productdata, function (i, item) {
                    if (item.Name == productName && item.Model == productModel) {
                        $("#WorkOrder_ProductId").val(item.Id);
                    }
                });
            });
            //选择工单类型
            //$("#ServiceCategoryName").change(function () {
            //    $("#ServiceCategoryID").val($(this).val());
            //});

            //初始化省市区域
            if ($("#Customer_Area")) {
               initArea("#sel_pro", "#sel_city", "#sel_distr", $("#WorkOrder_Area").val());
                $("#sel_distr").blur(function () { getAreaVal(); });
            }

            //Metronic.initSlimScroll('.scroller');//滚动条
            $("#editWorkOrderForm").removeData("validator");//删除验证器
            debugger;
            $.validator.unobtrusive.parse("#editWorkOrderForm");//添加验证器

            

        };

        this.save = function () {
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
            
                var formData = $("#editWorkOrderForm").serializeFormToObject();
                _modalManager.setBusy(true);
                var param = {
                    "workOrder": {
                        "id": formData["WorkOrder.Id"],
                        "serviceType": formData["WorkOrder.ServiceType"],
                        "customerId": formData["WorkOrder.CustomerId"],
                        "customerName": formData["WorkOrder.CustomerName"],
                        "address": formData["WorkOrder.Address"],
                        "customerPhone": formData["WorkOrder.CustomerPhone"],
                        "area": formData["WorkOrder.Area"],
                        "customerEmail": formData["WorkOrder.CustomerEmail"],
                        "saleMan": formData["WorkOrder.SaleMan"],
                        "saleManPhone": formData["WorkOrder.SaleManPhone"],
                        "description": formData["WorkOrder.Description"],
                        "productId": formData["WorkOrder.ProductId"],
                        "priority": formData["WorkOrder.Priority"]
                    }
                }
                _workOrderService.createOrUpdateWorkOrder( param ).done(function () {
                    abp.notify.info("保存工单成功");
                    _modalManager.close();
                    abp.event.trigger('app.createOrEditWorkOrderModalSaved');
                }).always(function () {
                    _modalManager.setBusy(false);
                });
            }

        

            //var assignedRoleNames = _findAssignedRoleNames();
            //var user = _$userInformationForm.serializeFormToObject();

            

            
          
        };
    };
})(jQuery);