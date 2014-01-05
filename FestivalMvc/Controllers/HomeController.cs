using Festival.Model;
using FestivalMvc.Models.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FestivalMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ObservableCollection<LineUp> ocLineUp = LineUpRepository.GetLineUps();

            return View(ocLineUp);
        }

        public ActionResult Details(int id = 0)
        {
            LineUp l = LineUp.GetLineUpByBand(id);

            return View(l);
        }
    }
}
