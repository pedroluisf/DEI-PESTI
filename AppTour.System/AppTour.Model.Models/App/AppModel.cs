using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using AppTour.Model.Models.Enterprise;
using AppTour.Model.Models.Helpers;
using DataAnnotationsExtensions;

namespace AppTour.Model.Models.App
{
    public class AppModel : BaseModel
    {
        public AppModel() { }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public EnterpriseModel Enterprise { get; set; }

        [DataMember]
        [Required]
        public string Name { get; set; }

        [DataMember]
        [Required]
        public string Description { get; set; }

        [DataMember]
        [Required]
        public string Version { get; set; }

        [DataMember]
        [Url]
        [Required]
        public string URL { get; set; }

        [DataMember]
        public DateTime CreationDate { get; set; }

        [DataMember]
        public DateTime UpdateDate { get; set; }

        public override FluentValidation.Results.ValidationResult SelfValidate()
        {
            return ValidationHelper.Validate<AppValidator, AppModel>(this);
        }
    }
}
