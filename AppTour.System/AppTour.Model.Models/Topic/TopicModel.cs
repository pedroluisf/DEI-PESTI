using System;
using System.Runtime.Serialization;
using AppTour.Model.Models.Helpers;
using AppTour.Model.Models.Theme;
using FluentValidation.Results;

namespace AppTour.Model.Models.Topic
{
    [DataContract]
    public class TopicModel : BaseModel
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public ThemeModel Theme { get; set; }

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
            return ValidationHelper.Validate<TopicValidator, TopicModel>(this);
        }
    }
}
