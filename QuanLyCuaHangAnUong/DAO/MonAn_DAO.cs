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
    public class MonAn_DAO
    {
        static SqlConnection conn;

        public static List<MonAn_DTO> LayMonAn()
        {
            List<MonAn_DTO> lstMon = new List<MonAn_DTO>();
            conn = DataProvider.MoKetNoi();
            string cauTruyVan = "Select * From SANPHAM";
            DataTable dt = DataProvider.LayDataTable(cauTruyVan, conn);
            if (dt.Rows.Count == 0)
                return null;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MonAn_DTO monan = new MonAn_DTO();

                monan.MaSP = int.Parse(dt.Rows[i]["MaSP"].ToString());
                monan.TenSP = dt.Rows[i]["TenSP"].ToString();
                monan.DonGia = int.Parse(dt.Rows[i]["DonGia"].ToString());
                monan.DonVi = dt.Rows[i]["DonVi"].ToString();
                monan.MoTa = dt.Rows[i]["MoTa"].ToString();
                monan.HinhAnh = dt.Rows[i]["HinhAnh"].ToString();
                monan.MaLoaiSP = int.Parse(dt.Rows[i]["MaLoaiSP"].ToString());

                lstMon.Add(monan);
            }
            DataProvider.DongKetNoi(conn);
            return lstMon;
        }

        public static int layIDMonAn(string tenSP)
        {
            string cauTruyVan = string.Format("Select SP.MaSP From SANPHAM AS SP Where SP.TenSP = N'{0}'", tenSP);
            conn = DataProvider.MoKetNoi();
            DataTable dtIDMonAn = DataProvider.LayDataTable(cauTruyVan, conn);
            if (dtIDMonAn.Rows.Count == 0)
                return -1;
            int idMonAn = int.Parse(dtIDMonAn.Rows[0]["MaSP"].ToString());
            DataProvider.DongKetNoi(conn);
            return idMonAn;
        }

        public static int Layloai(string tenLoai)
        {
            string cauTruyVan = string.Format("Select MaLoai From LOAISP where TenLoai = N'{0}'", tenLoai);
            conn = DataProvider.MoKetNoi();
            DataTable dt = DataProvider.LayDataTable(cauTruyVan, conn);
            if (dt.Rows.Count == 0)
                return -1;
            int idMonAn = int.Parse(dt.Rows[0]["MaLoai"].ToString());
            DataProvider.DongKetNoi(conn);
            return idMonAn;
        }

        public static List<MonAn_DTO> TimMonAn(string tenMonAn)
        {
            string chuoiTruyVan = "Select * From SANPHAM where TenSP like N'%" + tenMonAn + "%'";
            conn = DataProvider.MoKetNoi();
            DataTable dt = DataProvider.LayDataTable(chuoiTruyVan, conn);
            if (dt.Rows.Count == 0)
                return null;

            List<MonAn_DTO> lstMonAn = new List<MonAn_DTO>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MonAn_DTO monAn = new MonAn_DTO();
                monAn.MaSP = int.Parse(dt.Rows[i]["MaSP"].ToString());
                monAn.TenSP = dt.Rows[i]["TenSP"].ToString();
                monAn.DonGia = int.Parse(dt.Rows[i]["DonGia"].ToString());
                monAn.DonVi = dt.Rows[i]["DonVi"].ToString();
                monAn.MoTa = dt.Rows[i]["MoTa"].ToString();
                monAn.HinhAnh = dt.Rows[i]["HinhAnh"].ToString();
                monAn.MaLoaiSP = int.Parse(dt.Rows[i]["MaLoaiSP"].ToString());

                lstMonAn.Add(monAn);
            }
            DataProvider.DongKetNoi(conn);
            return lstMonAn;
        }

        public static bool ThemSP(MonAn_DTO ma)
        {
            string chuoiTruyVan = string.Format("INSERT INTO SANPHAM(TenSP,DonGia,DonVi,MoTa,MaLoaiSP) VALUES (N'{0}',{1},N'{2}',N'{3}',{4})", ma.TenSP,ma.DonGia,ma.DonVi,ma.MoTa,ma.MaLoaiSP);

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
        public static bool SuaSP(MonAn_DTO ma)
        {
            string chuoiTruyVan = string.Format("UPDATE SANPHAM SET TenSP = N'{0}',DonGia = {1},DonVi = N'{2}' ,MoTa = N'{3}', MaLoaiSP = {4} where MaSP = {5}", ma.TenSP, ma.DonGia, ma.DonVi, ma.MoTa, ma.MaLoaiSP,ma.MaSP);

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

        public static bool XoaSP(MonAn_DTO sp)
        {
            string chuoiTruyVan = string.Format("DELETE FROM SANPHAM where MaSP = N'{0}'", sp.MaSP);

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
