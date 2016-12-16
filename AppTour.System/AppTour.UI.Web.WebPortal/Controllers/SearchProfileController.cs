using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AppTour.Business.ServiceContracts.SearchProfile;
using AppTour.Business.Services.SearchProfile;
using AppTour.Business.Services.Theme;
using AppTour.Business.Services.Topic;
using AppTour.Business.Services.User;
using AppTour.Model.Models.SearchProfile;
using AppTour.Model.Models.Theme;
using AppTour.Model.Models.Topic;

namespace AppTour.UI.Web.WebPortal.Controllers
{
    public class SearchProfileController : Controller
    {
        #region - Atributos
        private ISearchProfileService service = new SearchProfileService();
        #endregion

        #region + ActionResult MyProfiles()
        public ActionResult MyProfiles()
        {
            UserService userService = new UserService();

            string userName = this.Session["USERNAME"].ToString();

            if (userName != null || userName != string.Empty)
            {
                Guid userId = userService.GetUser(userName).Id;

                return PartialView(service.GetSearchProfiles(userId));
            }
            return PartialView("error");
        }
        #endregion

        #region + ActionResult Create
        public ActionResult Create()
        {
            IList<ThemeModel> temas = new ThemeService().GetActiveThemes();
            IList<TopicsViewModel> _topics = new List<TopicsViewModel>();

            IList<TopicModel> topics = new TopicService().GetActiveTopics()
                                                    .OrderBy(x => x.Theme.Name)
                                                    .ToList();

            topics.ToList().ForEach(x => _topics.Add(new TopicsViewModel
            {
                Name = x.Name,
                Check = false,
                Id = x.Id,
                ThemeId = x.Theme.Id,
                ThemeName = x.Theme.Name
            }));

            SearchProfileModel model = new SearchProfileModel
            {
                SearchProfileCheckBoxs = _topics
            };

            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(SearchProfileModel model)
        {
            if (model.IsValid)
            {
                try
                {
                    string userName = this.Session["USERNAME"].ToString();

                    if (model.SearchProfileCheckBoxs.Where(x => x.Check).Count() == 0)
                    {
                        ModelState.AddModelError("", "Must choose a topic, at least!");
                        return View(model);
                    }

                    model.SearchProfileTopics = new List<TopicModel>();
                    model.Utilizador = new UserService().GetUser(userName);
                    model.IsActive = true;
                    foreach (TopicsViewModel item in model.SearchProfileCheckBoxs.Where(x => x.Check))
                    {
                        model.SearchProfileTopics.Add(new TopicService().GetTopic(item.Id));
                    }

                    service.InsertSearchProfile(model);
                    return RedirectToAction("Profile", "Account", new { id = userName });
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            IList<ThemeModel> temas = new ThemeService().GetActiveThemes();

            IList<TopicsViewModel> _topics = new List<TopicsViewModel>();

            IList<TopicModel> topics = new TopicService().GetActiveTopics().OrderBy(x => x.Theme.Name).ToList();

            topics.ToList().ForEach(x => _topics.Add(new TopicsViewModel { Name = x.Name, Check = false, Id = x.Id, ThemeId = x.Theme.Id, ThemeName = x.Theme.Name }));
            model = new SearchProfileModel
            {
                SearchProfileCheckBoxs = _topics
            };

            ModelState.AddModelError("", model.Error);
            return View(model);
        }
        #endregion
    }
}
