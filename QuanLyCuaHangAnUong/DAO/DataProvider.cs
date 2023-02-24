using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DAO
{
    public class DataProvider
    {
        
        public static SqlConnection MoKetNoi()
        {
            string strCon = ConfigurationManager.ConnectionStrings["strCon"].ConnectionString;
            SqlConnection conn = new SqlConnection(strCon);
            conn.Open();
            return conn;
        }

        public static void DongKetNoi(SqlConnection conn)
        {
            conn.Close();
        }

        public static DataTable LayDataTable(string chuoiTruyVan, SqlConnection conn)
        {
            SqlDataAdapter da = new SqlDataAdapter(chuoiTruyVan, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public static bool ThucThiTruyVanNonQuery(string chuoiTruyVan, SqlConnection conn)
        {
            try
            {
                SqlCommand cm = new SqlCommand(chuoiTruyVan, conn);
                cm.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string ConvertDateTime(string date)
        {
            string[] elements = date.Split('/');
            string dt = string.Format("{0}/{1}/{2}", elements[0], elements[1], elements[2]);
            return dt;
        }
    }
}
