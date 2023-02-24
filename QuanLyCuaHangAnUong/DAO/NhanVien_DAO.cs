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
    public class NhanVien_DAO
    {
        static SqlConnection conn;



        public static List<NhanVien_DTO> LayNhanVien()
        {
            List<NhanVien_DTO> lstNV = new List<NhanVien_DTO>();
            conn = DataProvider.MoKetNoi();
            string cauTruyVan = "Select * From NHANVIEN";
            DataTable dt = DataProvider.LayDataTable(cauTruyVan, conn);
            if (dt.Rows.Count == 0)
                return null;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                
                NhanVien_DTO NV = new NhanVien_DTO();

                NV.MaNV = dt.Rows[i]["MaNV"].ToString();
                NV.TenNV = dt.Rows[i]["TenNV"].ToString();
                NV.DiaChi = dt.Rows[i]["DiaChi"].ToString();
                NV.NgaySinh = dt.Rows[i]["NgaySinh"].ToString();
                NV.GioiTinh = dt.Rows[i]["GioiTinh"].ToString();
                NV.SDT = dt.Rows[i]["SDT"].ToString();
                NV.TenTK = dt.Rows[i]["TenTK"].ToString();
                NV.MK = dt.Rows[i]["MK"].ToString();
                NV.ChucVu = dt.Rows[i]["ChucVu"].ToString();

                lstNV.Add(NV);
            }
            DataProvider.DongKetNoi(conn);
            return lstNV;
        }

        public static List<NhanVien_DTO> TimNV(string tenNV)
        {
            string chuoiTruyVan = "Select * From NHANVIEN where TenNV like N'"+ tenNV+ "'";
            conn = DataProvider.MoKetNoi();
            DataTable dt = DataProvider.LayDataTable(chuoiTruyVan, conn);
            if (dt.Rows.Count == 0)
                return null;

            List<NhanVien_DTO> lstNV = new List<NhanVien_DTO>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                NhanVien_DTO NV = new NhanVien_DTO();

                NV.MaNV = dt.Rows[i]["MaNV"].ToString();
                NV.TenNV = dt.Rows[i]["TenNV"].ToString();
                NV.DiaChi = dt.Rows[i]["DiaChi"].ToString();
                NV.NgaySinh = dt.Rows[i]["NgaySinh"].ToString();
                NV.GioiTinh = dt.Rows[i]["GioiTinh"].ToString();
                NV.SDT = dt.Rows[i]["SDT"].ToString();
                NV.TenTK = dt.Rows[i]["TenTK"].ToString();
                NV.MK = dt.Rows[i]["MK"].ToString();
                NV.ChucVu = dt.Rows[i]["ChucVu"].ToString();

                lstNV.Add(NV);
            }
            DataProvider.DongKetNoi(conn);
            return lstNV;
        }

        public static bool ThemNV(NhanVien_DTO nv)
        {
            string chuoiTruyVan = string.Format("INSERT INTO NHANVIEN(MaNV,TenNV,DiaChi,NgaySinh,GioiTinh,SDT,TenTK,MK,ChucVu) VALUES (N'{0}',N'{1}',N'{2}',N'{3}',N'{4}',N'{5}',N'{6}',N'{7}',N'{8}')", nv.MaNV,nv.TenNV,nv.DiaChi,nv.NgaySinh,nv.GioiTinh,nv.SDT,nv.TenTK,nv.MK,nv.ChucVu);
            
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

        public static bool SuaNV(NhanVien_DTO nv)
        {
            string chuoiTruyVan = string.Format("UPDATE NHANVIEN SET TenNV = N'{0}',DiaChi = N'{1}',NgaySinh = N'{2}',GioiTinh = N'{3}',SDT = N'{4}',TenTK = N'{5}',MK = N'{6}',ChucVu = N'{7}' where MaNV = N'{8}'",nv.TenNV, nv.DiaChi, nv.NgaySinh, nv.GioiTinh, nv.SDT, nv.TenTK, nv.MK, nv.ChucVu, nv.MaNV);

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


        public static bool XoaNV(NhanVien_DTO nv)
        {
            string chuoiTruyVan = string.Format("DELETE FROM NHANVIEN where MaNV = N'{0}'",nv.MaNV);

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

        public static string LayTenNV(string tenDN)
        {
            string cauTruyVan = string.Format("Select TenNV from NHANVIEN where TenTK = N'{0}'", tenDN);
            conn = DataProvider.MoKetNoi();
            DataTable dt = DataProvider.LayDataTable(cauTruyVan, conn);
            string tenNV = dt.Rows[0]["TenNV"].ToString();
            DataProvider.DongKetNoi(conn);
            return tenNV;
        }



    }  
 }