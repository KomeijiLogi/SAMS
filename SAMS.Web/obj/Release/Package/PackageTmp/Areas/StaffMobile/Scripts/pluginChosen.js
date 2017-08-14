
$(document).ready(function () {

    
    var list = document.getElementById('list');
    var done = document.getElementById('done');
    mui.init();
    var header = document.querySelector('header.mui-bar');

    list.style.height = (document.body.offsetHeight - header.offsetHeight) + 'px';
    window.indexedList = new mui.IndexedList(list);
        
    done.addEventListener('tap', function () {
        var checkboxArray = [].slice.call(list.querySelectorAll('input[type="checkbox"]'));
        var checkedValues = [];
        checkboxArray.forEach(function (box) {
            if (box.checked) {
                checkedValues.push(box.parentNode.innerText);
            }
        });
        if (checkedValues.length > 0) {
            // mui.alert('你选择了: ' + checkedValues);
            confirmChosen();
        } else {
            // mui.alert('你没选择任何备件');
            confirmChosen();
        }
    }, false);

    mui('.mui-indexed-list-inner').on('change', 'input', function () {
            var count = list.querySelectorAll('input[type="checkbox"]:checked').length;
            var value = count ? "完成(" + count + ")" : "完成";
            done.innerHTML = value;
            if (count) {
                if (done.classList.contains("mui-disabled")) {

                    done.classList.remove("mui-disabled");

                }

            } else {
                if (!done.classList.contains("mui-disabled")) {
                    done.classList.add("mui-disabled");
                }
            }
     });

    checkedOrNot();

});


var pluginArray = new Array();
var pluginFlag = 0;

//将选择的备件暂存到localstorage中，等到确认回执单时再释放
function setPluginStorage(pluginItem, context) {


    var value = context.checked ? "true" : "false";

    if (value == "true") {


        pluginArray[pluginFlag] = pluginItem;
        pluginFlag++;

        var pluginString = JSON.stringify(ClearNullArr(pluginArray.unique()));

        localStorage.setItem("plugin", pluginString);
        //toast("添加成功！");


    } else if (value == "false") {



        //遍历pluginArray,如果有对应的pluginItem就删除
        for (var i = 0; i < pluginArray.length; i++) {

            if (pluginArray[i] == pluginItem) {

                removeByValue(pluginArray, pluginItem);

                localStorage.setItem("plugin", JSON.stringify(ClearNullArr(pluginArray.unique())));
            }



        }
        if (pluginArray.length == 0) {

            localStorage.removeItem("plugin");
        }



    } else {


    }


}

//检测备件是否被选择，选择过的话，就将checkbox选中
function checkedOrNot() {

    if (JSON.stringify(localStorage.getItem("plugin"))!=null) {
    
        var searchPluginNameContext = JSON.stringify(localStorage.getItem("plugin"));


        $("input[name=pluginNameChecked]").each(function () {
          

            if (searchPluginNameContext.includes($(this).parent().text().replace(/(^\s*)|(\s*$)/g, ""))) {

                $(this).attr("checked", true);
                var pluginItem = $(this).parent().text().replace(/(^\s*)|(\s*$)/g, "");
                pluginArray[pluginFlag] = pluginItem;
                pluginFlag++;
            }
        });
    }

    var pluginString = JSON.stringify(ClearNullArr(pluginArray.unique()));

    localStorage.setItem("plugin", pluginString);
}






//toast弹出信息
function toast(context) {

    var msg = document.createElement('div');
    msg.innerHTML = context;//"未找到对应备件！"
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

//判断返回JSON的长度
function getJSONLength(jsonData) {
    var jsonLength = 0;
    for (var item in jsonData) {
        jsonLength++;
    }
    return jsonLength;
}


//跳转方法    
function confirmChosen() {
    window.location.href = "/Home/Receipt"
}

//根据val删除数组中的特定的某一项
function removeByValue(arr, val) {
    for (var i = 0; i < arr.length; i++) {
        if (arr[i] == val) {
            arr.splice(i, 1);
            break;
        }
    }

}

//去重方法
Array.prototype.unique = function () {
    var res = [];
    var json = {};
    for (var i = 0; i < this.length; i++) {
        if (!json[this[i]]) {
            res.push(this[i]);
            json[this[i]] = 1;
        }
    }
    return res;
}
//去空方法
function ClearNullArr(arr) {
    for (var i = 0, len = arr.length; i < len; i++) {
        if (!arr[i] || arr[i] == '' || arr[i] === undefined) {
            arr.splice(i, 1);
            len--;
            i--;
        }
    }
    return arr;
}

