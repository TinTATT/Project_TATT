using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAO;

namespace BUS
{
    public class HoaDon_BUS
    {
        public static List<HoaDon_DTO> LoadHoaDon(int MaBan)
        {
            return HoaDon_DAO.LoadHoaDon(MaBan);
        }

        public static int layIDHoaDon(int MaBan)
        {
            return HoaDon_DAO.LayIDHoaDon(MaBan);
        }

        public static bool ThemThongTinHoaDon(int MaHD, int MaSP, int soLuong,int ThanhTien,string Ghichu)
        {
            return HoaDon_DAO.ThemThongTinHoaDon(MaHD, MaSP, soLuong, ThanhTien, Ghichu);
        }

        public static bool CapNhatSoLuongMonAn(int soLuong, int ThanhTien, int MaHD, int MaSP)
        {
            return HoaDon_DAO.CapNhatSoLuongMonAn(soLuong, ThanhTien, MaHD, MaSP);
        }

        public static bool XoaMonAn(int MaHD, int MaSP)
        {
            return HoaDon_DAO.XoaMonAn(MaHD, MaSP);
        }

        public static bool ThemHoaDon(int MaBan,int MaKH)
        {
            return HoaDon_DAO.ThemHoaDon(MaBan,MaKH);
        }

        public static bool ChuyenBan(int idFromTable, int idToTable)
        {
            return HoaDon_DAO.ChuyenBan(idFromTable, idToTable);
        }

        public static bool XoaCTHD(int MaHD)
        {
            return HoaDon_DAO.XoaChiTietHoaDon(MaHD);
        }

        public static bool UpdateHoaDon(string NgayBan, int TongTien, string TrangThai, int MaBan, int MaKH)
        {
            return HoaDon_DAO.UpdateHoaDon(NgayBan, TongTien, TrangThai, MaBan, MaKH);
        }
    }
}
