using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TModel;
using TDAL;
namespace TBLL
{
    public class BillMaxNumManager
    {
        //获取新单据编号
        public string GetMaxNumByBillType(string type)
        {
            string maxnum;
            BillMaxNumServer sr = new BillMaxNumServer();
            BillMaxNum billmaxnum = sr.GetMaxNumByBillType(type);
            string dt = DateTime.Now.ToString("yyyyMMdd");
            if (billmaxnum == null)
            {
                return "false";
            }
            else
            {
                if (billmaxnum.MaxNum == "0")
                    maxnum = dt + "001";
                else
                {
                    string s = billmaxnum.MaxNum.Substring(0, 8);
                    if (dt == s)
                    {
                        maxnum = (Convert.ToInt64(billmaxnum.MaxNum) + 1).ToString();
                        //maxnum = s + n;
                    }                        
                    else
                        maxnum = dt + "001";
                }               
                //int flag = sr.UpdateMaxNum(billmaxnum.Id, maxnum);
                return billmaxnum.TypeCode + maxnum;                
            }      
        }
        //更新最大号码
        public int UpdateMaxNum(string type,string maxnum)
        {
            return new BillMaxNumServer().UpdateMaxNum(type, maxnum);
        }
    }
}
