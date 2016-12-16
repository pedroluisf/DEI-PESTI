using System;
using System.Web.Mvc;
using AppTour.Business.Services.Language;
using AppTour.Business.Services.Translation;
using AppTour.Model.Models.Translation;

namespace AppTour.UI.Web.WebPortal.Controllers
{
    public class TranslationController : Controller
    {
        #region - Atributos
        private TranslationService service = new TranslationService();
        #endregion

        #region + Record(string Table, Guid ForeignId, string Instance)
        public ActionResult Record(string Table, Guid ForeignId, string Instance)
        {
            ViewBag.Table = Table;
            ViewBag.ForeignId = ForeignId;
            ViewBag.Modelo = Instance;

            return View(service.GetTranslations(Table, ForeignId));
        }
        #endregion

        #region + CreateRecord(string Table, Guid ForeignId, string Instance)
        public ActionResult CreateRecord(string Table, Guid ForeignId, string Instance)
        {
            ViewBag.Languages = new SelectList(new LanguageService().GetLanguages(), "ID", "NAME");

            ViewBag.Table = Table;
            ViewBag.ForeignId = ForeignId;
            ViewBag.Modelo = Instance;

            TranslationModel model = new TranslationModel
            {
                Id = Guid.NewGuid(),
                ForeignId = ForeignId,
                TableName = Table
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateRecord(TranslationModel model)
        {
            try
            {
                if (model.IsValid)
                {
                    service.InsertTranslation(model);
                    return RedirectToAction("Record", new { Table = model.TableName, ForeignId = model.ForeignId, Instance = model.Instance });
                }
                else
                {
                    ModelState.AddModelError("", model.Error);
                    ViewBag.Languages = new SelectList(new LanguageService().GetLanguages(), "ID", "NAME");
                    return View("CreateRecord", model);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ViewBag.Languages = new SelectList(new LanguageService().GetLanguages(), "ID", "NAME");
                return View("CreateRecord", model);
            }
        }
        #endregion

        #region + DeleteRecord(string id)
        public ActionResult DeleteRecord(string id)
        {
            if (id == null)
                return Content(ViewRes.SharedStrings.NotExists);
            try
            {
                TranslationModel model = service.GetTranslation(new Guid(id));

                if (model != null)
                {
                    service.DeleteTranslation(model);
                    return Content("Sucesso");
                }
            }
            catch (Exception)
            {
                return Content(ViewRes.SharedStrings.NotExists);
            }
            return Content(ViewRes.SharedStrings.NotExists);
        }
        #endregion

        #region + EditRecord(Guid Id, string Table, Guid ForeignId, string Instance)
        public ActionResult EditRecord(Guid Id, string Table, Guid ForeignId, string Instance)
        {
            TranslationModel model = service.GetTranslation(Id);
            ViewBag.Languages = new SelectList(new LanguageService().GetLanguages(), "ID", "NAME", model.Language.Id);
            return View(model);
        }
        #endregion

        #region + EditRecord(TranslationModel model)
        [HttpPost]
        public ActionResult EditRecord(TranslationModel model)
        {
            try
            {
                if (model.IsValid)
                {
                    service.UpdateTranslation(model);
                    return RedirectToAction("Record", new { Id = model.Id, Table = model.TableName, ForeignId = model.ForeignId, Instance = model.Instance });
                }
                ModelState.AddModelError("", model.Error);
                ViewBag.Languages = new SelectList(new LanguageService().GetLanguages(), "ID", "NAME", model.Language.Id);
                return RedirectToAction("EditRecord", model);
            }
            catch (Exception e)
            {
                ViewBag.Languages = new SelectList(new LanguageService().GetLanguages(), "ID", "NAME", model.Language.Id);
                ModelState.AddModelError("", e.Message);
                return RedirectToAction("EditRecord", model);
            }
        }
        #endregion
    }
}
