using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TModel;

namespace TDAL
{
    public  class BillMaxNumServer
    {
        string conStr = SQLHelper.connStr;
        //获取相关单据类型的对象
        public BillMaxNum GetMaxNumByBillType(string type)
        {
            DataSet ds = SQLHelper.ExecuteDataset(conStr, CommandType.Text, "select * from BillMaxNum where BillName='" + type.Trim() + "'");
            if (ds.Tables[0].Rows.Count > 0)
            {
                BillMaxNum billmaxnum = new BillMaxNum();
                billmaxnum.Id = (int)ds.Tables[0].Rows[0]["id"];
                billmaxnum.TypeCode = ds.Tables[0].Rows[0]["TypeCode"].ToString();
                billmaxnum.MaxNum = ds.Tables[0].Rows[0]["MaxNum"].ToString();
                return billmaxnum;
            }                 
            else
                return null;
        }
        //更新单据最大单据号
        public int UpdateMaxNum(string type,string maxnum)
        {
            string sql = "update BillMaxNum set MaxNum='" + maxnum + "' where BillName='" + type + "'";
            int flag = SQLHelper.ExecuteNonQuery(conStr, CommandType.Text, sql);
            return flag;
        }
    }
}
