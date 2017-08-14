
$(document).ready(function () {
    mui.init({

        pullRefresh: {
            container: "#wrapper",//下拉刷新容器标识，querySelector能定位的css选择器均可，比如：id、.class等
            down: {
                height: 50,//可选,默认50.触发下拉刷新拖动距离,
                auto: true,//可选,默认false.自动下拉刷新一次
                contentdown: "下拉可以刷新",//可选，在下拉可刷新状态时，下拉刷新控件上显示的标题内容
                contentover: "释放立即刷新",//可选，在释放可刷新状态时，下拉刷新控件上显示的标题内容
                contentrefresh: "正在刷新...",//可选，正在刷新状态时，下拉刷新控件上显示的标题内容
                callback: pullfresh_function_down //必选，刷新函数，根据具体业务来编写，比如通过ajax从服务器获取新数据；
            },
            up: {
                height: 50,//可选.默认50.触发上拉加载拖动距离
                auto: true,//可选,默认false.自动上拉加载一次
                contentrefresh: "正在加载...",//可选，正在加载状态时，上拉加载控件上显示的标题内容
                contentnomore: '没有更多数据了',//可选，请求完毕若没有更多数据时显示的提醒内容；
                callback: pullfresh_function_up //必选，刷新函数，根据具体业务来编写，比如通过ajax从服务器获取新数据；
            }
        }

    });

    mui('.mui-table-view').on('tap', '.mui-table-view-cell', function (e) {

        var id = $(this).find("a").attr("id");



        mui.openWindow({
            url: "/Home/Detail/" + id
        })



    })

});

function pullfresh_function_down() {
    //业务逻辑代码，比如通过ajax从服务器获取新数据；
    pullDownAction();
    //注意，加载完新数据后，必须执行如下代码，注意：若为ajax请求，则需将如下代码放置在处理完ajax响应数据之后
    mui('#wrapper').pullRefresh().endPulldownToRefresh();
}



function pullfresh_function_up() {
    //业务逻辑代码，比如通过ajax从服务器获取新数据；
    pullUpAction();
    //注意：
    //1、加载完新数据后，必须执行如下代码，true表示没有更多数据了：
    //2、若为ajax请求，则需将如下代码放置在处理完ajax响应数据之后
    this.endPullupToRefresh(true | false);
}





//下拉刷新动作加载数据
function pullDownAction() {




    var el, li, a, p, i;
    var itemContent, itemId, itemWorkOrder, itemWorkOrderUser, itemWorkOrderTime;
    el = document.getElementById('thelist');
    itemContent = new Array();

    $.ajax({
        type: "GET",
        url: "../JSON/itemListDataTest.json",
        dataType: "json",
        success: function (data) {

            var json = data;
            var itemContent = eval('json.' + 'itemContent');


            $(itemContent).each(function (index, jsonObj) {
                jsonObj = itemContent[index];

                //获取value值
                itemId = eval('jsonObj.' + 'itemId');
                itemWorkOrder = eval('jsonObj.' + 'itemWorkOrder');
                itemWorkOrderUser = eval('jsonObj.' + 'itemWorkOrderUser');
                itemWorkOrderTime = eval('jsonObj.' + 'itemWorkOrderTime');



                //创建组件
                li = document.createElement('li');
                li.setAttribute('class', 'mui-table-view-cell');
                a = document.createElement('a');
                a.setAttribute('class', 'mui-navigate-right');
                a.setAttribute('href', '/Home/Detail');
                //a.href = "@Url.Action(\"Detail\",\"Home\")";
                a.innerHTML = itemWorkOrder;
                p = document.createElement('p');
                p.innerHTML = itemWorkOrderUser + "&nbsp;" + itemWorkOrderTime;


                //将组件添加到页面上
                a.insertBefore(p, a.childNodes[1]);
                li.insertBefore(a, li.firstChild);
                el.insertBefore(li, el.lastChild);

            });

        }, error: function () {
            console.log('POST itemListDataTest error');
        }

    });





}
//上拉加载动作加载数据
function pullUpAction() {

    var el, li, i;
    el = document.getElementById('thelist');
    $.ajax({
        type: "GET",
        url: "../JSON/itemListDataTest.json",
        dataType: "json",
        success: function (data) {
            var json = data;
            var itemContent = eval('json.' + 'itemContent');
            //console.log(itemContent);

            $(itemContent).each(function (index, jsonObj) {
                jsonObj = itemContent[index];

                //获取value值
                itemId = eval('jsonObj.' + 'itemId');
                itemWorkOrder = eval('jsonObj.' + 'itemWorkOrder');
                itemWorkOrderUser = eval('jsonObj.' + 'itemWorkOrderUser');
                itemWorkOrderTime = eval('jsonObj.' + 'itemWorkOrderTime');


                //创建组件
                li = document.createElement('li');
                li.setAttribute('class', 'mui-table-view-cell');
                a = document.createElement('a');
                a.setAttribute('class', 'mui-navigate-right');
                a.setAttribute('href', '/Home/Detail');
                //a.href="@Url.Action(\"Detail\",\"Home\")";
                a.innerHTML = itemWorkOrder;
                p = document.createElement('p');
                p.innerHTML = itemWorkOrderUser + "&nbsp;" + itemWorkOrderTime;





                //将组件添加到页面上
                a.insertBefore(p, a.childNodes[1]);
                li.insertBefore(a, li.firstChild);
                el.insertBefore(li, el.lastChild);


            });
        }, error: function () {
            console.log('POST itemListDataTest error');
        }


    });




}




function checkBottom() {
    var itemListCount = $('li').size();
    if (itemListCount >= 30) {
        var el = document.getElementById('thelist');
        var div = document.createElement('div');
        var p = document.createElement('p');
        p.innerHTML = "到达底部";
        div.insertBefore(p, div.lastChild);
        el.insertBefore(div, el.lastChild);
        //console.log("到达底部");
        toast();
        return false;
    }
    return true;
}

function toast() {

    var msg = document.createElement('div');
    msg.innerHTML = "已经到达底部";
    msg.style.cssText = "width:60%; min-width:150px; background:#000; opacity:0.5; height:40px; color:#fff; line-height:40px; text-align:center; border-radius:5px; position:fixed; top:40%; left:20%; z-index:999999; font-weight:bold;";
    document.body.appendChild(msg);
    setTimeout(function () {
        var d = 0.5;
        msg.style.webkitTransform = '-webkit-transform ' + d + 's ease-in, opacity ' + d + 's ease-in';
        msg.style.opacity = '0';
        setTimeout(function () {
            document.body.removeChild(msg);
        }, d * 1000);
    }, 2000);

}

