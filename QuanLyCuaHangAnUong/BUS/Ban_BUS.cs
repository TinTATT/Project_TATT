using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class Ban_BUS
    {
        public static List<Ban_DTO> LoadBan()
        {
            return Ban_DAO.LayBan();
        }

        public static bool SuaTrangThaiBan(int maBan)
        {
            return Ban_DAO.SuaTrangThaiBan(maBan);
        }

        public static bool SuaTrangThaiBanTrong(int maBan)
        {
            return Ban_DAO.SuaTrangThaiBanTrong(maBan);
        }

        public static List<KhachHang_DTO> LayKH(string TenKH)
        {
            return Ban_DAO.LayKH(TenKH);
        }

        public static int layMaKH(string TenKH)
        {
            return Ban_DAO.layMaKH(TenKH);
        }

        //public static string laySDTKH(string SDT)
        //{
        //    return Ban_DAO.laySDTKH(SDT);
        //}

        public static bool ThemKhachHang(KhachHang_DTO khachhang)
        {
            return Ban_DAO.ThemKhachHang(khachhang);
        }
    }
}
