using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using AppTour.Model.Models.Helpers;
using AppTour.Model.Models.Topic;
using AppTour.Model.Models.User;
using FluentValidation.Results;

namespace AppTour.Model.Models.SearchProfile
{
    [DataContract]
    public class SearchProfileModel : BaseModel
    {
        public SearchProfileModel() { }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public UserModel Utilizador { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int? PointsRangeDistance { get; set; }

        [DataMember]
        public int? EventsSearchDays { get; set; }

        [DataMember]
        public string SearchCriteria { get; set; }

        [DataMember]
        public IList<TopicModel> SearchProfileTopics { get; set; }

        public IList<TopicsViewModel> SearchProfileCheckBoxs { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public DateTime CreationDate { get; set; }

        [DataMember]
        public DateTime UpdateDate { get; set; }

        public override ValidationResult SelfValidate()
        {
            return ValidationHelper.Validate<SearchProfileValidator, SearchProfileModel>(this);
        }
    }

    #region TopicsViewModel
    public class TopicsViewModel
    {
        public Guid ThemeId { get; set; }
        public string ThemeName { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Check { get; set; }
    }
    #endregion
}
