using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AppTour.DataAccess.Entity;
using AppTour.Model.Models.Enterprise;

namespace AppTour.DataAccess.Repository.Enterprise
{
    public sealed class EnterpriseRepository 
    {

        #region IQueryable GetEnterpise
        private IQueryable<EnterpriseModel> GetEnterprises(AppTourEntities data)
        {

            var query = from c in data.ENTERPRISE
                        orderby c.NAME
                        select new EnterpriseModel
                        {
                            Id = c.ID,
                            Name = c.NAME,
                            Description = c.DESCRIPTION,
                            CreationDate = c.CREATION_DATE,
                            UpdateDate = c.UPDATE_DATE
                        };
            return query;
        }
        #endregion

        #region Public GetEnterprises
        public IList<EnterpriseModel> GetEnterprises()
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetEnterprises(data).ToList();
            }
        }
        #endregion

        #region  GetEnterprise(Guid id)
        public EnterpriseModel GetEnterprise(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException();

            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetEnterprises(data).Where(x => x.Id == id).First();
            }
        }
        #endregion

        #region UpdateEnterprise(EnterpriseModel enterprise)
        public void UpdateEnterprise(EnterpriseModel enterprise)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                ENTERPRISE current = data.ENTERPRISE.Where(x => enterprise.Id == x.ID).SingleOrDefault();
                if (current != null)
                {
                    try
                    {
                        current.NAME = enterprise.Name;
                        current.DESCRIPTION = enterprise.Description;
                        current.CREATION_DATE = enterprise.CreationDate;
                        current.UPDATE_DATE = DateTime.Now;

                        data.SaveChanges();

                    }
                    catch (Exception e)
                    {
                        if (e.InnerException != null)
                            throw new Exception(e.InnerException.Message);
                        throw;
                    }
                }
            }
        }
        #endregion

        #region InsertEnterprise(EnterpriseModel enterprise)
        public Guid InsertEnterprise(EnterpriseModel enterprise)
        {
            Guid id = Guid.NewGuid();
            if (enterprise == null)
                throw new NullReferenceException();

            using (AppTourEntities data = new AppTourEntities())
            {
                ENTERPRISE _new = new ENTERPRISE
                {
                    ID = id,
                    NAME = enterprise.Name,
                    DESCRIPTION = enterprise.Description,
                    CREATION_DATE = DateTime.Now,
                    UPDATE_DATE = DateTime.Now
                };

                data.ENTERPRISE.AddObject(_new);
                data.SaveChanges();
            }

            return id;
        }
        #endregion

        #region DeleteEnterprise(EnterpriseModel enterprise)
        public void DeleteEnterprise(EnterpriseModel enterprise)
        {
            if (enterprise == null)
                throw new ArgumentNullException();
            using (AppTourEntities data = new AppTourEntities())
            {
                ENTERPRISE current = data.ENTERPRISE.Where(p => p.ID == enterprise.Id).SingleOrDefault();
                if (current != null)
                {
                    try
                    {
                        data.DeleteObject(current);
                        data.SaveChanges();
                    }
                    catch (UpdateException ex)
                    {
                        if (ex.InnerException != null)
                        {
                            throw new UpdateException(ex.InnerException.Message);
                        }
                        throw;
                    }
                    catch (Exception e)
                    {
                        if (e.InnerException != null)
                            throw new Exception(e.InnerException.Message);
                        throw;
                    }
                }
            }

        }
        #endregion

    }
}
