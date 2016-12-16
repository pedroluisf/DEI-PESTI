using System;
using System.Collections.Generic;
using System.Linq;
using AppTour.DataAccess.Entity;
using AppTour.Model.Models.Theme;
using AppTour.Model.Models.Topic;

namespace AppTour.DataAccess.Repository.Topic
{
    public sealed class TopicRepository
    {
        #region - IQueryable GetTopic
        private IQueryable<TopicModel> GetTopic(AppTourEntities data)
        {

            var query = from c in data.TOPIC
                        orderby c.NAME
                        select new TopicModel
                        {
                            Id = c.ID,
                            Theme = (from t in data.THEME
                                     where t.ID == c.THEME.ID
                                     select new ThemeModel
                                     {
                                         Id = t.ID,
                                         Name = t.NAME,
                                         Description = t.DESCRIPTION,
                                         Image = t.IMAGE,
                                         IsActive = t.IS_ACTIVE,
                                         CreationDate = t.CREATION_DATE,
                                         UpdateDate = t.UPDATE_DATE
                                     }).FirstOrDefault(),
                            Name = c.NAME,
                            Description = c.DESCRIPTION,
                            Image = c.IMAGE,
                            IsActive = c.IS_ACTIVE,
                            CreationDate = c.CREATION_DATE,
                            UpdateDate = c.UPDATE_DATE
                        };
            return query;
        }
        #endregion

        #region - TopicModel GetTopic(AppTourEntities data, string TopicName)
        private TopicModel GetTopic(AppTourEntities data, string TopicName)
        {
            var query = from c in data.TOPIC
                        where c.IS_ACTIVE && c.NAME.ToLower() == TopicName.ToLower()
                        select new TopicModel
                        {
                            Id = c.ID,
                            Theme = (from t in data.THEME
                                     where t.ID == c.THEME.ID
                                     select new ThemeModel
                                     {
                                         Id = t.ID,
                                         Name = t.NAME,
                                         Description = t.DESCRIPTION,
                                         Image = t.IMAGE,
                                         IsActive = t.IS_ACTIVE,
                                         CreationDate = t.CREATION_DATE,
                                         UpdateDate = t.UPDATE_DATE
                                     }).FirstOrDefault(),
                            Name = c.NAME,
                            Description = c.DESCRIPTION,
                            Image = c.IMAGE,
                            IsActive = c.IS_ACTIVE,
                            CreationDate = c.CREATION_DATE,
                            UpdateDate = c.UPDATE_DATE
                        };
            return query.FirstOrDefault();
        }
        #endregion

        #region - IQueryable<TopicModel> GetActiveTopics(AppTourEntities data)
        private IQueryable<TopicModel> GetActiveTopics(AppTourEntities data)
        {

            var query = from c in data.TOPIC
                        where c.IS_ACTIVE
                        orderby c.NAME
                        select new TopicModel
                        {
                            Id = c.ID,
                            Theme = (from t in data.THEME
                                     where t.ID == c.THEME.ID
                                     select new ThemeModel
                                     {
                                         Id = t.ID,
                                         Name = t.NAME,
                                         Description = t.DESCRIPTION,
                                         Image = t.IMAGE,
                                         IsActive = t.IS_ACTIVE,
                                         CreationDate = t.CREATION_DATE,
                                         UpdateDate = t.UPDATE_DATE
                                     }).FirstOrDefault(),
                            Name = c.NAME,
                            Description = c.DESCRIPTION,
                            Image = c.IMAGE,
                            IsActive = c.IS_ACTIVE,
                            CreationDate = c.CREATION_DATE,
                            UpdateDate = c.UPDATE_DATE
                        };
            return query;
        }
        #endregion

        #region - IQueryable GetTopicsForPoint(Guid PointId)
        private IQueryable<TopicModel> GetTopicsForPoint(AppTourEntities data, Guid PointModelId)
        {
            var query = from t in data.TOPIC
                        join p in data.POINT_TOPIC on t.ID equals p.ID_TOPIC
                        where p.ID_POINT.Equals(PointModelId)
                        select new TopicModel
                        {
                            Id = t.ID,
                            Name = t.NAME,
                            Description = t.DESCRIPTION,
                            IsActive = t.IS_ACTIVE,
                            CreationDate = t.CREATION_DATE,
                            UpdateDate = t.UPDATE_DATE
                        };
            return query;
        }
        #endregion

        #region Public GetTopics
        public IList<TopicModel> GetTopics()
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetTopic(data).ToList();
            }
        }
        #endregion

        #region Public GetActiveTopics
        public IList<TopicModel> GetActiveTopics()
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetTopic(data).Where(f => f.IsActive).ToList();
            }
        }
        #endregion

        #region GetTopicsForPoint(Guid PointId)
        public IList<TopicModel> GetTopicsForPoint(Guid PointId)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetTopicsForPoint(data, PointId).ToList();
            }
        }
        #endregion

        #region  GetTopic(Guid id)
        public TopicModel GetTopic(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException("id");

            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetTopic(data).Where(x => x.Id == id).First();
            }
        }
        #endregion

        #region UpdateTopic(TopicModel topic)
        public void UpdateTopic(TopicModel topic)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                TOPIC current = data.TOPIC.Where(x => topic.Id == x.ID).SingleOrDefault();
                if (current != null)
                {
                    try
                    {
                        current.NAME = topic.Name;
                        current.DESCRIPTION = topic.Description;
                        current.IMAGE = topic.Image;
                        current.IS_ACTIVE = topic.IsActive;
                        current.CREATION_DATE = topic.CreationDate;
                        current.UPDATE_DATE = DateTime.Now;

                        current.THEME = data.THEME.Where(x => x.ID == topic.Theme.Id).SingleOrDefault();

                        data.SaveChanges();

                    }
                    catch (Exception e)
                    {
                        if (e.InnerException != null)
                            throw new Exception(e.InnerException.Message);
                        throw;
                    }
                }
            }
        }
        #endregion

        #region InsertTopic(TopicModel topic)
        public Guid InsertTopic(TopicModel topic)
        {
            Guid id = Guid.NewGuid();
            if (topic == null)
                throw new NullReferenceException("theme");

            using (AppTourEntities data = new AppTourEntities())
            {
                TOPIC _new = new TOPIC
                {
                    ID = id,
                    ID_THEME = topic.Theme.Id,
                    NAME = topic.Name,
                    DESCRIPTION = topic.Description,
                    IS_ACTIVE = topic.IsActive,
                    IMAGE = topic.Image,
                    CREATION_DATE = DateTime.Now,
                    UPDATE_DATE = DateTime.Now
                };

                data.TOPIC.AddObject(_new);
                data.SaveChanges();
            }

            return id;
        }
        #endregion

        #region DeleteTopic(TopicModel topic)
        public void DeleteTopic(TopicModel topic)
        {
            if (topic == null)
                throw new ArgumentNullException();
            using (AppTourEntities data = new AppTourEntities())
            {
                TOPIC current = data.TOPIC.Where(p => p.ID == topic.Id).SingleOrDefault();
                if (current != null)
                {
                    try
                    {
                        data.DeleteObject(current);
                        data.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        if (e.InnerException != null)
                            throw new Exception(e.InnerException.Message);
                        throw;
                    }
                }
            }

        }
        #endregion

        #region + IList<TopicModel> GetActiveTopics(ThemeModel tema)
        public IList<TopicModel> GetActiveTopics(ThemeModel tema)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetTopic(data).Where(x => x.Theme.Id == tema.Id && x.IsActive).ToList();
            }
        }
        #endregion

        #region + IList<TopicModel> GetTopicsForSearchProfile(Guid searchProfileId)
        public IList<TopicModel> GetTopicsForSearchProfile(Guid searchProfileId)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetTopic(data).Where(x => x.Id == searchProfileId).ToList();
            }
        }
        #endregion

        #region + IList<TopicModel> GetTopics(string SearchTerm)
        public IList<TopicModel> GetTopics(string SearchTerm)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetActiveTopics(data).Where(x => (x.Description.Contains(SearchTerm) || x.Name.Contains(SearchTerm)) && x.IsActive).ToList();
            }
        }
        #endregion

        #region + TopicModel GetTopic(string TopicName)
        public TopicModel GetTopic(string TopicName)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetTopic(data, TopicName);
            }

        }
        #endregion

    }
}
