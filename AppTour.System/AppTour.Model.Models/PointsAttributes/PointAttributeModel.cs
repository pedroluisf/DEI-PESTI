using System;
using System.Runtime.Serialization;
using AppTour.Model.Models.Helpers;
using AppTour.Model.Models.Point;
using FluentValidation.Results;

namespace AppTour.Model.Models.PointsAttributes
{
    [DataContract]
    public class PointAttributeModel : BaseModel
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public PointModel Point { get; set; }

        [DataMember]
        public string KeyPair { get; set; }

        [DataMember]
        public bool? Value_bool { get; set; }

        [DataMember]
        public string Value_string { get; set; }

        [DataMember]
        public decimal? Value_number { get; set; }

        [DataMember]
        public DateTime? Value_Date { get; set; }

        [DataMember]
        public string Value_Type { get; set; }

        public int? NumberOfTranslations { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        public override ValidationResult SelfValidate()
        {
            return ValidationHelper.Validate<PointAttributeValidator, PointAttributeModel>(this);
        }
    }
}
