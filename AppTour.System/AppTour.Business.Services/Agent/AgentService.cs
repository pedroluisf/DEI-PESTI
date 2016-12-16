using System;
using System.Collections.Generic;
using System.Linq;
using AppTour.Business.ServiceContracts.Agent;
using AppTour.DataAccess.Repository.Agent;
using AppTour.Model.Models.Agent;

namespace AppTour.Business.Services.Agent
{
    public class AgentService : IAgentService
    {
        public IList<AgentModel> GetAgents()
        {
            return new AgentRepository().GetAgents();
        }

        public AgentModel GetAgent(Guid id)
        {
            return new AgentRepository().GetAgent(id);
        }

        public void UpdateAgent(AgentModel model)
        {
            new AgentRepository().UpdateAgent(model);
        }

        public AgentModel GetAgent(string AgentName)
        {
            return new AgentRepository().GetAgents().SingleOrDefault(x => x.Name.Equals(AgentName));
        }
    }
}
