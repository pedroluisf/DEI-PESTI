using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using AppTour.Model.Models.City;
using AppTour.Model.Models.Classification;
using AppTour.Model.Models.Comment;
using AppTour.Model.Models.Helpers;
using AppTour.Model.Models.Picture;
using AppTour.Model.Models.PointsAttributes;
using AppTour.Model.Models.Topic;
using DataAnnotationsExtensions;
using FluentValidation.Results;

namespace AppTour.Model.Models.Point
{
    [DataContract]
    public class PointModel : BaseModel
    {
        public PointModel() { }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string PostalCode { get; set; }

        [DataMember]
        public CityModel City { get; set; }

        [DataMember]
        public string Coordenate { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [Url]
        [DataMember]
        public string URL { get; set; }

        [Url]
        [DataMember]
        public string SourceURL { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public IList<PointAttributeModel> Attributes { get; set; }

        [DataMember]
        public IList<TopicModel> Topics { get; set; }

        [DataMember]
        public IList<PictureModel> Pictures { get; set; }

        [DataMember]
        public IList<CommentModel> Comments { get; set; }

        [DataMember]
        public IList<ClassificationModel> Classifications { get; set; }

        public override ValidationResult SelfValidate()
        {
            return ValidationHelper.Validate<PointValidator, PointModel>(this);
        }
    }
}
