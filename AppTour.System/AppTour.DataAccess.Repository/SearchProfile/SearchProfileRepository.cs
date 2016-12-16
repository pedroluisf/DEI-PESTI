using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AppTour.DataAccess.Entity;
using AppTour.DataAccess.Repository.Topic;
using AppTour.Model.Models.SearchProfile;
using AppTour.Model.Models.User;
using System.Threading.Tasks;

namespace AppTour.DataAccess.Repository.SearchProfile
{
    public sealed class SearchProfileRepository
    {

        #region - IQueryable GetSearchProfiles(AppTourEntities data)
        private IQueryable<SearchProfileModel> GetSearchProfiles(AppTourEntities data)
        {
            var query = (from c in data.SEARCH_PROFILE
                         select new SearchProfileModel
                         {
                             Id = c.ID,
                             Utilizador = new UserModel
                             {
                                 Id = c.USER.ID,
                                 RealName = c.USER.REALNAME,
                                 IsActive = c.USER.IS_ACTIVE,
                                 CreationDate = c.USER.CREATION_DATE,
                                 UpdateDate = c.USER.UPDATE_DATE
                             },
                             Name = c.NAME,
                             PointsRangeDistance = c.POINTS_RANGE_DISTANCE,
                             EventsSearchDays = c.EVENTS_SEARCH_DAYS,
                             SearchCriteria = c.SEARCH_CRITERIA,
                             IsActive = c.IS_ACTIVE,
                             CreationDate = c.CREATION_DATE,
                             UpdateDate = c.UPDATE_DATE
                         });

            return query;
        }
        #endregion

        #region - IQueryable GetSearchProfiles(AppTourEntities data, Guid PointId)
        private IQueryable<SearchProfileModel> GetSearchProfiles(AppTourEntities data, Guid UserId)
        {
            var query = (from c in data.SEARCH_PROFILE
                         where c.USER.ID == UserId
                         select new SearchProfileModel
                         {
                             Id = c.ID,
                             Utilizador = new UserModel
                             {
                                 Id = c.USER.ID,
                                 RealName = c.USER.REALNAME,
                                 IsActive = c.USER.IS_ACTIVE,
                                 CreationDate = c.USER.CREATION_DATE,
                                 UpdateDate = c.USER.UPDATE_DATE
                             },
                             Name = c.NAME,
                             PointsRangeDistance = c.POINTS_RANGE_DISTANCE,
                             EventsSearchDays = c.EVENTS_SEARCH_DAYS,
                             SearchCriteria = c.SEARCH_CRITERIA,
                             IsActive = c.IS_ACTIVE,
                             CreationDate = c.CREATION_DATE,
                             UpdateDate = c.UPDATE_DATE
                         });
            return query;
        }
        #endregion

        #region + GetSearchProfiles()
        public IList<SearchProfileModel> GetSearchProfiles()
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                IQueryable<SearchProfileModel> profileList = this.GetSearchProfiles(data);

                profileList.ToList().ForEach(x =>
                {
                    x.SearchProfileTopics = new TopicRepository().GetTopicsForSearchProfile(x.Id);
                });

                return profileList.ToList();
            }
        }
        #endregion

        #region + SearchProfileModel GetSearchProfile(Guid SearchProfileID)
        public SearchProfileModel GetSearchProfile(Guid SearchProfileID)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                SearchProfileModel profile = this.GetSearchProfiles(data).SingleOrDefault(x => x.Id == SearchProfileID);
                if (profile == null)
                    return null;

                profile.SearchProfileTopics = new TopicRepository().GetTopicsForSearchProfile(SearchProfileID);

                return profile;
            }
        }
        #endregion

        #region + IList<SearchProfileModel> GetSearchProfiles(Guid UserId)
        public IList<SearchProfileModel> GetSearchProfiles(Guid UserId)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                IList<SearchProfileModel> profileList = this.GetSearchProfiles(data, UserId).ToList();

                profileList.AsParallel().ForAll(x =>
                {
                    x.SearchProfileTopics = new TopicRepository().GetTopicsForSearchProfile(x.Id);
                });

                return profileList.ToList();
            }
        }

        #endregion

        #region + InsertSearchProfile(SearchProfileModel searchProfile)
        public Guid InsertSearchProfile(SearchProfileModel searchProfile)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                try
                {
                    SEARCH_PROFILE novo = new SEARCH_PROFILE
                    {
                        ID = searchProfile.Id == null || searchProfile.Id == Guid.Empty ? Guid.NewGuid() : searchProfile.Id,
                        NAME = searchProfile.Name,
                        POINTS_RANGE_DISTANCE = searchProfile.PointsRangeDistance,
                        SEARCH_CRITERIA = searchProfile.SearchCriteria,
                        EVENTS_SEARCH_DAYS = searchProfile.EventsSearchDays,
                        CREATION_DATE = DateTime.Now,
                        UPDATE_DATE = DateTime.Now,
                        IS_ACTIVE = searchProfile.IsActive,
                        USER = data.USER.Single(x => x.ID == searchProfile.Utilizador.Id)
                    };

                    if (searchProfile.SearchProfileTopics != null && searchProfile.SearchProfileTopics.Count() > 0)
                        // Adicionar Atributos
                        searchProfile.SearchProfileTopics.ToList().ForEach(x => novo.SEARCH_PROFILE_TOPIC.Add(new SEARCH_PROFILE_TOPIC
                        {
                            ID = Guid.NewGuid(),
                            TOPIC = data.TOPIC.SingleOrDefault(y => y.ID == x.Id),
                            SEARCH_PROFILE = novo
                        }));

                    // Guardar tudo

                    data.SEARCH_PROFILE.AddObject(novo);
                    data.SaveChanges();

                    return novo.ID;
                }
                catch (UpdateException upd)
                {
                    throw new UpdateException(upd.InnerException.Message);
                }
                catch (Exception e)
                {
                    if (e.InnerException != null)
                        throw new ApplicationException(e.InnerException.Message);

                    throw;
                }
            }
        }
        #endregion
    }
}
