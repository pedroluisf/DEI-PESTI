using System;
using System.Runtime.Serialization;

namespace AppTour.WebServices.WS.ViewModel
{
    [DataContract]
    public class PointSimpleModel
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string PostalCode { get; set; }

        [DataMember]
        public string Coordenate { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public string URL { get; set; }

        [DataMember]
        public string SourceURL { get; set; }

        [DataMember]
        public string TopicId { get; set; }

    }
}