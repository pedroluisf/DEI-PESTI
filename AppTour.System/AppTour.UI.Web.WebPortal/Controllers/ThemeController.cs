using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using AppTour.Business.ServiceContracts.Theme;
using AppTour.Business.Services.Theme;
using AppTour.Model.Models.Theme;

namespace AppTour.UI.Web.WebPortal.Controllers
{
    public class ThemeController : Controller
    {
        #region - Attributes
        private IThemeService service = new ThemeService();
        #endregion

        #region + ActionResult Index
        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            return View(service.GetThemes());
        }
        #endregion

        #region + ActionResult Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult Create(ThemeModel theme)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (theme.IsValid)
                    {
                        service.InsertTheme(theme);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", theme.Error);
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);

                }
            }
            return View(theme);
        }
        #endregion

        #region + ActionResult Edit
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(Guid id)
        {
            return View(service.GetTheme(id));
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(ThemeModel theme)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (theme.IsValid)
                    {
                        service.UpdateTheme(theme);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", theme.Error);
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            return View(theme);
        }
        #endregion

        #region + ActionResult Delete
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(Guid id)
        {
            return View(service.GetTheme(id));
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(Guid id, ThemeModel theme)
        {
            if (id == theme.Id)
            {
                try
                {
                    service.DeleteTheme(theme);
                    return RedirectToAction("Index");
                }
                catch (UpdateException)
                {
                    theme = service.GetTheme(id);
                    ViewData.Add(new KeyValuePair<string, object>(ViewRes.Theme.Erro + ':', ViewRes.Theme.UpdateException));
                }
                catch (Exception e)
                {
                    theme = service.GetTheme(id);
                    ModelState.AddModelError("", e.Message);
                    ViewData.Add(new KeyValuePair<string, object>(ViewRes.Theme.Erro + ':', ViewRes.Theme.UpdateException));
                }
            }

            return View(theme);
        }
        #endregion
    }
}
