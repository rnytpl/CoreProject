using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreProject.Models;
namespace CoreProject.Controllers
{
    public class BirimController : Controller
    {

        Context db = new Context();
        public IActionResult Index()
        {
            List<Birim> birimler = db.Birims.ToList();

            return View(birimler);
        }

        public IActionResult YeniBirim()
        {
            return View();
        }

        [HttpPost]
        public IActionResult YeniBirim(Birim d)
        {
            db.Birims.Add(d);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult BirimSil(int id)
        {
            var dep = db.Birims.Find(id);

            db.Birims.Remove(dep);

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult BirimGetir(int ID)
        {
            var dep = db.Birims.Find(ID);

            return View(dep);
        }

        [HttpPost]
        public IActionResult BirimGüncelle(Birim dep)
        {
            var birim = db.Birims.Find(dep.BirimID);

            birim.BirimAd = dep.BirimAd;


            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult BirimDetay(int ID)
        {
            List<Personel> personeller = db.Personels.Where(x => x.BirimID == ID).ToList();
            var birimAdı = db.Birims.Where(x => x.BirimID == ID).Select(y => y.BirimAd).FirstOrDefault();

            ViewBag.BirimAd = birimAdı;
            return View(personeller);
        }
    }
}
