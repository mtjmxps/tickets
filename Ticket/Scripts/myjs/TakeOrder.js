//已接单查询：查询按钮
function cklists() {
    var vnum = $("#odnum").val();
    $.ajax({
        type: "post",
        url: "/TakeOrder/JSOrderLists",
        datatype: "json",
        data: { num: vnum },
        success: function (data) {
            $("#ddmx2").children().remove();
            $("#ckxx2 tr td").parent().remove();
            $("#ddmx2").append('<tr><th class="w200">订单号</th><th class="w100">车次</th><th class="w150">出发日期</th><th class="w100">出发站</th>'
                + '<th class="w100">到达站</th><th class="w100">座位</th><th class="w100">单价</th><th class="w100">人数</th><th class="w100">费用</th>'
                + '<th class="w150">总价</th><th class="w100">下单时间</th><th class="w100">状态</th><th class="w60"></th></tr>');
            for (var i = 0; i < data.length; i++) {
                $("#ddmx2").append('<tr onclick="sps2(this)"><td class="w200">' + data[i].Sn + '</td><td class="w100">' + data[i].Ptrain + '</td><td class="w150">' + data[i].Pdate
                    + '</td><td class="w100">' + data[i].Pfrom + '</td><td class="w100">' + data[i].Pto + '</td><td class="w100">' + data[i].Pseat
                    + '</td><td class="w100">' + data[i].Price + '</td><td class="w100">' + data[i].Passager + '</td><td class="w100">' + data[i].Tip
                    + '</td><td class="w150">' + data[i].Total + '</td><td class="w100">' + jsonDateFormat(data[i].Cdate) + '</td><td class="w100">' + data[i].billstate.Name + '</td><td><input id="bjd" type="button" value="完结" onclick="ttk2(this)" /><input id="bjd" type="button" value="取消" onclick="ttk3(this)" /></td></tr>');
            }
            //$("#ckxx").append('<tr><th class="w100">序号</th><th class="w100">姓名</th><th class="w150">证件类型</th><th class="w200">证件号</th>'
            //    + '<th class="w100">出生日期</th><th class="w100">状态</th></tr>')
        },
        error: function (msg) {
            alert("ajax连接异常：" + msg);
        }
    })
}
//定时刷新订单
function timecheck() {
    var array = new Array();
    var trlist = $("#ddmx tr");
    //先判断是否存在值，再进行赋值，不然jq会报错null没有定义
    if ($("#ddmx tr td").length > 0) {
        for (var i = 1; i < trlist.length; i++) {
            array.push(trlist.eq(i).children("td:first").text());
        }
    }
    $.ajax({
        type: "get",
        url: "/TakeOrder/JSOrderListsByay",
        traditional: true,
        data: { "ay": array },
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                if (data[i].State != 1)
                    ($('#ddmx tr td:contains("' + data[i].Sn + '")').parent().remove());
                if (data[i].State == 10)
                    $("#ddmx").append('<tr onclick="sps(this)"><td class="w200">' + data[i].Sn + '</td><td class="w100">' + data[i].Ptrain + '</td><td class="w150">' + data[i].Pdate
                    + '</td><td class="w100">' + data[i].Pfrom + '</td><td class="w100">' + data[i].Pto + '</td><td class="w100">' + data[i].Pseat
                    + '</td><td class="w100">' + data[i].Price + '</td><td class="w100">' + data[i].Passager + '</td><td class="w100">' + data[i].Tip
                    + '</td><td class="w150">' + data[i].Total + '</td><td class="w100">' + jsonDateFormat(data[i].Cdate) + '</td><td class="w100">' + data[i].billstate.Name + '</td><td class="w60"><input type="button" value="接单" onclick="ttk(this)" /></td></tr>');
            }
        }
    })
}
timer=window.setInterval(timecheck, 2000); //每秒执行一次

//接单操作
function ttk(e) {
    //获取父级tr元素
    var vtr = $(e).parent().parent()
    //移除父级元素的事件    
    vtr.removeAttr("onclick");
    //如果点击按钮的行是被选中的行，则把乘客信息也删除
    if (vtr.attr("class") == "s_tr")
        $("#ckxx tr td").parent().remove();
    //获取所点击按钮所在行第一列的文本数据
    var vnum = $(e).parent().parent().find("td").eq(0).text();
    //删除所点击按钮所在行
    $(e).parent().parent().remove();
    
    //更改所点击单据未接单状态
    $.ajax({
        type: "post",
        url: "/TakeOrder/Stateto1",
        data: { num: vnum },
        success: function (data) {
            if (data > 0)
                //更新接单列表
                cklists();
        }
    });    
}
//已接单列表：点击订单列表显示乘客信息
function sps2(e) {
    var vnum = $(e).find("td").eq(0).text();
    $(e).addClass("s_tr")
   .siblings().removeClass('s_tr')//去除其他项的高亮形式
   .end();
    //alert(vnum);
    $.ajax({
        type: "post",
        url: "/Order/JSOdPassagers",
        datatype: "json",
        data: { num: vnum },
        success: function (data) {
            $("#ckxx2").children().remove();
            $("#ckxx2").append('<tr><th class="w100">序号</th><th class="w100">姓名</th><th class="w150">证件类型</th><th class="w200">证件号</th>'
                + '<th class="w100">出生日期</th><th class="w100">状态</th></tr>')
            for (var i = 0; i < data.length; i++) {
                $("#ckxx2").append('<tr><td class="w100">' + data[i].Psn + '</td><td class="w100">' + data[i].Name + '</td><td class="w150">' + data[i].Card
                    + '</td><td class="w200">' + data[i].Cnum + '</td><td class="w100">' + data[i].Bday + '</td><td class="w100">' + data[i].billstate.Name + '</td></tr>');
            }
        },
        error: function (msg) {
            alert("ajax连接异常：" + msg);
        }
    })
}
//测试
//function ttk2(e) {
//    event.stopPropagation();
//}
//接单列表点击完成操作
function ttk2(e) {
    //阻止事件冒泡：点击按钮，防止点击事件传递到按钮的父级元素tr触发它的点击事件
    event.stopPropagation();
    if (confirm("是否需要完结单据？")) {
        //获取父级tr元素
        var vtr = $(e).parent().parent()
        ////移除父级元素的事件    
        //vtr.removeAttr("onclick");
        //如果点击按钮的行是被选中的行，则把乘客信息也删除
        if (vtr.attr("class") == "s_tr")
            $("#ckxx2 tr td").parent().remove();
        //获取所点击按钮所在行第一列的文本数据
        var vnum = $(e).parent().parent().find("td").eq(0).text();
        //删除所点击按钮所在行
        $(e).parent().parent().remove();

        //更改所点击单据未接单状态
        $.ajax({
            type: "post",
            url: "/TakeOrder/Stateto3",
            data: { num: vnum },
            success: function (data) {
                if (data > 0)
                    alert(vnum + "单据完结成功");
                //更新接单列表
                //cklists();
            }
        });
    }
}