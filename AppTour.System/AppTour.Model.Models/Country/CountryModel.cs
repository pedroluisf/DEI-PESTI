using System;
using System.Runtime.Serialization;
using AppTour.Model.Models.Helpers;

namespace AppTour.Model.Models.Country
{
    [DataContract]
    public class CountryModel : BaseModel
    {
        public CountryModel() { }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string ISO { get; set; }

        [DataMember]
        public string CountryName { get; set; }

        [DataMember]
        public string ISO3 { get; set; }

        [DataMember]
        public short? CountryCode { get; set; }

        public override FluentValidation.Results.ValidationResult SelfValidate()
        {
            return ValidationHelper.Validate<CountryValidator, CountryModel>(this);
        }
    }
}
