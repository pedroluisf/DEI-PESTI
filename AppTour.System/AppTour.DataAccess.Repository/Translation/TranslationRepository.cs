using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AppTour.DataAccess.Entity;
using AppTour.Model.Models.Language;
using AppTour.Model.Models.Translation;

namespace AppTour.DataAccess.Repository.Translation
{
    public sealed class TranslationRepository
    {
        #region - IQueryable GetTranslations(AppTourEntities data)
        private IQueryable<TranslationModel> GetTranslations(AppTourEntities data)
        {

            var query = from c in data.TRANSLATIONS
                        orderby c.TABLE_NAME
                        select new TranslationModel
                        {
                            Id = c.ID,
                            TableName = c.TABLE_NAME,
                            FieldName = c.FIELD_NAME,
                            Value = c.VALUE,
                            Language = (from x in data.LANGUAGE
                                        where x.ID == c.LANGUAGE.ID
                                        select new LanguageModel
                                            {
                                                Id = c.LANGUAGE.ID,
                                                IsActive = c.LANGUAGE.IS_ACTIVE,
                                                ISO2 = c.LANGUAGE.ISO2,
                                                Name = c.LANGUAGE.NAME
                                            }).FirstOrDefault()
                        };
            return query;
        }
        #endregion

        #region - IQueryable GetTranslations(AppTourEntities data, Guid TranslationId)
        private IQueryable<TranslationModel> GetTranslations(AppTourEntities data, Guid TranslationId)
        {

            var query = from c in data.TRANSLATIONS
                        where c.ID.Equals(TranslationId)
                        select new TranslationModel
                        {
                            Id = c.ID,
                            TableName = c.TABLE_NAME,
                            FieldName = c.FIELD_NAME,
                            Value = c.VALUE,
                            Language = (from x in data.LANGUAGE
                                        where x.ID == c.LANGUAGE.ID
                                        select new LanguageModel
                                            {
                                                Id = c.LANGUAGE.ID,
                                                IsActive = c.LANGUAGE.IS_ACTIVE,
                                                ISO2 = c.LANGUAGE.ISO2,
                                                Name = c.LANGUAGE.NAME
                                            }).FirstOrDefault()
                        };
            return query;
        }
        #endregion

        #region - IQueryable GetTranslations(AppTourEntities data, string TableName)
        private IQueryable<TranslationModel> GetTranslations(AppTourEntities data, string TableName)
        {

            var query = from c in data.TRANSLATIONS
                        where c.TABLE_NAME == TableName
                        orderby c.TABLE_NAME
                        select new TranslationModel
                        {
                            Id = c.ID,
                            TableName = c.TABLE_NAME,
                            FieldName = c.FIELD_NAME,
                            Value = c.VALUE,
                            Language = (from x in data.LANGUAGE
                                        where x.ID == c.LANGUAGE.ID
                                        select new LanguageModel
                                        {
                                            Id = x.ID,
                                            IsActive = x.IS_ACTIVE,
                                            ISO2 = x.ISO2,
                                            Name = x.NAME
                                        }).FirstOrDefault()
                        };
            return query;
        }
        #endregion

        #region - IQueryable GetTranslations(AppTourEntities data, string TableName)
        private IQueryable<TranslationModel> GetTranslations(AppTourEntities data, string TableName, Guid ForeignId)
        {

            var query = (from c in data.TRANSLATIONS
                         where c.TABLE_NAME == TableName && c.FOREIGN_ID == ForeignId
                         orderby c.TABLE_NAME
                         select new TranslationModel
                         {
                             Id = c.ID,
                             TableName = c.TABLE_NAME,
                             FieldName = c.FIELD_NAME,
                             Value = c.VALUE,
                             Language = (from x in data.LANGUAGE
                                         where x.ID == c.LANGUAGE.ID
                                         select new LanguageModel
                                         {
                                             Id = x.ID,
                                             Name = x.NAME,
                                             ISO2 = x.ISO2,
                                             IsActive = x.IS_ACTIVE
                                         }).FirstOrDefault()
                         });
            return query;
        }
        #endregion

        #region - IQueryable GetTranslations(AppTourEntities data, string TableName, Guid LanguageId)
        private IQueryable<TranslationModel> GetTranslations(AppTourEntities data, string TableName, Guid ForeignId, Guid LanguageId)
        {
            return this.GetTranslations(data, TableName, ForeignId).Where(x => x.Language.Id.Equals(LanguageId));
        }
        #endregion

        #region + GetTranslations
        public IList<TranslationModel> GetTranslations()
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetTranslations().ToList();
            }
        }
        #endregion

        #region + GetTranslations(string TableName, Guid ForeignId, Guid? LanguageId)
        public IList<TranslationModel> GetTranslations(string TableName, Guid ForeignId, Guid? LanguageId)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                if (LanguageId != null)
                    return this.GetTranslations(data, TableName, ForeignId, (Guid)LanguageId).ToList();

                return this.GetTranslations(data, TableName, ForeignId).ToList();
            }
        }
        #endregion

        #region + GetTranslations(string TableName)
        public IList<TranslationModel> GetTranslations(string TableName)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetTranslations(data, TableName).ToList();
            }
        }
        #endregion

        #region + GetTranslation(Guid TranslationId)
        public TranslationModel GetTranslation(Guid TranslationId)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetTranslations(data, TranslationId).Single();
            }
        }
        #endregion

        #region + UpdateTranslation(TranslationModel translation)
        public void UpdateTranslation(TranslationModel translation)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                try
                {
                    TRANSLATIONS edited = data.TRANSLATIONS.Single(x => translation.Id == x.ID);

                    if (edited != null)
                    {
                        edited.TABLE_NAME = translation.TableName;
                        edited.FIELD_NAME = translation.FieldName;
                        edited.VALUE = translation.Value;
                        edited.LANGUAGE = data.LANGUAGE.Single(x => x.ID.Equals(translation.Language.Id));
                        edited.FOREIGN_ID = translation.ForeignId;
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

        #region + InsertTranslation(TranslationModel translation)
        public Guid InsertTranslation(TranslationModel translation)
        {
            if (translation == null)
                throw new ArgumentNullException("translation");


            using (AppTourEntities data = new AppTourEntities())
            {
                try
                {
                    TRANSLATIONS novo = new TRANSLATIONS
                    {
                        ID = translation.Id == null || translation.Id == Guid.Empty ? Guid.NewGuid() : translation.Id,
                        LANGUAGE = data.LANGUAGE.Single(x => x.ID.Equals(translation.Language.Id)),
                        TABLE_NAME = translation.TableName,
                        FIELD_NAME = translation.FieldName,
                        FOREIGN_ID = translation.ForeignId,
                        VALUE = translation.Value
                    };

                    data.TRANSLATIONS.AddObject(novo);
                    data.SaveChanges();

                    return novo.ID;
                }
                catch (UpdateException upd)
                {
                    throw new UpdateException(upd.InnerException.Message);
                }
                catch (Exception e)
                {
                    if (e.InnerException != null)
                        throw new ApplicationException(e.InnerException.Message);
                    throw;
                }
            }
        }
        #endregion

        #region + DeleteTranslation(TranslationModel translation)
        public bool DeleteTranslation(TranslationModel translation)
        {
            if (translation == null)
                throw new ArgumentNullException("translation");

            using (AppTourEntities data = new AppTourEntities())
            {
                try
                {
                    TRANSLATIONS actual = data.TRANSLATIONS.Single(x => x.ID == translation.Id);

                    if (actual != null)
                    {
                        data.TRANSLATIONS.DeleteObject(actual);
                        data.SaveChanges();

                        return true;
                    }
                    return false;
                }
                catch (UpdateException upd)
                {
                    throw new UpdateException(upd.InnerException.Message);
                }
                catch (Exception e)
                {
                    if (e.InnerException != null)
                        throw new ApplicationException(e.InnerException.Message);
                    throw;
                }
            }
        }
        #endregion
    }
}
