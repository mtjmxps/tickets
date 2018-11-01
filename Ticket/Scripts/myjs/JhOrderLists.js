//查询客户单个聚合订单信息
function jhcklist() {
    var vnum = $("#odnum").val();
    $.ajax({
        type: "post",
        url: "/Order/JhOrderListsSk",
        datatype: "json",
        data: { num: vnum },
        success: function (data) {
            $("#ddmx").children().remove();
            $("#ddmx").append('<tr><th class="w200">订单号</th><th class="w100">车次</th><th class="w150">出发日期</th><th class="w100">出发站</th>'
                + '<th class="w100">到达站</th><th class="w100">座位</th><th class="w100">单价</th><th class="w100">人数</th><th class="w100">费用</th>'
                + '<th class="w150">总价</th><th class="w100">下单时间</th><th class="w100">状态</th><th class="w60"></th></tr>');
            $("#ckxx").children().remove();
            $("#ckxx").append('<tr><th class="w60">序号</th><th class="w100">姓名</th><th class="w150">证件类型</th><th class="w200">证件号</th>'
                + '<th class="w150">票号</th><th class="w100">座位</th><th class="w100">状态</th></tr>')
            for (var i = 0; i < data.length; i++) {
                $("#ddmx").append('<tr onclick="jhsps(this)"><td class="w200">' + data[i].Sn + '</td><td class="w100">' + data[i].Ptrain + '</td><td class="w150">' + data[i].Pdate
                    + '</td><td class="w100">' + data[i].Pfrom + '</td><td class="w100">' + data[i].Pto + '</td><td class="w100">' + data[i].Pseat
                    + '</td><td class="w100">' + data[i].Price + '</td><td class="w100">' + data[i].Passager + '</td><td class="w100">' + data[i].Tip
                    + '</td><td class="w150">' + data[i].Total + '</td><td class="w100">' + jsonDateFormat(data[i].Cdate) + '</td><td class="w100">' + data[i].billstate.Name + '</td><td class="w60"><input type="button" value="查看状态" onclick="jhttk(this)" /></td></tr>');
            }
        },
        error: function (msg) {
            alert("ajax连接异常：" + msg);
        }
    })
}//按查询再次进入付款界面
function jhttk(e) {
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
        url: "/Order/JhOrderStateSk",
        data: { num: vnum },
        success: function (data) {
            if (data == "true")
                window.location = "/Order/JhOrderPay";
        }
    });    
}
//点击订单列表显示乘客信息
function jhsps(e) {
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
            $("#ckxx").children().remove();
            $("#ckxx").append('<tr><th class="w60">序号</th><th class="w100">姓名</th><th class="w150">证件类型</th><th class="w200">证件号</th>'
                + '<th class="w150">票号</th><th class="w100">座位</th><th class="w100">状态</th><th class="w60"></th></tr>')
            for (var i = 0; i < data.length; i++) {
                $("#ckxx").append('<tr><td class="w60">' + data[i].Psn + '</td><td class="w100">' + data[i].Name + '</td><td class="w150">' + data[i].Card
                    + '</td><td class="w200">' + data[i].Cnum + '</td><td class="w150">' + data[i].Ticket_no + '</td><td class="w150">' + data[i].Cxin
                    + '</td><td class="w100">' + data[i].billstate.Name + '</td><td class="w60"><input type="button" value="退票" onclick="jhtp(this)" /></td></tr>');
            }
        },
        error: function (msg) {
            alert("ajax连接异常：" + msg);
        }
    })
}