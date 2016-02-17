using System.Data.Entity.Core;
using System.Web.Mvc;
using Advertisement.Models;

namespace Advertisement.Controllers
{
    public class AdvertisementController : Controller
    {
        // GET: Advertisement
        public ActionResult Index()
        {
            return View();
        }

        // GET: Advertisement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Advertisement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Advertisement advertisement, string lat, string lng)
        {
            if (ModelState.IsValid && !string.IsNullOrEmpty(lat) && !string.IsNullOrEmpty(lng))
            {
                bool inserted = AdvertisementModel.SetNewAdvert(advertisement, lat, lng);
                var adverts = AdvertisementModel.GetAllAdverts();
                return RedirectToAction("Index","Home", adverts);
            }
           
                return View(advertisement);
        }

        // GET: Advertisement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                var advert = AdvertisementModel.FindAdvert(id);
                ViewBag.MaintenanceTime = advert.MaintenanceTime.ToString("yyyy-MM-dd");
                return View(advert);
            }
                var adverts = AdvertisementModel.GetAllAdverts();
                return RedirectToAction("Index", "Home", adverts);
        }

        // POST: Advertisement/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Models.Advertisement advertisement, string lat, string lng)
        {
            var updated = AdvertisementModel.UpdateAdvert(id, advertisement, lat, lng);
            var adverts = AdvertisementModel.GetAllAdverts();
            return RedirectToAction("Index", "Home", adverts);
        }

        // GET: Advertisement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                var deleted = AdvertisementModel.DeleteAdvert(id);
                var advert = AdvertisementModel.GetAllAdverts();
                return RedirectToAction("Index", "Home", advert);
            }
            var adverts = AdvertisementModel.GetAllAdverts();
            return RedirectToAction("Index", "Home", adverts);
        }
    }
}
