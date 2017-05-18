using System;
using System.Web.Mvc;
using MainSite.ViewModels;

namespace MainSite.Controllers
{
    [Authorize]
    public class CalculatorController : Controller
    {
        public ActionResult Index()
        {
            CalculatorViewModel model = getModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult ProcessForm(string submitVal)
        {
            CalculatorViewModel model = getModel();

            switch (submitVal)
            {
                case "10":
                case "20":
                case "30":
                case "40":
                case "50":
                case "60":
                case "70":
                case "80":
                case "90":
                    model.workingItem.RatingID = Convert.ToInt32(submitVal);
                    model.AddItem();
                    break;
                case "Bilateral Upper":
                    model.workingItem.BilateralFactorID = "1";
                    break;
                case "Right Upper":
                    model.workingItem.BilateralFactorID = "2";
                    break;
                case "Left Upper":
                    model.workingItem.BilateralFactorID = "3";
                    break;
                case "Left Lower":
                    model.workingItem.BilateralFactorID = "4";
                    break;
                case "Right Lower":
                    model.workingItem.BilateralFactorID = "5";
                    break;
                case "Bilateral Lower":
                    model.workingItem.BilateralFactorID = "6";
                    break;
                case "Clear":
                    model.Clear();
                    break;
                default:
                    break;
            }

            TempData["oCalcModel"] = model;
            return View("Index", model);
        }

        public ActionResult RemoveItem(int id)
        {
            CalculatorViewModel model = getModel();
            model.RemoveItem(id);
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