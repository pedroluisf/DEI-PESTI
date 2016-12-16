using System;
using System.Collections.Generic;
using System.ServiceModel;
using AppTour.Model.Models.Translation;

namespace AppTour.Business.ServiceContracts.Translation
{
    [ServiceContract]
    public interface ITranslationService
    {
        [OperationContract]
        IList<TranslationModel> GetTranslations();

        [OperationContract]
        IList<TranslationModel> GetTranslations(string TableName);

        [OperationContract]
        IList<TranslationModel> GetTranslations(string TableName, Guid ForeignId);

        [OperationContract]
        IList<TranslationModel> GetTranslations(string TableName, Guid ForeignId, Guid LanguageId);

        [OperationContract]
        TranslationModel GetTranslation(Guid Id);

        [OperationContract]
        void UpdateTranslation(TranslationModel translation);

        [OperationContract]
        Guid InsertTranslation(TranslationModel translation);

        [OperationContract]
        void DeleteTranslation(TranslationModel translation);
    }
}
