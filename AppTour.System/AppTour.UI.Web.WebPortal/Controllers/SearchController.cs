using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AppTour.Business.Services.SearchProfile;
using AppTour.Business.Services.Service;
using AppTour.Model.Models.Point;
using AppTour.Model.Models.Search;
using AppTour.Model.Models.SearchProfile;

namespace AppTour.UI.Web.WebPortal.Controllers
{
    public class SearchController : Controller
    {
        #region + ActionResult Index()
        public ActionResult Index()
        {
            IList<SearchProfileModel> SearchProfiles = new List<SearchProfileModel>();

            if (Session.Count > 2)
            {
                SearchProfiles = new SearchProfileService().GetSearchProfiles(new Guid(Session["USERNAME_KEY"].ToString()));
            }

            ViewBag.SearchProfile = new SelectList(SearchProfiles, "Id", "Name");

            SearchModel model = new SearchModel
            {
                Coordenate = Session["COORDENATES"].ToString(),
                Terms = string.Empty
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(SearchModel model)
        {
            if (model.IsValid)
            {
                IList<PointModel> points = new SearchService().Search(model);

                IList<SearchProfileModel> SearchProfiles = new List<SearchProfileModel>();
                if (Session.Count > 2)
                {
                    SearchProfiles = new SearchProfileService().GetSearchProfiles(new Guid(Session["USERNAME_KEY"].ToString()));
                }
                ViewBag.SearchProfile = new SelectList(SearchProfiles, "Id", "Name");
                ViewBag.Points = points;
                return View(model);
            }
            IList<SearchProfileModel> sp = new List<SearchProfileModel>();
            if (Session.Count > 2)
            {
                sp = new SearchProfileService().GetSearchProfiles(new Guid(Session["USERNAME_KEY"].ToString()));
            }
            ViewBag.SearchProfile = new SelectList(sp, "Id", "Name", model.SearchProfile.Id);


            return RedirectToAction("Index");
        }
        #endregion

    }
}
