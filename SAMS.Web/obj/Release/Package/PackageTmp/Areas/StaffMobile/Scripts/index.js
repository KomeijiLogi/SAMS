
//设置iframe高度自适应
function setAutoHeight(height) {

    var iframe = document.getElementById("index_content");
    if (iframe) {
        var iframeWin = iframe.contentWindow || iframe.contentDocument.parentWindow;

        if (iframeWin.document.body) {
            //iframe.height = iframeWin.document.documentElement.scrollHeight || iframeWin.document.body.scrollHeight;
            //iframe.height = iframe.height+0 ;
            //console.log(iframe.height);

            iframe.height = height;
            //console.log("height:" + height + " height-45:" + (height - 45));

        }
    }
}


window.onload = function () {

    mui.init();
    //var pageNum = ['home.html', 'message.html', 'assisstant.html', 'mine.html'];
    var frame = document.getElementById("index_content");
    var frameHeight = document.documentElement.clientHeight - 50;
    frame.setAttribute("style", "border:0px;min-height:" + frameHeight + "px;" + "width:" + document.documentElement.clientWidth + "px");

    
    //添加判断，如果是PC端调用onclick事件，如果是移动端调用ontouchstart事件
  
   

    //选项卡点击更新iframe事件
    $("#tab_undo").on('tap', function () {


        $("#index_content").attr("src", "/StaffMobile/Home/UndoOrder");

    });


    $("#tab_done").on('tap', function () {


        $("#index_content").attr("src", "/StaffMobile/Home/DoneOrder");

    });
    $("#tab_accessory").on('tap', function () {


        $("#index_content").attr("src", "/StaffMobile/Home/Accessory");

    });
    $("#tab_commit").on('tap', function () {


        $("#index_content").attr("src", "/StaffMobile/Home/AskOrder");
    });
    
}

//判断是否是PC
function isPC() {

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
        console.log("pc");
    } else {
        console.log("app");
    }
    return flag;
}
