using System;
using System.Collections.Generic;
using System.ServiceModel;
using AppTour.Model.Models.Topic;

namespace AppTour.Business.ServiceContracts.Topic
{
    public interface ITopicService
    {
        [OperationContract]
        IList<TopicModel> GetTopics();

        [OperationContract]
        IList<TopicModel> GetActiveTopics();

        [OperationContract]
        TopicModel GetTopic(Guid id);

        [OperationContract]
        void UpdateTopic(TopicModel topic);

        [OperationContract]
        Guid InsertTopic(TopicModel topic);

        [OperationContract]
        void DeleteTopic(TopicModel topic);

        [OperationContract]
        TopicModel GetTopic(string TopicName);
    }
}
