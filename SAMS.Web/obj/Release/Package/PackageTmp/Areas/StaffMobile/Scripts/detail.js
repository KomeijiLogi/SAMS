function showPopWindow() {
    mui('.mui-popover').popover('toggle', document.getElementById("menubars"));
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
    //if (flag) {
    //    //如果是PC端，则将显示detailHeader
    //    document.getElementById('detailHeader').style.display = 'block';
    //} else {
    //    //如果是移动端，则将掩藏detailHeader
    //    document.getElementById('detailHeader').style.display = 'none';
    //}

}


