using System;
using System.Runtime.Serialization;
using AppTour.Model.Models.Helpers;
using AppTour.Model.Models.Language;
using FluentValidation.Results;

namespace AppTour.Model.Models.Translation
{
    [DataContract]
    public class TranslationModel : BaseModel
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Guid? ForeignId { get; set; }

        [DataMember]
        public LanguageModel Language { get; set; }

        [DataMember]
        public string TableName { get; set; }

        [DataMember]
        public string FieldName { get; set; }

        [DataMember]
        public string Value { get; set; }

        public string Instance { get; set; }

        public override ValidationResult SelfValidate()
        {
            return ValidationHelper.Validate<TranslationValidator, TranslationModel>(this);
        }

    }
}
