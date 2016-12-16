using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using AppTour.Business.ServiceContracts.Point;
using AppTour.Business.Services.City;
using AppTour.Business.Services.Classification;
using AppTour.Business.Services.Comment;
using AppTour.Business.Services.Point;
using AppTour.Business.Services.PointAttribute;
using AppTour.Business.Services.Topic;
using AppTour.Business.Services.User;
using AppTour.Model.Models.Classification;
using AppTour.Model.Models.Comment;
using AppTour.Model.Models.Point;
using AppTour.Model.Models.Topic;
using AppTour.UI.Web.WebPortal.Models;
using PagedList;
using System.Linq;

namespace AppTour.UI.Web.WebPortal.Controllers
{
    public class PointController : Controller
    {
        #region - Atributos
        private IPointService service = new PointService();
        private static Regex isGuid = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);
        private const int QTD = 15;
        #endregion

        #region + ActionResult Index(int? page)
        [Authorize(Roles = "Administrador")]
        public ActionResult Index(int? page)
        {
            int pageNumber = page ?? 1;

            int begin = pageNumber * QTD;

            IEnumerable<PointModel> AllPoints = service.GetPoints(begin, QTD);
            var onePage = AllPoints.ToPagedList(1, QTD);

            int totalPoints = service.GetTotalPoints();
            IEnumerable<string> totalPages = Enumerable.Repeat(string.Empty, totalPoints);

            ViewBag.TotalPages = totalPages.ToPagedList(pageNumber, QTD);
            ViewBag.OnePage = onePage;

            return View();
        }
        #endregion

        #region + ActionResult Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            PointViewModel viewModel = new PointViewModel();

            ViewBag.Topics = new MultiSelectList(new TopicService().GetActiveTopics(), "Id", "Name");
            ViewBag.City = new SelectList(new CityService().GetCities(), "Id", "Name");

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(PointViewModel point)
        {
            if (ModelState.IsValid)
            {
                PointModel model = new PointModel
                {
                    Id = point.Id != Guid.Empty ? point.Id : Guid.NewGuid(),
                    Name = point.Name,
                    Address = point.Address,
                    PostalCode = point.PostalCode,
                    City = point.City,
                    Coordenate = point.Coordenate,
                    PhoneNumber = point.PhoneNumber,
                    URL = point.URL,
                    SourceURL = point.SourceURL,
                    IsActive = point.IsActive
                };

                if (point.SelectedTopicId != null && point.SelectedTopicId.Length > 0)
                {
                    model.Topics = new List<TopicModel>();
                    for (int i = 0; i < point.SelectedTopicId.Length; i++)
                    {
                        TopicModel temp = new TopicService().GetTopic(new Guid(point.SelectedTopicId[i].ToString()));
                        model.Topics.Add(temp);
                    }
                }

                #region Validate Model After Conversion from ViewModel
                try
                {
                    if (model.IsValid)
                    {
                        service.InsertPoint(model);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", model.Error);
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
                #endregion
            }

            ViewBag.Topics = new MultiSelectList(new TopicService().GetActiveTopics(), "Id", "Name");
            ViewBag.City = new SelectList(new CityService().GetCities(), "Id", "Name");

            return View(point);
        }
        #endregion

        #region + ActionResult Edit
        [Authorize(Roles = "Administrador,Gestor de Conteúdo")]
        public ActionResult Edit(Guid id)
        {
            PointModel point = new PointService().GetPoint(id);

            PointViewModel viewModel = new PointViewModel
            {
                Id = point.Id,
                Name = point.Name,
                Address = point.Address,
                PostalCode = point.PostalCode,
                City = point.City,
                Coordenate = point.Coordenate,
                PhoneNumber = point.PhoneNumber,
                URL = point.URL,
                SourceURL = point.SourceURL,
                IsActive = point.IsActive
            };
            if (point.Topics.Count > 0)
            {
                viewModel.SelectedTopicId = new string[point.Topics.Count];

                int i = 0;
                foreach (TopicModel item in point.Topics)
                {
                    viewModel.SelectedTopicId[i++] = item.Id.ToString();
                }
            }
            ViewBag.Topics = new MultiSelectList(new TopicService().GetActiveTopics(), "Id", "Name", viewModel.SelectedTopicId);
            ViewBag.City = new SelectList(new CityService().GetCities(), "Id", "Name");

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Gestor de Conteúdo")]
        public ActionResult Edit(PointViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Convert to Point Model
                PointModel point = new PointModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    Address = model.Address,
                    PostalCode = model.PostalCode,
                    City = model.City,
                    Coordenate = model.Coordenate,
                    PhoneNumber = model.PhoneNumber,
                    URL = model.URL,
                    SourceURL = model.SourceURL,
                    IsActive = model.IsActive
                };

                // Get Topics
                if (model.SelectedTopicId.Length > 0)
                {
                    point.Topics = new List<TopicModel>();
                    for (int i = 0; i < model.SelectedTopicId.Length; i++)
                    {
                        TopicModel temp = new TopicService().GetTopic(new Guid(model.SelectedTopicId[i].ToString()));
                        point.Topics.Add(temp);
                    }
                }
                try
                {
                    if (point.IsValid)
                    {
                        service.UpdatePoint(point);
                        return RedirectToAction("Index");
                    }
                    else
                        ModelState.AddModelError("", point.Error);

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            ViewBag.Topics = new MultiSelectList(new TopicService().GetActiveTopics(), "Id", "Name", model.SelectedTopicId);
            ViewBag.City = new SelectList(new CityService().GetCities(), "Id", "Name");

            return View(model);
        }
        #endregion

        #region + ActionResult POINTS_ATTRIBUTES(string id)
        [Authorize(Roles = "Administrador")]
        public ActionResult POINTS_ATTRIBUTES(string id)
        {
            return PartialView(new PointAttributeService().GetAttribute(new Guid(id)));
        }
        #endregion

        #region + ActionResult Empty
        public ActionResult Empty()
        {
            return View();
        }
        #endregion

        #region + ActionResult Show(string id, int? page)
        [ValidateInput(false)]
        public ActionResult Show(string id, int? page)
        {
            if (id == null || id == string.Empty)
                return RedirectToAction("Empty");

            string nomeDoTopic = Server.HtmlDecode(id);

            TopicModel topic = new TopicService().GetTopic(nomeDoTopic);

            if (topic != null)
            {
                var pageNumber = page ?? 1;

                ViewBag.Topic = topic;
                IList<PointModel> points = service.GetActivePoints(topic);
                return View(points.ToPagedList(pageNumber, 10));
            }

            return RedirectToAction("Empty");
        }
        #endregion

        #region + ActionResult Detail(string id)
        public ActionResult Detail(string id)
        {
            if (id == null || id == string.Empty)
                return RedirectToAction("Empty");

            if (isGuid.IsMatch(id))
            {
                Guid PointId = new Guid(id);

                PointModel point = new PointService().GetActivePoint(PointId);

                if (point != null)
                {
                    return View(point);
                }
            }
            return RedirectToAction("Empty");
        }
        #endregion

        #region + ActionResult InsertVote(string userId, string pointId, string Classifications)
        [HttpPost]
        public ActionResult InsertVote(string userId, string pointId, string Classifications)
        {
            if (userId == null || Classifications == null || pointId == null || userId == string.Empty
                || Classifications == string.Empty || pointId == string.Empty)
                return Content("error");

            if (isGuid.IsMatch(userId) && isGuid.IsMatch(pointId))
            {
                ClassificationModel classification = new ClassificationModel
                {
                    Id = Guid.Empty,
                    Point = new PointService().GetPoint(new Guid(pointId)),
                    Classification = Convert.ToInt32(Classifications),
                    User = new UserService().GetUser(new Guid(userId))
                };

                if (classification.IsValid)
                {
                    new ClassificationService().AddVote(classification);
                    return Content("sucesso");
                }
                return Content("Model Not Valid: " + classification.Error);
            }
            return RedirectToAction("Empty");

        }
        #endregion

        #region + ActionResult InsertComment(string userId, string pointId, string name, string msg)
        [HttpPost]
        public ActionResult InsertComment(string userId, string pointId, string name, string msg)
        {
            if (userId == null || name == null || msg == null
                || pointId == null || userId == string.Empty
                || name == string.Empty || msg == string.Empty
                || pointId == string.Empty)
                return Content("null parameters");

            if (isGuid.IsMatch(userId) && isGuid.IsMatch(pointId))
            {
                CommentModel comment = new CommentModel
                {
                    Id = Guid.Empty,
                    Point = new PointService().GetPoint(new Guid(pointId)),
                    Comment = msg,
                    User = new UserService().GetUser(new Guid(userId)),
                    IsActive = true,
                    IsReported = false
                };
                if (comment.IsValid)
                {
                    Guid id = new CommentService().AddComment(comment);
                    return Content("sucesso");
                }
                return Content("Model Not Valid: " + comment.Error);
            }
            return Content("error");

        }
        #endregion
    }
}
