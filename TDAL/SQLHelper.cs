using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDAL
{
    class SQLHelper
    {
        public static readonly string connStr = ConfigurationManager.ConnectionStrings["Ticket"].ConnectionString;
        private static void PreparCommand(SqlCommand cmd, CommandType commandType, SqlConnection conn, string commandText, params SqlParameter[] cmdParams)
        {
            if (conn.State != ConnectionState.Open)
            { conn.Open(); }
            cmd.Connection = conn;
            cmd.CommandText = commandText;
            cmd.CommandType = commandType;
            if (cmdParams != null)
            {
                foreach (SqlParameter param in cmdParams)
                { cmd.Parameters.Add(param); }
            }
        }
        public static int ExecuteNonQuery(string connStr, CommandType commandType, string commandText, params SqlParameter[] cmdParams)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                PreparCommand(cmd, commandType, conn, commandText, cmdParams);
                int val = cmd.ExecuteNonQuery();
                return val;
            }
        }
        public static DataSet ExecuteDataset(string connStr, CommandType cmdType, string cmdText, params SqlParameter[] cmdParameters)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                PreparCommand(cmd, cmdType, conn, cmdText, cmdParameters);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds;
                }
            }
        }
    }
}
