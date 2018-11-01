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
    public class CityCodeServer
    {
        string conStr = SQLHelper.connStr;
        public int AddCityCode(CityCode  citycode)
        {
            string sql = "insert into CityCode(name,code) values(@name,@ps)";
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@name",citycode.Name),
                new SqlParameter("@ps", citycode.Code)
            };
            int flag = SQLHelper.ExecuteNonQuery(conStr, CommandType.Text, sql, para);
            return flag;
        }
        public List<CityCode> GetCityCodeBySql(string sql)
        {
            List<CityCode> list = new List<CityCode>();
            DataSet ds = SQLHelper.ExecuteDataset(conStr, CommandType.Text, sql);
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    CityCode citycode = new CityCode();
                    citycode.Id = (int)dr["id"];
                    citycode.Name = (string)dr["name"];
                    citycode.Code = (string)dr["code"];
                    list.Add(citycode);
                }
            }
            return list;
        }
    }
}
