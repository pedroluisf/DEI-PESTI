using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using AppTour.Business.ServiceContracts.Country;
using AppTour.Business.Services.Country;
using AppTour.Model.Models.Country;
using PagedList;

namespace AppTour.UI.Web.WebPortal.Controllers
{
    public class CountryController : Controller
    {
        #region - ActionResult Attributes
        private ICountryService service = new CountryService();
        #endregion

        #region + ActionResult Index
        [Authorize(Roles = "Administrador")]
        public ActionResult Index(int? page)
        {
            IEnumerable<CountryModel> AllCountries = service.GetCountries();

            int pageNumber = page ?? 1;

            var onePageOfCountry = AllCountries.ToPagedList(pageNumber, 25);

            ViewBag.OnePage = onePageOfCountry;

            return View();
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
        public ActionResult Create(CountryModel country)
        {
            if (ModelState.IsValid)
            {
                if (country.IsValid)
                    try
                    {
                        service.InsertCountry(country);
                        return RedirectToAction("Index");
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", e.Message);
                    }
                else
                {
                    ModelState.AddModelError("", country.Error);
                }
            }
            return View(country);
        }
        #endregion

        #region + ActionResult Edit
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(Guid id)
        {
            return View(service.GetCountry(id));
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(CountryModel country)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (country.IsValid)
                    {
                        service.UpdateCountry(country);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", country.Error);
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            return View(country);
        }
        #endregion

        #region + ActionResult Delete
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(Guid id)
        {
            return View(service.GetCountry(id));
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(Guid id, CountryModel country)
        {
            if (id == country.Id)
            {
                try
                {
                    service.DeleteCountry(country);
                    return RedirectToAction("Index");
                }
                catch (UpdateException)
                {
                    country = service.GetCountry(id);
                    ViewData.Add(new KeyValuePair<string, object>(ViewRes.Country.Erro + ':', ViewRes.Country.UpdateException));
                }
                catch (Exception e)
                {
                    country = service.GetCountry(id);
                    ModelState.AddModelError("", e.Message);
                    ViewData.Add(new KeyValuePair<string, object>(ViewRes.Country.Erro + ':', ViewRes.Country.UpdateException));
                }
            }

            return View("Delete", country);
        }
        #endregion

    }
}
