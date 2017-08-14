/**
Custom module for you to write your own javascript functions
**/


var OperationTable = function () {
    //table列的全选功能
    var _columnAllSelect = function (table_id) {
        $(table_id).find('.group-checkable').change(function () {
            var set = jQuery(this).attr("data-set");
            var checked = jQuery(this).is(":checked");
            jQuery(set).each(function () {
                if (checked) {
                    $(this).attr("checked", true);
                } else {
                    $(this).attr("checked", false);
                }
            });
            //jQuery.uniform.update(set);
        });
    }

    //初始加载table列的显示或隐藏
    var _columnToggle = function (table_id) {
        $('input[type="checkbox"]', table_id + "_column_toggler").each(function () {
            var iCol = $(this).attr("data-column");
            if ($(this).attr('checked')) {
                $("th:eq(" + iCol + "),td:eq(" + iCol + ")", $("tr")).show();
            } else {
                $("th:eq(" + iCol + "),td:eq(" + iCol + ")", $("tr")).hide();
            }
        });
    }

    //单击‘列’复选框判断table列的显示或隐藏
    var _columnToggleCli = function (table_id) {
        $('input[type="checkbox"]', table_id + "_column_toggler").change(function () {
            var iCol = parseInt($(this).attr("data-column"));
            if ($(this).attr('checked')) {
                $("th:eq(" + iCol + "),td:eq(" + iCol + ")", $("tr")).show();
            } else {
                $("th:eq(" + iCol + "),td:eq(" + iCol + ")", $("tr")).hide();
            }
        });
    }

    //table列的排序
    var _columnSort = function (table_id, sort_id, direction_id, fun) {
        var sort = $(sort_id).val();
        var direction = $(direction_id).val();
        if (sort && direction) {
            $("#tableList thead th[aria-data]").each(function () {
                if ($(this).attr("aria-label") == sort) {
                    $(this).attr("class", "sorting_" + direction).attr("aria-data", direction);
                }
            });
        }

        $(table_id + " thead th[aria-data]").click(function () {
            if ($(this).hasClass("sorting_desc")) {
                $(this).removeClass().addClass("sorting_asc");
                $(this).attr("aria-data", "asc");
            } else {
                $(this).removeClass().addClass("sorting_desc");
                $(this).attr("aria-data", "desc");
            }

            fun($(this).attr("aria-label"), $(this).attr("aria-data"));
        });
    }

    return {
        init: function (table_id, sort_id, direction_id, fun) {
            initUniform();
            _columnAllSelect(table_id);
            _columnToggle(table_id);
            _columnToggleCli(table_id);
            _columnSort(table_id, sort_id, direction_id, fun)
        },
        initPart: function (table_id) {
            initUniform();
            _columnAllSelect(table_id);
            _columnToggle(table_id);
            _columnToggleCli(table_id);
        },

        initHideColumn: function (columns) {

            $.each(columns, function (i, col) {
                $("th:eq(" + col + "),td:eq(" + col + ")", $("tr")).hide();
            });

        }

    };
}();



//初始化check、radio样式
function initUniform() {
    if (!$().uniform) {
        return;
    }
    var uncheck = $("input[type=checkbox]:not(.toggle,.magic-checkbox, .md-check, .md-radiobtn, .make-switch, .icheck), input[type=radio]:not(.toggle, .md-check, .md-radiobtn, .star, .make-switch, .icheck)");
    if (uncheck.size() > 0) {
        uncheck.each(function () {
            if ($(this).parents(".checker").size() === 0) {
                $(this).show();
                $(this).uniform();
            }
        });
    }
}

//初始化时间控件
function initDataPicker() {
    $('.date-picker').datepicker({
        autoclose: true,
        language: 'zh-CN',/*加载日历语言包，可自定义*/
        weekStart: 1,
        todayBtn: 'linked',
        todayHighlight: 1,
        startView: 3,
        minView: 2,
        forceParse: 1,
        format: 'yyyy-mm-dd'
    });
}

//验证邮箱
function validEmail(email) {
    return /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test(email);
}

//验证固话，手机号码，400热线合法返回true,反之,返回false
function IsTelephone(value) {
    var isPhone = /^([0-9]{3,4}-)?[0-9]{7,8}$/;
    var isMob = /^((\+?86)|(\(\+86\)))?(13[012356789][0-9]{8}|15[012356789][0-9]{8}|18[02356789][0-9]{8}|147[0-9]{8}|1349[0-9]{7})$/;
    var isServer = /^400[016789]\d{6}$/;
    if (isMob.test(value) || isPhone.test(value) || isServer.test(value)) {
        return true;
    }
    else {
        return false;
    }
}

//验证url地址
function IsURL(str_url) {
    var strRegex = "^((https|http)://)?[wW]{3}\.[a-z0-9A-Z][a-z0-9A-Z]{0,61}?[a-z0-9A-Z]\.com|com.cn|net|cn|cc (:s[0-9]{1-4})?/$";
    var re = new RegExp(strRegex);
    if (re.test(str_url)) {
        return true;
    } else {
        return false;
    }
}

//省市联动  areaval：省-市-区
function initArea(sel_pro, sel_city, sel_distr, areaval) {
    var province, city, town;
    var loc = new Location();
    var title = ['省份', '城市', '区县'];
    $.each(title, function (k, v) {

        title[k] = '<option value="">' + v + '</option>';
    })

    $(sel_pro).change(function () {
        $(sel_city).empty();
        $(sel_city).append(title[1]);
        $(sel_distr).empty();
        $(sel_distr).append(title[2]);
        loc.fillOption(sel_city, '0,' + $(sel_pro).val());
        $(sel_city).change()
    })

    $(sel_city).change(function () {
        $(sel_distr).empty();
        $(sel_distr).append(title[2]);
        loc.fillOption(sel_distr, '0,' + $(sel_pro).val() + ',' + $(sel_city).val());
    })

    $(sel_distr).change(function () {
        //$('input[@name=location_id]').val($(this).val());
    })

    if (province) {
        loc.fillOption(sel_pro, '0', province);

        if (city) {
            loc.fillOption(sel_city, '0,' + province, city);

            if (town) {
                loc.fillOption(sel_distr, '0,' + province + ',' + city, town);
            }
        }
    } else {
        loc.fillOption(sel_pro, '0');
    }

    if (areaval) {
        var area = areaval.split("-");//设置区域
        loc.setOpetion(sel_pro, '0', area[0]);
        loc.setOpetion(sel_city, '0,' + $(sel_pro).val(), area[1]);
        loc.setOpetion(sel_distr, '0,' + $(sel_pro).val() + ',' + $(sel_city).val(), area[2]);
    }

}

//省市联动  areaval：省-市-区
function initArea2(sel_pro, sel_city, sel_distr, areaval) {
    var province, city, town;
    var loc = new Location();
    var title = ['省份', '全部', '全部'];
    $.each(title, function (k, v) {

        title[k] = '<option value="">' + v + '</option>';
    })

    $(sel_pro).change(function () {
        $(sel_city).empty();
        $(sel_city).append(title[1]);
        $(sel_distr).empty();
        $(sel_distr).append(title[2]);
        loc.fillOption2(sel_city, '0,' + $(sel_pro).val());
        $(sel_city).change()
    })

    $(sel_city).change(function () {
        $(sel_distr).empty();
        $(sel_distr).append(title[2]);
        loc.fillOption2(sel_distr, '0,' + $(sel_pro).val() + ',' + $(sel_city).val());
    })

    $(sel_distr).change(function () {
        //$('input[@name=location_id]').val($(this).val());
    })

    if (province) {
        loc.fillOption2(sel_pro, '0', province);

        if (city) {
            loc.fillOption2(sel_city, '0,' + province, city);

            if (town) {
                loc.fillOption2(sel_distr, '0,' + province + ',' + city, town);
            }
        }
    } else {
        loc.fillOption2(sel_pro, '0');
    }

    if (areaval) {
        $(sel_pro).empty();
        $(sel_city).empty();
        $(sel_distr).empty();
        var area = areaval.split("-");//设置区域
        loc.setOpetion2(sel_pro, '0', area[0], "省份");
        if (area.length > 1) {
            loc.setOpetion2(sel_city, '0,' + $(sel_pro).val(), area[1], "全部");
        }
        else {
            loc.setOpetion2(sel_city, '0,' + $(sel_pro).val(), "全部", "全部");
        }
        if (area.length > 2) {
            loc.setOpetion2(sel_distr, '0,' + $(sel_pro).val() + ',' + $(sel_city).val(), area[2], "全部");
        }
        else {
            loc.setOpetion2(sel_distr, '0,' + $(sel_pro).val() + ',' + $(sel_city).val(), "全部", "全部");
        }
    }
}


//初始化select2控件
function initSelect() {
    if ($().select2) {
        $('.select2').select2({
            placeholder: "Select",
            allowClear: true
        });
    }
};


//初始化侧位菜单栏方法
function initSideMenu() {
    $('.side-menu-project-list>ul.nav li').click(function () {
        $(this).addClass('active').siblings().removeClass('active');
    });

    SideMenuView();
    Metronic.addResizeHandler(function () {
        SideMenuView();
    });

    function SideMenuView() {

        if (Metronic.getViewPort().width <= 992) {
            $(".side-menu-sidebar .portlet-title>.caption").attr("data-toggle", "collapse")
            $(".side-menu-project-list-content").addClass("collapse");
            $(".side-menu-sidebar .scroller,.side-menu-sidebar .slimScrollDiv").css("height", "auto");
        } else {
            $(".side-menu-sidebar .portlet-title>.caption").attr("data-toggle", "")
            $('.side-menu-project-list-content').removeClass("collapse").css("height", "auto");
            $(".side-menu-sidebar .scroller,.side-menu-sidebar .slimScrollDiv").css("height", ($(window).height() - $(".page-header").height() - parseInt($(".side-menu-sidebar").css("padding-top"))) + "px");
        }
    }
}


//去除字符串最后一个符号
function trimEnd(data) {
    if (data.length > 0) {
        return data.substr(0, data.length - 1);
    }

    return data;
}

//重新定义bootbox控件事件
var botbox = function () {
    return {
        alert: function (title, message) {
            bootbox.alert({
                buttons: {
                    ok: {
                        label: '确定',
                        className: "btn green"
                    }
                },
                message: message,
                title: title
            });
        },
        confirm: function (title, message, callback) {
            bootbox.confirm({
                buttons: {
                    confirm: {
                        className: "btn green"
                    }
                },
                message: message,
                callback: callback,
                title: title
            });
        },
        prompt: function (message, callback) {
            bootbox.prompt({
                buttons: {
                    confirm: {
                        className: "btn green"
                    }
                },
                title: message,
                callback: callback
            })
        },
        dialog: function (title, message, callback) {
            bootbox.dialog({
                buttons: {
                    success: {
                        label: '确定',
                        className: "btn green",
                        callback: callback
                    },
                    cancel: {
                        label: '取消',
                        className: 'btn default',
                        callback: function () { }
                    }
                },
                title: title,
                message: message

            });
        }
    };
}();

var growlAlert = function () {
    var _bootstrapGrowl = function (msg, type) {
        $.bootstrapGrowl(msg, {
            ele: 'body', // which element to append to
            type: type, // (null, 'info', 'danger', 'success', 'warning')
            offset: {
                from: "top",
                amount: 50
            }, // 'top', or 'bottom'
            align: "center", // ('left', 'right', or 'center')
            width: "auto", // (integer, or 'auto')
            delay: "2500", // Time while the message will be displayed. It's not equivalent to the *demo* timeOut!
            allow_dismiss: false, // If true then will display a cross to close the popup.
            stackup_spacing: 10 // spacing between consecutively stacked growls.
        });
    }

    return {
        success: function (msg) {
            _bootstrapGrowl(msg, "success")
        },
        info: function (msg) {
            _bootstrapGrowl(msg, "info")
        },
        warning: function (msg) {
            _bootstrapGrowl(msg, "warning")
        },
        danger: function (msg) {
            _bootstrapGrowl(msg, "danger")
        }
    };
}();

//显示加载中
function showLoading() { Metronic.blockUI({ target: '.page-content', boxed: true }); }

//隐藏加载中
function hideLoading() { Metronic.unblockUI('.page-content'); }

//显示加载中
function showPopLoading() { Metronic.blockUI({ target: '.modal-content', boxed: true }); }

//隐藏加载中
function hidePopLoading() { Metronic.unblockUI('.modal-content'); }

function customerShowLoading(options) {
    options = $.extend(true, {}, options);
    var html = '<div class="loading-message ' + (options.boxed ? 'loading-message-boxed' : '') + '">' + '<div class="block-spinner-bar"><div class="bounce1"></div><div class="bounce2"></div><div class="bounce3"></div></div>' + '</div>';

    if (options.target) { // element blocking
        var el = $(options.target);
        if (el.height() <= ($(window).height())) {
            options.cenrerY = true;
        }
        el.block({
            message: html,
            baseZ: options.zIndex ? options.zIndex : 1000,
            centerY: options.cenrerY !== undefined ? options.cenrerY : false,
            css: {
                top: '10%',
                border: '0',
                padding: '0',
                backgroundColor: 'none'
            },
            overlayCSS: {
                backgroundColor: options.overlayColor ? options.overlayColor : '#555',
                opacity: options.boxed ? 0.05 : 0.1,
                cursor: 'wait'
            }
        });
    } else { // page blocking
        $.blockUI({
            message: html,
            baseZ: options.zIndex ? options.zIndex : 1000,
            css: {
                border: '0',
                padding: '0',
                backgroundColor: 'none'
            },
            overlayCSS: {
                backgroundColor: options.overlayColor ? options.overlayColor : '#555',
                opacity: options.boxed ? 0.05 : 0.1,
                cursor: 'wait'
            }
        });
    }
}

function customerHideLoading(target) {
    if (target) {
        $(target).unblock({
            onUnblock: function () {
                $(target).css('position', '');
                $(target).css('zoom', '');
            }
        });
    } else {
        $.unblockUI();
    }
}

//浏览器显示异常信息
function showException(data) { growlAlert.danger("操作请求失败，请稍后再试！"); }

//统计图表
var ChartsAmcharts = function () {
    var _initPieCharts = function (chart_id, chart_data) {
        AmCharts.makeChart(chart_id, {
            "type": "pie",
            "theme": "light",
            "color": '#777',
            "fontFamily": 'Open Sans',
            "dataProvider": chart_data,
            "valueField": "Value",
            "titleField": "Key"
        });
    }
    var _initSerialCharts = function (chart_id, chart_data) {
        AmCharts.makeChart(chart_id, {
            "type": "serial",
            "theme": "light",
            "color": '#777',
            "startDuration": 2,
            "fontFamily": 'Open Sans',
            "dataProvider": chart_data,
            "valueAxes": [{
                "minorGridAlpha": 0.08,
                "minorGridEnabled": true,
                "position": "left",
                "axisAlpha": 0
            }],
            "graphs": [{
                "balloonText": "[[category]]:[[value]]",
                "colorField": "color",
                "fillAlphas": 1,
                "type": "column",
                "valueField": "Value",
                "labelText": "[[value]]"
            }],
            "chartCursor": {
                "categoryBalloonEnabled": false,
                "cursorAlpha": 0,
                "zoomable": false
            },
            "categoryField": "Key",
            "categoryAxis": {
                "gridPosition": "start",
                "labelRotation": 15
            }
        });
    }

    var _initLineCharts = function (chart_id, chart_data, title) {
        AmCharts.makeChart(chart_id, {
            "type": "serial",
            "theme": "light",
            "fontFamily": 'Open Sans',
            "color": '#777',
            "startDuration": 2,
            "dataProvider": chart_data,
            "valueAxes": [{
                "axisAlpha": 0,
                "position": "left"
            }],
            "graphs": [{
                "balloonText": "[[title]]:[[value]]",
                "colorField": "color",
                "bullet": "round",
                "bulletBorderAlpha": 1,
                "bulletBorderThickness": 1,
                "dashLengthField": "dashLength",
                "legendValueText": "[[value]]",
                "title": title,
                "fillAlphas": 0.5,
                "type": "smoothedLine",
                "valueField": "Value"
            }],
            "chartCursor": {
                "valueLineBalloonEnabled": false,
                "valueLineAlpha": 0.5,
                "fullWidth": false,
                "cursorAlpha": 0.085,
                "zoomable": false
            },
            "categoryField": "Key",
            "categoryAxis": {
                "dateFormats": [{
                    "period": "DD",
                    "format": "DD"
                }, {
                    "period": "WW",
                    "format": "MMM DD"
                }, {
                    "period": "MM",
                    "format": "MMM"
                }, {
                    "period": "YYYY",
                    "format": "YYYY"
                }],
                "parseDates": true

            }
        });
    }
    var _initMultLineCharts = function (chart_id, chart_data, title1, title2, title3) {
        AmCharts.makeChart(chart_id, {
            "type": "serial",
            "theme": "light",
            "fontFamily": 'Open Sans',
            "color": '#777',
            "startDuration": 2,
            "legend": {
                "equalWidths": false,
                "useGraphSettings": true,
                "valueAlign": "left"
            },
            "dataProvider": chart_data,
            "valueAxes": [{
                "axisAlpha": 0,
                "position": "left"

            }],
            "graphs": [{
                "balloonText": "[[title]]:[[value]]",
                "bullet": "round",
                "bulletBorderAlpha": 1,
                "useLineColorForBulletBorder": true,
                "bulletColor": "#FFFFFF",
                "dashLengthField": "dashLength",
                "legendValueText": "[[value]]",
                "title": title1,
                "fillAlphas": 0,
                "valueField": "Value1"
            }, {
                "balloonText": "[[title]]:[[value]]",
                "bullet": "square",
                "bulletBorderAlpha": 1,
                "bulletBorderThickness": 1,
                "dashLengthField": "dashLength",
                "legendValueText": "[[value]]",
                "title": title2,
                "fillAlphas": 0,
                "valueField": "Value2"
            }, {
                "balloonText": "[[title]]:[[value]]",
                "bullet": "diamond",
                "bulletBorderAlpha": 1,
                "bulletBorderThickness": 1,
                "dashLengthField": "dashLength",
                "legendValueText": "[[value]]",
                "title": title3,
                "fillAlphas": 0,
                "valueField": "Value3"
            }],
            "chartCursor": {
                "cursorAlpha": 0.1,
                "cursorColor": "#000000",
                "valueBalloonsEnabled": false,
                "zoomable": false
            },
            "categoryField": "Key",
            "categoryAxis": {
                "dateFormats": [{
                    "period": "DD",
                    "format": "DD"
                }, {
                    "period": "WW",
                    "format": "MMM DD"
                }, {
                    "period": "MM",
                    "format": "MMM"
                }, {
                    "period": "YYYY",
                    "format": "YYYY"
                }],
                "parseDates": true,
                "axisColor": "#777",
                "gridAlpha": 0.5,
                "gridColor": "#fff",
                "gridCount": 50
            }
        });
    }
    var _initDoubleLineCharts = function (chart_id, chart_data, title1, title2) {
        AmCharts.makeChart(chart_id, {
            "type": "serial",
            "theme": "light",
            "fontFamily": 'Open Sans',
            "color": '#777',
            "startDuration": 2,
            "legend": {
                "equalWidths": true,
                "useGraphSettings": true,
                "valueAlign": "left"
            },
            "dataProvider": chart_data,
            "valueAxes": [{
                "axisAlpha": 0,
                "position": "left"

            }],
            "graphs": [{
                "balloonText": "[[title]]:[[value]]",
                "bullet": "round",
                "bulletBorderAlpha": 1,
                "useLineColorForBulletBorder": true,
                "bulletColor": "#FFFFFF",
                "dashLengthField": "dashLength",
                "legendValueText": "[[value]]",
                "title": title1,
                "fillAlphas": 0,
                "valueField": "Value1"
            }, {
                "balloonText": "[[title]]:[[value]]",
                "bullet": "square",
                "bulletBorderAlpha": 1,
                "bulletBorderThickness": 1,
                "dashLengthField": "dashLength",
                "legendValueText": "[[value]]",
                "title": title2,
                "fillAlphas": 0,
                "valueField": "Value2"
            }],
            "chartCursor": {
                "cursorAlpha": 0.1,
                "cursorColor": "#000000",
                "valueBalloonsEnabled": false,
                "zoomable": false
            },
            "categoryField": "Key",
            "categoryAxis": {
                "dateFormats": [{
                    "period": "DD",
                    "format": "DD"
                }, {
                    "period": "WW",
                    "format": "MMM DD"
                }, {
                    "period": "MM",
                    "format": "MMM"
                }, {
                    "period": "YYYY",
                    "format": "YYYY"
                }],
                "parseDates": true,
                "axisColor": "#777",
                "gridAlpha": 0,
                "gridColor": "#fff",
                "gridCount": 50
            }
        });
    }


    var _initMultSerialCharts = function (chart_id, chart_data, title1, title2, title3) {
        AmCharts.makeChart(chart_id, {
            "type": "serial",
            "theme": "light",
            "fontFamily": 'Open Sans',
            "color": '#555',
            "startDuration": 2,
            "legend": {
                "equalWidths": false,
                "useGraphSettings": true,
                "valueAlign": "left"
            },
            "dataProvider": chart_data,
            "valueAxes": [{
                "stackType": "regular",
                "axisAlpha": 0,
                "position": "left"

            }],
            "graphs": [{
                "balloonText": "[[title]]:[[value]]",
                "colorField": "color",
                "lineAlpha": 0,
                "fillAlphas": 0.8,
                "type": "column",
                "dashLengthField": "dashLength",
                "legendValueText": "[[value]]",
                "title": title1,
                "valueField": "Value1"
            }, {
                "balloonText": "[[title]]:[[value]]",
                "colorField": "color",
                "lineAlpha": 0,
                "fillAlphas": 0.8,
                "type": "column",
                "dashLengthField": "dashLength",
                "legendValueText": "[[value]]",
                "title": title2,
                "valueField": "Value2"
            }, {
                "balloonText": "[[title]]:[[value]]",
                "colorField": "color",
                "fillAlphas": 0.8,
                "type": "column",
                "dashLengthField": "dashLength",
                "legendValueText": "[[value]]",
                "title": title3,
                "fillAlphas": 1,
                "valueField": "Value3"
            }],
            "chartCursor": {
                "cursorAlpha": 0.1,
                "cursorColor": "#000000",
                "valueBalloonsEnabled": true,
                "zoomable": false
            },
            "categoryField": "Key",
            "categoryAxis": {
                "axisColor": "#777",
                "gridAlpha": 0.5,
                "gridColor": "#fff",
                "gridCount": 50
            }
        });
    }
    return {

        //页面初始化方法
        AmPieChart: function (chart_id, chart_data) {
            _initPieCharts(chart_id, chart_data)
        },
        AmSerialChart: function (chart_id, chart_data) {
            _initSerialCharts(chart_id, chart_data)
        }, AmMultLineChart: function (chart_id, chart_data, title1, title2, title3) {
            _initMultLineCharts(chart_id, chart_data, title1, title2, title3)
        }, AmMultSerialChart: function (chart_id, chart_data, title1, title2, title3) {
            _initMultSerialCharts(chart_id, chart_data, title1, title2, title3)
        }, AmLineChart: function (chart_id, chart_data, title) {
            _initLineCharts(chart_id, chart_data, title)
        }, AmDoubleLineChart: function (chart_id, chart_data, title1, title2) {
            _initDoubleLineCharts(chart_id, chart_data, title1, title2)
        }
    };
}();

//上下调节按钮
function initTouchSpin(inputId, maxvalue) {
    if (!maxvalue) {
        maxvalue = 1;
    }
    $(inputId).keyup(function () {
        this.value = this.value.replace(/[^\d]/g, '');
    });

    $(inputId).TouchSpin({
        buttondown_class: 'btn green',
        buttonup_class: 'btn green',
        min: 1,
        max: maxvalue,
        step: 1
    });
}

//modal加载过渡动画
function modalLoadding(wrapId, title, height) {
    var header = '<div class="modal-header"><button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button><h4 class="modal-title">' + title + '</h4></div>';
    var html = '<div class="esh-modal-loadding" style="height:' + height + 'px"><div class="loading-message"><div class="block-spinner-bar"><div class="bounce1"></div><div class="bounce2"></div><div class="bounce3"></div></div></div></div>';
    $(wrapId + " .modal-content").html(header + html)
}

