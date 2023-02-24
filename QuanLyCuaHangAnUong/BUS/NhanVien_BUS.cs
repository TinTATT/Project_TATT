using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BUS
{
    public class NhanVien_BUS
    {
        public static List<NhanVien_DTO> LoadNV()
        {
            return NhanVien_DAO.LayNhanVien();
        }

        public static List<NhanVien_DTO> TimNV(string tenNV)
        {
            return NhanVien_DAO.TimNV(tenNV);
        }

        public static bool ThemNV(NhanVien_DTO nv)
        {
            return NhanVien_DAO.ThemNV(nv);
        }
        public static bool SuaNV(NhanVien_DTO nv)
        {
            return NhanVien_DAO.SuaNV(nv);
        }
        public static bool XoaNV(NhanVien_DTO nv)
        {
            return NhanVien_DAO.XoaNV(nv) ;
        }

        public static string layTenNV(string TenDN)
        {
            return NhanVien_DAO.LayTenNV(TenDN);
        }
    }
}
