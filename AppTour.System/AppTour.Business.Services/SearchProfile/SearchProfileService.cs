using System;
using System.Collections.Generic;
using AppTour.Business.ServiceContracts.SearchProfile;
using AppTour.DataAccess.Repository.SearchProfile;
using AppTour.Model.Models.SearchProfile;
using System.Linq;

namespace AppTour.Business.Services.SearchProfile
{
    public class SearchProfileService : ISearchProfileService
    {
        public IList<SearchProfileModel> GetSearchProfiles()
        {
            return new SearchProfileRepository().GetSearchProfiles();
        }

        public IList<SearchProfileModel> GetSearchProfiles(Guid UserId)
        {
            return new SearchProfileRepository().GetSearchProfiles(UserId);
        }

        public IList<SearchProfileModel> GetSearchProfiles(Guid UserId, bool Active)
        {
            throw new System.NotImplementedException();
        }

        public SearchProfileModel GetSearchProfile(Guid id)
        {
            return new SearchProfileRepository().GetSearchProfile(id);
        }

        public SearchProfileModel GetSearchProfile(Guid id, Guid UserId)
        {
            SearchProfileModel model = new SearchProfileRepository().GetSearchProfile(id);

            if (model == null)
                return null;

            return (model.Utilizador.Id == UserId ? model : null);
        }

        public SearchProfileModel GetDefaultSearchProfile(Guid UserId)
        {
            return new SearchProfileRepository().GetSearchProfiles(UserId).OrderByDescending(x => x.CreationDate).FirstOrDefault();
        }

        public void UpdateSearchProfile(SearchProfileModel searchProfile)
        {
            throw new NotImplementedException();
        }

        public Guid InsertSearchProfile(SearchProfileModel searchProfile)
        {
            return new SearchProfileRepository().InsertSearchProfile(searchProfile);
        }

        public bool DeleteSearchProfile(SearchProfileModel searchProfile)
        {
            throw new NotImplementedException();
        }
    }
}
