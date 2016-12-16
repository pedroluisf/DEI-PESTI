using System;
using System.Collections.Generic;
using System.Linq;
using AppTour.DataAccess.Entity;
using AppTour.Model.Models.Picture;

namespace AppTour.DataAccess.Repository.Picture
{
    public sealed class PictureRepository
    {
        #region - IQueryable<PictureModel> GetPicturesFromPoint(AppTourEntities data, PointModel Point)
        private IQueryable<PictureModel> GetPicturesFromPoint(AppTourEntities data, Guid PointId)
        {

            var query = (from c in data.PICTURE
                         where c.POINT.ID.Equals(PointId)
                         select new PictureModel
                         {
                             Id = c.ID,
                             Picture_URL = c.PIC_URL
                         });
            return query;
        }
        #endregion

        #region + IList<PictureModel> GetPicturesFromPoint(Guid PointId)
        public IList<PictureModel> GetPicturesFromPoint(Guid PointId)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetPicturesFromPoint(data, PointId).ToList();
            }
        }
        #endregion
    }
}
