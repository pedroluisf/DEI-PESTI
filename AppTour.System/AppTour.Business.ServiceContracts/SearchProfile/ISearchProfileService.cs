using System;
using System.Collections.Generic;
using System.ServiceModel;
using AppTour.Model.Models.SearchProfile;

namespace AppTour.Business.ServiceContracts.SearchProfile
{
    public interface ISearchProfileService
    {
        [OperationContract]
        IList<SearchProfileModel> GetSearchProfiles();

        [OperationContract]
        IList<SearchProfileModel> GetSearchProfiles(Guid UserId);

        [OperationContract]
        IList<SearchProfileModel> GetSearchProfiles(Guid UserId, bool Active);

        [OperationContract]
        SearchProfileModel GetSearchProfile(Guid id);

        [OperationContract]
        SearchProfileModel GetDefaultSearchProfile(Guid UserId);

        [OperationContract]
        SearchProfileModel GetSearchProfile(Guid id, Guid UserId);

        [OperationContract]
        void UpdateSearchProfile(SearchProfileModel searchProfile);

        [OperationContract]
        Guid InsertSearchProfile(SearchProfileModel searchProfile);

        [OperationContract]
        bool DeleteSearchProfile(SearchProfileModel searchProfile);
    }
}
