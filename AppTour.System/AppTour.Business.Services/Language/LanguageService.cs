using System;
using System.Collections.Generic;
using AppTour.Business.ServiceContracts.Language;
using AppTour.DataAccess.Repository.Language;
using AppTour.Model.Models.Language;

namespace AppTour.Business.Services.Language
{
    public class LanguageService : ILanguageService
    {

        public IList<LanguageModel> GetLanguages()
        {
            return new LanguageRepository().GetLanguages();
        }

        public LanguageModel GetLanguage(Guid id)
        {
            return new LanguageRepository().GetLanguage(id);
        }

        public void UpdateLanguage(LanguageModel language)
        {
            new LanguageRepository().UpdateLanguage(language);
        }

        public Guid InsertLanguage(LanguageModel language)
        {
            return new LanguageRepository().InsertLanguage(language);
        }

        public void DeleteLanguage(LanguageModel language)
        {
            new LanguageRepository().DeleteEnterprise(language);
        }

        public IList<LanguageModel> GetActiveLanguages()
        {
            return new LanguageRepository().GetActiveLanguages();
        }
    }
}
