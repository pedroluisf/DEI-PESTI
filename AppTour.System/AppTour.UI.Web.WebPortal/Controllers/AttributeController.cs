using System;
using System.Data;
using System.Globalization;
using System.Web.Mvc;
using AppTour.Business.ServiceContracts.PointAttribute;
using AppTour.Business.Services.Point;
using AppTour.Business.Services.PointAttribute;
using AppTour.Model.Models.PointsAttributes;
using AppTour.UI.Web.WebPortal.Models;

namespace AppTour.UI.Web.WebPortal.Controllers
{
    public class AttributeController : Controller
    {
        #region - Atributos
        private IPointAttributeService service = new PointAttributeService();
        #endregion

        #region + ActionResult Index(Guid id)
        [Authorize(Roles = "Administrador")]
        public ActionResult Index(Guid id)
        {
            if (id != null)
            {
                PointAttributeViewModel model = new PointAttributeViewModel();
                model.Point = new PointService().GetPoint(id);
                model.Attributes = new PointAttributeService().GetAttributesForPoint(id);

                return View(model);

            }
            return RedirectToAction("Index", "Point");
        }
        #endregion

        #region + ActionResult InsertAttribute(string PointId, string attrkey, string attrvalue, string attrtype, string active)
        [Authorize(Roles = "Administrador")]
        public ActionResult InsertAttribute(string PointId, string attrkey,
                        string attrvalue, string attrtype, string active)
        {
            if (PointId == null || PointId.Equals(string.Empty) || attrkey == null
                || attrkey.Equals(string.Empty) || attrvalue == null 
                || attrvalue.Equals(string.Empty) || attrtype == null
                || attrtype.Equals(string.Empty))
                return Content(ViewRes.Attribute.MissingParams);
            try
            {
                bool isActive = (active != null ? true : false);
                PointAttributeModel model = new PointAttributeModel
                {
                    Id = Guid.NewGuid(),
                    Point = new PointService().GetPoint(new Guid(PointId)),
                    KeyPair = attrkey,
                    Value_bool = (attrtype.Equals("System.Boolean") 
                            ? (attrvalue.ToLower().Equals("true")
                            || attrvalue.ToLower().Equals("1") ? true : false) 
                            : (bool?)null),
                    Value_number = (attrtype.Equals("System.Decimal") 
                            ? Convert.ToDecimal(attrvalue, CultureInfo.GetCultureInfo("en-US"))
                            : (Decimal?)null),
                    Value_Date = (attrtype.Equals("System.DateTime") 
                            ? DateTime.Parse(attrvalue) : (DateTime?)null),
                    Value_string = (attrtype.Equals("System.String") ? attrvalue : null),
                    Value_Type = attrtype,
                    IsActive = isActive
                };
                Guid id = service.InsertAttribute(model);
                return Content("Sucesso");
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    return Content(e.InnerException.Message);
                return Content(e.Message);
            }

        }
        #endregion

        #region + ActionResult EditAttribute(string PointId, string AttributeId, string attrkey, string attrvalue, string attrtype, string active)
        public ActionResult EditAttribute(string PointId, string AttributeId, string attrkey, string attrvalue, string attrtype, string active)
        {
            if (AttributeId == null || AttributeId.Equals(string.Empty) || PointId == null || PointId.Equals(string.Empty) || attrkey == null || attrkey.Equals(string.Empty) || attrvalue == null || attrvalue.Equals(string.Empty) || attrtype == null || attrtype.Equals(string.Empty))
                return Content(ViewRes.Attribute.MissingParams);
            try
            {
                bool isActive = (active != "undefined" ? true : false);
                PointAttributeModel model = new PointAttributeService().GetAttribute(new Guid(AttributeId));

                if (model != null)
                {
                    model.KeyPair = attrkey;
                    model.Value_bool = (attrtype.Equals("System.Boolean") ? (attrvalue.ToLower().Equals("true") || attrvalue.ToLower().Equals("1") ? true : false) : (bool?)null);
                    model.Value_Date = (attrtype.Equals("System.DateTime") ? DateTime.Parse(attrvalue) : (DateTime?)null);
                    model.Value_number = (attrtype.Equals("System.Decimal") ? Convert.ToDecimal(attrvalue, CultureInfo.GetCultureInfo("en-US")) : (Decimal?)null);
                    model.Value_string = (attrtype.Equals("System.String") ? attrvalue : null);
                    model.IsActive = isActive;
                    model.Value_Type = attrtype;
                    model.Point = new PointService().GetPoint(new Guid(PointId));

                    service.UpdateAttribute(model);

                    return Content("Sucesso");
                }
                return Content(ViewRes.SharedStrings.NotExists);



            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    return Content(e.InnerException.Message);
                return Content(e.Message);
            }
        }
        #endregion

        #region + ActionResult DeleteAttribute(string PointId, string AttributeID)
        [Authorize(Roles = "Administrador")]
        public ActionResult DeleteAttribute(string PointId, string AttributeID)
        {
            if (PointId == null || PointId.Equals(string.Empty) || AttributeID == null || AttributeID.Equals(string.Empty))
                return Content(ViewRes.Attribute.MissingParams);
            try
            {
                PointAttributeModel point = service.GetAttribute(new Guid(AttributeID));
                if (point != null)
                {
                    service.DeleteAttribute(point);
                    return Content("Sucesso");
                }
                else
                    return Content(ViewRes.SharedStrings.NotExists);
            }
            catch (UpdateException)
            {
                return Content(ViewRes.Attribute.UpdateException);
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    return Content(e.InnerException.Message);
                return Content(e.Message);
            }
        }
        #endregion
    }
}
