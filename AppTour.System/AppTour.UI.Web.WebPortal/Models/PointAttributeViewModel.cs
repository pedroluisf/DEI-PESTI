using System.Collections.Generic;
using AppTour.Model.Models.Point;
using AppTour.Model.Models.PointsAttributes;

namespace AppTour.UI.Web.WebPortal.Models
{
    public class PointAttributeViewModel
    {
        public PointModel Point { get; set; }

        public IList<PointAttributeModel> Attributes { get; set; }
    }
}