using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLNhaHang.Controllers
{
    public class KhuVucController : Controller
    {
        QLNhaHang.Models.QLNhaHangEntities db = new Models.QLNhaHangEntities();
        // GET: KhuVuc
        public ActionResult Index()
        {
            var dsKhuVuc = db.KhuVucs.OrderBy(x=> x.KhuVucID);
            return View(dsKhuVuc);
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
        public ActionResult CapNhat(QLNhaHang.Models.KhuVuc nd = null, HttpPostedFileBase AnhDaiDien = null)
        {
            string[] url1 = new string[6];
            if (AnhDaiDien != null) url1 = uploadFile(AnhDaiDien);
            string UrlAnhDaiDien = ((AnhDaiDien != null) ? url1[1].Substring(1, url1[1].Length - 1) : null);
            if (AnhDaiDien != null)
            {
                nd.Anh = UrlAnhDaiDien;
            }
           
            if (nd.KhuVucID == 0)
            {
                db.KhuVucs.Add(nd);
                db.SaveChanges();
            }
            else
            {
                var data = db.KhuVucs.Where(a => a.KhuVucID == nd.KhuVucID).FirstOrDefault();
                data.TenKhuVuc = nd.TenKhuVuc;
     
                if (AnhDaiDien != null)
                {
                    data.Anh = nd.Anh;
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult CapNhat(int id = 0)
        {
            if (id != 0)
            {
                var nd = db.KhuVucs.Where(a => a.KhuVucID == id).FirstOrDefault();
                return View(nd);
            }
            else
            {

                return View(new Models.KhuVuc());
            }
        }
        public ActionResult Xoa(int id = 0)
        {
            if (id != 0)
            {
                var nd = db.KhuVucs.Where(a => a.KhuVucID == id).FirstOrDefault();
                db.KhuVucs.Remove(nd);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}