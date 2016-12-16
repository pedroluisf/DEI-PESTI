using System;
using System.Runtime.Serialization;
using AppTour.Model.Models.Helpers;

namespace AppTour.Model.Models.Agent
{
    [DataContract]
    public class AgentModel : BaseModel
    {
        public AgentModel() { }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string FullClassName { get; set; }

        [DataMember]
        public string DLLFile { get; set; }

        [DataMember]
        public int MaxRequestPerDay { get; set; }

        [DataMember]
        public string LastReference { get; set; }

        [DataMember]
        public int Periodicity { get; set; }

        [DataMember]
        public int Rating { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public DateTime? LastExecutionDate { get; set; }

        [DataMember]
        public DateTime CreationDate { get; set; }

        public override FluentValidation.Results.ValidationResult SelfValidate()
        {
            return ValidationHelper.Validate<AgentValidator, AgentModel>(this);
        }
    }
}
