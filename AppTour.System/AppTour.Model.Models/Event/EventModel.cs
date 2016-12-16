using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using AppTour.Model.Models.Helpers;
using AppTour.Model.Models.Point;
using DataAnnotationsExtensions;
using FluentValidation.Results;

namespace AppTour.Model.Models.Event
{
    public class EventModel : BaseModel
    {
        public EventModel() { }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public IList<PointModel> Points { get; set; }

        [Url]
        [DataMember]
        public string URL { get; set; }

        [Url]
        [DataMember]
        public string SourceURL { get; set; }

        public override ValidationResult SelfValidate()
        {
            return ValidationHelper.Validate<EventValidator, EventModel>(this);
        }
    }
}
