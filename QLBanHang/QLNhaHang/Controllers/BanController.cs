using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLNhaHang.Models;
using System.Data.Entity.Core.Objects;

namespace QLNhaHang.Controllers
{
    public class BanController : Controller
    {
        QLNhaHangEntities db = new QLNhaHangEntities();
        // GET: Ban
        public ActionResult Index(int? KhuVucID = 0)
        {
            ViewBag.KhuVucID = KhuVucID;

            if (KhuVucID != 0)
            {
                return View(db.Bans.Where(a => a.KhuVucID == KhuVucID));
            }
            return View(db.Bans);
        }
        public ActionResult BanHang(int? vID = null)
        {
            var dmKhuVucs = db.KhuVucs.OrderBy(x => x.KhuVucID);
            QLNhaHang.Models.KhuVuc dmKhuV = db.KhuVucs.FirstOrDefault(x => x.KhuVucID == vID);
            dmKhuV = dmKhuV == null ? db.KhuVucs.OrderBy(x => x.KhuVucID).FirstOrDefault() : dmKhuV;
            ViewBag.dmKhuV = dmKhuV == null ? new KhuVuc() : dmKhuV;

            return View(dmKhuVucs);
        }
        public ActionResult LoadBan(int? khuVucID = null)
        {
            var bans = db.Bans.Where(x => x.KhuVucID == khuVucID).OrderBy(x => x.BanID);

            return PartialView("_DanhSach", bans);


        }
        public ActionResult ThongKe()
        {
            ViewBag.tuNgay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
            ViewBag.denNgay = DateTime.Now.ToString("dd/MM/yyyy");
            return View();

        }

        public ActionResult DanhSachThongKe(string _TuNgay = "", string _DenNgay = "")
        {
            DateTime tuNgay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            try
            {
                tuNgay = new DateTime(int.Parse(_TuNgay.Split('/')[2]), int.Parse(_TuNgay.Split('/')[1]), int.Parse(_TuNgay.Split('/')[0]));
            }
            catch { }

            DateTime denNgay = DateTime.Now.Date;
            try
            {
                denNgay = new DateTime(int.Parse(_DenNgay.Split('/')[2]), int.Parse(_DenNgay.Split('/')[1]), int.Parse(_DenNgay.Split('/')[0]));
            }
            catch { }

        
            //var hoaDons = db.HoaDons.Where(p => EntityFunctions.TruncateTime(p.NgayTao.Value) >= tuNgay.Date &&
            //                                    EntityFunctions.TruncateTime(p.NgayTao.Value) <= denNgay.Date &&
            //                                    p.DaThanhToan == EnumThanhToan.DA_THANH_TOAN &&
            //                                    p.HuyBan == EnumHuyBan.KHONG_HUY_BAN );
             var hoaDon = (from a in db.HoaDons
                          join b in db.NguoiDungs on a.NguoiDungID equals b.NguoiDungID
                          where  EntityFunctions.TruncateTime(a.NgayTao.Value) >= tuNgay.Date &&
                                 EntityFunctions.TruncateTime(a.NgayTao.Value) <= denNgay.Date &&
                                 a.DaThanhToan == EnumThanhToan.DA_THANH_TOAN &&
                                 a.HuyBan == EnumHuyBan.KHONG_HUY_BAN
                          select new HoaDon_NguoiDung()
                          {
                              HoaDon = a,
                              NguoiDung = b
                          });
          
            return PartialView("_ThongKe", hoaDon);
        }
        public ActionResult ChonBan(int? banID = null)
        {
          
            var ban = db.Bans.FirstOrDefault(x => x.BanID == banID && x.TrangThai == EnumTrangThaiBan.TRONG);
          
            if (ban == null)
                return Json(new { kq = "err", msg = "Không thể chọn bàn!" }, JsonRequestBehavior.AllowGet);
            ban.TrangThai = EnumTrangThaiBan.CONGUOI;
        
            db.SaveChanges();
            
            var kiemTraBanTrong = db.HoaDons.Count(x => x.BanID == banID && x.DaThanhToan == EnumThanhToan.CHUA_THANH_TOAN && x.HuyBan == EnumHuyBan.KHONG_HUY_BAN);
            if (kiemTraBanTrong > 0)
                return Json(new { kq = "err", msg = "Bàn đang sử dụng, vui lòng chọn bàn khác!", JsonRequestBehavior.AllowGet });

            var nd_dv = db.NguoiDungs.FirstOrDefault();

            HoaDon hoaDon = new HoaDon();
            hoaDon.HoaDonID = Guid.NewGuid();
            hoaDon.BanID = banID;
            hoaDon.NguoiDungID = nd_dv.NguoiDungID;
            hoaDon.DaThanhToan = EnumThanhToan.CHUA_THANH_TOAN;
            hoaDon.HuyBan = EnumHuyBan.KHONG_HUY_BAN;
            hoaDon.NgayTao = DateTime.Now;
            db.HoaDons.Add(hoaDon);
            db.SaveChanges();
            

            return Json(new { kq = "ok", msg = "Chọn bạn thành công!"}, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult CapNhat(Ban nd = null)
        {

            if (nd.BanID == 0)
            {
                db.Bans.Add(nd);
                db.SaveChanges();
            }
            else
            {
                var data = db.Bans.Where(a => a.BanID == nd.BanID).FirstOrDefault();
                data.TenBan = nd.TenBan;
                data.KhuVucID = nd.KhuVucID;
                data.TrangThai = nd.TrangThai;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult CapNhat(int id = 0)
        {
            if (id != 0)
            {
                var nd = db.Bans.Where(a => a.BanID == id).FirstOrDefault();
                return View(nd);
            }
            else
            {

                return View(new Ban());
            }
        }
        public ActionResult Xoa(int id = 0)
        {
            if (id != 0)
            {
                var nd = db.Bans.Where(a => a.BanID == id).FirstOrDefault();
                db.Bans.Remove(nd);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult HoaDon(int? id = null)
        {
            var hoaDon = (from a in db.HoaDons
                          join b in db.NguoiDungs on a.NguoiDungID equals b.NguoiDungID
                          where a.BanID == id && a.DaThanhToan == EnumThanhToan.CHUA_THANH_TOAN && a.HuyBan == EnumHuyBan.KHONG_HUY_BAN
                          select new HoaDon_NguoiDung()
                          {
                              HoaDon = a,
                              NguoiDung = b
                          }).FirstOrDefault();
            hoaDon = hoaDon == null ? new HoaDon_NguoiDung() : hoaDon;
            hoaDon.HoaDon = hoaDon.HoaDon == null ? new HoaDon() : hoaDon.HoaDon;

            var chiTietHoaDons = from a in db.ChiTietHoaDons
                                 join b in db.MatHangs on a.MatHangID equals b.MatHangID into b1
                                 from b in b1.DefaultIfEmpty()
                                 join c in db.KhuyenMais on a.KhuyenMaiID equals c.KhuyenMaiID into c1
                                 from c in c1.DefaultIfEmpty()
                                 where a.HoaDonID == hoaDon.HoaDon.HoaDonID
                                 select new ChiTietHoaDon_MatHang()
                                 {
                                     ChiTietHoaDon = a,
                                     MatHang = b,
                                     KhuyenMai = c,
                                 };
            ViewBag.chiTietHoaDons = chiTietHoaDons;

            var nhomHangs = from a in db.MatHangs
                            join b in db.LoaiHangs on a.LoaiHangID equals b.LoaiHangID into b1
                            from b in b1.DefaultIfEmpty()
                            where b!=null
                            orderby b.TenLoaiHang ascending
                            select new NhomHang()
                            {
                                MatHang = a,
                                LoaiHang = b,
                            };
            ViewBag.nhomHangs = nhomHangs;

            return PartialView("_BoxHoaDon", hoaDon);
        }

        public ActionResult ChonMon(Guid? hoaDonID = null, int? matHangID = null)
        {
            var matHang = db.MatHangs.FirstOrDefault(x => x.MatHangID == matHangID);
            if(matHang == null)
                return Json(new { kq = "err", msg = "Mặt hàng không xác định!" }, JsonRequestBehavior.AllowGet);
            var hoaDon = db.HoaDons.FirstOrDefault(x => x.HoaDonID == hoaDonID);
            if (hoaDon == null)
                return Json(new { kq = "err", msg = "Hóa đơn không xác định!" }, JsonRequestBehavior.AllowGet);
            var chiTietHoaDon = db.ChiTietHoaDons.FirstOrDefault(x => x.HoaDonID == hoaDonID && x.MatHangID == matHangID);
            if(chiTietHoaDon == null)
            {
                var khuyenMai = db.KhuyenMais.FirstOrDefault(x => x.MatHangID == matHang.MatHangID && x.TrangThai == EnumTrangThaiKhuyenMai.HIEU_LUC);
                khuyenMai = khuyenMai == null ? new KhuyenMai() : khuyenMai;
                chiTietHoaDon = new ChiTietHoaDon();
                chiTietHoaDon.MatHangID = matHangID;
                chiTietHoaDon.HoaDonID = hoaDonID;
                chiTietHoaDon.SoLuong = 1;
                chiTietHoaDon.Gia = matHang.Gia;
                chiTietHoaDon.ThanhTien = matHang.Gia;
                if(khuyenMai.KhuyenMaiID != 0)
                {
                    chiTietHoaDon.KhuyenMaiID = khuyenMai.KhuyenMaiID;
                    chiTietHoaDon.PhanTramKM = khuyenMai.PhanTramKM;
                }

                db.ChiTietHoaDons.Add(chiTietHoaDon);
                db.SaveChanges();

                hoaDon.TongTien = db.ChiTietHoaDons.Where(x => x.HoaDonID == hoaDonID).Sum(x => x.ThanhTien);
             
                
                db.SaveChanges();

                string chonMon = "<tr class=\"info\"  data-id=\"" + matHang.MatHangID + "\"" + "data-chitietid=\"" +chiTietHoaDon.ChiTietHoaDonID + "\">" +
                                        "<td class=\"text-center\">" + matHang.TenMatHang + "</td>" +
                                        "<td class=\"text-center\">" + (matHang.Gia != null ? matHang.Gia.Value.ToString("#,###") : "") + "</td>" +
                                        "<td class=\"text-center so-luong \">1</td>" +
                                          //"<td class=\"text-right\">" + (khuyenMai.PhanTramKM != null ? khuyenMai.PhanTramKM.Value.ToString("#,###.##") : "-") + "</td>" +
                                          //"<td class=\"text-center  thanh-tien \">" + (matHang.Gia != null ? (((khuyenMai.PhanTramKM != null ? (100 - (float)khuyenMai.PhanTramKM.Value) / 100 : 1) * matHang.Gia.Value).ToString("#,###")) : "") + "</td>" +
                                          "<td class=\"text-center  thanh-tien \">" + (matHang.Gia != null ? matHang.Gia.Value.ToString("#,###") : "") + "</td>" +
                                          "<td class=\"text-center\">" + 
                                            "<a class=\"btn btn-default cong\"><i class=\"fa fa-plus\"></i></a>" +
                                            "<a class=\"btn btn-default tru\"><i class=\"fa fa-minus\"></i></a>"  +
                                            "<a class=\"btn btn-default xoa\"><i class=\"fa fa-trash\"></i></a>"  +
                                        "</td>" +
                                    "</tr>";

                var chiTietHoaDons = db.ChiTietHoaDons.Where(x => x.HoaDonID == chiTietHoaDon.HoaDonID);
                string tongGiaHoaDon = "-";
                if(chiTietHoaDons != null)
                {
                    var tongTien = chiTietHoaDons.Sum(x => x.ThanhTien);
                    tongGiaHoaDon = tongTien == null ? "-" : tongTien.Value.ToString("#,###;-;-");
                }

                return Json(new { kq = "ok", msg = "", themMoi = 1, matHangID = matHang.MatHangID, chonMon = chonMon, tongGiaHoaDon= tongGiaHoaDon }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                chiTietHoaDon.SoLuong += 1;
                chiTietHoaDon.ThanhTien += matHang.Gia;
                db.SaveChanges();
                hoaDon.TongTien = db.ChiTietHoaDons.Where(x => x.HoaDonID == hoaDonID).Sum(x => x.ThanhTien);
              
                db.SaveChanges();


                var chiTietHoaDons = db.ChiTietHoaDons.Where(x => x.HoaDonID == chiTietHoaDon.HoaDonID);
                string tongGiaHoaDon = "-";
                if (chiTietHoaDons != null)
                {
                    var tongTien = chiTietHoaDons.Sum(x => x.ThanhTien);
                    tongGiaHoaDon = tongTien == null ? "-" : tongTien.Value.ToString("#,###;-;-");
                }
                return Json(new { kq = "ok", msg = "", themMoi = 0, matHangID = matHang.MatHangID, chonMon = chiTietHoaDon.SoLuong , tongGiaHoaDon = tongGiaHoaDon }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GiamMon(int? chiTietHoaDonID = null)
        {

            var chiTietHoaDon = db.ChiTietHoaDons.FirstOrDefault(x=> x.ChiTietHoaDonID == chiTietHoaDonID);
            
            if(chiTietHoaDon == null)
                return Json(new { kq = "err", msg = ""}, JsonRequestBehavior.AllowGet);
            if(chiTietHoaDon.SoLuong <= 1)
                return Json(new { kq = "err", msg = "Bạn hãy xóa món!" }, JsonRequestBehavior.AllowGet);
            chiTietHoaDon.SoLuong -= 1;
            chiTietHoaDon.ThanhTien -= chiTietHoaDon.Gia;
            db.SaveChanges();


            var chiTietHoaDons = db.ChiTietHoaDons.Where(x => x.HoaDonID == chiTietHoaDon.HoaDonID);
            string tongGiaHoaDon = "-";
            if (chiTietHoaDons != null)
            {
                var tongTien = chiTietHoaDons.Sum(x => x.ThanhTien);
                tongGiaHoaDon = tongTien == null ? "-" : tongTien.Value.ToString("#,###;-;-");
            }
            return Json(new { kq = "ok", msg = "", soLuong = chiTietHoaDon.SoLuong.Value.ToString("#,###"), thanhTien = chiTietHoaDon.ThanhTien.Value.ToString("#,###") , tongGiaHoaDon = tongGiaHoaDon }, JsonRequestBehavior.AllowGet);



        }
        public ActionResult CongMon(int? chiTietHoaDonID = null)
        {
            var chiTietHoaDon = db.ChiTietHoaDons.FirstOrDefault(x => x.ChiTietHoaDonID == chiTietHoaDonID);
            if (chiTietHoaDon == null)
                return Json(new { kq = "err", msg = "" }, JsonRequestBehavior.AllowGet);
            chiTietHoaDon.SoLuong += 1;
            chiTietHoaDon.ThanhTien += chiTietHoaDon.Gia;
            db.SaveChanges();

            var chiTietHoaDons = db.ChiTietHoaDons.Where(x => x.HoaDonID == chiTietHoaDon.HoaDonID);
            string tongGiaHoaDon = "-";
            if (chiTietHoaDons != null)
            {
                var tongTien = chiTietHoaDons.Sum(x => x.ThanhTien);
                tongGiaHoaDon = tongTien == null ? "-" : tongTien.Value.ToString("#,###;-;-");
            }
            return Json(new { kq = "ok", msg = "", soLuong = chiTietHoaDon.SoLuong.Value.ToString("#,###"), thanhTien = chiTietHoaDon.ThanhTien.Value.ToString("#,###") , tongGiaHoaDon = tongGiaHoaDon }, JsonRequestBehavior.AllowGet);



        }
        public ActionResult XoaMon(int? chiTietHoaDonID = null)
        {
            var chiTietHoaDon = db.ChiTietHoaDons.FirstOrDefault(x => x.ChiTietHoaDonID == chiTietHoaDonID);
            if (chiTietHoaDon == null)
                return Json(new { kq = "err", msg = "" }, JsonRequestBehavior.AllowGet);
            db.ChiTietHoaDons.Remove(chiTietHoaDon);
            db.SaveChanges();

            var chiTietHoaDons = db.ChiTietHoaDons.Where(x => x.HoaDonID == chiTietHoaDon.HoaDonID);
            string tongGiaHoaDon = "-";
            if (chiTietHoaDons != null)
            {
                var tongTien = chiTietHoaDons.Sum(x => x.ThanhTien);
                tongGiaHoaDon = tongTien == null ? "-" : tongTien.Value.ToString("#,###;-;-");
            }
            return Json(new { kq = "ok", msg = "", tongGiaHoaDon = tongGiaHoaDon }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult CapNhatHoaDon(Guid? hoaDonID = null, float? giamTien = null, float? thanhToan = null)
        {
            var hoaDon = db.HoaDons.FirstOrDefault(x => x.HoaDonID == hoaDonID);
            hoaDon.GiamTien = giamTien;
            hoaDon.ThanhToan = thanhToan;
            hoaDon.DaThanhToan = EnumThanhToan.DA_THANH_TOAN;
            var ban = db.Bans.FirstOrDefault(x=> x.BanID == hoaDon.BanID);
            ban.TrangThai = EnumTrangThaiBan.TRONG;
            db.SaveChanges();
            return Json(new { kq = "ok", msg = "Thanh toán thành công!" }, JsonRequestBehavior.AllowGet);
        }
    }
}
