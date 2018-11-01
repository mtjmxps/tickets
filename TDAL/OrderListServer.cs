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
    public class OrderListServer
    {
        string conStr = SQLHelper.connStr;
        public int AddOrderList(OrderList orderlist)
        {
            string sql = "insert into OrderList(sn,state,type,cdate,pdate,pfrom,pstime,pto,petime,ptrain,pseat,Contacts,phone,price,dprice,passager,isposs,possprice,total,tip,adress) values(@sn,@state,@type,@cdate,@pdate,@pfrom,@pstime,@pto,@petime,@ptrain,@pseat,@Contacts,@phone,@price,@dprice,@passager,@isposs,@possprice,@total,@tip,@adress)";
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@sn",orderlist.Sn),new SqlParameter("@state",orderlist.State),new SqlParameter("@type",orderlist.Type),new SqlParameter("@cdate", orderlist.Cdate),
                new SqlParameter("@pdate",orderlist.Pdate),new SqlParameter("@pfrom",orderlist.Pfrom),new SqlParameter("@pstime",orderlist.Pstime),new SqlParameter("@pto",orderlist.Pto),new SqlParameter("@petime",orderlist.Petime),
                new SqlParameter("@ptrain",orderlist.Ptrain),new SqlParameter("@pseat",orderlist.Pseat),new SqlParameter("@Contacts",orderlist.Contacts),new SqlParameter("@phone",orderlist.Phone),
                new SqlParameter("@price",orderlist.Price),new SqlParameter("@dprice",orderlist.Dprice),new SqlParameter("@passager",orderlist.Passager),new SqlParameter("@isposs",orderlist.Isposs),new SqlParameter("@possprice",orderlist.Possprice),new SqlParameter("@total",orderlist.Total),
                new SqlParameter("tip",orderlist.Tip),new SqlParameter("@adress",orderlist.Adress)
            };
            int flag = SQLHelper.ExecuteNonQuery(conStr, CommandType.Text, sql, para);
            return flag;
        }
        public List<OrderList> GetOrderListsBySql(string sql)
        {
            List<OrderList> list = new List<OrderList>();
            DataSet ds = SQLHelper.ExecuteDataset(conStr, CommandType.Text, sql);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    OrderList orderlist = new OrderList();
                    orderlist.Id = (int)dr["id"];
                    orderlist.Sn = (string)dr["sn"];
                    orderlist.State = (int)dr["state"];
                    orderlist.Cdate = (DateTime)dr["cdate"];
                    orderlist.Pdate = (string)dr["pdate"];
                    orderlist.Pfrom = (string)dr["pfrom"];
                    orderlist.Pto = (string)dr["pto"];
                    orderlist.Pstime = (string)dr["pstime"];
                    orderlist.Petime = (string)dr["petime"];
                    orderlist.Ptrain = (string)dr["ptrain"];
                    orderlist.Pseat = (string)dr["pseat"];
                    orderlist.Phone = (string)dr["phone"];
                    orderlist.Price = Convert.ToSingle(dr["price"]);
                    orderlist.Passager = (int)dr["passager"];
                    orderlist.Total = Convert.ToSingle(dr["total"]);
                    orderlist.Tip = Convert.ToSingle(dr["tip"]);
                    orderlist.Jhorderid = (string)dr["jhorderid"];
                    list.Add(orderlist);
                }
            }
            return list;
        }
        public List<OrderList> GetAllOrderList()
        {
            List<OrderList> list = new List<OrderList>();
            string sql = "select o.*,b.name as name from OrderList o left join BillState b on o.state=b.code";
            DataSet ds = SQLHelper.ExecuteDataset(conStr, CommandType.Text, sql);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    OrderList orderlist = new OrderList();
                    orderlist.Id = (int)dr["id"];
                    orderlist.Sn = (string)dr["sn"];
                    orderlist.State = (int)dr["state"];
                    orderlist.Type = (string)dr["type"];
                    orderlist.Cdate = (DateTime)dr["cdate"];
                    orderlist.Pdate = (string)dr["pdate"];
                    orderlist.Pfrom = (string)dr["pfrom"];
                    orderlist.Pto = (string)dr["pto"];
                    orderlist.Ptrain = (string)dr["ptrain"];
                    orderlist.Pseat = (string)dr["pseat"];
                    orderlist.Phone = (string)dr["phone"];
                    orderlist.Price = Convert.ToSingle(dr["price"]);
                    orderlist.Passager = (int)dr["passager"];
                    orderlist.Total =Convert.ToSingle(dr["total"]);
                    orderlist.Tip = Convert.ToSingle(dr["tip"]);
                    orderlist.billstate = new BillState();
                    orderlist.billstate.Name= dr["name"].ToString();
                    list.Add(orderlist);
                }
            }
            return list;
        }
        public string Nvl(string a, string b)
        {
            if (a == "")
                return b;
            else
                return a;       
        }
        //返回原查询界面单据列表中已接单单据，与新增的未接单单据
        public List<OrderList> GetAllOrderListByay(List<string> ay)
        {
            string str = "";
            string sql = "";
            string sql1 = "";            
            List<OrderList> list = new List<OrderList>();
            //必须先判断集合是否为空，再引用count
            if (ay!=null)
            {
                for (int i = 0; i < ay.Count; i++)
                {
                    str += ay[i] + "'" + "," + "'";
                }
                str = str.Substring(0, str.Length - 3);
                //搜索界面上需要删除的单据信息
                sql = "select o.*,b.name as name from OrderList o left join BillState b on o.state=b.code where o.state<>10 and o.sn in('" + str + "') and o.type='0'";
                //搜索新增的单据信息
                sql1 = "select o.*,b.name as name from OrderList o left join BillState b on o.state=b.code where o.state=10 and o.sn not in('" + str + "') and o.type='0'";
                DataSet ds = SQLHelper.ExecuteDataset(conStr, CommandType.Text, sql);
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        OrderList orderlist = new OrderList();
                        orderlist.Id = (int)dr["id"];
                        orderlist.Sn = (string)dr["sn"];
                        orderlist.State = (int)dr["state"];
                        orderlist.Cdate = (DateTime)dr["cdate"];
                        orderlist.Pdate = (string)dr["pdate"];
                        orderlist.Pfrom = (string)dr["pfrom"];
                        orderlist.Pto = (string)dr["pto"];
                        orderlist.Ptrain = (string)dr["ptrain"];
                        orderlist.Pseat = (string)dr["pseat"];
                        orderlist.Phone = (string)dr["phone"];
                        orderlist.Price = Convert.ToSingle(dr["price"]);
                        orderlist.Passager = (int)dr["passager"];
                        orderlist.Total = Convert.ToSingle(dr["total"]);
                        orderlist.Tip = Convert.ToSingle(dr["tip"]);
                        orderlist.billstate = new BillState();
                        orderlist.billstate.Name = dr["name"].ToString();
                        list.Add(orderlist);
                    }
                }
                DataSet ds1 = SQLHelper.ExecuteDataset(conStr, CommandType.Text, sql1);
                if (ds1.Tables.Count > 0)
                {
                    DataTable dt = ds1.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        OrderList orderlist = new OrderList();
                        orderlist.Id = (int)dr["id"];
                        orderlist.Sn = (string)dr["sn"];
                        orderlist.State = (int)dr["state"];
                        orderlist.Cdate = (DateTime)dr["cdate"];
                        orderlist.Pdate = (string)dr["pdate"];
                        orderlist.Pfrom = (string)dr["pfrom"];
                        orderlist.Pto = (string)dr["pto"];
                        orderlist.Ptrain = (string)dr["ptrain"];
                        orderlist.Pseat = (string)dr["pseat"];
                        orderlist.Phone = (string)dr["phone"];
                        orderlist.Price = Convert.ToSingle(dr["price"]);
                        orderlist.Passager = (int)dr["passager"];
                        orderlist.Total = Convert.ToSingle(dr["total"]);
                        orderlist.Tip = Convert.ToSingle(dr["tip"]);
                        orderlist.billstate = new BillState();
                        orderlist.billstate.Name = dr["name"].ToString();
                        list.Add(orderlist);
                    }
                }
            }
            else
            {
                //如何开始界面上没有任何单据，直接查询新增的单据
                sql = "select o.*,b.name as name from OrderList o left join BillState b on o.state=b.code where o.state=10 and o.type='0'";
                DataSet ds = SQLHelper.ExecuteDataset(conStr, CommandType.Text, sql);
                if (ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        OrderList orderlist = new OrderList();
                        orderlist.Id = (int)dr["id"];
                        orderlist.Sn = (string)dr["sn"];
                        orderlist.State = (int)dr["state"];
                        orderlist.Cdate = (DateTime)dr["cdate"];
                        orderlist.Pdate = (string)dr["pdate"];
                        orderlist.Pfrom = (string)dr["pfrom"];
                        orderlist.Pto = (string)dr["pto"];
                        orderlist.Ptrain = (string)dr["ptrain"];
                        orderlist.Pseat = (string)dr["pseat"];
                        orderlist.Phone = (string)dr["phone"];
                        orderlist.Price = Convert.ToSingle(dr["price"]);
                        orderlist.Passager = (int)dr["passager"];
                        orderlist.Total = Convert.ToSingle(dr["total"]);
                        orderlist.Tip = Convert.ToSingle(dr["tip"]);
                        orderlist.billstate = new BillState();
                        orderlist.billstate.Name = dr["name"].ToString();
                        list.Add(orderlist);
                    }
                }
            }            
            return list;
        }
        //更新单据状态为已接单
        public int UpdateStateByNum(string num)
        {
            string sql = "update OrderList set state=11 where sn='" + num + "'";
            int flag = SQLHelper.ExecuteNonQuery(conStr, CommandType.Text, sql);
            return flag;
        }
        //更新单据状态为已完成
        public int UpdateStateByNum3(string num)
        {
            string sql = "update OrderList set state=13 where sn='" + num + "'";
            int flag = SQLHelper.ExecuteNonQuery(conStr, CommandType.Text, sql);
            return flag;
        }
        public int UpdateBySql(string sql)
        {
            int flag = SQLHelper.ExecuteNonQuery(conStr, CommandType.Text, sql);
            return flag;
        }
    }
}
