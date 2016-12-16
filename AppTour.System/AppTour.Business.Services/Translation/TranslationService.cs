using System;
using System.Collections.Generic;
using AppTour.Business.ServiceContracts.Translation;
using AppTour.DataAccess.Repository.Translation;
using AppTour.Model.Models.Translation;

namespace AppTour.Business.Services.Translation
{
    public class TranslationService : ITranslationService
    {
        public IList<TranslationModel> GetTranslations()
        {
            return new TranslationRepository().GetTranslations();
        }

        public IList<TranslationModel> GetTranslations(string TableName)
        {
            return new TranslationRepository().GetTranslations(TableName);
        }

        public IList<TranslationModel> GetTranslations(string TableName, Guid ForeignId)
        {
            return new TranslationRepository().GetTranslations(TableName, ForeignId, null);
        }

        public TranslationModel GetTranslation(Guid Id)
        {
            return new TranslationRepository().GetTranslation(Id);
        }

        public void UpdateTranslation(TranslationModel translation)
        {
            new TranslationRepository().UpdateTranslation(translation);
        }

        public Guid InsertTranslation(TranslationModel translation)
        {
            return new TranslationRepository().InsertTranslation(translation);
        }

        public void DeleteTranslation(TranslationModel translation)
        {
            new TranslationRepository().DeleteTranslation(translation);
        }

        public IList<TranslationModel> GetTranslations(string TableName, Guid ForeignId, Guid LanguageId)
        {
            return new TranslationRepository().GetTranslations(TableName, ForeignId, LanguageId);
        }
    }
}
