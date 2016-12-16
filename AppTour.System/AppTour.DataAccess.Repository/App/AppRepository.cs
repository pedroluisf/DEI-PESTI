using System;
using System.Collections.Generic;
using System.Linq;
using AppTour.DataAccess.Entity;
using AppTour.Model.Models.App;
using AppTour.Model.Models.Enterprise;

namespace AppTour.DataAccess.Repository.App
{
    public class AppRepository
    {
        #region IQueryable GetApps
        private IQueryable<AppModel> GetApps(AppTourEntities data)
        {
            var query = from c in data.APP
                        orderby c.NAME
                        select new AppModel
                        {
                            Id = c.ID,
                            Enterprise = new EnterpriseModel
                            {
                                Id = c.ENTERPRISE.ID,
                                Name = c.ENTERPRISE.NAME,
                                Description = c.ENTERPRISE.DESCRIPTION,
                                UpdateDate = c.ENTERPRISE.UPDATE_DATE,
                                CreationDate = c.ENTERPRISE.CREATION_DATE
                            },
                            Name = c.NAME,
                            Description = c.DESCRIPTION,
                            Version = c.VERSION,
                            URL = c.URL,
                            CreationDate = c.CREATION_DATE,
                            UpdateDate = c.UPDATE_DATE
                        };
            return query;
        }
        #endregion

        #region Public GetApps
        public IList<AppModel> GetApps()
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetApps(data).ToList();
            }
        }
        #endregion

        #region  GetApp(Guid id)
        public AppModel GetApp(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException();

            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetApps(data).Where(x => x.Id == id).First();
            }
        }
        #endregion

        #region UpdateApp(AppModel App)
        public void UpdateApp(AppModel App)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                APP current = data.APP.Where(x => App.Id == x.ID).SingleOrDefault();
                if (current != null)
                {
                    try
                    {
                        current.NAME = App.Name;
                        current.ENTERPRISE = data.ENTERPRISE.Where(f => f.ID == App.Enterprise.Id).FirstOrDefault();
                        current.DESCRIPTION = App.Description;
                        current.VERSION = App.Version;
                        current.URL = App.URL;
                        current.CREATION_DATE = App.CreationDate;
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

        #region InsertApp(AppModel App)
        public Guid InsertApp(AppModel App)
        {
            Guid id = Guid.NewGuid();
            if (App == null)
                throw new NullReferenceException();

            using (AppTourEntities data = new AppTourEntities())
            {

                APP _new = new APP
                {
                    ID = id,
                    ENTERPRISE = data.ENTERPRISE.Where(f => f.ID == App.Enterprise.Id).FirstOrDefault(),
                    NAME = App.Name,
                    DESCRIPTION = App.Description,
                    VERSION = App.Version,
                    URL = App.URL,
                    CREATION_DATE = DateTime.Now,
                    UPDATE_DATE = DateTime.Now
                };

                data.APP.AddObject(_new);
                data.SaveChanges();
            }

            return id;
        }
        #endregion

        #region DeleteApp(AppModel App)
        public void DeleteApp(AppModel App)
        {
            if (App == null)
                throw new ArgumentNullException();
            using (AppTourEntities data = new AppTourEntities())
            {
                APP current = data.APP.Where(p => p.ID == App.Id).SingleOrDefault();
                if (current != null)
                {
                    try
                    {
                        data.DeleteObject(current);
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
    }
}
