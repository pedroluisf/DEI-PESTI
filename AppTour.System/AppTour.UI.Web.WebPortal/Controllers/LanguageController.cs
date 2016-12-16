using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.Mvc;
using AppTour.Business.Services.Language;
using AppTour.Model.Models.Language;
using AppTour.Business.ServiceContracts.Language;

namespace AppTour.UI.Web.WebPortal.Controllers
{
    public class LanguageController : Controller
    {
        #region - Attributos
        ILanguageService service = new LanguageService();
        #endregion

        #region + ActionResult Index
        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            return View(service.GetLanguages());
        }
        #endregion

        #region + ActionResult Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public ActionResult Create(LanguageModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    service.InsertLanguage(model);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    return View("Index", model);
                }
            }
            return View("Create", model);
        }
        #endregion

        #region + ActionResult Edit
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(Guid id)
        {
            return View(service.GetLanguage(id));
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public ActionResult Edit(LanguageModel language)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (language.IsValid)
                    {
                        service.UpdateLanguage(language);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", language.Error);
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            return View(language);
        }
        #endregion

        #region + ActionResult Delete
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(Guid id)
        {
            return View(service.GetLanguage(id));
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(Guid id, LanguageModel language)
        {
            if (id == language.Id)
            {
                try
                {
                    service.DeleteLanguage(language);
                    return RedirectToAction("Index");
                }
                catch (UpdateException)
                {
                    language = service.GetLanguage(id);
                    ViewData.Add(new KeyValuePair<string, object>(ViewRes.Language.Erro + ':', ViewRes.Language.UpdateException));
                }
                catch (Exception e)
                {
                    language = service.GetLanguage(id);
                    ModelState.AddModelError("", e.Message);
                    ViewData.Add(new KeyValuePair<string, object>(ViewRes.Language.Erro + ':', ViewRes.Language.UpdateException));
                }
            }

            return View("Delete", language);
        }
        #endregion

        #region + ActionResult ChangeCulture(string lang, string returnUrl)
        public ActionResult ChangeCulture(string lang, string returnUrl)
        {
            Session["Culture"] = new CultureInfo(lang);
            return Redirect(returnUrl);
        }
        #endregion

    }
}
