using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using AppTour.Business.Services.Theme;
using AppTour.Business.Services.Topic;
using AppTour.Model.Models.Topic;
using AppTour.Business.ServiceContracts.Topic;

namespace AppTour.UI.Web.WebPortal.Controllers
{
    public class TopicController : Controller
    {
        #region - Attributes
        private ITopicService service = new TopicService();
        #endregion

        #region + ActionResult Index
        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            return View(service.GetTopics().ToList().OrderBy(x => x.Theme.Name).ThenBy(x => x.Name));
        }
        #endregion

        #region + ActionResult Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            ViewBag.themes = new SelectList(new ThemeService().GetActiveThemes(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult Create(TopicModel topic)
        {
            if (topic.Theme.Id != Guid.Empty && topic.Theme.Id != null)
            {
                topic.Theme = new ThemeService().GetTheme(topic.Theme.Id);
            }
            var context = new ValidationContext(topic, null, null);
            var results = new List<ValidationResult>();
            if (Validator.TryValidateObject(topic, context, results, true))
            {
                try
                {
                    if (topic.IsValid)
                    {
                        service.InsertTopic(topic);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", topic.Error);
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            ViewBag.themes = new SelectList(new ThemeService().GetActiveThemes(), "Id", "Name");
            return View(topic);
        }

        #endregion

        #region + ActionResult Edit
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(Guid id)
        {
            TopicModel c = service.GetTopic(id);
            ViewBag.Themes = new SelectList(new ThemeService().GetActiveThemes(), "ID", "NAME", c.Theme.Id);
            return View(c);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(TopicModel topic)
        {
            if (topic.Theme.Id != Guid.Empty && topic.Theme.Id != null)
            {
                topic.Theme = new ThemeService().GetTheme(topic.Theme.Id);
            }
            var context = new ValidationContext(topic, null, null);
            var results = new List<ValidationResult>();

            if (Validator.TryValidateObject(topic, context, results, true))
            {
                try
                {
                    if (topic.IsValid)
                    {
                        service.UpdateTopic(topic);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", topic.Error);
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            ViewBag.Themes = new SelectList(new ThemeService().GetActiveThemes(), "ID", "NAME", topic.Theme.Id);
            return View(topic);
        }

        #endregion

        #region + ActionResult Delete
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(Guid id)
        {
            return View(service.GetTopic(id));
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(Guid id, TopicModel topic)
        {
            if (id == topic.Id)
            {
                try
                {
                    service.DeleteTopic(topic);
                    return RedirectToAction("Index");
                }
                catch (UpdateException)
                {
                    topic = service.GetTopic(id);
                    ViewData.Add(new KeyValuePair<string, object>(ViewRes.Topic.Erro + ':', ViewRes.Topic.UpdateException));
                }
                catch (Exception e)
                {
                    topic = service.GetTopic(id);
                    ModelState.AddModelError("", e.Message);
                    ViewData.Add(new KeyValuePair<string, object>(ViewRes.Topic.Erro + ':', ViewRes.Topic.UpdateException));
                }
            }

            return View(topic);
        }

        #endregion

    }
}
