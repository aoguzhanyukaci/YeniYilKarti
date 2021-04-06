using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YeniYilKarti.Models;

namespace YeniYilKarti.Areas.Admin.Controllers
{
    [Authorize]
    public class KartlarController : AdminBaseController
    {
        // GET: Admin/Kartlar
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Yeni(string ResimAd)
        {
            ViewBag.ResimAd = ResimAd;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Yeni(Kart kart)
        {
            if (ModelState.IsValid)
            {
                kart.UserId = User.Identity.GetUserId();
                db.Kartlar.Add(kart);
                db.SaveChanges();
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        public ActionResult Duzenle(int? id)
        {
            var kart = db.Kartlar.FirstOrDefault(x => x.Id == id);
            if (kart == null)
            {
                return HttpNotFound();
            }

            return View(kart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle(Kart kart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Dashboard");
            }

            return View(kart);
        }

        public ActionResult Sil(int id)
        {
            var kart = db.Kartlar.FirstOrDefault(x => x.Id == id);
            db.Kartlar.Remove(kart);
            db.SaveChanges();
            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult Indir(int id)
        {
            var kart = db.Kartlar.FirstOrDefault(x => x.Id == id);
            string mesaj = kart.Mesaj;
            string mesaj1;
            string mesaj2;
            if (mesaj.Length > 200)
            {
                mesaj1 = mesaj.Substring(0, mesaj.Length / 2);
                mesaj2 = mesaj.Substring((mesaj.Length / 2) + 1);

            }
            else
            {
                mesaj1 = mesaj;
                mesaj2 = "";
            }

            Bitmap bmp = new Bitmap(Server.MapPath($"/Image/0{kart.ResimAd}.png"));
            Bitmap bmp2 = ResizeBitmap(bmp, 420, 330);
            Bitmap bmp3 = new Bitmap(1000, 1000);
            Graphics g = Graphics.FromImage(bmp3);
            g.FillRectangle(Brushes.LightGray, 0, 0, bmp3.Width, bmp3.Height);
            g.DrawImage(bmp2, new PointF(300, 500));
            g.DrawString($"Sevgili {kart.AliciAd},", new Font("Arial", 20, FontStyle.Bold), new SolidBrush(Color.DarkRed), new PointF(100, 100));
            g.DrawString($"{mesaj1}", new Font("Arial", 14, FontStyle.Bold), new SolidBrush(Color.Black), new PointF(30, 250));
            g.DrawString($"{mesaj2}", new Font("Arial", 14, FontStyle.Bold), new SolidBrush(Color.Black), new PointF(30, 250));

            g.DrawString($"Gönderen : {kart.GonderenAd}", new Font("Arial", 20, FontStyle.Bold), new SolidBrush(Color.DarkRed), new PointF(100, 450));
            byte[] data;
            using (MemoryStream ms = new MemoryStream())
            {
                bmp3.Save(ms, ImageFormat.Jpeg);
                data = ms.ToArray();
            }
            string fileName = "resim.jpeg";
            return File(data, "image/jpeg", fileName);
        }

        private Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(bmp, 0, 0, width, height);
            }

            return result;
        }

        public ActionResult Goruntule(int id)
        {
            var kart = db.Kartlar.FirstOrDefault(x => x.Id == id);
            if (kart == null)
            {
                return HttpNotFound();
            }
            return PartialView("_KartPartial", kart);
        }
    }
}