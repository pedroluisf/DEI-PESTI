using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web.Mvc;
using AppTour.Business.Services.App;
using AppTour.Business.Services.Enterprise;
using AppTour.Model.Models.App;
using AppTour.UI.Web.WebPortal.Helpers;
using AppTour.Business.ServiceContracts.App;
namespace AppTour.UI.Web.WebPortal.Controllers
{
    public class AppController : Controller
    {
        #region + Attributes
        private IAppService service = new AppService();
        #endregion

        #region + ActionResult Index
        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            return View(service.GetApps());
        }
        #endregion

        #region + ActionResult Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            ViewBag.Enterprises = new SelectList(new EnterpriseService().GetEnterprises(), "Id", "Name");
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult Create(AppModel app)
        {
            if (app.Enterprise.Id != Guid.Empty && app.Enterprise.Id != null)
            {
                app.Enterprise = new EnterpriseService().GetEnterprise(app.Enterprise.Id);
            }
            var context = new ValidationContext(app, null, null);
            var results = new List<ValidationResult>();

            if (Validator.TryValidateObject(app, context, results, true))
            {
                if (app.IsValid)
                    try
                    {
                        service.InsertApp(app);
                    }
                    catch (Exception e)
                    {
                        ViewBag.Enterprises = new SelectList(new EnterpriseService().GetEnterprises(), "Id", "Name");
                        ModelState.AddModelError("", e.Message);
                        return View("Create", app);
                    }
                else
                {
                    ViewBag.Enterprises = new SelectList(new EnterpriseService().GetEnterprises(), "Id", "Name");
                    ModelState.AddModelError("", app.Error);
                    return View("Create", app);
                }
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region + ActionResult Edit
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(Guid id)
        {
            AppModel a = service.GetApp(id);
            ViewBag.Enterprises = new SelectList(new EnterpriseService().GetEnterprises(), "Id", "Name", a.Enterprise.Id);
            return View(service.GetApp(id));
        }

        [HttpPost]
        public ActionResult Edit(AppModel app)
        {
            if (app.Enterprise.Id != Guid.Empty && app.Enterprise.Id != null)
            {
                app.Enterprise = new EnterpriseService().GetEnterprise(app.Enterprise.Id);
            }
            var context = new ValidationContext(app, null, null);
            var results = new List<ValidationResult>();

            if (Validator.TryValidateObject(app, context, results, true))
            {
                try
                {
                    if (app.IsValid)
                    {
                        service.UpdateApp(app);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Enterprises = new SelectList(new EnterpriseService().GetEnterprises(), "Id", "Name", app.Enterprise.Id);
                        ModelState.AddModelError("", app.Error);
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Enterprises = new SelectList(new EnterpriseService().GetEnterprises(), "Id", "Name", app.Enterprise.Id);
                    ModelState.AddModelError("", e.Message);
                }
            }

            return View("Edit", app);
        }
        #endregion

        #region + ActionResult Delete
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(Guid id)
        {
            return View(service.GetApp(id));
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(Guid id, AppModel app)
        {
            if (id == app.Id)
            {
                try
                {
                    service.DeleteApp(app);
                    return RedirectToAction("Index");
                }
                catch (UpdateException)
                {
                    app = service.GetApp(id);
                    ViewData.Add(new KeyValuePair<string, object>(ViewRes.App.Erro + ":", ViewRes.App.UpdateException));
                }
                catch (Exception e)
                {
                    app = service.GetApp(id);
                    ModelState.AddModelError("", e.Message);
                    ViewData.Add(new KeyValuePair<string, object>(ViewRes.App.Erro + ":", ViewRes.App.UpdateException));
                }
            }

            return View("Delete", app);
        }
        #endregion

    }
}
