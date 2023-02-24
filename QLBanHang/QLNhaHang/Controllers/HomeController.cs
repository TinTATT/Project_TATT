using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLNhaHang.Models;
namespace QLNhaHang.Controllers
{
    public class HomeController : Controller
    {
        QLNhaHangEntities db = new QLNhaHangEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ThongTinCaNhan(int id = 0)
        {

            return View();

        }

        private string[] uploadFile(HttpPostedFileBase file, int? idFile = 0)
        {
            string Root_Path = "~/FileUpload";
            string[] arrayFile = new string[6];
            arrayFile[4] = "false";
            if (file != null)
            {
                List<string> listEx = new List<string>("pdf".Split('|'));
                string extension = Path.GetExtension(file.FileName).ToLower();
                //if (listEx.IndexOf(extension.Replace(".", "")) > -1)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var datenow = DateTime.Now;
                    //string[] time = datenow.ToString("dd:MM:yy:HH:mm:ss").Split(':');
                    var filePath = Path.Combine(Request.MapPath(Root_Path));
                    if (!System.IO.File.Exists(filePath)) Directory.CreateDirectory(filePath);
                    string fileNameOnyly = idFile.ToString() + "-";
                    //foreach (string item in time) fileNameOnyly += item;
                    fileNameOnyly += Guid.NewGuid();
                    fileName = fileNameOnyly + extension;
                    filePath = Path.Combine(filePath, fileName);
                    file.SaveAs(filePath);
                    arrayFile[0] = fileName;
                    arrayFile[1] = Root_Path + @"/" + fileName;
                    arrayFile[2] = extension;
                    arrayFile[3] = Root_Path;
                    FileInfo info = new FileInfo(filePath);
                    arrayFile[4] = "true";
                    arrayFile[5] = info.Length.ToString();
                }
            }
            return arrayFile;
        }

        [HttpPost]
        public ActionResult ThongTinCaNhan(NguoiDung nd = null, string _NgaySinh = "", HttpPostedFileBase AnhDaiDien = null)
        {
            string[] url1 = new string[6];
            if (AnhDaiDien != null) url1 = uploadFile(AnhDaiDien);
            string UrlAnhDaiDien = ((AnhDaiDien != null) ? url1[1].Substring(1, url1[1].Length - 1) : null);
            if (AnhDaiDien != null)
            {
                nd.AnhDaiDien = UrlAnhDaiDien;
            }
            nd.NgaySinh = DateTime.Parse(String.Format("{0:d/M/yyyy HH:mm:ss}", _NgaySinh));
            if (nd.NguoiDungID == 0)
            {
                db.NguoiDungs.Add(nd);
                db.SaveChanges();
            }
            else
            {
                var data = db.NguoiDungs.Where(a => a.NguoiDungID == nd.NguoiDungID).FirstOrDefault();
                data.NgaySinh = nd.NgaySinh;
                data.MatKhau = nd.MatKhau;
                data.HoTen = nd.HoTen;
                data.GioiTinh = nd.GioiTinh;
                data.DiaChi = nd.DiaChi;
                data.SoDienThoai = nd.SoDienThoai;
                data.Email = nd.Email;
             

                data.NgaySinh = nd.NgaySinh;
                data.GioiTinh = nd.GioiTinh;
                data.Email = nd.Email;
                data.DiaChi = nd.DiaChi;
                data.SoDienThoai = nd.SoDienThoai;
                if (AnhDaiDien != null)
                {
                    data.AnhDaiDien = nd.AnhDaiDien;
                }
                Session["NguoiDung"] = data;

                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult DangNhap()
        {
            ViewBag.TaiKhoan = "";
            ViewBag.MatKhau = "";
            ViewBag.TT = 0;
            return PartialView();
        }
        public ActionResult DangXuat()
        {

            Session["NguoiDung"] = null;
            return RedirectToAction("DANGNHAP");
        }
       
        [HttpPost]
        public ActionResult DangNhap(string TaiKhoan = "", string MatKhau = "")
        {
            var data = db.NguoiDungs.Where(a => a.TenDangNhap == TaiKhoan && a.MatKhau == MatKhau).FirstOrDefault();
            if (data != null)
            {
                Session["NguoiDung"] = data;
                return RedirectToAction("index", "Home");
            }
            else
            {
                ViewBag.TaiKhoan = TaiKhoan;
                ViewBag.MatKhau = MatKhau;
                ViewBag.TT = 1;
            }
            return PartialView();
        }

    }
}