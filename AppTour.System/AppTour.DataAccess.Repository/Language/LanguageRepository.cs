using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AppTour.DataAccess.Entity;
using AppTour.Model.Models.Language;

namespace AppTour.DataAccess.Repository.Language
{
    public class LanguageRepository
    {
        #region IQueryable GetLanguages
        private IQueryable<LanguageModel> GetLanguages(AppTourEntities data)
        {

            var query = from c in data.LANGUAGE
                        orderby c.NAME
                        select new LanguageModel
                        {
                            Id = c.ID,
                            Name = c.NAME,
                            ISO2 = c.ISO2,
                            IsActive = c.IS_ACTIVE
                        };
            return query;
        }
        #endregion

        #region Public GetLanguages
        public IList<LanguageModel> GetLanguages()
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetLanguages(data).ToList();
            }
        }
        #endregion

        #region Public GetActiveLanguages
        public IList<LanguageModel> GetActiveLanguages()
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetLanguages(data).Where(x => x.IsActive).ToList();
            }
        }
        #endregion

        #region  GetLanguage(Guid id)
        public LanguageModel GetLanguage(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("id");

            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetLanguages(data).Where(x => x.Id == id).First();
            }
        }
        #endregion

        #region UpdateLanguage(LanguageModel language)
        public void UpdateLanguage(LanguageModel language)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                LANGUAGE current = data.LANGUAGE.Where(x => language.Id == x.ID).SingleOrDefault();

                if (current != null)
                {
                    try
                    {
                        current.NAME = language.Name;
                        current.ISO2 = language.ISO2;
                        current.IS_ACTIVE = language.IsActive;

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

        #region InsertLanguage(LanguageModel language)
        public Guid InsertLanguage(LanguageModel language)
        {

            if (language == null)
                throw new NullReferenceException("language");

            Guid id = (language.Id == null || language.Id == Guid.Empty ? Guid.NewGuid() : language.Id);

            using (AppTourEntities data = new AppTourEntities())
            {
                LANGUAGE _new = new LANGUAGE
                {
                    ID = id,
                    NAME = language.Name,
                    ISO2 = language.ISO2,
                    IS_ACTIVE = language.IsActive
                };

                data.LANGUAGE.AddObject(_new);
                data.SaveChanges();
            }

            return id;
        }
        #endregion

        #region DeleteEnterprise(LanguageModel language)
        public void DeleteEnterprise(LanguageModel language)
        {
            if (language == null)
                throw new ArgumentNullException("language");

            using (AppTourEntities data = new AppTourEntities())
            {
                LANGUAGE current = data.LANGUAGE.Where(p => p.ID == language.Id).SingleOrDefault();
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
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }

        }
        #endregion

    }
}
