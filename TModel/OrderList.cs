using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TModel
{
    public class OrderList
    {
        private int state;
        private int id;
        private string sn;
        private DateTime cdate;
        private string pdate;
        private string pfrom;
        private string pto;
        private string ptrain;
        private string pseat;
        private string phone;
        private string type;
        private float price;
        private int passager;
        private float total;
        private float tip;
        private int isnew;
        private string pstime;
        private string petime;
        private string contacts;
        private int isposs;
        private string adress;
        private float possprice;
        private string jhorderid; 
        public int State { get; set; }
        public int Id { get; set; }
        public string Sn { get; set; }
        public DateTime Cdate { get; set; }
        public string Pdate { get; set; }
        public string Pfrom { get; set; }
        public string Pto { get; set; }
        public string Ptrain { get; set; }
        public string Pseat { get; set; }
        public string Phone { get; set; }
        public string Type { get; set; }
        public float Price { get; set; }
        public float Dprice { get; set; }
        public int Passager { get; set; }
        public float Total { get; set; }
        public float Tip { get; set; }
        public int Isnew { get; set; }
        public string Pstime { get; set; }
        public string Petime { get; set; }
        public string Contacts { get; set; }
        public int Isposs { get; set; }
        public string Adress { get; set; }
        public float Possprice { get; set; }
        public string Rtime { get; set; }
        public List<OdPassagers> odpassagers { get; set; }
        public BillState billstate { get; set; }
        public string Jhorderid { get; set; }
        public string Remarks { get; set; }
    }
}
