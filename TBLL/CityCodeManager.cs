using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TModel;
using TDAL;

namespace TBLL
{
    public class CityCodeManager
    {
        public int AddCityCode(CityCode citycode)
        {
            int flag = new CityCodeServer().AddCityCode(citycode);
            return flag;
        }
        public List<CityCode> GetCityCodeBySql(string sql)
        {
            List<CityCode> citycode = new List<CityCode>();
            citycode = new CityCodeServer().GetCityCodeBySql(sql);
            return citycode;
        }
    }
}
