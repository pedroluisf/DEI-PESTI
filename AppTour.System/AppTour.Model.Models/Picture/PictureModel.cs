using System;
using System.Runtime.Serialization;
using AppTour.Model.Models.Event;
using AppTour.Model.Models.Helpers;
using AppTour.Model.Models.Point;
using FluentValidation.Results;

namespace AppTour.Model.Models.Picture
{
    [DataContract]
    public class PictureModel : BaseModel
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public PointModel Point { get; set; }

        [DataMember]
        public EventModel Event { get; set; }

        [DataMember]
        public string Picture_URL { get; set; }

        public override ValidationResult SelfValidate()
        {
            return ValidationHelper.Validate<PictureValidator, PictureModel>(this);
        }
    }
}
