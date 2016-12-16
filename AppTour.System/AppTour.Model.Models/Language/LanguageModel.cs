using System;
using System.Runtime.Serialization;
using AppTour.Model.Models.Helpers;
using FluentValidation.Results;

namespace AppTour.Model.Models.Language
{
    public class LanguageModel : BaseModel
    {
        public LanguageModel() { }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string ISO2 { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        public override ValidationResult SelfValidate()
        {
            return ValidationHelper.Validate<LanguageValidator, LanguageModel>(this);
        }
    }
}
