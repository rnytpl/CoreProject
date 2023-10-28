using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace CoreProject.Controllers
{
    public class PersonelController : Controller
    {
        Context db = new Context();

        [Authorize]
        public IActionResult Index()
        {
            List<Personel> personeller = db.Personels.Include(x => x.Birim).ToList();
            return View(personeller);
        }

        public IActionResult YeniPersonel()
        {
            List<SelectListItem> degerler = (from x in db.Birims.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.BirimAd,
                                                 Value = x.BirimID.ToString()
                                             }
                                             ).ToList();

            ViewBag.dgr = degerler;

            return View();
        }

        [HttpPost]

        public IActionResult YeniPersonel(Personel p)
        {
            
            // Filter by matching each instance id of Birims collection in database
            // with incoming input's id and show the first result
            var per = db.Birims.Where(x => x.BirimID == p.Birim.BirimID).FirstOrDefault();
            // Assign incoming parameter's Birim property to retrieved data from database
            p.Birim = per;
            db.Personels.Add(p);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult PersonelSil (int ID)
        {
            var personel = db.Personels.Find(ID);
            db.Personels.Remove(personel);
            db.SaveChanges();

            return RedirectToAction("Index");
        }



        public IActionResult PersonelGetir(int ID)
        {
            List<SelectListItem> birimler = (from x in db.Birims.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.BirimAd,
                                                 Value = x.BirimID.ToString()
                                             }
                                             ).ToList();

            var personel = db.Personels.Find(ID);

            ViewBag.birims = birimler;
            return View(personel);
        }

        public IActionResult PersonelGüncelle(Personel personel)
        {
            var findPersonel = db.Personels.Find(personel.PersonelID);
            findPersonel.Ad = personel.Ad;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
