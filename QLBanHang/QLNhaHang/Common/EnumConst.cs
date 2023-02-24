using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLNhaHang
{
    public static class EnumTrangThaiBan
    {
        public const int TRONG = 0;
        public const int CONGUOI = 1;

        public static string ToString(int value)
        {
            switch (value)
            {
                case TRONG:
                    return "Trống";
                case CONGUOI:
                    return "Có người";
                default:
                    return "";
            }
        }
    }
    public static class EnumTrangThaiKhuyenMai
    {
        public const int HIEU_LUC = 1;
        public const int HET_HIEU_LUC = 0;

        public static string ToString(int value)
        {
            switch (value)
            {
                case HIEU_LUC:
                    return "Hiệu lực";
                case HET_HIEU_LUC:
                    return "Hết hiệu lực";
                default:
                    return "";
            }
        }
    }
    public static class EnumThanhToan
    {
        public const int CHUA_THANH_TOAN = 0;
        public const int DA_THANH_TOAN = 1;

        public static string ToString(int value)
        {
            switch (value)
            {
                case CHUA_THANH_TOAN:
                    return "Chưa thanh toán";
                case DA_THANH_TOAN:
                    return "Đã thanh toán";
                default:
                    return "";
            }
        }
    }
    public static class EnumHuyBan
    {
        public const int KHONG_HUY_BAN = 0;
        public const int DA_HUY_BAN = 1;

        public static string ToString(int value)
        {
            switch (value)
            {
                case KHONG_HUY_BAN:
                    return "Không hủy bàn";
                case DA_HUY_BAN:
                    return "Đã hủy bàn";
                default:
                    return "";
            }
        }
    }
}