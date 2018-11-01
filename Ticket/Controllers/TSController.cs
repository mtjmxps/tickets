using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TModel;
using TBLL;

namespace Ticket.Controllers
{
    public class TSController : Controller
    {
        // GET: TS
        public ActionResult Index()
        {
            return View();
        }
        //返回json字符串实例：http://localhost:3944/TS/GetDrugList?term=广州
        public JsonResult GetDrugList(String term)
        {
            List<CityCode> lcc = new List<CityCode>();
            if (term.Trim() == "")
                lcc = null;
            else
                lcc = new CityCodeManager().GetCityCodeBySql("select * from CityCode where name like '" + term.Trim() + "%'");
            return Json(lcc, JsonRequestBehavior.AllowGet);
        }
    }
}