using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDAL;
using TModel;

namespace TBLL
{
    public class OrderListManager
    {
        //增加新订单
        public int AddOrderList(OrderList orderlist)
        {
            int flag = new OrderListServer().AddOrderList(orderlist);
            return flag;
        }
        //通过SQL获取订单列表
        public List<OrderList> GetOrderListsBySql(string sql)
        {
            List<OrderList> orderlists = new List<OrderList>();
            orderlists = new OrderListServer().GetOrderListsBySql(sql);
            return orderlists;
        }
        //通过单据号查询唯一单据
        public List<OrderList> GetOrderListsByOnum(string num)
        {
            List<OrderList> orderlists = new List<OrderList>();
            orderlists = (from s in new OrderListServer().GetAllOrderList()
                              where s.Sn == num && s.Type=="0"
                          select s).ToList<OrderList>();
            return orderlists;
        }
        //通过单据号查询唯一单据
        public List<OrderList> GetJhOrderListsByOnum(string num)
        {
            List<OrderList> orderlists = new List<OrderList>();
            orderlists = (from s in new OrderListServer().GetAllOrderList()
                          where s.Sn == num && s.Type == "1"
                          select s).ToList<OrderList>();
            return orderlists;
        }
        //订单管理初始化为未接单列表
        public  List<OrderList> Indexs()
        {
            List<OrderList> orderlists = (from s in new OrderListServer().GetAllOrderList()
                                          where s.State==10 && s.Type=="0"
                                          select s).ToList<OrderList>();
            return orderlists;
        }
        //订单管理初始化为已接单列表
        public List<OrderList> Indexs1()
        {
            List<OrderList> orderlists = (from s in new OrderListServer().GetAllOrderList()
                                          where s.State == 11 && s.Type=="0"
                                          select s).ToList<OrderList>();
            return orderlists;
        }
        //订单管理查询单据
        public List<OrderList> GetOrderListsLikeOnum(string num)
        {
            List<OrderList> orderlists = new List<OrderList>();
            orderlists = (from s in new OrderListServer().GetAllOrderList()
                          where s.Sn.Contains(num)&&s.State==11 &&s.Type=="0"
                          select s).ToList<OrderList>();
            return orderlists;
        }
        //查询完结订单
        public List<OrderList> GetOverList(string num)
        {
            List<OrderList> orderlists = (from s in new OrderListServer().GetAllOrderList()
                                          where s.Sn.Contains(num)&&s.State==13 && s.Type=="0"
                                          select s).ToList<OrderList>();
            return orderlists;
        }
        //更新原显示未接单的数据，并获取其他未接单的单据
        public List<OrderList> GetUDOrderLists(List<string> ay)
        {
            List<OrderList> orderlists = new OrderListServer().GetAllOrderListByay(ay);
            return orderlists;
        }
        //按接单后更新单据状态为已接单
        public int UpdateStateByNum(string num)
        {
            int flag = new OrderListServer().UpdateStateByNum(num);
            return flag;
        }
        //更新单据状态为已完成
        public int UpdateStateByNum3(string num)
        {
            int flag = new OrderListServer().UpdateStateByNum3(num);
            return flag;
        }
        //通过Sql更新表数据
        public int UpdateBySql(string sql)
        {
            int flag = new OrderListServer().UpdateBySql(sql);
            return flag;
        }
    }
}
