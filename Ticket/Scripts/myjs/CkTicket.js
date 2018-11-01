$(document).ready(function () {
    sessionStorage.clear();
})
//抓取12306数据
function seed() {
    var dates = $("#date").val();
    //var vfrom = $("#start").attr("data-value");
    //var vto = $("#end").attr("data-value");
    var vfrom = $("#start").val();
    var vto = $("#end").val();
    alert(dates);
    //alert("123");
    $.ajax({
        type: "post",
        url: "/Home/CkTicket",
        datatype: "text",
        data: {date:dates,from:vfrom,to:vto},
        success: function (data) {
            alert(data.length);
            $("#searchList").children().remove();
            for (var i = 0; i < data.length; i++) {
                if (i == 0) {
                    $("#searchList").append("<div class='trainItem'></div>");
                }
                else {
                    $(".trainItem").eq(i-1).after("<div class='trainItem'></div>");
                }
                $(".trainItem").eq(i).append('<ul id="t_' + i + '"></ul>');
                $("#t_" + i).append('<li class="lst_time"><strong>' + data[i].start_time + '</strong><br><span>' + data[i].end_time
                    + '</span></li><li class="lst_no"><strong>' + data[i].train_name + '</strong><br><span>经停站</span></li><li class="lst_place">'
                    + data[i].start_station + '<br>' + data[i].end_station + '</li><li class="lst_duration">'
                    + data[i].run_time + '</li>');
                var pline = data[i].price_list;
                var htmlstr = "";
                var htmlyp = "";
                var htmlbt = "";
                for (var j = 0; j < pline.length; j++) {
                    htmlstr = htmlstr + "<p><span class='jzxs'>" + pline[j].price_type + "</span>      <dfn>¥</dfn><span class='base_price'>" + pline[j].price + "</span></p>";
                    htmlyp = htmlyp + "<p style='width:80px'>余票数<span>" + pline[j].counts + "</span></p>";
                    if (pline[j].counts == "无") {
                        htmlbt = htmlbt + '<p><a href="javascript:void(0)" class="bt_book" onclick="tock(this)" data-time="' + data[i].date + '" data-trainno="' + data[i].train_no
                        + '"data-trainname="' + data[i].train_name + '" data-from="' + data[i].start_station + '" data-to="' + data[i].end_station + '" data-seat="' + pline[j].price_type + '"data-seatcode="' + pline[j].seat_code
                        + '" data-price="' + pline[j].price + '" data-stime="' + data[i].start_time + '" data-etime="' + data[i].end_time
                            + '" data-rtime="' + data[i].run_time + '" data-cts="' + pline[j].counts + '">抢票</a></p>';
                    }
                    else {
                        htmlbt = htmlbt + '<p><a href="javascript:void(0)" class="bt_book" onclick="tock(this)" data-time="' + data[i].date + '" data-trainno="' + data[i].train_no + '"data-seatcode="' + pline[j].seat_code
                        + '"data-trainname="' + data[i].train_name + '" data-from="' + data[i].start_station + '" data-to="' + data[i].end_station + '" data-seat="' + pline[j].price_type
                        + '" data-price="' + pline[j].price + '" data-stime="' + data[i].start_time + '" data-etime="' + data[i].end_time
                        + '" data-rtime="' + data[i].run_time + '"data-cts="' + pline[j].counts + '">预定</a></p>';
                    }
                }
                $("#t_" + i).append('<li class="lst_seat">' + htmlstr + '</li>');
                $("#t_" + i).append('<li class="lst_yp">' + htmlyp + '</li>');
                $("#t_" + i).append('<li class="lst_btn">' + htmlbt + '</li>');
            }
            $(".trainItem").append('<div class="clearfix"></div>');
            $(".trainItem").prepend('<div class="clearfix"></div>');
        }
    })
};

function tock(e) {
    var cts = $(e).attr("data-cts");
    var tname=$(e).attr("data-trainname")
    var stime = $(e).attr("data-stime");
    var etime = $(e).attr("data-etime");
    var rtime = $(e).attr("data-rtime");
    var time = $(e).attr("data-time");
    var no = $(e).attr("data-trainno");
    var froms = $(e).attr("data-from");
    var tos = $(e).attr("data-to");
    var seat = $(e).attr("data-seat");
    var seatcode = $(e).attr("data-seatcode");
    var price = $(e).attr("data-price");
    $.ajax({
        type: 'post',
        url: "/home/Orders",
        data: { time: time, no: no, from: froms, to: tos, seat: seat,seatcode:seatcode, price: price, stime: stime, etime: etime, rtime: rtime,tname:tname },
        success: function (data, textStatus) {
            if (cts == "0")
                cts = "无";
            if (cts == "无")
                window.location = "/Order/TicketRb";
            else {
                window.location = "/Order/Index";
            }
        },
        error: function () {
            alert("请求失败！");
        }
    });
}

//选站1
$(function () {
    $("#start").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/TS/GetDrugList",
                type: "POST",
                dataType: "json",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.Name,
                            value: item.Code
                        }
                    }
                        ));
                }, //end  success
                scrollHeight: 200,
                max: 10,
                scroll:true
            }); //end ajax
        }, //end  source
        focus: function (event, ui) {

            //$("#Vmodel").val(ui.item.value);
            //$("#CustomerName").val(ui.item.CustomerName);
            //$("#DepartDate").val(ui.item.DeliveryDate);
            return false;
        }, //end focus
        select: function (event, ui) {
            $("#start").val(ui.item.label);
            $("#start").attr("data-value",ui.item.value);
            return false;
        }, //end  select
        minLength: 0
    }); //end autocomplete
});
//到站2
$(function () {
    $("#end").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/TS/GetDrugList",
                type: "POST",
                dataType: "json",
                data: { term: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.Name,
                            value: item.Code
                        }
                    }
                        ));
                } //end  success
            }); //end ajax
        }, //end  source
        focus: function (event, ui) {

            //$("#Vmodel").val(ui.item.value);
            //$("#CustomerName").val(ui.item.CustomerName);
            //$("#DepartDate").val(ui.item.DeliveryDate);
            return false;
        }, //end focus
        select: function (event, ui) {
            $("#end").val(ui.item.label);
            $("#end").attr("data-value",ui.item.value);
            return false;
        }, //end  select
        minLength: 0
    }); //end autocomplete
});
//通过聚合接口返回列车数据
function jhseed() {
    var dates = $("#date").val();
    //var vfrom = $("#start").attr("data-value");
    //var vto = $("#end").attr("data-value");
    var vfrom = $("#start").val();
    var vto = $("#end").val();
    $.ajax({
        type: "post",
        url: "/Home/JhGetTicket",
        datatype: "text",
        data: { date: dates, from: vfrom, to: vto },
        success: function (data) {
            alert(data.result.list[0].train_code);
            var ds = data.result.list;
            $("#searchList").children().remove();
            for (var i = 0; i < ds.length; i++) {
                if (i == 0) {
                    $("#searchList").append("<div class='trainItem'></div>");
                }
                else {
                    $(".trainItem").eq(i - 1).after("<div class='trainItem'></div>");
                }
                $(".trainItem").eq(i).append('<ul id="t_' + i + '"></ul>');
                $("#t_" + i).append('<li class="lst_time"><strong>' + ds[i].start_time + '</strong><br><span>' + ds[i].arrive_time
                    + '</span></li><li class="lst_no"><strong>' + ds[i].train_code + '</strong><br><span>经停站</span></li><li class="lst_place">'
                    + ds[i].from_station_name + '<br>' + ds[i].to_station_name + '</li><li class="lst_duration">'
                    + ds[i].run_time + '</li>');
                var htmlstr = "";
                var htmlyp = "";
                var htmlbt = "";
                //商务座
                if ($.isNumeric(ds[i].swz_num)) {
                    var seatname = "商务座";
                    var seatcode = "swz";
                    var count = ds[i].swz_num;
                    var price = ds[i].swz_price;
                    htmlstr = htmlstr + "<p><span class='jzxs'>" + seatname + "</span>      <dfn>¥</dfn><span class='base_price'>" + price + "</span></p>";
                    htmlyp = htmlyp + "<p style='width:80px'>余票数<span>" + count + "</span></p>";
                    if (count == "0") {
                        htmlbt = htmlbt + '<p><a href="javascript:void(0)" class="bt_book" onclick="tock(this)" data-time="' + dates + '" data-trainno="'
                            + ds[i].train_no + '"data-trainname="' + ds[i].train_code + '" data-from="' + ds[i].from_station_name + '" data-to="'
                            + ds[i].to_station_name + '" data-seat="' + seatname + '" data-seatcode="' + seatcode + '" data-price="' + price + '" data-stime="'
                            + ds[i].start_time + '" data-etime="' + ds[i].arrive_time + '" data-rtime="' + ds[i].run_time + '" data-cts="' + count + '">抢票</a></p>';
                    }
                    else {
                        htmlbt = htmlbt + '<p><a href="javascript:void(0)" class="bt_book" onclick="tock(this)" data-time="' + dates + '" data-trainno="'
                            + ds[i].train_no + '"data-trainname="' + ds[i].train_code + '" data-from="' + ds[i].from_station_name + '" data-to="'
                            + ds[i].to_station_name + '" data-seat="' + seatname + '" data-seatcode="' + seatcode + '" data-price="' + price + '" data-stime="'
                            + ds[i].start_time + '" data-etime="' + ds[i].arrive_time + '" data-rtime="' + ds[i].run_time + '" data-cts="' + count + '">预定</a></p>';
                    }
                }
                //一等座
                if ($.isNumeric(ds[i].ydz_num)) {
                    var seatname = "一等座";
                    var seatcode = "ydz";
                    var count = ds[i].ydz_num;
                    var price = ds[i].ydz_price;
                    htmlstr = htmlstr + "<p><span class='jzxs'>" + seatname + "</span>      <dfn>¥</dfn><span class='base_price'>" + price + "</span></p>";
                    htmlyp = htmlyp + "<p style='width:80px'>余票数<span>" + count + "</span></p>";
                    if (count == "0") {
                        htmlbt = htmlbt + '<p><a href="javascript:void(0)" class="bt_book" onclick="tock(this)" data-time="' + dates + '" data-trainno="'
                            + ds[i].train_no + '"data-trainname="' + ds[i].train_code + '" data-from="' + ds[i].from_station_name + '" data-to="'
                            + ds[i].to_station_name + '" data-seat="' + seatname + '" data-seatcode="' + seatcode + '" data-price="' + price + '" data-stime="'
                            + ds[i].start_time + '" data-etime="' + ds[i].arrive_time + '" data-rtime="' + ds[i].run_time + '" data-cts="' + count + '">抢票</a></p>';
                    }
                    else {
                        htmlbt = htmlbt + '<p><a href="javascript:void(0)" class="bt_book" onclick="tock(this)" data-time="' + dates + '" data-trainno="'
                            + ds[i].train_no + '"data-trainname="' + ds[i].train_code + '" data-from="' + ds[i].from_station_name + '" data-to="'
                            + ds[i].to_station_name + '" data-seat="' + seatname + '" data-seatcode="' + seatcode + '" data-price="' + price + '" data-stime="'
                            + ds[i].start_time + '" data-etime="' + ds[i].arrive_time + '" data-rtime="' + ds[i].run_time + '" data-cts="' + count + '">预定</a></p>';
                    }
                }
                //二等座
                if ($.isNumeric(ds[i].edz_num)) {
                    var seatname = "二等座";
                    var seatcode = "edz";
                    var count = ds[i].edz_num;
                    var price = ds[i].edz_price;
                    htmlstr = htmlstr + '<p><span class="jzxs">' + seatname + "</span>      <dfn>¥</dfn><span class='base_price'>" + price + "</span></p>";
                    htmlyp = htmlyp + "<p style='width:80px'>余票数<span>" + count + "</span></p>";
                    if (count == "0") {
                        htmlbt = htmlbt + '<p><a href="javascript:void(0)" class="bt_book" onclick="tock(this)" data-time="' + dates + '" data-trainno="'
                            + ds[i].train_no + '"data-trainname="' + ds[i].train_code + '" data-from="' + ds[i].from_station_name + '" data-to="'
                            + ds[i].to_station_name + '" data-seat="'+seatname+'" data-seatcode="'+seatcode+'" data-price="' + price + '" data-stime="'
                            + ds[i].start_time + '" data-etime="' + ds[i].arrive_time + '" data-rtime="' + ds[i].run_time + '" data-cts="' + count + '">抢票</a></p>';
                    }
                    else {
                        htmlbt = htmlbt + '<p><a href="javascript:void(0)" class="bt_book" onclick="tock(this)" data-time="' + dates + '" data-trainno="'
                            + ds[i].train_no + '"data-trainname="' + ds[i].train_code + '" data-from="' + ds[i].from_station_name + '" data-to="'
                            + ds[i].to_station_name + '" data-seat="'+seatname+'" data-seatcode="'+seatcode+'" data-price="' + price + '" data-stime="'
                            + ds[i].start_time + '" data-etime="' + ds[i].arrive_time + '" data-rtime="' + ds[i].run_time + '" data-cts="' + count + '">预定</a></p>';
                    }
                }
                //高级软卧
                if ($.isNumeric(ds[i].gjrw_num)) {
                    var seatname = "高级软卧";
                    var seatcode = "gjrw";
                    var count = ds[i].gjrw_num;
                    var price = ds[i].gjrw_price;
                    htmlstr = htmlstr + "<p><span class='jzxs'>" + seatname + "</span>      <dfn>¥</dfn><span class='base_price'>" + price + "</span></p>";
                    htmlyp = htmlyp + "<p style='width:80px'>余票数<span>" + count + "</span></p>";
                    if (count == "0") {
                        htmlbt = htmlbt + '<p><a href="javascript:void(0)" class="bt_book" onclick="tock(this)" data-time="' + dates + '" data-trainno="'
                            + ds[i].train_no + '"data-trainname="' + ds[i].train_code + '" data-from="' + ds[i].from_station_name + '" data-to="'
                            + ds[i].to_station_name + '" data-seat="' + seatname + '" data-seatcode="' + seatcode + '" data-price="' + price + '" data-stime="'
                            + ds[i].start_time + '" data-etime="' + ds[i].arrive_time + '" data-rtime="' + ds[i].run_time + '" data-cts="' + count + '">抢票</a></p>';
                    }
                    else {
                        htmlbt = htmlbt + '<p><a href="javascript:void(0)" class="bt_book" onclick="tock(this)" data-time="' + dates + '" data-trainno="'
                            + ds[i].train_no + '"data-trainname="' + ds[i].train_code + '" data-from="' + ds[i].from_station_name + '" data-to="'
                            + ds[i].to_station_name + '" data-seat="' + seatname + '" data-seatcode="' + seatcode + '" data-price="' + price + '" data-stime="'
                            + ds[i].start_time + '" data-etime="' + ds[i].arrive_time + '" data-rtime="' + ds[i].run_time + '" data-cts="' + count + '">预定</a></p>';
                    }
                }
                //软卧
                if ($.isNumeric(ds[i].rw_num)) {
                    var seatname = "软卧";
                    var seatcode = "rw";
                    var count = ds[i].rw_num;
                    var price = ds[i].rw_price;
                    htmlstr = htmlstr + "<p><span class='jzxs'>软&emsp;卧</span>      <dfn>¥</dfn><span class='base_price'>" + price + "</span></p>";
                    htmlyp = htmlyp + "<p style='width:80px'>余票数<span>" + count + "</span></p>";
                    if (count == "0") {
                        htmlbt = htmlbt + '<p><a href="javascript:void(0)" class="bt_book" onclick="tock(this)" data-time="' + dates + '" data-trainno="'
                            + ds[i].train_no + '"data-trainname="' + ds[i].train_code + '" data-from="' + ds[i].from_station_name + '" data-to="'
                            + ds[i].to_station_name + '" data-seat="' + seatname + '" data-seatcode="' + seatcode + '" data-price="' + price + '" data-stime="'
                            + ds[i].start_time + '" data-etime="' + ds[i].arrive_time + '" data-rtime="' + ds[i].run_time + '" data-cts="' + count + '">抢票</a></p>';
                    }
                    else {
                        htmlbt = htmlbt + '<p><a href="javascript:void(0)" class="bt_book" onclick="tock(this)" data-time="' + dates + '" data-trainno="'
                            + ds[i].train_no + '"data-trainname="' + ds[i].train_code + '" data-from="' + ds[i].from_station_name + '" data-to="'
                            + ds[i].to_station_name + '" data-seat="' + seatname + '" data-seatcode="' + seatcode + '" data-price="' + price + '" data-stime="'
                            + ds[i].start_time + '" data-etime="' + ds[i].arrive_time + '" data-rtime="' + ds[i].run_time + '" data-cts="' + count + '">预定</a></p>';
                    }
                }
                //动卧
                if ($.isNumeric(ds[i].dw_num)) {
                    var seatname = "动卧";
                    var seatcode = "dw";
                    var count = ds[i].dw_num;
                    var price = ds[i].dw_price;
                    htmlstr = htmlstr + "<p><span class='jzxs'>动&emsp;卧</span>      <dfn>¥</dfn><span class='base_price'>" + price + "</span></p>";
                    htmlyp = htmlyp + "<p style='width:80px'>余票数<span>" + count + "</span></p>";
                    if (count == "0") {
                        htmlbt = htmlbt + '<p><a href="javascript:void(0)" class="bt_book" onclick="tock(this)" data-time="' + dates + '" data-trainno="'
                            + ds[i].train_no + '"data-trainname="' + ds[i].train_code + '" data-from="' + ds[i].from_station_name + '" data-to="'
                            + ds[i].to_station_name + '" data-seat="' + seatname + '" data-seatcode="' + seatcode + '" data-price="' + price + '" data-stime="'
                            + ds[i].start_time + '" data-etime="' + ds[i].arrive_time + '" data-rtime="' + ds[i].run_time + '" data-cts="' + count + '">抢票</a></p>';
                    }
                    else {
                        htmlbt = htmlbt + '<p><a href="javascript:void(0)" class="bt_book" onclick="tock(this)" data-time="' + dates + '" data-trainno="'
                            + ds[i].train_no + '"data-trainname="' + ds[i].train_code + '" data-from="' + ds[i].from_station_name + '" data-to="'
                            + ds[i].to_station_name + '" data-seat="' + seatname + '" data-seatcode="' + seatcode + '" data-price="' + price + '" data-stime="'
                            + ds[i].start_time + '" data-etime="' + ds[i].arrive_time + '" data-rtime="' + ds[i].run_time + '" data-cts="' + count + '">预定</a></p>';
                    }
                }
                //硬卧
                if ($.isNumeric(ds[i].yw_num)) {
                    var seatname = "硬卧";
                    var seatcode = "yw";
                    var count = ds[i].yw_num;
                    var price = ds[i].yw_price;
                    htmlstr = htmlstr + "<p><span class='jzxs'>硬&emsp;卧</span>      <dfn>¥</dfn><span class='base_price'>" + price + "</span></p>";
                    htmlyp = htmlyp + "<p style='width:80px'>余票数<span>" + count + "</span></p>";
                    if (count == "0") {
                        htmlbt = htmlbt + '<p><a href="javascript:void(0)" class="bt_book" onclick="tock(this)" data-time="' + dates + '" data-trainno="'
                            + ds[i].train_no + '"data-trainname="' + ds[i].train_code + '" data-from="' + ds[i].from_station_name + '" data-to="'
                            + ds[i].to_station_name + '" data-seat="' + seatname + '" data-seatcode="' + seatcode + '" data-price="' + price + '" data-stime="'
                            + ds[i].start_time + '" data-etime="' + ds[i].arrive_time + '" data-rtime="' + ds[i].run_time + '" data-cts="' + count + '">抢票</a></p>';
                    }
                    else {
                        htmlbt = htmlbt + '<p><a href="javascript:void(0)" class="bt_book" onclick="tock(this)" data-time="' + dates + '" data-trainno="'
                            + ds[i].train_no + '"data-trainname="' + ds[i].train_code + '" data-from="' + ds[i].from_station_name + '" data-to="'
                            + ds[i].to_station_name + '" data-seat="' + seatname + '" data-seatcode="' + seatcode + '" data-price="' + price + '" data-stime="'
                            + ds[i].start_time + '" data-etime="' + ds[i].arrive_time + '" data-rtime="' + ds[i].run_time + '" data-cts="' + count + '">预定</a></p>';
                    }
                }
                //软座
                if ($.isNumeric(ds[i].rz_num)) {
                    var seatname = "软座";
                    var seatcode = "rz";
                    var count = ds[i].rz_num;
                    var price = ds[i].rz_price;
                    htmlstr = htmlstr + "<p><span class='jzxs'>软&emsp;座</span>      <dfn>¥</dfn><span class='base_price'>" + price + "</span></p>";
                    htmlyp = htmlyp + "<p style='width:80px'>余票数<span>" + count + "</span></p>";
                    if (count == "0") {
                        htmlbt = htmlbt + '<p><a href="javascript:void(0)" class="bt_book" onclick="tock(this)" data-time="' + dates + '" data-trainno="'
                            + ds[i].train_no + '"data-trainname="' + ds[i].train_code + '" data-from="' + ds[i].from_station_name + '" data-to="'
                            + ds[i].to_station_name + '" data-seat="' + seatname + '" data-seatcode="' + seatcode + '" data-price="' + price + '" data-stime="'
                            + ds[i].start_time + '" data-etime="' + ds[i].arrive_time + '" data-rtime="' + ds[i].run_time + '" data-cts="' + count + '">抢票</a></p>';
                    }
                    else {
                        htmlbt = htmlbt + '<p><a href="javascript:void(0)" class="bt_book" onclick="tock(this)" data-time="' + dates + '" data-trainno="'
                            + ds[i].train_no + '"data-trainname="' + ds[i].train_code + '" data-from="' + ds[i].from_station_name + '" data-to="'
                            + ds[i].to_station_name + '" data-seat="' + seatname + '" data-seatcode="' + seatcode + '" data-price="' + price + '" data-stime="'
                            + ds[i].start_time + '" data-etime="' + ds[i].arrive_time + '" data-rtime="' + ds[i].run_time + '" data-cts="' + count + '">预定</a></p>';
                    }
                }
                //硬座
                if ($.isNumeric(ds[i].yz_num)) {
                    var seatname = "硬座";
                    var seatcode = "yz";
                    var count = ds[i].yz_num;
                    var price = ds[i].yz_price;
                    htmlstr = htmlstr + "<p><span class='jzxs'>硬&emsp;座</span>      <dfn>¥</dfn><span class='base_price'>" + price + "</span></p>";
                    htmlyp = htmlyp + "<p style='width:80px'>余票数<span>" + count + "</span></p>";
                    if (count == "0") {
                        htmlbt = htmlbt + '<p><a href="javascript:void(0)" class="bt_book" onclick="tock(this)" data-time="' + dates + '" data-trainno="'
                            + ds[i].train_no + '"data-trainname="' + ds[i].train_code + '" data-from="' + ds[i].from_station_name + '" data-to="'
                            + ds[i].to_station_name + '" data-seat="' + seatname + '" data-seatcode="' + seatcode + '" data-price="' + price + '" data-stime="'
                            + ds[i].start_time + '" data-etime="' + ds[i].arrive_time + '" data-rtime="' + ds[i].run_time + '" data-cts="' + count + '">抢票</a></p>';
                    }
                    else {
                        htmlbt = htmlbt + '<p><a href="javascript:void(0)" class="bt_book" onclick="tock(this)" data-time="' + dates + '" data-trainno="'
                            + ds[i].train_no + '"data-trainname="' + ds[i].train_code + '" data-from="' + ds[i].from_station_name + '" data-to="'
                            + ds[i].to_station_name + '" data-seat="' + seatname + '" data-seatcode="' + seatcode + '" data-price="' + price + '" data-stime="'
                            + ds[i].start_time + '" data-etime="' + ds[i].arrive_time + '" data-rtime="' + ds[i].run_time + '" data-cts="' + count + '">预定</a></p>';
                    }
                }
                //无座
                if ($.isNumeric(ds[i].wz_num)) {
                    var seatname = "无座";
                    var seatcode = "wz";
                    var count = ds[i].wz_num;
                    var price = ds[i].wz_price;
                    htmlstr = htmlstr + "<p><span class='jzxs'>无&emsp;座</span>      <dfn>¥</dfn><span class='base_price'>" + price + "</span></p>";
                    htmlyp = htmlyp + "<p style='width:80px'>余票数<span>" + count + "</span></p>";
                    if (count == "0") {
                        htmlbt = htmlbt + '<p><a href="javascript:void(0)" class="bt_book" onclick="tock(this)" data-time="' + dates + '" data-trainno="'
                            + ds[i].train_no + '"data-trainname="' + ds[i].train_code + '" data-from="' + ds[i].from_station_name + '" data-to="'
                            + ds[i].to_station_name + '" data-seat="' + seatname + '" data-seatcode="' + seatcode + '" data-price="' + price + '" data-stime="'
                            + ds[i].start_time + '" data-etime="' + ds[i].arrive_time + '" data-rtime="' + ds[i].run_time + '" data-cts="' + count + '">抢票</a></p>';
                    }
                    else {
                        htmlbt = htmlbt + '<p><a href="javascript:void(0)" class="bt_book" onclick="tock(this)" data-time="' + dates + '" data-trainno="'
                            + ds[i].train_no + '"data-trainname="' + ds[i].train_code + '" data-from="' + ds[i].from_station_name + '" data-to="'
                            + ds[i].to_station_name + '" data-seat="' + seatname + '" data-seatcode="' + seatcode + '" data-price="' + price + '" data-stime="'
                            + ds[i].start_time + '" data-etime="' + ds[i].arrive_time + '" data-rtime="' + ds[i].run_time + '" data-cts="' + count + '">预定</a></p>';
                    }
                }
                //其他
                if ($.isNumeric(ds[i].qtxb_num)) {
                    var seatname = "其他";
                    var seatcode = "qtxb";
                    var count = ds[i].qtxb_num;
                    var price = ds[i].qtxb_price;
                    htmlstr = htmlstr + "<p><span class='jzxs'>其&emsp;他</span>      <dfn>¥</dfn><span class='base_price'>" + price + "</span></p>";
                    htmlyp = htmlyp + "<p style='width:80px'>余票数<span>" + count + "</span></p>";
                    if (count == "0") {
                        htmlbt = htmlbt + '<p><a href="javascript:void(0)" class="bt_book" onclick="tock(this)" data-time="' + dates + '" data-trainno="'
                            + ds[i].train_no + '"data-trainname="' + ds[i].train_code + '" data-from="' + ds[i].from_station_name + '" data-to="'
                            + ds[i].to_station_name + '" data-seat="' + seatname + '" data-seatcode="' + seatcode + '" data-price="' + price + '" data-stime="'
                            + ds[i].start_time + '" data-etime="' + ds[i].arrive_time + '" data-rtime="' + ds[i].run_time + '" data-cts="' + count + '">抢票</a></p>';
                    }
                    else {
                        htmlbt = htmlbt + '<p><a href="javascript:void(0)" class="bt_book" onclick="tock(this)" data-time="' + dates + '" data-trainno="'
                            + ds[i].train_no + '"data-trainname="' + ds[i].train_code + '" data-from="' + ds[i].from_station_name + '" data-to="'
                            + ds[i].to_station_name + '" data-seat="' + seatname + '" data-seatcode="' + seatcode + '" data-price="' + price + '" data-stime="'
                            + ds[i].start_time + '" data-etime="' + ds[i].arrive_time + '" data-rtime="' + ds[i].run_time + '" data-cts="' + count + '">预定</a></p>';
                    }
                }
                //------------------------------------------------------------
                $("#t_" + i).append('<li class="lst_seat">' + htmlstr + '</li>');
                $("#t_" + i).append('<li class="lst_yp">' + htmlyp + '</li>');
                $("#t_" + i).append('<li class="lst_btn">' + htmlbt + '</li>');
            }
            $(".trainItem").append('<div class="clearfix"></div>');
            $(".trainItem").prepend('<div class="clearfix"></div>');
        }
    })
};
    