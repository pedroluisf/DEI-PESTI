using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppTour.Business.ServiceContracts.Point;
using AppTour.Business.Services.User;
using AppTour.DataAccess.Repository.Point;
using AppTour.Model.Models.Point;
using AppTour.Model.Models.Topic;

namespace AppTour.Business.Services.Point
{
    public class PointService : IPointService
    {
        public IList<PointModel> GetPoints()
        {
            return new PointRepository().GetPoints();
        }

        public IList<PointModel> GetActivePoints()
        {
            return new PointRepository().GetActivePoints();
        }

        public PointModel GetPoint(Guid id)
        {
            return new PointRepository().GetPoint(id);
        }

        public PointModel GetActivePoint(Guid id)
        {
            PointModel model = new PointRepository().GetPoint(id);

            Parallel.ForEach(model.Comments, x =>
            {
                x.User = new UserService().GetUser(x.User.Id);
            });

            return model;
        }

        public void UpdatePoint(PointModel point)
        {
            new PointRepository().UpdatePoint(point);
        }

        public Guid InsertPoint(PointModel point)
        {
            return new PointRepository().InsertPoint(point);
        }

        public void DeletePoint(PointModel point)
        {
            throw new NotImplementedException();
        }

        public IList<PointModel> GetActivePoints(TopicModel Topic)
        {
            return new PointRepository().GetActivePoints(Topic);
        }

        public IList<PointModel> GetPoints(int Skip, int Total)
        {
            return new PointRepository().GetPoints(Skip, Total);
        }
        
        public int GetTotalPoints()
        {
            return new PointRepository().GetTotalPoints();
        }
    }
}
