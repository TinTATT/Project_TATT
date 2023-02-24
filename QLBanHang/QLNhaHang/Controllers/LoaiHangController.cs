using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLNhaHang.Controllers
{
    public class LoaiHangController : Controller
    {
        QLNhaHang.Models.QLNhaHangEntities db = new Models.QLNhaHangEntities();
        // GET: LoaiHang
        public ActionResult Index()
        {
            var dsLoaiHangs = db.LoaiHangs.OrderBy(x=> x.LoaiHangID);

            return View(dsLoaiHangs);
        }

        [HttpPost]
        public ActionResult CapNhat(QLNhaHang.Models.LoaiHang nd = null)
        {
            if (nd.LoaiHangID == 0)
            {
                db.LoaiHangs.Add(nd);
                db.SaveChanges();
            }
            else
            {
                var data = db.LoaiHangs.Where(a => a.LoaiHangID == nd.LoaiHangID).FirstOrDefault();
                data.TenLoaiHang = nd.TenLoaiHang;
              
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult CapNhat(int id = 0)
        {
            if (id != 0)
            {
                var nd = db.LoaiHangs.Where(a => a.LoaiHangID == id).FirstOrDefault();
                return View(nd);
            }
            else
            {

                return View(new Models.LoaiHang());
            }
        }
        public ActionResult Xoa(int id = 0)
        {
            if (id != 0)
            {
                var nd = db.LoaiHangs.Where(a => a.LoaiHangID == id).FirstOrDefault();
                db.LoaiHangs.Remove(nd);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}