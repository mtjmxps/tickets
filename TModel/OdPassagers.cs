using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TModel
{
    public class OdPassagers
    {
        private int id;
        private string odnum;
        private string name;
        private string card;
        private string cnum;
        private string bday;
        private int state;
        private int psn;
        public int Id { get; set; }
        public string Odnum { get; set; }
        public string Name { get; set; }
        public string Card { get; set; }
        public string Cnum { get; set; }
        public string Bday { get; set; }
        public int State { get; set; }
        public int Psn { get; set; }
        public BillState billstate { get; set; }
        public float Price { get; set; }
        public string Ticket_no { get; set; }
        public string Cxin { get; set; }
    }
}
