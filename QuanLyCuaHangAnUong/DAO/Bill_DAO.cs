using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class Bill_DAO
    {
        static SqlConnection conn;
        public static List<Bill_DTO> LayDanhSachBill()
        {
            List<Bill_DTO> lstBill = new List<Bill_DTO>();
            conn = DataProvider.MoKetNoi();
            string cauTruyVan = "Select * From [dbo].[HOADON] AS HD JOIN [dbo].[KHACHHANG] AS KH ON HD.MaKH = KH.MaKH JOIN [dbo].[BAN] AS B ON HD.MaBan = B.MaBan";
            DataTable dt = DataProvider.LayDataTable(cauTruyVan, conn);
            if (dt.Rows.Count == 0)
                return null;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                Bill_DTO Bill = new Bill_DTO();

                Bill.MaHD = int.Parse(dt.Rows[i]["MaHD"].ToString());
                Bill.NgayBan = dt.Rows[i]["NgayBan"].ToString();
                Bill.TongTien = int.Parse(dt.Rows[i]["TongTien"].ToString());
                Bill.TrangThai = dt.Rows[i]["TrangThai"].ToString();
                Bill.TenBan = dt.Rows[i]["TenBan"].ToString();
                Bill.TenKH = dt.Rows[i]["TenKH"].ToString();
               

                lstBill.Add(Bill);
            }
            DataProvider.DongKetNoi(conn);
            return lstBill;
        }
    }
}
