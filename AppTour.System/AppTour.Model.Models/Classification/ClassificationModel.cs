using System;
using System.Runtime.Serialization;
using AppTour.Model.Models.Event;
using AppTour.Model.Models.Helpers;
using AppTour.Model.Models.Point;
using AppTour.Model.Models.User;

namespace AppTour.Model.Models.Classification
{
    [DataContract]
    public class ClassificationModel : BaseModel
    {
        public ClassificationModel() { }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public PointModel Point { get; set; }

        [DataMember]
        public EventModel Event { get; set; }

        [DataMember]
        public int Classification { get; set; }

        [DataMember]
        public UserModel User { get; set; }

        [DataMember]
        public DateTime CreationDate { get; set; }

        public override FluentValidation.Results.ValidationResult SelfValidate()
        {
            return ValidationHelper.Validate<ClassificationValidator, ClassificationModel>(this);
        }
    }
}
