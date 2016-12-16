using System;
using System.Collections.Generic;
using AppTour.Business.ServiceContracts.Topic;
using AppTour.DataAccess.Repository.Topic;
using AppTour.Model.Models.Theme;
using AppTour.Model.Models.Topic;

namespace AppTour.Business.Services.Topic
{
    public class TopicService : ITopicService
    {
        public IList<TopicModel> GetTopics()
        {
            return new TopicRepository().GetTopics();
        }

        public IList<TopicModel> GetActiveTopics()
        {
            return new TopicRepository().GetActiveTopics();
        }

        public TopicModel GetTopic(Guid id)
        {
            return new TopicRepository().GetTopic(id);
        }

        public void UpdateTopic(TopicModel topic)
        {
            new TopicRepository().UpdateTopic(topic);
        }

        public Guid InsertTopic(TopicModel topic)
        {
            return new TopicRepository().InsertTopic(topic);
        }

        public void DeleteTopic(TopicModel topic)
        {
            new TopicRepository().DeleteTopic(topic);
        }

        public IList<TopicModel> GetActiveTopics(ThemeModel tema)
        {
            return new TopicRepository().GetActiveTopics(tema);
        }

        public IList<TopicModel> GetTopics(string SearchTerm)
        {
            return new TopicRepository().GetTopics(SearchTerm);
        }

        public TopicModel GetTopic(string TopicName)
        {
            return new TopicRepository().GetTopic(TopicName);
        }
    }
}
