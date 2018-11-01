using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TModel;
using TDAL;

namespace TBLL
{
    public class OdPassagersManager
    {
        public int AddOdPassagers(OdPassagers odpassagers)
        {
            int flag = new OdPassagersServer().AddOdPassagers(odpassagers);
            return flag;
        }
        public List<OdPassagers> GetOdPassagersBySql(string sql)
        {
            List<OdPassagers> odpassagers = new List<OdPassagers>();
            odpassagers = new OdPassagersServer().GetOdPassagersBySql(sql);
            return odpassagers;
        }
        public List<OdPassagers> GetOdpassagersByOnum(string num)
        {
            List<OdPassagers> odpassagers = new List<OdPassagers>();
            odpassagers = (from s in new OdPassagersServer().GetAllOdpassagers()
                          where s.Odnum == num
                          select s).ToList<OdPassagers>();
            return odpassagers;
        }
        //通过sql语句更新数据
        public int UpdateBySql(string sql)
        {
            int flag = new OdPassagersServer().UpdateBySql(sql);
            return flag;
        }
    }
}
