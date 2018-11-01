using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TModel
{
    [DataContract]
    public class JhOrderState
    {
        [DataMember]
        public string reason { get; set; }
        [DataMember]
        public Result result { get; set; }
        [DataMember]
        public int error_code { get; set; }
    }
    [DataContract]
    public class Result
    {
        [DataMember]
        public string orderid { get; set; }
        [DataMember]
        public string user_orderid { get; set; }
        [DataMember]
        public string msg { get; set; }
        [DataMember]
        public string orderamount { get; set; }
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public List<Passengers> passengers { get; set; }
        [DataMember]
        public string checi { get; set; }
        [DataMember]
        public string ordernumber { get; set; }
        [DataMember]
        public string submit_time { get; set; }
        [DataMember]
        public string deal_time { get; set; }
        [DataMember]
        public string cancel_time { get; set; }
        [DataMember]
        public string pay_time { get; set; }
        [DataMember]
        public string finished_time { get; set; }
        [DataMember]
        public string refund_time { get; set; }
        [DataMember]
        public string juhe_refund_time { get; set; }
        [DataMember]
        public string train_date { get; set; }
        [DataMember]
        public string from_station_name { get; set; }
        [DataMember]
        public string from_station_code { get; set; }
        [DataMember]
        public string to_station_name { get; set; }
        [DataMember]
        public string to_station_code { get; set; }
        //[DataMember]
        //public float refund_money { get; set; }
    }
    [DataContract]
    public class Passengers
    {
        [DataMember]
        public int passengerid { get; set; }
        [DataMember]
        public string passengersename { get; set; }
        [DataMember]
        public int piaotype { get; set; }
        [DataMember]
        public string piaotypename { get; set; }
        [DataMember]
        public string passporttypeseid { get; set; }
        [DataMember]
        public string passporttypeseidname { get; set; }
        [DataMember]
        public string passportseno { get; set; }
        [DataMember]
        public string price { get; set; }
        [DataMember]
        public string zwcode { get; set; }
        [DataMember]
        public string zwname { get; set; }
        [DataMember]
        public int reason { get; set; }
        [DataMember]
        public string ticket_no { get; set; }
        [DataMember]
        public string cxin { get; set; }
    }
}
