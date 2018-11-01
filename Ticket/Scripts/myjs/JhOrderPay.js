//保存界面值
//$(document).ready(function () {
//    if (sessionStorage['jorder'] == null)
//        sessionStorage['jorder'] = $("#order").html();
//    else
//        $("#order").html(sessionStorage['jorder']);
//});
timer = window.setInterval(waitfor, 3000);
function waitfor() { 
    var vsn = $("#order").html().trim();
    $.ajax({
        url: '/order/JhOrderState',
        type: 'post',//换成 get 无效
        datatype: "text",
        data: {sn:vsn},
        success: function (data) {
            if (data.result.status != "0"){
                window.clearInterval(timer);
            }
            if (data.result.status == "1"){
                alert(data.result.msg + "，请重新下单！");
                window.location = "/Home/CkTicket";
            }
            if (data.result.status == "2") {
                $("#msg").html("占座成功，待支付");
                $("#pmsg").css("display", "none");
                $(".process_steps li").eq(0).addClass("first doing");
                $(".base_txtsilver").eq(0).html(data.result.submit_time);
                //列车位置信息
                var plist = "";
                //alert(data.result.passengers.length);
                for(var i=0;i<data.result.passengers.length;i++){
                    plist =plist+ '<dd style="padding: 0 20px 0 130px;"><span><strong>' + data.result.passengers[i].passengersename + '</strong>'
                        + data.result.passengers[i].zwname + '</span></dd>';
                }
                $("#zwlist").after(plist);
            }
        }
    });
}
//支付订单
function pay() {
    $("#msg").html("正在支付中");
    $("#pmsg").css("display", "inline-block");
    var vnum = $("#order").html().trim();
    var vtotal = $(".base_price").html();
    $.ajax({
        url: '/Order/PayOrder',
        type: 'post',
        datatype: 'text',
        data: { num: vnum, total: vtotal },
        success: function (data) {
            var reason = data.substr(data.IndexOf("\"reason\":") + 10, (data.IndexOf(",\"result\"") - data.IndexOf("\"reason\":") - 10));
            alert(reason)
            //setTimeout("pay1()", 3000);
        }
    })
    //
}
function pay1() {
    $("#msg").html("支付成功待出票，请稍后");
    //
    var myDate = new Date();
    //获取当前年
    var year = myDate.getFullYear();
    //获取当前月
    var month = myDate.getMonth() + 1;
    //获取当前日
    var date = myDate.getDate();
    var h = myDate.getHours();       //获取当前小时数(0-23)
    var m = myDate.getMinutes();     //获取当前分钟数(0-59)
    var s = myDate.getSeconds();
    var now = year + '-' + getNow(month) + "-" + getNow(date) + " " + getNow(h) + ':' + getNow(m) + ":" + getNow(s);
    //$("#pmsg").css("display", "none");
    $(".process_steps li").eq(1).addClass("first doing");
    $(".process_steps li").eq(2).addClass("first doing");
    $(".base_txtsilver").eq(1).html(now);
    $(".base_txtsilver").eq(2).html(now);
}
/* 获取当前时间 */
function getNow(s) {
    return s < 10 ? '0' + s : s;
}
//取消待支付的订单
function gotoCancel() {
    var vnum = $("#order").html().trim();
    //alert(vnum)
    $.ajax({
        url: '/Order/CancleJhOrder2',
        type: 'post',
        datatype: "text",
        data: {num:vnum},
        success: function(data){
            alert(data);
            window.location = "/Home/CkTicket";
        }
    })
}