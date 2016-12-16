using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web.Mvc;
using AppTour.Business.Services.Author;
using AppTour.Business.Services.Enterprise;
using AppTour.Model.Models.Author;
using AppTour.Business.ServiceContracts.Author;

namespace AppTour.UI.Web.WebPortal.Controllers
{
    public class AuthorController : Controller
    {

        #region - Attributes
        private IAuthorService service = new AuthorService();
        #endregion

        #region + ActionResult Index
        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            return View(service.GetAuthors());
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
        public ActionResult Create(AuthorModel author)
        {
            if (author.Enterprise.Id != Guid.Empty && author.Enterprise.Id != null)
            {
                author.Enterprise = new EnterpriseService().GetEnterprise(author.Enterprise.Id);
            }
            var context = new ValidationContext(author, null, null);
            var results = new List<ValidationResult>();

            if (Validator.TryValidateObject(author, context, results, true))
            {
                try
                {
                    if (author.IsValid)
                    {
                        service.InsertAuthor(author);
                    }
                    else
                    {
                        ViewBag.Enterprises = new SelectList(new EnterpriseService().GetEnterprises(), "Id", "Name");
                        ModelState.AddModelError("", author.Error);
                        return View("Create", author);
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Enterprises = new SelectList(new EnterpriseService().GetEnterprises(), "Id", "Name");
                    ModelState.AddModelError("", e.Message);
                    return View("Create", author);
                }
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region + ActionResult Edit
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(Guid id)
        {
            AuthorModel a = service.GetAuthor(id);
            ViewBag.Enterprises = new SelectList(new EnterpriseService().GetEnterprises(), "Id", "Name", a.Enterprise.Id);
            return View(service.GetAuthor(id));
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(AuthorModel author)
        {
            if (author.Enterprise.Id != Guid.Empty && author.Enterprise.Id != null)
            {
                author.Enterprise = new EnterpriseService().GetEnterprise(author.Enterprise.Id);
            }
            var context = new ValidationContext(author, null, null);
            var results = new List<ValidationResult>();

            if (Validator.TryValidateObject(author, context, results, true))
            {
                try
                {
                    if (author.IsValid)
                    {
                        service.UpdateAuthor(author);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Enterprises = new SelectList(new EnterpriseService().GetEnterprises(), "Id", "Name", author.Enterprise.Id);
                        ModelState.AddModelError("", author.Error);
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Enterprises = new SelectList(new EnterpriseService().GetEnterprises(), "Id", "Name", author.Enterprise.Id);
                    ModelState.AddModelError("", e.Message);
                }
            }
            return View(author);
        }
        #endregion

        #region + ActionResult Delete
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(Guid id)
        {
            return View(service.GetAuthor(id));
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(Guid id, AuthorModel author)
        {
            if (id == author.Id)
            {
                try
                {
                    service.DeleteAuthor(author);
                    return RedirectToAction("Index");
                }
                catch (UpdateException)
                {
                    author = service.GetAuthor(id);
                    ViewData.Add(new KeyValuePair<string, object>(ViewRes.Author.Erro + ":", ViewRes.Author.UpdateException));
                }
                catch (Exception e)
                {
                    author = service.GetAuthor(id);
                    ModelState.AddModelError("", e.Message);
                    ViewData.Add(new KeyValuePair<string, object>(ViewRes.Author.Erro + ":", ViewRes.Author.UpdateException));
                }
            }

            return View(author);
        }
        #endregion

    }
}
