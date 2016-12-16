using System.Runtime.Serialization;
using AppTour.Model.Models.Helpers;
using AppTour.Model.Models.SearchProfile;
using FluentValidation.Results;

namespace AppTour.Model.Models.Search
{
    [DataContract]
    public class SearchModel : BaseModel
    {
        public SearchModel() { }

        [DataMember]
        public SearchProfileModel SearchProfile { get; set; }

        [DataMember]
        public string Terms { get; set; }

        [DataMember]
        public string Coordenate { get; set; }

        public override ValidationResult SelfValidate()
        {
            return ValidationHelper.Validate<SearchValidator, SearchModel>(this);
        }
    }
}
