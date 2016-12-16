using System;
using System.Collections.Generic;
using System.ServiceModel;
using AppTour.Model.Models.PointsAttributes;

namespace AppTour.Business.ServiceContracts.PointAttribute
{
    [ServiceContract]
    public interface IPointAttributeService
    {
        [OperationContract]
        IList<PointAttributeModel> GetAttributesForPoint(Guid PointId);

        [OperationContract]
        IList<PointAttributeModel> GetActiveAttributesForPoint();

        [OperationContract]
        PointAttributeModel GetAttribute(Guid id);

        [OperationContract]
        void UpdateAttribute(PointAttributeModel pointAttributeModel);

        [OperationContract]
        Guid InsertAttribute(PointAttributeModel pointAttributeModel);

        [OperationContract]
        void DeleteAttribute(PointAttributeModel pointAttributeModel);
    }
}
