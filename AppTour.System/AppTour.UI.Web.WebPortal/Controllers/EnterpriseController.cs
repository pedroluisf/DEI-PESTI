using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using AppTour.Business.ServiceContracts.Enterprise;
using AppTour.Business.Services.Enterprise;
using AppTour.Model.Models.Enterprise;

namespace AppTour.UI.Web.WebPortal.Controllers
{
    public class EnterpriseController : Controller
    {
        #region - Atributos
        private IEnterpriseService service = new EnterpriseService();
        #endregion

        #region + ActionResult Index
        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            return View(service.GetEnterprises());
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
        public ActionResult Create(EnterpriseModel enterprise)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (enterprise.IsValid)
                        service.InsertEnterprise(enterprise);
                    else
                    {
                        ModelState.AddModelError("", enterprise.Error);
                        return View("Create", enterprise);
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    return View("Create", enterprise);
                }
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region + ActionResult Edit
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(Guid id)
        {
            return View(service.GetEnterprise(id));
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(EnterpriseModel enterprise)
        {
            if (enterprise.IsValid)
            {
                try
                {
                    service.UpdateEnterprise(enterprise);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            else
            {
                ModelState.AddModelError("", enterprise.Error);
            }
            return View(enterprise);
        }
        #endregion

        #region + ActionResult Delete
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(Guid id)
        {
            return View(service.GetEnterprise(id));
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(Guid id, EnterpriseModel enterprise)
        {
            if (id == enterprise.Id)
            {
                try
                {
                    service.DeleteEnterprise(enterprise);
                    return RedirectToAction("Index");
                }
                catch (UpdateException)
                {
                    enterprise = service.GetEnterprise(id);
                    ViewData.Add(new KeyValuePair<string, object>(ViewRes.Enterprise.Erro + ':', ViewRes.Enterprise.UpdateException));
                }
                catch (Exception e)
                {
                    enterprise = service.GetEnterprise(id);
                    ModelState.AddModelError("", e.Message);
                    ViewData.Add(new KeyValuePair<string, object>(ViewRes.Enterprise.Erro + ':', ViewRes.Enterprise.UpdateException));
                }
            }

            return View("Delete", enterprise);
        }
    }
        #endregion

}
