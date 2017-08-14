function showPopWindow() {
    mui('.mui-popover').popover('toggle', document.getElementById("menubars"));
}


//处理JSON数据
function dealJSON() {
    var workOrderNo, workOrderProduct, workOrderProject, workOrderTime, workOrderServer,
        customName, customPeople, customPhone, customAddress, dataValue;
    var el, ul, li, div;
    el = document.getElementById('thedetaillist');
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "../JSON/detailDataTest.json",
        success: function (data) {
            var json = data;
            var dataValue = eval('json.' + 'dataValue');
            $(dataValue).each(function (index, jsonObj) {
                jsonObj = dataValue[index];

                //获取value值
                workOrderNo = eval('jsonObj.' + 'workOrderNo');
                workOrderProduct = eval('jsonObj.' + 'workOrderProduct');
                workOrderProject = eval('jsonObj.' + 'workOrderProject');
                workOrderServer = eval('jsonObj.' + 'workOrderServer');
                workOrderTime = eval('jsonObj.' + 'workOrderTime');
                customName = eval('jsonObj.' + 'customName');
                customPeople = eval('jsonObj.' + 'customPeople');
                customPhone = eval('jsonObj.' + 'customPhone');
                customAddress = eval('jsonObj.' + 'customAddress');
                //console.log('success');



            })

        },
        error: function () {

        }
    })

}


window.onload = function () {
    mui.init();
    var userAgentInfo = navigator.userAgent;
    var Agents = ["Android", "iPhone",
                "SymbianOS", "Windows Phone",
                "iPad", "iPod"];
    var flag = true;
    for (var v = 0; v < Agents.length; v++) {
        if (userAgentInfo.indexOf(Agents[v]) > 0) {
            flag = false;
            break;
        }
    }
    if (flag) {
        //如果是PC端，则将显示detailHeader
        document.getElementById('detailHeader').style.display = 'block';
    } else {
        //如果是移动端，则将掩藏detailHeader
        document.getElementById('detailHeader').style.display = 'none';
    }
    dealJSON();
}


function btConfirmEvent() {

    window.location.href = "/Home/Receipt";
}