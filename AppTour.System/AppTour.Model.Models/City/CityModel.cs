using System;
using System.Runtime.Serialization;
using AppTour.Model.Models.Country;

namespace AppTour.Model.Models.City
{
    public class CityModel : BaseModel
    {
        public CityModel() { }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public CountryModel Country { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string NameTranslated { get; set; }
    }
}
