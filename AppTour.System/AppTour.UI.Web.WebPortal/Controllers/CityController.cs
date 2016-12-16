using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web.Mvc;
using AppTour.Business.ServiceContracts.City;
using AppTour.Business.Services.City;
using AppTour.Business.Services.Country;
using AppTour.Model.Models.City;

namespace AppTour.UI.Web.WebPortal.Controllers
{
    public class CityController : Controller
    {

        #region - Attributes
        private ICityService service = new CityService();
        #endregion

        #region + ActionResult Index()
        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            return View(service.GetCities());
        }

        #endregion

        #region + ActionResult Create()
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            ViewBag.Countries = new SelectList(new CountryService().GetCountries(), "ID", "NAME");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult Create(CityModel City)
        {
            if (City.Country.Id != Guid.Empty && City.Country.Id != null)
            {
                City.Country = new CountryService().GetCountry(City.Country.Id);
            }
            var context = new ValidationContext(City, null, null);
            var results = new List<ValidationResult>();

            if (Validator.TryValidateObject(City, context, results, true))
            {
                try
                {
                    service.InsertCity(City);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            ViewBag.Countries = new SelectList(new CountryService().GetCountries(), "ID", "NAME");
            return View(City);
        }

        #endregion

        #region + ActionResult Edit(Guid id)
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(Guid id)
        {
            CityModel c = service.GetCity(id);
            ViewBag.Countries = new SelectList(new CountryService().GetCountries(), "ID", "NAME", c.Country.Id);
            return View(c);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(CityModel City)
        {
            if (City.Country.Id != Guid.Empty && City.Country.Id != null)
            {
                City.Country = new CountryService().GetCountry(City.Country.Id);
            }
            var context = new ValidationContext(City, null, null);
            var results = new List<ValidationResult>();

            if (Validator.TryValidateObject(City, context, results, true))
            {
                try
                {
                    service.UpdateCity(City);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            ViewBag.Countries = new SelectList(new CountryService().GetCountries(), "ID", "NAME", City.Country.Id);
            return View(City);
        }

        #endregion

        #region + ActionResult Delete(Guid id)
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(Guid id)
        {
            return View(service.GetCity(id));
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(Guid id, CityModel City)
        {
            if (id == City.Id)
            {
                try
                {
                    service.DeleteCity(City);
                    return RedirectToAction("Index");
                }
                catch (UpdateException)
                {
                    City = service.GetCity(id);
                    ViewData.Add(new KeyValuePair<string, object>(ViewRes.City.Erro + ':', ViewRes.City.UpdateException));
                }
                catch (Exception e)
                {
                    City = service.GetCity(id);
                    ModelState.AddModelError("", e.Message);
                    ViewData.Add(new KeyValuePair<string, object>(ViewRes.City.Erro + ':', ViewRes.City.UpdateException));
                }
            }

            return View(City);
        }

        #endregion

    }
}
