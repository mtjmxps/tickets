using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TModel;
using TBLL;

namespace Ticket.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult CkTicket()
        {
            //string url1 = "https://kyfw.12306.cn/otn/leftTicket/queryTicketPrice?train_no=5l0000D94190&from_station_no=01&to_station_no=05&seat_types=AOF&train_date=2018-10-20";
            ////url1 = string.Format(url1, "5500000Z9930", "01", "04", "113", "2018-10-20");
            ////url1 = string.Format(url1, "5l00000G9910", "01", "08", "OM9", "2018-10-20");
            //string str1 = GetRemoteHtmlCodeByEncoding(url1, "utf-8");
            //int a28 = str1.IndexOf("A3\":\"¥");
            //int b28 = str1.IndexOf("\"", a28 + 6);
            //ViewBag.ts = str1;
                //str1.Substring((a28 + 6), (b28 - a28 - 6));
            ViewBag.ddate= System.DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }
        //测试模型返回JSON
        //[HttpPost]
        //public JsonResult CkTicket(string from,string to,string time)
        //{
        //    List<Trainlist> trainlists = new List<Trainlist>();
        //    Trainlist tralist = new Trainlist();
        //    tralist.train_no = "G7571";
        //    tralist.train_type = "G";
        //    tralist.start_station = "苏州";
        //    tralist.start_station_type = "过";
        //    tralist.end_station = "温州南";
        //    tralist.end_station_type = "终";
        //    tralist.start_time = "15:38";
        //    tralist.end_time = "20:18";
        //    tralist.run_time = "4小时40分";
        //    List<Price_list> price_list = new List<Price_list>
        //        {
        //            new Price_list(){price_type = "二等座",price = "264.5"},
        //            new Price_list(){price_type = "一等座",price = "397.0"},
        //            new Price_list(){price_type = "特等座",price = "497.0"},
        //            new Price_list(){price_type = "商务座",price = "1097.0"}
        //        };
        //    tralist.price_list = price_list;
        //    trainlists.Add(tralist);
        //    Trainlist tralist1 = new Trainlist();
        //    tralist1.train_no = "G7585";
        //    tralist1.train_type = "G";
        //    tralist1.start_station = "苏州";
        //    tralist1.start_station_type = "过";
        //    tralist1.end_station = "温州南";
        //    tralist1.end_station_type = "终";
        //    tralist1.start_time = "16:39";
        //    tralist1.end_time = "21:16";
        //    tralist1.run_time = "4小时37分";
        //    List<Price_list> price_list1 = new List<Price_list>
        //        {
        //            new Price_list(){price_type = "特等座",price = "350.5"},
        //            new Price_list(){price_type = "一等座",price = "250.5"}
        //        };
        //    tralist1.price_list = price_list1;
        //    trainlists.Add(tralist1);
        //    //返回方法一
        //    return Json(trainlists);
        //    //返回方法二
        //    //JsonResult json = new JsonResult { Data = trainlists };
        //    //return Json(json);
        //}
        //抓取网页JSON数据--12306数据抓取接口
        [HttpPost]
        public JsonResult CkTicket(string date,string from ,string to)
        {            
            Session.Clear();
            string dates = date;
            string fromc = new CityCodeManager().GetCityCodeBySql("select * from CityCode where name='" + from + "'")[0].Code.ToString();
            string toc= new CityCodeManager().GetCityCodeBySql("select * from CityCode where name='" + to + "'")[0].Code.ToString();
            string urls = @"https://kyfw.12306.cn/otn/leftTicket/query?leftTicketDTO.train_date={0}&leftTicketDTO.from_station={1}&leftTicketDTO.to_station={2}&purpose_codes=ADULT";
            urls = string.Format(urls, dates, fromc, toc);
            string str = GetRemoteHtmlCodeByEncoding(urls, "utf-8");
            //第一次截取有用字符串
            int i = str.IndexOf("result");
            int x = str.IndexOf("[", i)+1;
            int y = str.IndexOf("]", i);
            int z = y - x;
            str = str.Substring(x, z);
            //第一次分割字符串（每数组元素为一个列车信息）
            string[] s1 = str.Split(',');
            List<Trainlist> trainlists = new List<Trainlist>();
            for(int a=0;a<s1.Length;a++)
            {
                //第二次截取有用字符串
                int j1 = s1[a].IndexOf("|") + 4;
                int j2 = s1[a].LastIndexOf('"') - j1;
                s1[a] = s1[a].Substring(j1, j2);
                //第二次分割字符串（每个元素为该列车明细信息）
                string[] s2 = s1[a].Split('|');
                Trainlist ts = new Trainlist();          
                ts.train_no = s2[0].ToString();
                ts.train_name = s2[1].ToString();
                //ts.start_station = s2[4].ToString();
                ts.start_station = new CityCodeManager().GetCityCodeBySql("select * from CityCode where code='"+ s2[4].ToString() + "'")[0].Name.ToString();
                //ts.end_station = s2[5].ToString();
                ts.end_station= new CityCodeManager().GetCityCodeBySql("select * from CityCode where code='" + s2[5].ToString() + "'")[0].Name.ToString();
                ts.start_time = s2[6].ToString();
                ts.end_time = s2[7].ToString();
                ts.run_time = s2[8].ToString();
                //ts.date = s2[11].ToString();
                ts.date= date;
                ts.start_station_no = s2[14].ToString();
                ts.end_station_no = s2[15].ToString();
                ts.seat_type = s2[32].ToString();
                //获取列车座位价格
                string url1 = "https://kyfw.12306.cn/otn/leftTicket/queryTicketPrice?train_no={0}&from_station_no={1}&to_station_no={2}&seat_types={3}&train_date={4}";
                url1 = string.Format(url1, ts.train_no, ts.start_station_no, ts.end_station_no, ts.seat_type, date);
                string str1 = GetRemoteHtmlCodeByEncoding(url1, "utf-8");
                List<Price_list> pls = new List<Price_list>();
                if (s2[19] != "")
                {
                    Price_list pl19 = new Price_list();
                    pl19.price_type = "高级软卧";
                    pl19.seat_code = "A6";
                    pl19.counts = s2[19].ToString();
                    int a19 = str1.IndexOf("A6\":\"¥");
                    if (a19 > 0)
                    {
                        int b19 = str1.IndexOf("\"", a19 + 6);
                        pl19.price = str1.Substring((a19 + 6), (b19 - a19 - 6));
                        pls.Add(pl19);
                    }
                }
                if (s2[21] != "")
                {
                    Price_list pl21 = new Price_list();
                    pl21.price_type = "软卧";
                    pl21.seat_code = "A4";
                    pl21.counts = s2[21].ToString();
                    int a21 = str1.IndexOf("A4\":\"¥");
                    if (a21 > 0)
                    {
                        int b21 = str1.IndexOf("\"", a21 + 6);
                        pl21.price = str1.Substring((a21 + 6), (b21 - a21 - 6));
                        pls.Add(pl21);
                    }
                }
                if (s2[24] != "")
                {
                    Price_list pl24 = new Price_list();
                    pl24.price_type = "无座";
                    pl24.seat_code = "WZ";
                    pl24.counts = s2[24].ToString();
                    int a24 = str1.IndexOf("WZ\":\"¥");
                    if (a24 > 0)
                    {
                        int b24 = str1.IndexOf("\"", a24 + 6);
                        pl24.price = str1.Substring((a24 + 6), (b24 - a24 - 6));
                        pls.Add(pl24);
                    }
                }
                if (s2[26] != "")
                {
                    Price_list pl26 = new Price_list();
                    pl26.price_type = "硬卧";
                    pl26.seat_code = "A3";
                    pl26.counts = s2[26].ToString();
                    int a26 = str1.IndexOf("A3\":\"¥");
                    if (a26 > 0)
                    {
                        int b26 = str1.IndexOf("\"", a26 + 6);
                        pl26.price = str1.Substring((a26 + 6), (b26 - a26 - 6));
                        pls.Add(pl26);
                    }
                }
                if (s2[27] != "")
                {
                    Price_list pl27 = new Price_list();
                    pl27.price_type = "硬座";
                    pl27.seat_code = "A1";
                    pl27.counts = s2[27].ToString();
                    int a27 = str1.IndexOf("A1\":\"¥");
                    if (a27 > 0)
                    {
                        int b27 = str1.IndexOf("\"", a27 + 6);
                        pl27.price = str1.Substring((a27 + 6), (b27 - a27 - 6));
                        pls.Add(pl27);
                    }
                }
                if (s2[28] != "")
                {
                    Price_list pl28 = new Price_list();
                    pl28.price_type = "二等座";
                    pl28.seat_code = "O";
                    pl28.counts = s2[28].ToString();
                    int a28 = str1.IndexOf("O\":\"¥");
                    if (a28 > 0)
                    {
                        int b28 = str1.IndexOf("\"", a28 + 5);
                        pl28.price = str1.Substring((a28 + 5), (b28 - a28 - 5));
                        pls.Add(pl28);
                    }
                }
                if (s2[29] != "")
                {
                    Price_list pl29 = new Price_list();
                    pl29.price_type = "一等座";
                    pl29.seat_code = "M";
                    pl29.counts = s2[29].ToString();
                    int a29 = str1.IndexOf("M\":\"¥");
                    if (a29 > 0)
                    {
                        int b29 = str1.IndexOf("\"", a29 + 5);
                        pl29.price = str1.Substring((a29 + 5), (b29 - a29 - 5));
                        pls.Add(pl29);
                    }
                }
                if (s2[30] != "")
                {
                    Price_list pl30 = new Price_list();
                    pl30.price_type = "商务座";
                    pl30.seat_code = "A9";
                    pl30.counts = s2[30].ToString();
                    int a30 = str1.IndexOf("A9\":\"¥");
                    if (a30 > 0)
                    {
                        int b30 = str1.IndexOf("\"", a30 + 6);
                        pl30.price = str1.Substring((a30 + 6), (b30 - a30 - 6));
                        pls.Add(pl30);
                    }
                }
                if (s2[31] != "")
                {
                    Price_list pl31 = new Price_list();
                    pl31.price_type = "动卧";
                    pl31.seat_code = "F";
                    pl31.counts = s2[31].ToString();
                    int a31 = str1.IndexOf("F\":\"¥");
                    if (a31 > 0)
                    {
                        int b31 = str1.IndexOf("\"", a31 + 5);
                        pl31.price = str1.Substring((a31 + 5), (b31 - a31 - 5));
                        pls.Add(pl31);
                    }
                }
                ts.price_list = pls;
                trainlists.Add(ts);
                ts = null;
                pls = null;
            }
            return Json(trainlists);
        }
        //通过聚合接口返回列车数据
        public JsonResult JhGetTicket(string date,string from ,string to)
        {
            string fromc = new CityCodeManager().GetCityCodeBySql("select * from CityCode where name='" + from + "'")[0].Code.ToString();
            string toc = new CityCodeManager().GetCityCodeBySql("select * from CityCode where name='" + to + "'")[0].Code.ToString();
            string urls = "http://op.juhe.cn/trainTickets/ticketsAvailable?dtype=&train_date={0}&from_station={1}&to_station={2}&key=750f09b37a18dfd05d5980680be18759";
            //urls = string.Format(urls, date, from, to);
            urls = string.Format(urls, date, fromc, toc);
            string str = GetRemoteHtmlCodeByEncoding(urls, "utf-8");
            //如需保存JSON字符串数据，需要按以下进行反序列化处理，读取进数据库：将字符串转换成JSON对象，进行对象处理
            JhTrain jsons = JsonConvert.DeserializeObject<JhTrain>(str);
            //jsons.result.list[1].train_code.ToString();
            return Json(jsons);
        }
        //返回JSON字符串统一入口
        public static string GetRemoteHtmlCodeByEncoding(string Url, string encode)
        {
            HttpWebResponse Result = null;

            try
            {
                HttpWebRequest Req = (HttpWebRequest)System.Net.WebRequest.Create(Url);
                Req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.63 Safari/537.36";
                Req.Method = "get";
                Req.ContentType = "application/x-www-form-urlencoded";
                Req.Credentials = CredentialCache.DefaultCredentials;
                Result = (HttpWebResponse)Req.GetResponse();

                StreamReader ReceiveStream = new StreamReader(Result.GetResponseStream(), Encoding.GetEncoding(encode));
                string OutPutString;
                try
                {
                    OutPutString = ReceiveStream.ReadToEnd();
                }
                catch
                {
                    OutPutString = string.Empty;
                }
                finally
                {
                    ReceiveStream.Close();
                }

                return OutPutString;
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                if (Result != null)
                {
                    Result.Close();
                }
            }
        }
        //0.解释JSONG字符串更新列车简码信息(聚合列车简码接口)
        public ActionResult trainsname(string id)
        {
            if (id == "1")
            {
                List<CityCode> citycodes = new List<CityCode>();
                citycodes = new CityCodeManager().GetCityCodeBySql("select * from CityCode");
                if (citycodes.Count==0)
                {
                    string urls = @"http://op.juhe.cn/trainTickets/cityCode?dtype=&stationName=&all=1&key=750f09b37a18dfd05d5980680be18759";
                    //urls = string.Format(urls, dates, "SHH", "GZQ");
                    string str = GetRemoteHtmlCodeByEncoding(urls, "utf-8");
                    Root jsons = JsonConvert.DeserializeObject<Root>(str);
                    //result jsons = JsonConvert.DeserializeObject<result>(str);
                    for (int i = 0; i < jsons.result.Count; i++)
                    //for (int i=0;i<10;i++)
                    {
                        CityCode citycode = new CityCode();
                        citycode.Name = jsons.result[i].name.ToString();
                        citycode.Code = jsons.result[i].code.ToString();
                        int flag = new CityCodeManager().AddCityCode(citycode);
                    }
                    ViewBag.ts = "更新成功！";
                    //ViewBag.str = str;
                }
                else
                    ViewBag.ts = "已经存在数据！";
            }
            return View();
        }
        //2.下订单
        public ActionResult Orders(string time,string no,string from ,string to ,string seat,string seatcode,string price,string stime,string etime,string rtime,string tname)
        {
            TempData["time"] = time;
            TempData["no"] = no;
            TempData["froms"] = from;
            TempData["tos"] = to;
            TempData["seat"] = seat;
            TempData["seatcode"] = seatcode;
            TempData["price"] = price;
            TempData["stime"] = stime;
            TempData["etime"] = etime;
            TempData["rtime"] = rtime;
            TempData["tname"] = tname;
            return null;
        }
    }
    //------------------------------------------数据契约-------------------------------
    //站点简码
    [DataContract]
    public class Root
    {
        [DataMember]
        public string reason { get; set; }
        [DataMember]
        public List<result> result { get; set; }
    }
    //站点简码明细
    public class result
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string code { get; set; }
    }
    //票价信息模型
    [DataContract]
    public class Price_list
    {
        [DataMember]
        public string price_type { get; set; }
        [DataMember]
        public string seat_code { get; set; }
        [DataMember]
        public string price { get; set; }
        [DataMember]
        public string counts { get; set; }
    }
    //列车信息模型
    [DataContract]
    public class Trainlist
    {
        [DataMember]
        public string train_no { get; set; }
        [DataMember]
        public string train_name { get; set; }
        [DataMember]
        public string train_type { get; set; }
        [DataMember]
        public string start_station { get; set; }
        [DataMember]
        public string start_station_no { get; set; }
        [DataMember]
        public string end_station { get; set; }
        [DataMember]
        public string end_station_no { get; set; }
        [DataMember]
        public string date { get; set; }
        [DataMember]
        public string start_time { get; set; }
        [DataMember]
        public string end_time { get; set; }
        [DataMember]
        public string run_time { get; set; }
        [DataMember]
        public string seat_type { get; set; }
        [DataMember]
        public List<Price_list> price_list { get; set; }
    }
}