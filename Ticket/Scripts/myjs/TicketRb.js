$(document).ready(function () {
    //alert($("#bp1").text());
    var price = $("#bp1").text();
    var tip = $("#bp2").text();
    var total = Number(price) + Number(tip);
    $("#totalMoney").text(total);
})
function bill() {
    alert("创建订单");
    var vfrom = $("#sfrom").html();
    var vstime = $("#selectedTrainNumbers .yiban").attr("starttime");
    var vetime = $("#selectedTrainNumbers .yiban").attr("endtime");
    var vto = $("#eto").html();
    var vdate = $("#selectRecDate span small").text();
    var vtrainname = $("#selectedTrainNumbers span").attr("trainname");
    var vseat = $("#selectedSeats span").attr("seat");
    var vp1name = $(".name-input").val();
    var vcard1 = $("#selectCard option:selected").text();
    var vid1 = $(".id-input").val();
    var vyear1 = $(".year option:selected").val();
    var vmonth1 = $(".month option:selected").val();
    var vday1 = $(".day option:selected").val();
    var vphone = $("#contactMobile").val();
    var vprice = $("#selectedSeats span").attr("data-price");
    var total = $("#totalMoney").text();
    var vtip = $("#bp2").text();
    alert(vstime);
    $.ajax({
        type: "post",
        url: "/Order/CreateOrder",
        datatype: "text",
        data: { from:vfrom,stime:vstime,etime:vetime,to:vto,date: vdate, trainname: vtrainname, seat: vseat, p1name: vp1name, card1: vcard1, id1: vid1, year1: vyear1, month1: vmonth1, day1: vday1,price:vprice, phone: vphone, total: total, tip: vtip },
        success: function (data) {
            if(data=="true")
                window.location = "/Order/OrderLists";
        }
    })
}