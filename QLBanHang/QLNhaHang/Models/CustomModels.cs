using QLNhaHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLNhaHang
{
    public class HoaDon_NguoiDung
    {
         public HoaDon HoaDon { get; set; }
        public NguoiDung NguoiDung { get; set; }
    }
    public class NguoiDung_HoaDon
    {
        public HoaDon HoaDon { get; set; }
        public NguoiDung NguoiDung { get; set; }
    }

    public class ChiTietHoaDon_MatHang
    {
        public ChiTietHoaDon ChiTietHoaDon { get; set; }
        public MatHang MatHang { get; set; }
        public KhuyenMai KhuyenMai { get; set; }
    }

    public class NhomHang
    {
        public LoaiHang LoaiHang { get; set; }
        public MatHang MatHang { get; set; }
    }
}