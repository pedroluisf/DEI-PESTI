using System;
using System.Collections.Generic;
using System.ServiceModel;
using AppTour.Model.Models.Point;
using AppTour.Model.Models.Topic;

namespace AppTour.Business.ServiceContracts.Point
{
    [ServiceContract]
    public interface IPointService
    {
        [OperationContract]
        IList<PointModel> GetPoints();

        [OperationContract]
        IList<PointModel> GetPoints(int Skip, int Total);

        [OperationContract]
        IList<PointModel> GetActivePoints();

        [OperationContract]
        PointModel GetPoint(Guid id);

        [OperationContract]
        void UpdatePoint(PointModel point);

        [OperationContract]
        Guid InsertPoint(PointModel point);

        [OperationContract]
        void DeletePoint(PointModel point);

        [OperationContract]
        IList<PointModel> GetActivePoints(TopicModel Topic);

        [OperationContract]
        PointModel GetActivePoint(Guid id);

        [OperationContract]
        int GetTotalPoints();
    }
}
