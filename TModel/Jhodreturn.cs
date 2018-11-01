using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TModel
{
    [DataContract]
    public class Jhodreturn
    {
        [DataMember]
        public string reason { get; set; }
        [DataMember]
        public results result { get; set; }
        [DataMember]
        public string error_code { get; set; }
    }
    [DataContract]
    public class results
    {
        [DataMember]
        public string orderid { get; set; }
    }
}
