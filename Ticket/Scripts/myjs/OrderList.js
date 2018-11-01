function cklist() {
    var vnum = $("#odnum").val();
    $.ajax({
        type: "post",
        url: "/Order/JSOrderLists",
        datatype: "json",
        data: { num:vnum },
        success: function (data) {
            $("#ddmx").children().remove();
            $("#ddmx").append('<tr><th class="w200">订单号</th><th class="w100">车次</th><th class="w150">出发日期</th><th class="w100">出发站</th>'
                + '<th class="w100">到达站</th><th class="w100">座位</th><th class="w100">单价</th><th class="w100">人数</th><th class="w100">费用</th>'
                + '<th class="w150">总价</th><th class="w100">下单时间</th><th class="w100">状态</th></tr>');
            $("#ckxx").children().remove();
            $("#ckxx").append('<tr><th class="w100">序号</th><th class="w100">姓名</th><th class="w150">证件类型</th><th class="w200">证件号</th>'
                + '<th class="w100">出生日期</th><th class="w100">状态</th></tr>')
            for (var i = 0; i < data.length; i++) {
                $("#ddmx").append('<tr onclick="sps(this)"><td class="w200">' + data[i].Sn + '</td><td class="w100">' + data[i].Ptrain + '</td><td class="w150">' + data[i].Pdate
                    + '</td><td class="w100">' + data[i].Pfrom + '</td><td class="w100">' + data[i].Pto + '</td><td class="w100">' + data[i].Pseat
                    + '</td><td class="w100">' + data[i].Price + '</td><td class="w100">' + data[i].Passager + '</td><td class="w100">' + data[i].Tip
                    + '</td><td class="w150">' + data[i].Total + '</td><td class="w100">' + jsonDateFormat(data[i].Cdate) + '</td><td class="w100">' + data[i].billstate.Name + '</td></tr>');
            }
        },
        error: function (msg) {
            alert("ajax连接异常：" + msg);
        }
    })
}
//json日期格式转换为正常格式
function jsonDateFormat(jsonDate) {
    try {
        var date = new Date(parseInt(jsonDate.replace("/Date(", "").replace(")/", ""), 10));
        var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
        var day = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
        var hours = date.getHours();
        var minutes = date.getMinutes();
        var seconds = date.getSeconds();
        var milliseconds = date.getMilliseconds();
        return date.getFullYear() + "-" + month + "-" + day + " " + hours + ":" + minutes + ":" + seconds;
       // + "." + milliseconds 毫秒显示
    } catch (ex) {
        return "";
    }
}
//点击订单列表显示乘客信息
function sps(e) {
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
            $("#ckxx").append('<tr><th class="w100">序号</th><th class="w100">姓名</th><th class="w150">证件类型</th><th class="w200">证件号</th>'
                + '<th class="w100">出生日期</th><th class="w100">状态</th><th class="w100"></th></tr>')
            for (var i = 0; i < data.length; i++) {
                $("#ckxx").append('<tr><td class="w100">' + data[i].Psn + '</td><td class="w100">' + data[i].Name + '</td><td class="w150">' + data[i].Card
                    + '</td><td class="w200">' + data[i].Cnum + '</td><td class="w100">' + data[i].Bday + '</td><td class="w100">' + data[i].billstate.Name + '</td></tr>');
            }
        },
        error: function (msg) {
            alert("ajax连接异常：" + msg);
        }
    })
}