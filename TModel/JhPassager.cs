using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TModel
{
    [DataContract]
    public class JhPassager
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
        public float price { get; set; }
        [DataMember]
        public string zwcode { get; set; }
        [DataMember]
        public string zwname { get; set; }
    }
}
