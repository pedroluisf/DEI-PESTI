using System;
using System.Runtime.Serialization;
using AppTour.Model.Models.Helpers;
using FluentValidation.Results;

namespace AppTour.Model.Models.Enterprise
{
    public class EnterpriseModel : BaseModel
    {
        public EnterpriseModel() { }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public DateTime CreationDate { get; set; }

        [DataMember]
        public DateTime UpdateDate { get; set; }

        public override ValidationResult SelfValidate()
        {
            return ValidationHelper.Validate<EnterpriseValidator, EnterpriseModel>(this);
        }
    }
}
