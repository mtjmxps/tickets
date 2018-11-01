using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TModel;

namespace TDAL
{
    public class OdPassagersServer
    {
        string conStr = SQLHelper.connStr;
        public int AddOdPassagers(OdPassagers odpassagers)
        {
            string sql = "insert into OdPassagers(odnum,psn,name,card,cnum,bday,state) values(@odnum,@psn,@name,@card,@cnum,@bday,@state)";
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@odnum",odpassagers.Odnum),new SqlParameter("@psn",odpassagers.Psn),
                new SqlParameter("@name",odpassagers.Name),new SqlParameter("@card",odpassagers.Card),
                new SqlParameter("@cnum", odpassagers.Cnum),new SqlParameter("@bday",odpassagers.Bday),new SqlParameter("@state",odpassagers.State)
            };
            int flag = SQLHelper.ExecuteNonQuery(conStr, CommandType.Text, sql, para);
            return flag;
        }
        public List<OdPassagers> GetOdPassagersBySql(string sql)
        {
            List<OdPassagers> list = new List<OdPassagers>();
            DataSet ds = SQLHelper.ExecuteDataset(conStr, CommandType.Text, sql);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    OdPassagers odpassagers = new OdPassagers();
                    odpassagers.Id = (int)dr["id"];
                    odpassagers.Odnum = (string)dr["odnum"];
                    odpassagers.Name = (string)dr["name"];
                    odpassagers.Card = (string)dr["card"];
                    odpassagers.Cnum = (string)dr["cnum"];
                    odpassagers.Bday = (string)dr["bday"];
                    odpassagers.State = (int)dr["state"];
                    odpassagers.Psn = (int)dr["psn"];
                    list.Add(odpassagers);
                }
            }
            return list;
        }
        public List<OdPassagers> GetAllOdpassagers()
        {
            List<OdPassagers> list = new List<OdPassagers>();
            string sql = "select a.*,b.name bname from OdPassagers a left join BillState b on a.state=b.code";
            DataSet ds = SQLHelper.ExecuteDataset(conStr, CommandType.Text, sql);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    OdPassagers odpassager = new OdPassagers();
                    odpassager.Id = (int)dr["id"];
                    odpassager.Odnum = (string)dr["odnum"];
                    odpassager.Name = (string)dr["name"];
                    odpassager.Card = (string)dr["card"];
                    odpassager.Cnum = (string)dr["cnum"];
                    odpassager.Bday = (string)dr["bday"];
                    odpassager.billstate = new BillState();
                    odpassager.billstate.Name=(string)dr["bname"];
                    odpassager.Psn = (int)dr["psn"];
                    odpassager.Price = Convert.ToSingle(Nvl(dr["price"].ToString(), "0"));
                    odpassager.Ticket_no = Nvl(dr["ticket_no"].ToString(), "");
                    odpassager.Cxin = Nvl(dr["cxin"].ToString(), "");
                    list.Add(odpassager);
                }
            }
            return list;
        }
        //防止空值报错
        public string Nvl(string a, string b)
        {
            if (a == "")
                return b;
            else
                return a;
        }
        public int UpdateBySql(string sql)
        {
            int flag = SQLHelper.ExecuteNonQuery(conStr, CommandType.Text, sql);
            return flag;
        }
    }
}
