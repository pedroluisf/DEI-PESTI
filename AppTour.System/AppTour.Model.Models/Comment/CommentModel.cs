using System;
using System.Runtime.Serialization;
using AppTour.Model.Models.Event;
using AppTour.Model.Models.Helpers;
using AppTour.Model.Models.Point;
using AppTour.Model.Models.User;

namespace AppTour.Model.Models.Comment
{
    [DataContract]
    public class CommentModel : BaseModel
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public UserModel User { get; set; }

        [DataMember]
        public PointModel Point { get; set; }

        [DataMember]
        public EventModel Event { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public bool IsReported { get; set; }

        [DataMember]
        public DateTime CreationDate { get; set; }

        public override FluentValidation.Results.ValidationResult SelfValidate()
        {
            return ValidationHelper.Validate<CommentValidator, CommentModel>(this);
        }

    }
}
