using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TModel;
using TBLL;
using System.Net;
using System.Text;
using System.IO;
using System.Web.Helpers;
using System.Net.Http;
using Newtonsoft.Json;

namespace Ticket.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            //if(Session["time"] == null)
            //{
            //    Session["time"] = TempData["time"];
            //    ViewBag.time = Session["time"];
            //}else ViewBag.time = Session["time"];
            ViewBag.time = TempData["time"];
            //if (Session["no"] == null)
            //{
            //    Session["no"] = TempData["no"];
            //    ViewBag.no = Session["no"];
            //}
            //else ViewBag.no = Session["no"];
            ViewBag.no = TempData["no"];
            //if (Session["froms"] == null)
            //{
            //    Session["froms"] = TempData["froms"];
            //    ViewBag.from = Session["froms"];
            //}
            //else ViewBag.from = Session["froms"];
            ViewBag.from = TempData["froms"];
            //if (Session["tos"] == null)
            //{
            //    Session["tos"] = TempData["tos"];
            //    ViewBag.to = Session["tos"];
            //}
            //else ViewBag.to = Session["tos"];
            ViewBag.to = TempData["tos"];
            //if (Session["seat"] == null)
            //{
            //    Session["seat"] = TempData["seat"];
            //    ViewBag.seat = Session["seat"];
            //}
            //else ViewBag.seat = Session["seat"];
            ViewBag.seat = TempData["seat"];
            //if (Session["seatcode"] == null)
            //{
            //    Session["seatcode"] = TempData["seatcode"];
            //    ViewBag.seatcode = Session["seatcode"];
            //}
            //else ViewBag.seatcode = Session["seatcode"];
            ViewBag.seatcode = TempData["seatcode"];
            //if (Session["price"] == null)
            //{
            //    Session["price"] = TempData["price"];
            //    ViewBag.price = Session["price"];
            //}else ViewBag.price = Session["price"];
            ViewBag.price= TempData["price"];
            //if (Session["stime"] == null)
            //{
            //    Session["stime"] = TempData["stime"];
            //    ViewBag.stime = Session["stime"];
            //}
            //else ViewBag.stime = Session["stime"];
            ViewBag.stime =TempData["stime"];
            //if (Session["etime"] == null)
            //{
            //    Session["etime"] = TempData["etime"];
            //    ViewBag.etime = Session["etime"];
            //}
            //else ViewBag.etime = Session["etime"];
            ViewBag.etime = TempData["etime"];
            //if (Session["rtime"] == null)
            //{
            //    Session["rtime"] = TempData["rtime"];
            //    ViewBag.rtime = Session["rtime"];
            //}
            //else ViewBag.rtime = Session["rtime"];
            ViewBag.rtime = TempData["rtime"];
            //if (Session["tname"] == null)
            //{
            //    Session["tname"] = TempData["tname"];
            //    ViewBag.tname = Session["tname"];
            //}
            //else ViewBag.tname = Session["tname"];
            ViewBag.tname = TempData["tname"];
            return View();
        }
        //抢票订单界面
        public ActionResult TicketRb()
        {
            ViewBag.time = TempData["time"];
            ViewBag.no = TempData["no"];
            ViewBag.from = TempData["froms"];
            ViewBag.to = TempData["tos"];
            ViewBag.seat = TempData["seat"];
            ViewBag.seatcode = TempData["seatcode"];
            ViewBag.price = TempData["price"];
            ViewBag.stime = TempData["stime"];
            ViewBag.etime = TempData["etime"];
            ViewBag.rtime = TempData["rtime"];
            ViewBag.tname = TempData["tname"];
            return View();
        }
        //生成抢票订单
        public string CreateOrder(string from ,string stime,string etime,string to,string date, string trainname,string seat,string p1name,string card1,string id1,string year1,string month1,string day1,string phone,float price,float total,float tip)
        {
            OrderList orderlist = new OrderList();
            orderlist.Sn= new BillMaxNumManager().GetMaxNumByBillType("OrderList");
            TempData["sn"] = orderlist.Sn;
            orderlist.Type = "0";
            orderlist.Cdate = DateTime.Now;
            orderlist.Pdate = date;
            orderlist.Pfrom = from;
            orderlist.Pstime = stime;
            orderlist.Petime = etime;
            orderlist.Pto = to;
            orderlist.Ptrain = trainname;
            orderlist.Pseat = seat;
            orderlist.Phone = phone;
            TempData["phone"] = phone;
            orderlist.Price = price;
            orderlist.Passager = 1;
            orderlist.Total = total;
            orderlist.State = 10;
            orderlist.Tip = tip;
            orderlist.Contacts = p1name;
            //不填默认
            orderlist.Isposs = 0;
            orderlist.Adress = " ";
            orderlist.Dprice = 0;
            //--------------
            OdPassagers odpassagers = new OdPassagers();
            odpassagers.Odnum = orderlist.Sn;
            odpassagers.Psn = 1;
            odpassagers.Name = p1name;
            odpassagers.Card = card1;
            odpassagers.Cnum = id1;
            string bday = year1+'-' + month1.ToString().PadLeft(2, '0')+'-' + day1.ToString().PadLeft(2, '0');
            odpassagers.Bday = bday;
            odpassagers.State = 0;
            int f1 = new OrderListManager().AddOrderList(orderlist);
            int f2 = new OdPassagersManager().AddOdPassagers(odpassagers);
            if (f1 > 0 && f2 > 0)
            {
                int f = new BillMaxNumManager().UpdateMaxNum("OrderList", orderlist.Sn.Substring(1));
                return "true";
            }
            else
                return "false";
        }
        //显示订单列表
        public ActionResult OrderLists()
        {
            //string sn = "R20181013002";
            //string phone = "13625458457";
            if (TempData["sn"] != null)
            {
                string sn = TempData["sn"].ToString();
                ViewBag.sn = sn;
                if (TempData["phone"] != null)
                {
                    string phone = TempData["phone"].ToString();
                    ViewBag.phone = phone;
                }
                List<OrderList> orderlists = new OrderListManager().GetOrderListsByOnum(sn);
                List<OdPassagers> odpassagers = new OdPassagersManager().GetOdpassagersByOnum(sn);
                ViewData["ol"] = orderlists;
                ViewData["op"] = odpassagers;
            }
            else
            {
                List<OrderList> orderlists = new List<OrderList>();
                List<OdPassagers> odpassagers = new List<OdPassagers>();
                ViewData["ol"] = orderlists;
                ViewData["op"] = odpassagers;
            }
            return View();
        }
        //根据条件查询抢票订单        
        public JsonResult JSOrderLists(string num)
        {
            List<OrderList> orderlists = new List<OrderList>();
            orderlists= new OrderListManager().GetOrderListsByOnum(num);
            return Json(orderlists,JsonRequestBehavior.AllowGet);
        }
        //点击订单显示乘客信息        
        public JsonResult JSOdPassagers(string num)
        {
            List<OdPassagers> odpassagers = new List<OdPassagers>();
            odpassagers = new OdPassagersManager().GetOdpassagersByOnum(num);
            return Json(odpassagers, JsonRequestBehavior.AllowGet);
        }
        //生成聚合订单并提交
        public JsonResult SubJhOd(OrderList orderlistobj, List<OdPassagers> passagers)
        {
            OrderList orderlist = new OrderList();
            orderlist.Sn = new BillMaxNumManager().GetMaxNumByBillType("OrderList");
            orderlist.State = 0;
            orderlist.Type = "1";
            orderlist.Cdate= DateTime.Now;
            orderlist.Pdate = orderlistobj.Pdate;            
            orderlist.Ptrain = orderlistobj.Ptrain;
            orderlist.Pfrom = orderlistobj.Pfrom;
            orderlist.Pstime = orderlistobj.Pstime;
            orderlist.Pto = orderlistobj.Pto;
            orderlist.Petime = orderlistobj.Petime;
            orderlist.Pseat = orderlistobj.Pseat;
            orderlist.Price = orderlistobj.Price;
            orderlist.Dprice = orderlistobj.Dprice;
            orderlist.Passager = orderlistobj.Passager;
            orderlist.Isposs = orderlistobj.Isposs;
            orderlist.Possprice = orderlistobj.Possprice;
            orderlist.Total = orderlistobj.Total;
            orderlist.Contacts = orderlistobj.Contacts;
            orderlist.Phone = orderlistobj.Phone;
            orderlist.Adress = orderlistobj.Adress;
            int flag1 = new OrderListManager().AddOrderList(orderlist);
            List<JhPassager> jps = new List<JhPassager>();
            for(int i=0;i< passagers.Count;i++)
            {
                OdPassagers odpassager = new OdPassagers();
                odpassager.Odnum = orderlist.Sn;
                odpassager.Psn = passagers[i].Psn;
                odpassager.Name = passagers[i].Name;
                odpassager.Card = passagers[i].Card;
                odpassager.Cnum = passagers[i].Cnum;
                odpassager.Bday = "";
                odpassager.State = 0;
                int flag2 = new OdPassagersManager().AddOdPassagers(odpassager);
                //把乘客转换为聚合要求的格式
                JhPassager jp = new JhPassager();
                jp.passengerid = passagers[i].Psn;
                jp.passengersename = passagers[i].Name;
                jp.piaotype = 1;
                jp.piaotypename = "成人票";                
                switch (passagers[i].Card)
                { 
                    case "二代身份证":
                        jp.passporttypeseid = "1";
                        break;
                    case "护照":
                        jp.passporttypeseid = "B";
                        break;
                    case "台湾通行证":
                        jp.passporttypeseid = "G";
                        break;
                    case "港澳通行证":
                        jp.passporttypeseid = "C";
                        break;
                }
                jp.passporttypeseidname = passagers[i].Card;
                jp.passportseno = passagers[i].Cnum;
                jp.price = orderlistobj.Price;
                switch (orderlistobj.Pseat)
                {
                    case "商务座":
                        jp.zwcode = "9";
                        break;
                    case "一等座":
                        jp.zwcode = "M";
                        break;
                    case "二等座":
                        jp.zwcode = "O";
                        break;
                    case "高级软卧":
                        jp.zwcode = "6";
                        break;
                    case "软卧":
                        jp.zwcode = "4";
                        break;
                    case "动卧":
                        jp.zwcode = "F";
                        break;
                    case "硬卧":
                        jp.zwcode = "3";
                        break;
                    case "软座":
                        jp.zwcode = "2";
                        break;
                    case "硬座":
                        jp.zwcode = "1";
                        break;
                    case "无座":
                        jp.zwcode = "1";
                        break;
                }
                if (orderlistobj.Pseat == "无座")
                    jp.zwname = "硬座";
                else
                    jp.zwname = orderlistobj.Pseat;
                jps.Add(jp);
            }
            int f = new BillMaxNumManager().UpdateMaxNum("OrderList", orderlist.Sn.Substring(1));
            string jspassager = Newtonsoft.Json.JsonConvert.SerializeObject(jps);
            string str = "key=750f09b37a18dfd05d5980680be18759&user_orderid={0}&train_date={1}&from_station_code={2}&to_station_code={3}&checi={4}&passengers={5}";
            string fromc = new CityCodeManager().GetCityCodeBySql("select * from CityCode where name='" + orderlist.Pfrom + "'")[0].Code.ToString();
            string toc = new CityCodeManager().GetCityCodeBySql("select * from CityCode where name='" + orderlist.Pto + "'")[0].Code.ToString();
            str = string.Format(str, orderlist.Sn, orderlist.Pdate, fromc, toc, orderlist.Ptrain, jspassager);
            string url = "http://op.juhe.cn/trainTickets/submit";
            string result = Post_Http(url, str, "utf-8");
            Jhodreturn jsons = JsonConvert.DeserializeObject<Jhodreturn>(result);
            if (jsons.error_code == "0")
            {
                int flag2 = new OrderListManager().UpdateBySql("update OrderList set jhorderid='" + jsons.result.orderid + "',ecode='0',reason='"+jsons.reason+"' where sn='"+orderlist.Sn+"'");
            }
            else
            {
                int flag3= new OrderListManager().UpdateBySql("update OrderList set state='1',ecode='"+jsons.error_code+"',reason='" + jsons.reason + "' where sn='" + orderlist.Sn + "'");
            }
            //传递页面值：单据号
            TempData["order"] = orderlist.Sn;
            Session["order"] = null;
            return Json(jsons);
            //return Json(jspassager, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 以POST方式抓取远程页面内容
        /// </summary>
        /// <param name="url">请求的url</param>
        /// <param name="postData">参数列表</param>
        /// <param name="encodeType">编码类型</param>
        /// <returns></returns>
        public static string Post_Http(string url, string postData, string encodeType)
        {
            HttpWebResponse myResponse = null;
            string strResult = null;
            try
            {
                Encoding encoding = Encoding.GetEncoding(encodeType);
                byte[] POST = encoding.GetBytes(postData);
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Method = "POST";
                myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.ContentLength = POST.Length;
                Stream newStream = myRequest.GetRequestStream();
                newStream.Write(POST, 0, POST.Length); //设置POST
                newStream.Close();
                // 获取结果数据
                myResponse = (HttpWebResponse)myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.GetEncoding(encodeType));
                strResult = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                strResult = ex.Message;
            }
            finally
            {
                if (myResponse != null)
                    myResponse.Close();
            }
            return strResult;
        }
        //聚合订单付款界面
        public ActionResult JhOrderPay()
        {
            string od;
            if (TempData["order"] != null)
            {
                od = TempData["order"].ToString();
                Session["order"] = od;
            }
            else if(Session["order"]==null)
            {
                od = null;
                Response.Redirect("/Order/JhOrderLists");}
            else
            {od = Session["order"].ToString();}
            //查询单据信息
            string sql = "select * from OrderList where sn='" + od + "'";
            List<OrderList> orderlists = new OrderListManager().GetOrderListsBySql(sql);
            ViewBag.order = od;
            ViewBag.cdate = orderlists[0].Cdate.ToString("yyyy-MM-dd HH:mm:ss");
            ViewBag.train = orderlists[0].Ptrain;
            ViewBag.from = orderlists[0].Pfrom;
            ViewBag.to = orderlists[0].Pto;
            ViewBag.date = orderlists[0].Pdate;
            ViewBag.stime = orderlists[0].Pstime;
            ViewBag.etime = orderlists[0].Petime;
            ViewBag.total = orderlists[0].Total;
            return View();
        }
        //获取聚合订单状态显示于界面
        public JsonResult JhOrderState(string sn)
        {
            //string url = "http://op.juhe.cn/trainTickets/orderStatus?key=750f09b37a18dfd05d5980680be18759&orderid={0}";
            //string jhorderid = new OrderListManager().GetOrderListsBySql("select * from orderlist where sn='" + sn + "'")[0].Jhorderid.ToString();
            //url = string.Format(url, jhorderid);
            //string str = HomeController.GetRemoteHtmlCodeByEncoding(url, "utf-8");
            ////反序列化处理,将JSON字符串转换成对象，可进行对象处理
            //JhOrderState jstr = JsonConvert.DeserializeObject<JhOrderState>(str);
            JhOrderState jstr = rjhodst(sn);
            //当提交订单后把聚合处理1或者2的状态更新到数据库(刷新界面，如果不是0状态，即处理完的单据，先判断是不是已经更新过后台数据库)
            int a = new OrderListManager().GetOrderListsBySql("select * from OrderList where sn='" + sn + "'")[0].State;
            if (a == 0)
            {
                if (jstr.result.status == "1")
                {
                    //更新单据状态
                    int flag1 = new OrderListManager().UpdateBySql("update OrderList set state=1,Remarks='" + jstr.result.msg + "' where sn='" + sn + "'");
                    //更新乘客列表状态
                    int flag2 = new OdPassagersManager().UpdateBySql("update OdPassagers set state=1 where odnum='" + sn + "'");
                }
                if (jstr.result.status == "2")
                {
                    //更新单据状态
                    int flag1 = new OrderListManager().UpdateBySql("update OrderList set state=2,ordernumber='" + jstr.result.ordernumber + "',orderamount='" + jstr.result.orderamount + "',Remarks='" + jstr.result.msg + "' where sn='" + sn + "'");
                    //更新乘客列表状态
                    for (int i = 0; i < jstr.result.passengers.Count; i++)
                    {
                        int flag2 = new OdPassagersManager().UpdateBySql("update OdPassagers set state=2,price='" + jstr.result.passengers[i].price + "',ticket_no='" + jstr.result.passengers[i].ticket_no + "',cxin='" + jstr.result.passengers[i].cxin + "' where odnum='" + sn + "' and psn='" + jstr.result.passengers[i].passengerid + "'");
                    }
                }
            }
            return Json(jstr);
        }
        //返回聚合订单信息
        public JhOrderState rjhodst(string sn)
        {
            string url = "http://op.juhe.cn/trainTickets/orderStatus?key=750f09b37a18dfd05d5980680be18759&orderid={0}";
            string jhorderid = new OrderListManager().GetOrderListsBySql("select * from orderlist where sn='" + sn + "'")[0].Jhorderid.ToString();
            url = string.Format(url, jhorderid);
            string str = HomeController.GetRemoteHtmlCodeByEncoding(url, "utf-8");
            //反序列化处理,将JSON字符串转换成对象，可进行对象处理
            JhOrderState jstr = JsonConvert.DeserializeObject<JhOrderState>(str);
            return jstr;
        }
        //客户查询聚合订单界面
        public ActionResult JhOrderLists()
        {
            List<OrderList> orderlists = new List<OrderList>();
            List<OdPassagers> odpassagers = new List<OdPassagers>();
            ViewData["ol"] = orderlists;
            ViewData["op"] = odpassagers;
            return View();
        }
        //聚合订单查询功能
        public JsonResult JhOrderListsSk(string num)
        {
            List<OrderList> orderlists = new OrderListManager().GetJhOrderListsByOnum(num);
            return Json(orderlists, JsonRequestBehavior.AllowGet);
        }
        //查询聚合订单状态
        public string JhOrderStateSk(string num)
        {
            TempData["order"] = num;            
            return "true";
        }
        //付款
        public string PayOrder(string num,string total)
        {
            string url = "http://op.juhe.cn/trainTickets/pay?key=750f09b37a18dfd05d5980680be18759&orderid={0}";
            //string jhorderid = new OrderListManager().GetOrderListsBySql("select * from orderlist where sn='" + num + "'")[0].Jhorderid.ToString();
            string jhorderid = "JZ154097547717948";
            url = string.Format(url, jhorderid);
            string str = HomeController.GetRemoteHtmlCodeByEncoding(url, "utf-8");
            string reason = str.Substring(str.IndexOf("\"reason\":") + 10, (str.LastIndexOf(",\"result\"") - str.IndexOf("\"reason\":") - 10));
            string result = str.Substring(str.IndexOf("\"result\":") + 10, (str.LastIndexOf(",\"error_code\"") - str.IndexOf("\"result\":") - 10));
            string error_code = str.Substring(str.IndexOf("\"error_code\":") + 13, (str.LastIndexOf("}") - str.IndexOf("\"error_code\":") - 13));
            return str;
            //return num;
        }
        //取消待付款订单
        public string CancleJhOrder2(string num)
        {
            JhOrderState jstr = rjhodst(num);
            string s = jstr.result.status;
            if(s=="2")
            {
                //聚合取消待支付订单接口
                string url = "http://op.juhe.cn/trainTickets/cancel?orderid={0}&key=750f09b37a18dfd05d5980680be18759";
                //string jhorderid = new OrderListManager().GetOrderListsBySql("select * from orderlist where sn='" + num + "'")[0].Jhorderid.ToString();
                url = string.Format(url, jstr.result.orderid);
                string str = HomeController.GetRemoteHtmlCodeByEncoding(url, "utf-8");
                string ecode = str.Substring(str.IndexOf("\"error_code\":")+13,(str.LastIndexOf("}")- str.IndexOf("\"error_code\":") - 13));
                str = str.Substring(str.IndexOf("\"msg\":") + 7, (str.LastIndexOf("\",\"status\":") - str.IndexOf("\"msg\":") - 7));
                if (ecode == "0")
                {
                    int flag1 = new OrderListManager().UpdateBySql("update OrderList set state=1,Remarks='用户取消订单' where sn='" + num + "'");
                    int flag2 = new OdPassagersManager().UpdateBySql("update OdPassagers set state=1 where sn='" + num + "'");
                }
                return str;
            }
            else
            {
                return "false";
            }            
        }
        //创建回调接口
        //public string RpMessage(string data)
        //{
        //}
    }
}