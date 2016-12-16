using System;
using System.Collections.Generic;
using System.Linq;
using AppTour.DataAccess.Entity;
using AppTour.Model.Models.Point;
using AppTour.Model.Models.PointsAttributes;

namespace AppTour.DataAccess.Repository.PointAttribute
{
    public class PointAttributeRepository
    {
        #region - IQueryable GetPointsAttributes(AppTourEntities data)
        private IQueryable<PointAttributeModel> GetPointsAttributes(AppTourEntities data)
        {
            var query = (from c in data.POINTS_ATTRIBUTES
                         select new PointAttributeModel
                         {
                             Id = c.ID,
                             IsActive = c.IS_ACTIVE,
                             KeyPair = c.KEYPAIR,
                             Value_bool = c.VALUE_BOOL,
                             Value_Date = c.VALUE_DATE,
                             Value_number = c.VALUE_NUMBER,
                             Value_string = c.VALUE_STRING,
                             Value_Type = c.VALUE_TYPE,
                             Point = new PointModel
                             {
                                 Id = c.POINT.ID,
                                 Address = c.POINT.ADDRESS,
                                 Coordenate = c.POINT.COORDENATE,
                                 IsActive = c.POINT.IS_ACTIVE,
                                 Name = c.POINT.NAME,
                                 PhoneNumber = c.POINT.PHONE_NUMBER,
                                 PostalCode = c.POINT.POSTAL_CODE,
                                 SourceURL = c.POINT.SOURCE_URL,
                                 URL = c.POINT.URL
                             },
                             NumberOfTranslations = data.TRANSLATIONS.Where(x => x.FOREIGN_ID == c.ID && x.TABLE_NAME == "POINTS_ATTRIBUTES").Count()
                         });

            return query;
        }
        #endregion

        #region - IQueryable GetPointsAttributes(AppTourEntities data, Guid PointId)
        private IQueryable<PointAttributeModel> GetPointsAttributes(AppTourEntities data, Guid PointId)
        {
            var query = (from c in data.POINTS_ATTRIBUTES
                         where c.POINT.ID.Equals(PointId)
                         select new PointAttributeModel
                         {
                             Id = c.ID,
                             IsActive = c.IS_ACTIVE,
                             KeyPair = c.KEYPAIR,
                             Value_bool = c.VALUE_BOOL,
                             Value_Date = c.VALUE_DATE,
                             Value_number = c.VALUE_NUMBER,
                             Value_string = c.VALUE_STRING,
                             Value_Type = c.VALUE_TYPE,
                             Point = new PointModel
                             {
                                 Id = c.POINT.ID,
                                 Address = c.POINT.ADDRESS,
                                 Coordenate = c.POINT.COORDENATE,
                                 IsActive = c.POINT.IS_ACTIVE,
                                 Name = c.POINT.NAME,
                                 PhoneNumber = c.POINT.PHONE_NUMBER,
                                 PostalCode = c.POINT.POSTAL_CODE,
                                 SourceURL = c.POINT.SOURCE_URL,
                                 URL = c.POINT.URL
                             },
                             NumberOfTranslations = data.TRANSLATIONS.Where(x => x.FOREIGN_ID == c.ID && x.TABLE_NAME == "POINTS_ATTRIBUTES").Count()
                         });

            return query;
        }
        #endregion

        #region + GetAttributeForPoints(Guid PointId)
        public IList<PointAttributeModel> GetAttributeForPoints(Guid PointId)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetPointsAttributes(data, PointId).ToList();
            }

        }
        #endregion

        #region + InsertPointAttribute(PointAttributeModel pointAttribute)
        public Guid InsertPointAttribute(PointAttributeModel pointAttribute)
        {

            if (pointAttribute == null)
                throw new NullReferenceException("pointAttribute");

            using (AppTourEntities data = new AppTourEntities())
            {
                POINTS_ATTRIBUTES novo = new POINTS_ATTRIBUTES
                {
                    ID = (pointAttribute.Id == null || pointAttribute.Id == Guid.Empty ? Guid.NewGuid() : pointAttribute.Id),
                    POINT = data.POINT.Single(x => x.ID.Equals(pointAttribute.Point.Id)),
                    KEYPAIR = pointAttribute.KeyPair,
                    VALUE_BOOL = pointAttribute.Value_bool,
                    VALUE_STRING = pointAttribute.Value_string,
                    VALUE_DATE = pointAttribute.Value_Date,
                    VALUE_NUMBER = pointAttribute.Value_number,
                    VALUE_TYPE = pointAttribute.Value_Type,
                    IS_ACTIVE = pointAttribute.IsActive
                };

                data.POINTS_ATTRIBUTES.AddObject(novo);
                data.SaveChanges();

                return novo.ID;
            }
        }
        #endregion

        #region + GetAttribute(Guid PointAttributeId)
        public PointAttributeModel GetAttribute(Guid PointAttributeId)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetPointsAttributes(data).Single(x => x.Id.Equals(PointAttributeId));
            }
        }
        #endregion

        #region + DeletePointAttribute(PointAttributeModel pointAttributeModel)
        public void DeletePointAttribute(PointAttributeModel pointAttributeModel)
        {
            if (pointAttributeModel == null)
                throw new ArgumentNullException("pointAttributeModel");

            using (AppTourEntities data = new AppTourEntities())
            {
                try
                {
                    POINTS_ATTRIBUTES current = data.POINTS_ATTRIBUTES.Single(p => p.ID == pointAttributeModel.Id);

                    if (current != null)
                    {

                        data.POINTS_ATTRIBUTES.DeleteObject(current);
                        data.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    if (e.InnerException != null)
                        throw new Exception(e.InnerException.Message);
                    throw;
                }
            }
        }
        #endregion

        #region + UpdateAttribute(PointAttributeModel pointAttributeModel)
        public void UpdateAttribute(PointAttributeModel pointAttributeModel)
        {
            if (pointAttributeModel == null)
                throw new ArgumentNullException("pointAttributeModel");

            using (AppTourEntities data = new AppTourEntities())
            {
                try
                {
                    POINTS_ATTRIBUTES current = data.POINTS_ATTRIBUTES.Single(p => p.ID == pointAttributeModel.Id);

                    if (current != null)
                    {
                        current.KEYPAIR = pointAttributeModel.KeyPair;
                        current.VALUE_BOOL = pointAttributeModel.Value_bool;
                        current.VALUE_DATE = pointAttributeModel.Value_Date;
                        current.VALUE_NUMBER = pointAttributeModel.Value_number;
                        current.VALUE_STRING = pointAttributeModel.Value_string;
                        current.VALUE_TYPE = pointAttributeModel.Value_Type;
                        current.IS_ACTIVE = pointAttributeModel.IsActive;

                        data.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    if (e.InnerException != null)
                        throw new Exception(e.InnerException.Message);
                    throw;
                }
            }
        }
        #endregion
    }
}
