using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;

using DTO;

namespace DAO
{
    public class Ban_DAO
    {
        static SqlConnection conn;

        public static List<Ban_DTO> LayBan()
        {
            List<Ban_DTO> lstBan = new List<Ban_DTO>();
            conn = DataProvider.MoKetNoi();
            string cauTruyVan = "Select * From BAN";
            DataTable dt = DataProvider.LayDataTable(cauTruyVan, conn);
            if (dt.Rows.Count == 0)
                return null;

            for (int i = 0; i< dt.Rows.Count; i++)
            {
                Ban_DTO ban = new Ban_DTO();

                ban.MaBan = int.Parse(dt.Rows[i]["MaBan"].ToString());
                ban.TenBan = dt.Rows[i]["TenBan"].ToString();
                ban.LoaiBan = dt.Rows[i]["LoaiBan"].ToString();
                ban.TrangThai = dt.Rows[i]["TrangThai"].ToString();

                lstBan.Add(ban);
            }
            DataProvider.DongKetNoi(conn);
            return lstBan;
        }

        //public static List<Ban_DTO> LayLoaiBan()
        //{
        //    List<Ban_DTO> lstBan = new List<Ban_DTO>();
        //    conn = DataProvider.MoKetNoi();
        //    string cauTruyVan = "Select DISTINCT LoaiBan From BAN";
        //    DataTable dt = DataProvider.LayDataTable(cauTruyVan, conn);
        //    if (dt.Rows.Count == 0)
        //        return null;

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        Ban_DTO ban = new Ban_DTO();       
        //        ban.LoaiBan = dt.Rows[i]["LoaiBan"].ToString();

        //        lstBan.Add(ban);
        //    }
        //    DataProvider.DongKetNoi(conn);
        //    return lstBan;
        //}

        // sửa trạng thái bàn ăn thành có người
        public static bool SuaTrangThaiBan(int maBan)
        {
            string cauTruyVan = string.Format("Update BAN Set TrangThai = N'Có Người' Where MaBan = {0}", maBan);
            conn = DataProvider.MoKetNoi();
            try
            {
                DataProvider.ThucThiTruyVanNonQuery(cauTruyVan, conn);
                DataProvider.DongKetNoi(conn);
                return true;
            }
            catch (Exception)
            {
                DataProvider.DongKetNoi(conn);
                return false;
            }
        }

        // sửa trạng thái bàn ăn thành bàn trống
        public static bool SuaTrangThaiBanTrong(int maBan)
        {
            string cauTruyVan = string.Format("Update BAN Set TrangThai = N'Trống' Where MaBan = {0}", maBan);
            conn = DataProvider.MoKetNoi();
            try
            {
                DataProvider.ThucThiTruyVanNonQuery(cauTruyVan, conn);
                DataProvider.DongKetNoi(conn);
                return true;
            }
            catch (Exception)
            {
                DataProvider.DongKetNoi(conn);
                return false;
            }
        }

        //Lấy danh sách khách hàng
        public static List<KhachHang_DTO> LayKH(string TenKH)
        {
            List<KhachHang_DTO> lstKH = new List<KhachHang_DTO>();
            conn = DataProvider.MoKetNoi();
            string cauTruyVan = "Select * From KHACHHANG Where TenKH LIKE N'%" + TenKH + "%'";
            DataTable dt = DataProvider.LayDataTable(cauTruyVan, conn);
            if (dt.Rows.Count == 0)
                return null;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                KhachHang_DTO khachhang = new KhachHang_DTO();

                khachhang.MaKH = int.Parse(dt.Rows[i]["MaKH"].ToString());
                khachhang.TenKH = dt.Rows[i]["TenKH"].ToString();
               


                lstKH.Add(khachhang);
            }
            DataProvider.DongKetNoi(conn);
            return lstKH;
        }

        public static int layMaKH(string TenKH)
        {
            string cauTruyVan = "Select MaKH From KHACHHANG Where TenKH LIKE N'%" + TenKH + "%'";
            conn = DataProvider.MoKetNoi();
            DataTable dtMaKH = DataProvider.LayDataTable(cauTruyVan, conn);
            int maKH = int.Parse(dtMaKH.Rows[0]["MaKH"].ToString());
            DataProvider.DongKetNoi(conn);
            return maKH;
        }

        //public static string laySDTKH(string SDT)
        //{
        //    string cauTruyVan = "Select SDT From KHACHHANG Where TenKH LIKE N'%" + SDT + "%'";
        //    conn = DataProvider.MoKetNoi();
        //    DataTable dtSDTKH = DataProvider.LayDataTable(cauTruyVan, conn);
        //    string SDTKH = dtSDTKH.Rows[0]["SDT"].ToString();
        //    DataProvider.DongKetNoi(conn);
        //    return SDTKH;
        //}

        public static bool ThemKhachHang(KhachHang_DTO khachhang)
        {
            string chuoiTruyVan = string.Format("Insert into KHACHHANG(TenKH) Values (N'{0}')", khachhang.TenKH);
            conn = DataProvider.MoKetNoi();
            try
            {
                DataProvider.ThucThiTruyVanNonQuery(chuoiTruyVan, conn);
                DataProvider.DongKetNoi(conn);
                return true;
            }
            catch (Exception)
            {
                DataProvider.DongKetNoi(conn);
                return false;
            }
        }
    }
   
}
