using System.Linq;
using System.Web.Mvc;
using Advertisement.Models;

namespace Advertisement.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            var a = AdvertisementModel.GetAllAdverts();
            return View(a.ToList());
        }

        public ActionResult GetPopUp(int? id)
        {
            return View();
        }
    }
}