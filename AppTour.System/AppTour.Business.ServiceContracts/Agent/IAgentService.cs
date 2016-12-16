using System;
using System.Collections.Generic;
using System.ServiceModel;
using AppTour.Model.Models.Agent;

namespace AppTour.Business.ServiceContracts.Agent
{
    [ServiceContract]
    public interface IAgentService
    {
        [OperationContract]
        IList<AgentModel> GetAgents();

        [OperationContract]
        AgentModel GetAgent(Guid id);

        [OperationContract]
        AgentModel GetAgent(string AgenteName);

    }
}
