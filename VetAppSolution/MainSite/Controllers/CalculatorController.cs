using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MainSite.ViewModels;

namespace MainSite.Controllers
{

    public class CalculatorController : Controller
    {
        public ActionResult Index()
        {
            CalculatorViewModel model = getModel();
            return View(model);
        }
        public ActionResult doWorkingItemRating(string id)
        {
            CalculatorViewModel model = getModel();
            model.workingItem.RatingID = id;
            TempData["oCalcModel"] = model;
            return View("Index", model);
        }
        public ActionResult doWorkingItemBilateral(string id)
        {
            CalculatorViewModel model = getModel();
            model.workingItem.BilateralFactorID = id;
            TempData["oCalcModel"] = model;
            return View("Index", model);
        }

        private CalculatorViewModel getModel()
        {
            CalculatorViewModel model = null;
            if (TempData["oCalcModel"] == null)
            {
                model = new CalculatorViewModel();
                TempData["oCalcModel"] = model;
            }
            else
            {
                model = (CalculatorViewModel)TempData["oCalcModel"];
            }
            return model;
        }
    }
}