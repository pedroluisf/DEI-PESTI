using System;
using System.Runtime.Serialization;
using AppTour.Model.Models.Enterprise;
using AppTour.Model.Models.Helpers;
using FluentValidation.Results;

namespace AppTour.Model.Models.Author
{
    public class AuthorModel : BaseModel
    {
        public AuthorModel() { }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public EnterpriseModel Enterprise { get; set; }
        
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string RealName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public DateTime CreationDate { get; set; }

        [DataMember]
        public DateTime UpdateDate { get; set; }

        public override ValidationResult SelfValidate()
        {
            return ValidationHelper.Validate<AuthorValidator, AuthorModel>(this);
        }
    }
}
