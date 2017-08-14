

$(document).ready(function () {
    
    mui.init();
    if (localStorage != null) {
        try {

            getPluginStorge();

            getReasonStorage();

        } catch (e) {


        }
           
       
    } else {
        //console.log('localstorage is empty');

    }

    var encodeArray = JSON.parse(localStorage.getItem("encode"));
    var encodeFilled;
    if (encodeArray != null) {
        encodeFilled = encodeArray[encodeArray.length-1];              //始终获取最后一个，保持最新

        $("#equipEncode").val(encodeFilled);
    }


   
    mui('.mui-scroll-wrapper').scroll();
    mui('body').on('shown', '.mui-popover', function (e) {
        //console.log('shown', e.detail.id);//detail为当前popover元素
       
        
        checkedOrNot();

    });
    mui('body').on('hidden', '.mui-popover', function (e) {
        //console.log('hidden', e.detail.id);//detail为当前popover元素
        
    });
    
    
    mui('.mui-numbox').numbox();
    

   
})

//从localstorage中加载备件相关信息
function getPluginStorge() {

    var ul, li_pluginName, span, div_parent, li_pluginNumber,li_pluginEncode,div_numberbox,bt_minus,bt_add;
    
    var input_pluginNu,input_encode;

    var div_list = document.getElementById('pluginList');
    
    var pluginNameArray = JSON.parse(localStorage.getItem("plugin"));
        
    var pluginName;

        if (pluginNameArray != null) {

            for (var i = 0; i < pluginNameArray.length; i++) {

                pluginName = pluginNameArray[i];

                //生成组件部分
                div_parent = document.createElement('div');
                div_parent.setAttribute("id", "pluginListPa");
                div_parent.setAttribute("class","mui-card")
                //删除组件生成
                span = document.createElement('span');
                span.setAttribute("id", "deletePlugin");
                span.setAttribute("class", "pluginSpan mui-icon mui-icon-close mui-pull-right");
                span.setAttribute("onclick", "javascript:deletePluginStorage(this)");
                li_pluginName = document.createElement('li');
                li_pluginName.setAttribute("class", "mui-table-view-cell");

                li_pluginNumber = document.createElement('li');
                li_pluginNumber.setAttribute("class", "mui-table-view-cell ");

                

                ul = document.createElement('ul');
                ul.setAttribute("class", "mui-table-view");
               


                li_pluginName.innerHTML = "备件名称&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + pluginName;
                li_pluginNumber.innerHTML = "备件数量&nbsp;";
               


                //数字增减控件
                bt_minus = document.createElement('button');
                bt_minus.setAttribute("class", "mui-btn mui-btn-numbox-minus");
                bt_minus.setAttribute("type", "button"); 
                bt_minus.innerHTML = "-";

                bt_add = document.createElement('button');
                bt_add.setAttribute("class", "mui-btn mui-btn-numbox-plus");
                bt_add.setAttribute("type", "button");           
                bt_add.innerHTML = "+";

                input_pluginNu = document.createElement('input');
                input_pluginNu.setAttribute("type", "number");
                input_pluginNu.setAttribute("class", "input_pluginCount mui-input-numbox");
                input_pluginNu.setAttribute("id", "input_pluginNu");
                input_pluginNu.setAttribute("onchange", "javascript:getInputToStorage(0,this)");
                input_pluginNu.setAttribute("onkeyup", "javascript:positiveInt(this)");
                        

                div_numberbox = document.createElement('div');
                div_numberbox.setAttribute("class", "colorfulCount mui-numbox");
                div_numberbox.setAttribute("id", "numboxDiv" + i);
                div_numberbox.setAttribute("data-numbox-min", "0");                  //设置最小值
                div_numberbox.setAttribute("data-numbox-max", "999");                //设置最大值  


                
                //添加控件到父容器中
                li_pluginName.insertBefore(span, li_pluginName.lastChild);
                ul.insertBefore(li_pluginName, ul.firstChild);

                div_numberbox.insertBefore(bt_minus, div_numberbox.childNodes[0]);
                div_numberbox.insertBefore(input_pluginNu, div_numberbox.childNodes[1]);
                div_numberbox.insertBefore(bt_add, div_numberbox.childNodes[2]);
              
                li_pluginNumber.appendChild(div_numberbox);
                ul.insertBefore(li_pluginNumber, ul.childNodes[1]);
                
                
                div_parent.insertBefore(ul, div_parent.firstChild);
                div_list.insertBefore(div_parent, div_list.lastChild);

             

            }

        }
     
}

var inputCountArray = new Array();
var inputEncodeArray = new Array();
var countStructs = new DictionarySelf();
var countStructsTemp = new DictionarySelf();
var countFlag = 0;
var encodeFlag = 0;
//将设备编码保存到localstorage中的函数
function getInputToStorage(flag,context) {

    var inputValue = $(context).val();
    
    
    if (flag == 0) {

       //保存备件数量
        

        //{"xx":[{"c1":"1"},{"c2":"2"}]}<=目标<=获取作为value的数组<=获取每一对键值对{pluginNameArray[i],inputValue}<=将对应的键和值组合起来<=遍历plugin名称数组
        //<=找寻包含plugin名称数组的项

        var pluginNameArray = JSON.parse(localStorage.getItem("plugin"));
        for (var i = 0; i < pluginNameArray.length;i++){
         
            if($(context).parent().parent().parent().text().includes(pluginNameArray[i])){
            
                countStructsTemp.put(pluginNameArray[i], inputValue);
            }
           
        }
        
       
        countStructs.put("pluginMsg", countStructsTemp);
        //console.log(countStructs.get("pluginMsg"));
        
        
        

    } else if (flag == 1) {

       //保存设备编码
        inputEncodeArray[encodeFlag] = inputValue;
        encodeFlag++;
        localStorage.setItem("encode",JSON.stringify(inputEncodeArray));

       
    } else {



    }

}


var reasonArray = new Array();
var reasonFlag = 0;
//选择列表根据是否选中的状态来调用不同的函数实现reason列表的创建和删除
function setReasonStorage(reasonItem, context) {

    if (context.checked == true) {

        //选中状态

        //需要判断组件是否已存在，如果没有就创建组件
        
        

        if ($("#reasonDiv").length <= 0 || !$("#reasonList").text().includes(reasonItem)) {
           
            addReason(context);    //生成组件
            
        }

        mui('.mui-popover').popover('hide');

        reasonArray[reasonFlag] = reasonItem;
        reasonFlag++;


        var reasonString = JSON.stringify(ClearNullArr(reasonArray.unique()));


        localStorage.setItem("reason", reasonString);
        
    } else if (context.checked == false) {

        //取消选中
        
        var reasonArrayUnique = new Array();
        reasonArrayUnique = reasonArray.unique();
        //遍历reasonArray,如果有对应的reasonItem就删除
        for (var i = 0; i < reasonArrayUnique.length; i++) {

            if ((reasonArrayUnique)[i] == reasonItem) {


                var reasonArrayReceipt = removeByValue(ClearNullArr(reasonArrayUnique), reasonItem);

                var reasonString = JSON.stringify(reasonArrayReceipt);

                localStorage.setItem("reason", reasonString);

                reasonArray = reasonArrayReceipt;

                if ($("#reasonList").text().replace(/(^\s*)|(\s*$)/g, "").replace(/\s/g, "").includes(reasonItem)) {

                    reasonItem = reasonItem.replace(/(^\s*)|(\s*$)/g, "").replace(/\s/g, "");
                    $("#reasonDiv:contains('" + reasonItem + "')").remove();
                }

                break;
            }



        }
        if (reasonArray.unique().length == 0) {

            localStorage.removeItem("reason");
        }



        mui('.mui-popover').popover('hide');

    } else {


    }

   


}

//遍历localstorage，让对应的checkbox选中
function checkedOrNot() {
   

    if (JSON.stringify(localStorage.getItem("reason")) != null) {

       
        var searchReasonNameContext = JSON.stringify(localStorage.getItem("reason"));
        

        $("input[name=reasonNameChecked]").each(function () {


            if (searchReasonNameContext.includes($(this).parent().text().replace(/(^\s*)|(\s*$)/g, ""))) {

                $(this).attr("checked", true);

                var reasonItem = $(this).parent().text().replace(/(^\s*)|(\s*$)/g, "").replace(/\s/g, "");
                reasonArray[reasonFlag] = reasonItem;
                reasonFlag++;

            } else {

                $(this).attr("checked", false);
            }
        });
        //var reasonString = JSON.stringify(ClearNullArr(reasonArray.unique()));

        //localStorage.setItem("reason", reasonString);
    }

    
    
}

//在页面中生成对应的reason项
function addReason(context) {


    var reasonText = $(context).parent().text();

    var reasonCreateString = JSON.stringify(localStorage.getItem("reason"));


    var div_parent, span, li, div_li;


    div_li = document.getElementById("reasonList");
    div_parent = document.createElement('div');
    div_parent.setAttribute("id", "reasonDiv");


    span = document.createElement('span');
    span.setAttribute("id", "deleteReason");
    span.setAttribute("class", "reasonSpan mui-icon mui-icon-close mui-pull-right");
    span.setAttribute("onclick", "javascript:deleteReason(this)");
    li = document.createElement('li');
    li.setAttribute("class", "mui-table-view-cell");
    li.innerHTML = "" + reasonText;


    li.insertBefore(span, li.firstChild);
    div_parent.insertBefore(li, div_parent.firstChild);
    div_li.insertBefore(div_parent, div_li.firstChild);


}



//点击确认时调用函数，将保存的信息发送出去
function receiptComplete() {

    history.go(0);
    
    //需要添加检验localstorage中对应内容是否存在的方法

    try {
        //var pluginData = JSON.parse(localStorage.getItem("plugin"));
        var reasonData = JSON.parse(localStorage.getItem("reason"));
        var encodeData = JSON.parse(localStorage.getItem("encode"));
        //var countData = JSON.parse(localStorage.getItem("count"));
        var pluginData = countStructs.get("pluginMsg");
        
        var receiptData = {
            encodeData,
            reasonData,
            pluginData
        }

    } catch (err) {

        console.log(err);
    } finally {

        //发送数据

        $.ajax({
            type: "GET",
            dataType: "",
            data: receiptData,
            url: "",
            sucess: function (data) {


            }, error: function () {


            }
        });

    }
    
    if (localStorage != null) {
        localStorage.clear();
    }
    
    
}

//删除备件项
function deletePluginStorage(context) {


    //通过key获取到数组，然后通过value删除数组中的某一项，然后将新的内容重新保存到localstorage中

    var pluginValue = JSON.parse(localStorage.getItem("plugin"));
   
    var deletePluginValue = $(context).parent().text().replace("备件名称","");
    deletePluginValue = deletePluginValue.replace(/(^\s*)|(\s*$)/g, "");               //删除空格

    var  pluginValueReceipt = removeByValue(ClearNullArr( pluginValue.unique()), deletePluginValue);


    localStorage.setItem("plugin",JSON.stringify(pluginValueReceipt));

   
    $(context).parent().parent().fadeToggle("slow");                    //组件淡出效果

    
}



//删除原因项
function deleteReason(context) {

    
    var reasonValue = JSON.parse(localStorage.getItem("reason"));

    var deleteReasonValue = $(context).parent().text().replace(/(^\s*)|(\s*$)/g, "");
    console.log(deleteReasonValue);

    var reasonValueReceipt = removeByValue(ClearNullArr(reasonValue.unique()), deleteReasonValue);
    reasonValueReceipt = JSON.stringify(reasonValueReceipt);
    

    localStorage.setItem("reason", reasonValueReceipt);



    $(context).parent().fadeOut("slow");                           //淡出效果

  
}






//从localstorage中获取故障原因信息，生成相关项
function getReasonStorage() {
       
    
        var reasonItemArray = JSON.parse(localStorage.getItem("reason"));

        var reasonItem;

        if(reasonItemArray!=null){
        

            for (var i = 0; i < reasonItemArray.length; i++) {
                reasonItem = reasonItemArray[i];

                var div_parent, span, li, div_li;

                div_li = document.getElementById("reasonList");
                div_parent = document.createElement('div');
                div_parent.setAttribute("id", "reasonDiv");
                span = document.createElement('span');
                span.setAttribute("id", "deleteReason");
                span.setAttribute("class", "reasonSpan mui-icon mui-icon-close mui-pull-right");
                span.setAttribute("onclick", "javascript:deleteReason(this)");
                li = document.createElement('li');
                li.setAttribute("class", "mui-table-view-cell");
                li.innerHTML = "" + reasonItem;

                li.insertBefore(span, li.firstChild);
                div_parent.insertBefore(li, div_parent.firstChild);
                div_li.insertBefore(div_parent, div_li.firstChild);


            }

        }   
    
}



//toast弹出信息
function toast(context) {

    var msg = document.createElement('div');
    msg.innerHTML = context;//提示信息
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




//根据val删除数组中的特定的某一项
function removeByValue(arr, val) {
    for (var i = 0; i < arr.length; i++) {
        if (arr[i] == val) {
            arr.splice(i, 1);

            break;
        }
    }
    return arr;
}

//强制限制输入为非0整数
function positiveInt(obj) {

    //用正则表达式限制input只能输入数字
    obj.value = obj.value.replace(/[^0-9]/g, '');

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

function DictionarySelf() {

    this.data = new Array();
    //放置键值对数据
    this.put = function (key, value) {
        this.data[key] = value;

    };
    //根据key获取value
    this.get = function (key) {
        return this.data[key];
    };
    //删除某一对键值对
    this.remove = function (key) {
        this.data[key] = null;

    };
    //判断是否为空
    this.isEmpty = function () {
        return this.data.length == 0;
    };
    //获取长度大小
    this.size = function () {
        return this.data.length;
    };
    
}