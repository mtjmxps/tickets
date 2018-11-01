using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TModel;
using TBLL;
using System.Collections;

namespace Ticket.Controllers
{
    public class TakeOrderController : Controller
    {
        // GET: TakeOrder界面初始化
        public ActionResult Index()
        {
            //获取未接单单据列表
            List<OrderList> orderlists = new OrderListManager().Indexs();
            List<OdPassagers> odpassagers = new List<OdPassagers>();
            //获取已接单列表
            List<OrderList> orderlists1 = new OrderListManager().Indexs1();
            ViewData["ol"] = orderlists;
            ViewData["op"] = odpassagers;
            ViewData["ol1"] = orderlists1;
            return View();
        }
        //根据条件查询抢票订单        
        public JsonResult JSOrderLists(string num)
        {
            List<OrderList> orderlists = new OrderListManager().GetOrderListsLikeOnum(num);
            return Json(orderlists, JsonRequestBehavior.AllowGet);
        }
        //刷新待接单列表
        public JsonResult JSOrderListsByay(List<string> ay)
        {
            List<OrderList> orderlists = new OrderListManager().GetUDOrderLists(ay);
            return Json(orderlists, JsonRequestBehavior.AllowGet);
        }
        //接单后更新单据状态
        public int Stateto1(string num)
        {
            int flag = new OrderListManager().UpdateStateByNum(num);
            return flag;
        }
        //更新单据状态已完成
        public int Stateto3(string num)
        {
            int flag = new OrderListManager().UpdateStateByNum3(num);
            return flag;
        }
        //已完结订单界面
        public ActionResult CkOverList()
        {
            string num = "";
            List<OrderList> orderlists = new OrderListManager().GetOverList(num);
            List<OdPassagers> odpassagers = new List<OdPassagers>();
            ViewData["ols"] = orderlists;
            ViewData["ops"] = odpassagers;
            return View();
        }
        //查询功能：已完结订单查询
        public JsonResult JsOverList(string num)
        {
            List<OrderList> orderlists = new OrderListManager().GetOverList(num);
            return Json(orderlists, JsonRequestBehavior.AllowGet);
        }
    }
}