using System.Web.Mvc;
namespace AppTour.UI.Web.WebPortal.Controllers
{
    public class HomeController : Controller
    {
        #region + ActionResult Index()
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region + About()
        public ActionResult About()
        {
            return View();
        }
        #endregion

        #region + ActionResult SaveMyPosition(string id)
        public ActionResult SaveMyPosition(string id)
        {
            if (id == null || id == string.Empty)
                return null;

            this.Session["COORDENATES"] = id;
            return Content("sucesso");
        }
        #endregion
    }
}
