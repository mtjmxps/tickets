using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TModel
{
    [DataContract]
    public class JhTrain
    {
        [DataMember]
        public string reason { get; set; }
        [DataMember]
        public result result { get; set; }
        [DataMember]
        public int error_code { get; set; }
    }
    [DataContract]
    public class result
    {
        [DataMember]
        public List<list> list { get; set; }
    }
    [DataContract]
    public class list
    {
        [DataMember]
        public string sale_date_time { get; set; }/*"1230",车票开售时间*/
        [DataMember]
        public string can_buy_now { get; set; }/*"Y", 当前是否可以接受预定*/
        [DataMember]
        public int arrive_days { get; set; }/*"0",列车从出发站到达目的站的运行天数 0:当日到达1,: 次日到达,2:三日到达,3:四日到达,依此类推*/
        [DataMember]
        public string train_start_date { get; set; }/*"20150711",列车从始发站出发的日期*/
        [DataMember]
        public string train_code { get; set; }/*"G101",车次*/
        [DataMember]
        public int access_byidcard { get; set; }/*"1",是否可凭二代身份证直接进出站*/
        [DataMember]
        public string train_no { get; set; }/*"240000G1010A",列车号*/
        [DataMember]
        public string train_type { get; set; }/*"G",列车类型*/
        [DataMember]
        public string from_station_code { get; set; }/*"VNP", 出发车站简码*/
        [DataMember]
        public string from_station_name { get; set; }/*"北京南", 出发车站名*/
        [DataMember]
        public string to_station_code { get; set; }/*"AOH",到达车站简码*/
        [DataMember]
        public string to_station_name { get; set; }/*"上海虹桥",到达车站名*/
        [DataMember]
        public string start_station_name { get; set; }/*"北京南",始发站名*/
        [DataMember]
        public string end_station_name { get; set; }/*"上海虹桥",终到站名*/
        [DataMember]
        public string start_time { get; set; }/*"07:00",当前站出发时刻*/
        [DataMember]
        public string arrive_time { get; set; }/*"12:37",到达时刻*/
        [DataMember]
        public string run_time { get; set; }/* "05:37",历时（出发站到目的站）*/
        [DataMember]
        public int run_time_minute { get; set; }/*"337",历时分钟合计*/
        [DataMember]
        public string gjrw_num { get; set; }/*"--",高级软卧余票数量*/
        [DataMember]
        public decimal gjrws_price { get; set; }/* 0,高级软卧（上）票价*/
        [DataMember]
        public decimal gjrw_price { get; set; }/* 0,高级软卧票价*/
        [DataMember]
        public string qtxb_num { get; set; }/* "qtxb_num": "--", 其他席别余票数量*/
        [DataMember]
        public decimal qtxb_price { get; set; }/*"qtxb_price": 0, 其他席别对应的票价*/
        [DataMember]
        public string rw_num { get; set; }/* "rw_num": "--", 软卧余票数量*/
        [DataMember]
        public string rw_price { get; set; } /*"rw_price": 0, 软卧（上）票价*/
        [DataMember]
        public decimal rwx_price { get; set; }/*"rwx_price":10, 软卧(下)票价*/
        [DataMember]
        public string rz_num { get; set; }/*"rz_num": "--", 软座的余票数量*/
        [DataMember]
        public decimal rz_price { get; set; }/*"rz_price": 0, 软座的票价*/
        [DataMember]
        public string swz_num { get; set; }/*"swz_num": "15", 商务座余票数量*/
        [DataMember]
        public decimal swz_price { get; set; }/*"swz_price": 1748, 商务座票价*/
        [DataMember]
        public string tdz_num { get; set; }/*"tdz_num": "--", 特等座的余票数量*/
        [DataMember]
        public decimal tdz_price { get; set; }/*"tdz_price": 0,  特等座票价*/
        [DataMember]
        public string wz_num { get; set; }/*"wz_num": "--", 无座的余票数量*/
        [DataMember]
        public decimal wz_price { get; set; }/*"wz_price": 0, 无座票价*/
        [DataMember]
        public string dw_num { get; set; }/*"dw_num":"8",动卧的余票数量*/
        [DataMember]
        public decimal dw_price { get; set; }/*"dw_price":"",动卧(上)票价[12306新增]*/
        [DataMember]
        public decimal dwx_price { get; set; }/*"dwx_price":"",动卧(下)票价[12306新增]*/
        [DataMember]
        public string yw_num { get; set; }/*"yw_num": "--", 硬卧的余票数量*/
        [DataMember]
        public decimal yw_price { get; set; }/*"yw_price": 0, 硬卧(上)票价（因为硬卧上中下铺价格不同，这个价格一般是均价）*/
        [DataMember]
        public decimal ywz_price { get; set; }/*" ywz_price":90,硬卧(中)票价*/
        [DataMember]
        public decimal ywx_price { get; set; }/*"ywx_price":86.5,硬卧(下)票价*/
        [DataMember]
        public string yz_num { get; set; }/*"yz_num": "--", 硬座的余票数量*/
        [DataMember]
        public decimal yz_price { get; set; }/*"yz_price": 0, 硬座票价*/
        [DataMember]
        public string edz_num { get; set; }/*"edz_num": "900", 二等座的余票数量*/
        [DataMember]
        public decimal edz_price { get; set; }/*"edz_price": 553, 二等座票价*/
        [DataMember]
        public string ydz_num { get; set; }/*"ydz_num": "54", 一等座余票数量*/
        [DataMember]
        public decimal ydz_price { get; set; }/*"ydz_price": 933, 一等座票价*/
        [DataMember]
        public int distance { get; set; }/*"distance":0, 里程数*/
    }
}
