using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLNhaHang.Controllers
{
    public class MatHangController : Controller
    {
        QLNhaHang.Models.QLNhaHangEntities db = new Models.QLNhaHangEntities();
        // GET: MatHang
        public ActionResult Index(int LoaiHangID = 0)
        {
            ViewBag.LoaiHangID = LoaiHangID;
    
            if (LoaiHangID != 0)
            {
                return View(db.MatHangs.Where(a => a.LoaiHangID == LoaiHangID));

            }
     
            return View(db.MatHangs);
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
        public ActionResult CapNhat(QLNhaHang.Models.MatHang nd = null, HttpPostedFileBase AnhDaiDien = null)
        {
            string[] url1 = new string[6];
            if (AnhDaiDien != null) url1 = uploadFile(AnhDaiDien);
            string UrlAnhDaiDien = ((AnhDaiDien != null) ? url1[1].Substring(1, url1[1].Length - 1) : null);
            if (AnhDaiDien != null)
            {
                nd.FileAnh = UrlAnhDaiDien;
            }

            if (nd.MatHangID == 0)
            {
                db.MatHangs.Add(nd);
                db.SaveChanges();
            }
            else
            {
                var data = db.MatHangs.Where(a => a.MatHangID == nd.MatHangID).FirstOrDefault();
                data.TenMatHang = nd.TenMatHang;
                data.LoaiHangID = nd.LoaiHangID;
                data.Gia = nd.Gia;
                if (AnhDaiDien != null)
                {
                    data.FileAnh = nd.FileAnh;
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult CapNhat(int id = 0)
        {
            if (id != 0)
            {
                var nd = db.MatHangs.Where(a => a.MatHangID == id).FirstOrDefault();
                return View(nd);
            }
            else
            {

                return View(new Models.MatHang());
            }
        }
        public ActionResult Xoa(int id = 0)
        {
            if (id != 0)
            {
                var nd = db.MatHangs.Where(a => a.MatHangID == id).FirstOrDefault();
                db.MatHangs.Remove(nd);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
