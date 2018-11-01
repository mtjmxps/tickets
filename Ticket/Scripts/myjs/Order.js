//显示证件筛选列表
function showContactPop(e) {
    var et = $(e).attr("id");
    if (et == "adp1")
    { $("#dp1").show(); $("#dp2").hide(); $("#dp3").hide(); $("#dp4").hide(); $("#dp5").hide(); }
    if (et == "adp2")
    { $("#dp2").show(); $("#dp1").hide(); $("#dp3").hide(); $("#dp4").hide(); $("#dp5").hide(); }
    if (et == "adp3")
    { $("#dp3").show(); $("#dp2").hide(); $("#dp1").hide(); $("#dp4").hide(); $("#dp5").hide(); }
    if (et == "adp4")
    { $("#dp4").show(); $("#dp2").hide(); $("#dp3").hide(); $("#dp1").hide(); $("#dp5").hide(); }
    if (et == "adp5")
    { $("#dp5").show(); $("#dp2").hide(); $("#dp3").hide(); $("#dp4").hide(); $("#dp1").hide(); }
};
//不显示证件筛选列表
document.onclick = function (e) {
    var src = e.target;
    if (src.id === "adp1" || src.id === "adp2" || src.id === "adp3" || src.id === "adp4" || src.id === "adp5" || src.id === "dp" || src.id === "pop_title" || src.id === "ulCards") {
        return false;
    } else {
        $("#dp1").hide();
        $("#dp2").hide();
        $("#dp3").hide();
        $("#dp4").hide();
        $("#dp5").hide();
    }
}
//加载完页面后为A增加点击事件
$(document).ready(function () {
    if (sessionStorage['pdate'] == null)
        sessionStorage['pdate'] = $("#pdate").html();
    else
        $("#pdate").html(sessionStorage['pdate']);
    if (sessionStorage['ptrain'] == null)
        sessionStorage['ptrain'] = $("#ptrain").html();
    else
        $("#ptrain").html(sessionStorage['ptrain']);
    if (sessionStorage['pfrom'] == null)
        sessionStorage['pfrom'] = $("#pfrom").html();
    else
        $("#pfrom").html(sessionStorage['pfrom']);
    if (sessionStorage['pstime'] == null)
        sessionStorage['pstime'] = $("#pstime").html();
    else
        $("#pstime").html(sessionStorage['pstime']);
    if (sessionStorage['rtime'] == null)
        sessionStorage['rtime'] = $("#rtime").html();
    else
        $("#rtime").html(sessionStorage['rtime']);
    if (sessionStorage['pto'] == null)
        sessionStorage['pto'] = $("#pto").html();
    else
        $("#pto").html(sessionStorage['pto']);
    if (sessionStorage['petime'] == null)
        sessionStorage['petime'] = $("#petime").html();
    else
        $("#petime").html(sessionStorage['petime']);
    if (sessionStorage['seatname'] == null)
        sessionStorage['seatname'] = $("#seatname").html();
    else
        $("#seatname").html(sessionStorage['seatname']);
    if (sessionStorage['seatprice'] == null)
        sessionStorage['seatprice'] = $("#seatprice").html();
    else
        $("#seatprice").html(sessionStorage['seatprice']);
    if (sessionStorage['psgprice'] == null)
        sessionStorage['psgprice'] = $("#psgprice").html();
    else {
        $("#psgprice").html(sessionStorage['psgprice']);
        //$("#spantotalprice").html(sessionStorage['psgprice']);
    }
    if (sessionStorage['dprice'] == null)
        sessionStorage['dprice'] = $("#dprice").html();
    else
        $("#dprice").html(sessionStorage['dprice']);
    $("#spantotalprice").html(Number(sessionStorage['seatprice']) + Number(sessionStorage['dprice']));
//    //已有删除按钮初始化绑定删除事件
//    $(".options a").click(function () {
//        if ($(this).attr("type") =="1")
//            $(".current").text("身份证");
//        if ($(this).attr("type") == "2")
//            $(".current").text("护照");
//        if ($(this).attr("type") == "7")
//            $(".current").text("回乡证");
//        if ($(this).attr("type") == "8")
//            $(".current").text("同胞证");
//        if ($(this).attr("type") == "32")
//            $(".current").text("港澳台居民居住证");
    //    });

});
//////summary
//A自身点击事件
    //$(".current").text("身份证2");
//$(".input_text").val("123");
//////summary//////
function she(e) {
    var x=$(e).parent().parent().parent().attr("id").charAt(2);
    var adpx = "adp" + x;
    if ($(e).attr("type") == "1")
        $("#" + adpx + "").text("二代身份证");
    if ($(e).attr("type") == "2")
        $("#"+adpx+"").text("护照");
    //if ($(e).attr("type") == "7")
    //    $("#" + adpx + "").text("回乡证");
    if ($(e).attr("type") == "8")
        $("#" + adpx + "").text("台湾通行证");
    if ($(e).attr("type") == "32")
        $("#" + adpx + "").text("港澳通行证");
    $("#bdp"+x+"").val("");
}
function zqp()
{
    $("#tbodySelfTicket").css("display", "block");
    $("#tbodyDeliveryTicket").css("display", "none");
}
function spsm()
{
    $("#tbodySelfTicket").css("display", "none");
    $("#tbodyDeliveryTicket").css("display", "block");
}
//添加客户按钮
function addps() {
    var pss = $(".single_passenger").length + 1;
    if (pss < 6) {
        $(".bt_addps").before('<div id="p_adult' + pss + '" class="single_passenger"> <div><strong class="title">第' + pss + '位乘客</strong><a href="javascript:void(0)" onclick="dlps(this)">删除</a></div>'
           + '<table cellpadding="0" cellspacing="0" class="tb_form"><tbody><tr><th>姓名</th><td><div><input type="text" class="input_text" placeholder="姓名" style=" width: 308px;" /></div></td></tr>'
           + '<tr><th>证件信息</th><td><div class="dropdown"><a href="javascript:void(0)" id="adp' + pss + '" class="current current_sfz" onclick="showContactPop(this)">二代身份证</a>'
           + '<div id="dp' + pss + '" class="options" style="display:none;"><div class="pop_title" id="pop_title">证件类型</div><ul class="pop_bd" id="ulCards">'
           + '<li><a href="javascript:void(0)" type="1" style="cursor: pointer;"onclick="she(this)"><span class="float_right"></span>二代身份证</a></li>'
           + '<li><a href="javascript:void(0)"type="2" style="cursor: pointer;" onclick="she(this)"><span class="float_right"></span>护照</a></li>'
           //+ '<li><a href="javascript:void(0)"type="7" style="cursor: pointer;" onclick="she(this)"><span class="float_right"></span>回乡证</a></li>'
           + '<li><a href="javascript:void(0)"type="8" style="cursor: pointer;" onclick="she(this)"><span class="float_right"></span>台湾通行证</a></li>'
           + '<li class="border_none"><a href="javascript:void(0)"type="32" style="cursor: pointer;" onclick="she(this)"><span class="float_right"></span>港澳通行证</a></li></ul></div></div>'
           + '<input id="bdp' + pss + '" type="text" name="" class="input_text cardNo bigcode input_alert" bigcode="input_text cardNo bigcode" placeholder="证件号码" style=" width: 158px;" maxlength="50"></td></tr></tbody></table></div>');
        $("#psgct").html(pss);
        $("#dct").html(pss);
        var piaojia = Number(sessionStorage["psgprice"]) * Number(pss);
        var daishoujia = Number(sessionStorage["dprice"]) * Number(pss);
        if ($("#spsm").prop("checked"))
            $("#spantotalprice").html(piaojia + daishoujia + 20);
        else
            $("#spantotalprice").html(piaojia+daishoujia);
    }
    else
        alert("最多添加5名乘客，如需添加其他乘客，请另外下单！");
}
//删除乘客
function dlps(e) {
    var pss = $(".single_passenger").length
    if (pss > 1) {
        $(e).parent().parent().remove();
        $("#psgct").html(pss - 1);
        $("#dct").html(pss-1);
        if ($("#spsm").prop("checked"))
            $("#spantotalprice").html((Number(sessionStorage["psgprice"]) + Number(sessionStorage["dprice"])) * Number(pss - 1) + 20);
        else
            $("#spantotalprice").html((Number(sessionStorage["psgprice"]) + Number(sessionStorage["dprice"])) * Number(pss - 1));
    }
    //alert( $(".p_info .title").eq(0).html());
    for (var i = 0; i < pss; i++) {
        var j = i + 1;
        $(".p_info .title").eq(i).html("第" + j + "位乘客");
        $(".p_info .single_passenger").eq(i).attr("id", "p_adult" + j + "");
        $(".p_info .dropdown .current_sfz").eq(i).attr("id", "adp" + j + "");
        $(".p_info .dropdown .options").eq(i).attr("id", "dp" + j + "");
        $(".p_info .single_passenger .cardNo").eq(i).attr("id", "bdp" + j + "");
    }
}
//是否送票上门
//function qupiaoway() {
//    if ($(this).prop("checked")) {
//        alert("选中");
//        $("#tbodySelfTicket").attr("display", "block");
//        $("#tbodyDeliveryTicket").attr("display", "none");
//    }
//    else {
//        alert("b选中");
//        $("#tbodyDeliveryTicket").attr("display", "#block");
//        $("#tbodySelfTicket").attr("display", "none");
//    }
//}
$("#spsm").change(function () {
    var pss = $(".single_passenger").length;
    if ($(this).prop("checked")) {
        $("#liTicketReceive1").css("background-color", "");
        $("#liTicketReceive2").css("background-color", "#4f87b3");
        $("#tbodySelfTicket").css("display", "none");
        $("#tbodyDeliveryTicket").css("display", "block");
        $("#sideBoxDeliveryPrice").css("display", "block");
        $("#spantotalprice").html((Number(sessionStorage["psgprice"]) + Number(sessionStorage["dprice"])) * Number(pss) + 20);
    }
    else {
        $("#liTicketReceive1").css("background-color", "#4f87b3");
        $("#liTicketReceive2").css("background-color", "");
        $("#tbodySelfTicket").css("display", "block");
        $("#tbodyDeliveryTicket").css("display", "none");
        $("#sideBoxDeliveryPrice").css("display", "none");
        $("#spantotalprice").html((Number(sessionStorage["psgprice"]) + Number(sessionStorage["dprice"])) * Number(pss));
    }
});

function InputPassengersFocus(e) {
    //alert($(e.target).attr("tooltip"));
    //if (e.target.value == $(e.target).attr("tooltip"))
    //    e.target.value = ""
    //if(e.target.value == "")
    //    e.target.value = $(e.target).attr("tooltip")
}
//支付提交生成订单
$("#btnNext").on("click", function () {
    var vpdate = sessionStorage['pdate'];
    var vptrain = sessionStorage['ptrain'];
    var vpfrom = sessionStorage['pfrom'];
    var vpstime = sessionStorage['pstime'];
    var vrtime = sessionStorage['rtime'];
    var vpto = sessionStorage['pto'];
    var vpetime = sessionStorage['petime'];
    var vseatname = sessionStorage['seatname'];
    var vseatprice = sessionStorage['seatprice'];
    var vdprice = sessionStorage['dprice'];
    var vpassager = $(".single_passenger").length;
    if ($("#spsm").prop("checked")) {
        var visposs = 1;
        var vpossprice = 20;
        var vtotal = (Number(vseatprice)+Number(vdprice)) * Number(vpassager) + 20;
        var vcontacts = $("#txtDeliveryContactName").val();
        var vphone = $("#txtDeliveryMobile").val();
        var vadr = $("#txtDeliveryAddress").val();
    } else {
        var visposs = 0;
        var vpossprice = 0;
        var vtotal = (Number(vseatprice) + Number(vdprice)) * Number(vpassager);
        var vcontacts = $("#contactName").val();
        var vphone = $("#contactMobile").val();
        var vadr = " ";
    }
    //订单对象
    var orderlistObj = {
        Pdate: vpdate,
        Ptrain: vptrain,
        Pfrom: vpfrom,
        Pstime: vpstime,
        Rtime: vrtime,
        Pto: vpto,
        Petime: vpetime,
        Pseat: vseatname,
        Price: vseatprice,
        Dprice:vdprice,
        Passager: vpassager,
        Isposs: visposs,
        Possprice: vpossprice,
        Total: vtotal,
        Contacts: vcontacts,
        Phone: vphone,
        Adress:vadr
    }
    alert(vpdate);
    var passagersObj = [];
    //var op = $(".single_passenger")
    for (var i = 0; i < vpassager; i++) {
        var j = Number(i) + 1;
        passagersObj.push({
            Psn:j,
            Name: $(".single_passenger").eq(i).find("table").find("tr").eq(0).find("input").val(),
            Card: $("#adp" + j + "").html(),
            Cnum: $("#bdp"+j+"").val()
        });
        //alert($("#adp" + j + "").html());
    }
    $.ajax({
        url: '/order/SubJhOd',
        type: 'post',//换成 get 无效
        contentType: 'application/json',
        data: JSON.stringify({
            orderlistobj: orderlistObj,
            passagers: passagersObj
        }),
        success: function (data) {
            if (data.error_code == "0")
                window.location = "/Order/JhOrderPay";
            else {
                alert(data.reason + "，请重新下单！");
                window.location = "/Order/Index";
            }
        }
    });
})