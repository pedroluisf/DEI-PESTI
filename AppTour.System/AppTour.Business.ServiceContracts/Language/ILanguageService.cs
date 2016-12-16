using System;
using System.Collections.Generic;
using System.ServiceModel;
using AppTour.Model.Models.Language;

namespace AppTour.Business.ServiceContracts.Language
{
    public interface ILanguageService
    {
        [OperationContract]
        IList<LanguageModel> GetLanguages();

        [OperationContract]
        IList<LanguageModel> GetActiveLanguages();

        [OperationContract]
        LanguageModel GetLanguage(Guid id);

        [OperationContract]
        void UpdateLanguage(LanguageModel language);

        [OperationContract]
        Guid InsertLanguage(LanguageModel language);

        [OperationContract]
        void DeleteLanguage(LanguageModel language);
    }
}
