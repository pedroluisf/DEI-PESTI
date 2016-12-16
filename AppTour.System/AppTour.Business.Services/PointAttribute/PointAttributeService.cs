using System;
using System.Collections.Generic;
using AppTour.Business.ServiceContracts.PointAttribute;
using AppTour.DataAccess.Repository.PointAttribute;
using AppTour.Model.Models.PointsAttributes;

namespace AppTour.Business.Services.PointAttribute
{
    public class PointAttributeService : IPointAttributeService
    {

        public IList<PointAttributeModel> GetAttributesForPoint(Guid PointId)
        {
            return new PointAttributeRepository().GetAttributeForPoints(PointId);
        }

        public IList<PointAttributeModel> GetActiveAttributesForPoint()
        {
            throw new NotImplementedException();
        }

        public PointAttributeModel GetAttribute(Guid id)
        {
            return new PointAttributeRepository().GetAttribute(id);
        }

        public void UpdateAttribute(PointAttributeModel pointAttributeModel)
        {
            new PointAttributeRepository().UpdateAttribute(pointAttributeModel);
        }

        public Guid InsertAttribute(PointAttributeModel pointAttributeModel)
        {
            return new PointAttributeRepository().InsertPointAttribute(pointAttributeModel);
        }

        public void DeleteAttribute(PointAttributeModel pointAttributeModel)
        {
            new PointAttributeRepository().DeletePointAttribute(pointAttributeModel);
        }
    }
}
