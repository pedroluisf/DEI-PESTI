using System;
using System.Runtime.Serialization;
using AppTour.Model.Models.Helpers;
using FluentValidation.Results;

namespace AppTour.Model.Models.Theme
{
    [DataContract]
    public class ThemeModel : BaseModel
    {
        public ThemeModel() { }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }
        
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Image { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public DateTime CreationDate { get; set; }

        [DataMember]
        public DateTime UpdateDate { get; set; }

        public override ValidationResult SelfValidate()
        {
            return ValidationHelper.Validate<ThemeValidator, ThemeModel>(this);
        }
    }
}
