using System;
using System.Collections.Generic;

namespace AppTour.Agents.Service.Core
{
    public sealed class TopicMapper
    {
        #region Atributos
        IList<Tuple<string, Guid>> Topics;
        private readonly static TopicMapper _instance = new TopicMapper();
        #endregion

        #region + Construtor
        private TopicMapper()
        {
            Topics = new List<Tuple<string, Guid>>();
            Fill();
        }
        #endregion

        #region - Fill
        private void Fill()
        {
            this.Topics.Add(Tuple.Create("airport", new Guid("0B646D05-9172-447C-85DA-64A5BF336EB6")));
            this.Topics.Add(Tuple.Create("university", new Guid("A496DE8E-183E-4331-955D-C29123D67778")));

            this.Topics.Add(Tuple.Create("lodging", new Guid("0733BC9D-5306-4796-9CF8-F11C37E83FA2")));
            this.Topics.Add(Tuple.Create("health", new Guid("B1F76F51-677E-4383-87DD-A9E71F215891")));

            this.Topics.Add(Tuple.Create("school", new Guid("A496DE8E-183E-4331-955D-C29123D67778")));
            this.Topics.Add(Tuple.Create("spa", new Guid("28D67718-47C5-4D34-893B-2F45500F018D")));

            this.Topics.Add(Tuple.Create("clothing_store", new Guid("6D901B85-93EE-46D6-AA61-2E3E911DDEDE")));
            this.Topics.Add(Tuple.Create("store", new Guid("6D901B85-93EE-46D6-AA61-2E3E911DDEDE")));
            this.Topics.Add(Tuple.Create("establishment", new Guid("6D901B85-93EE-46D6-AA61-2E3E911DDEDE")));

        }
        #endregion

        #region + static TopicMapper Instance
        public static TopicMapper Instance
        {
            get
            {
                return _instance;
            }

        }
        #endregion

        #region + GetTopicIdFromType
        public Guid GetTopicIdFromSentence(string Value)
        {
            foreach (Tuple<string, Guid> entry in this.Topics)
            {
                if (entry.Item1.Equals(Value))
                    return entry.Item2;
            }
            return Guid.Empty;
        }
        #endregion

    }
}
