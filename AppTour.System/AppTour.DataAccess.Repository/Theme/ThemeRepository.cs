using System;
using System.Collections.Generic;
using System.Linq;
using AppTour.DataAccess.Entity;
using AppTour.Model.Models.Theme;

namespace AppTour.DataAccess.Repository.Theme
{
    public sealed class ThemeRepository
    {
        #region IQueryable GetTheme
        private IQueryable<ThemeModel> GetTheme(AppTourEntities data)
        {

            var query = from c in data.THEME
                        orderby c.NAME
                        select new ThemeModel
                        {
                            Id = c.ID,
                            Name = c.NAME,
                            Description = c.DESCRIPTION,
                            Image = c.IMAGE,
                            IsActive = c.IS_ACTIVE,
                            CreationDate = c.CREATION_DATE,
                            UpdateDate = c.UPDATE_DATE
                        };
            return query;
        }
        #endregion

        #region Public GetThemes
        public IList<ThemeModel> GetThemes()
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetTheme(data).ToList();
            }
        }
        #endregion

        #region Public GetActiveThemes
        public IList<ThemeModel> GetActiveThemes()
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetTheme(data).Where(f => f.IsActive).ToList();
            }
        }
        #endregion

        #region  GetTheme(Guid id)
        public ThemeModel GetTheme(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("id");

            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetTheme(data).Where(x => x.Id == id).First();
            }
        }
        #endregion

        #region UpdateTheme(ThemeModel theme)
        public void UpdateTheme(ThemeModel theme)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                THEME current = data.THEME.Where(x => theme.Id == x.ID).SingleOrDefault();
                if (current != null)
                {
                    try
                    {
                        current.NAME = theme.Name;
                        current.DESCRIPTION = theme.Description;
                        current.IMAGE = theme.Image;
                        current.IS_ACTIVE = theme.IsActive;
                        current.CREATION_DATE = theme.CreationDate;
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

        #region InsertTheme(ThemeModel theme)
        public Guid InsertTheme(ThemeModel theme)
        {
            Guid id = Guid.NewGuid();
            if (theme == null)
                throw new NullReferenceException("theme");

            using (AppTourEntities data = new AppTourEntities())
            {
                THEME _new = new THEME
                {
                    ID = id,
                    NAME = theme.Name,
                    DESCRIPTION = theme.Description,
                    IMAGE = theme.Image,
                    IS_ACTIVE = theme.IsActive,
                    CREATION_DATE = DateTime.Now,
                    UPDATE_DATE = DateTime.Now
                };

                data.THEME.AddObject(_new);
                data.SaveChanges();
            }

            return id;
        }
        #endregion

        #region DeleteTheme(ThemeModel theme)
        public void DeleteTheme(ThemeModel theme)
        {
            if (theme == null)
                throw new ArgumentNullException();
            using (AppTourEntities data = new AppTourEntities())
            {
                THEME current = data.THEME.Where(p => p.ID == theme.Id).SingleOrDefault();
                if (current != null)
                {
                    try
                    {
                        current.IS_ACTIVE = false;

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
