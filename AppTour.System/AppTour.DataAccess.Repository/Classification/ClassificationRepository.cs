using System.Collections.Generic;
using System.Linq;
using AppTour.DataAccess.Entity;
using AppTour.Model.Models.Classification;
using AppTour.Model.Models.Point;
using AppTour.Model.Models.User;
using System;

namespace AppTour.DataAccess.Repository.Classification
{
    public sealed class ClassificationRepository
    {
        #region - IQueryable<ClassificationModel> GetClassificationForPoint(AppTourEntities data, Guid PointId)
        private IQueryable<ClassificationModel> GetClassificationForPoint(AppTourEntities data, Guid PointId)
        {
            var query = (from c in data.CLASSIFICATION
                         where c.POINT.ID == PointId
                         select new ClassificationModel
                         {
                             Id = c.ID,
                             Classification = c.CLASSIFICATION1,
                             CreationDate = c.CREATION_DATE,
                             User = (from u in data.USER
                                     where u.ID == c.ID_USER
                                     select new UserModel
                                     {
                                         Id = u.ID,
                                         RealName = u.REALNAME,
                                         IsActive = u.IS_ACTIVE,
                                         CreationDate = u.CREATION_DATE,
                                         UpdateDate = u.UPDATE_DATE
                                     }).FirstOrDefault()
                         });
            return query;
        }
        #endregion

        #region + IList<ClassificationModel> GetClassificationForPoint(PointModel Point)
        public IList<ClassificationModel> GetClassificationForPoint(PointModel Point)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                IList<ClassificationModel> list = GetClassificationForPoint(data, Point.Id).ToList();
                list.ToList().ForEach(x => x.Point = Point);

                return list;
            }
        }
        #endregion

        #region + void AddNewVote(ClassificationModel Classification)
        public void AddNewVote(ClassificationModel Classification)
        {
            if (Classification == null)
                throw new NullReferenceException();

            if (Classification.Point != null)
            {
                int value = GetClassificationForPoint(Classification.Point).Where(x => x.User.Id == Classification.User.Id).Count();

                if (value > 0)
                    return;
            }

            using (AppTourEntities data = new AppTourEntities())
            {
                CLASSIFICATION _new = new CLASSIFICATION
                {
                    ID = (Classification.Id == null || Classification.Id == Guid.Empty ? Guid.NewGuid() : Classification.Id),
                    EVENT = (Classification.Event != null ? data.EVENT.SingleOrDefault(x => x.ID.Equals(Classification.Event.Id)) : null),
                    POINT = (Classification.Point != null ? data.POINT.SingleOrDefault(x => x.ID.Equals(Classification.Point.Id)) : null),
                    CLASSIFICATION1 = Classification.Classification,
                    USER = data.USER.SingleOrDefault(x => x.ID.Equals(Classification.User.Id)),
                    CREATION_DATE = DateTime.Now
                };

                data.CLASSIFICATION.AddObject(_new);
                data.SaveChanges();
            }
        }
        #endregion
    }
}
