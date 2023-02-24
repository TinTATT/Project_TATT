using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLNhaHang.Models;
namespace QLNhaHang.Controllers
{
    public class KhuyenMaiController : Controller
    {
        QLNhaHangEntities db = new QLNhaHangEntities();
        // GET: KhuyenMai
        public ActionResult Index()
        {
            var khuyenMai = db.KhuyenMais.OrderBy(x=> x.KhuyenMaiID);
            return View(khuyenMai);
        }
        public ActionResult CapNhat(int id = 0)
        {
            if (id != 0)
            {
                var nd = db.KhuyenMais.Where(a => a.KhuyenMaiID == id).FirstOrDefault();
                return View(nd);
            }
            else
            {

                return View(new Models.KhuyenMai());
            }
        }
        [HttpPost]
        public ActionResult CapNhat(KhuyenMai nd = null, string _tuNgay = "", string _denNgay = "")
        {

            nd.TuNgay = DateTime.Parse(String.Format("{0:d/M/yyyy HH:mm:ss}", _tuNgay));
            nd.DenNgay = DateTime.Parse(String.Format("{0:d/M/yyyy HH:mm:ss}", _denNgay));
            if (nd.KhuyenMaiID == 0)
            {
                db.KhuyenMais.Add(nd);
                db.SaveChanges();
            }
            else
            {
                var data = db.KhuyenMais.Where(a => a.KhuyenMaiID == nd.KhuyenMaiID).FirstOrDefault();
                data.MatHangID = nd.MatHangID;
                data.TrangThai = nd.TrangThai;
                data.PhanTramKM = nd.PhanTramKM;
                data.TuNgay = nd.TuNgay;
                data.DenNgay = nd.DenNgay;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Xoa(int id = 0)
        {
            if (id != 0)
            {
                var nd = db.KhuyenMais.Where(a => a.KhuyenMaiID == id).FirstOrDefault();
                db.KhuyenMais.Remove(nd);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}