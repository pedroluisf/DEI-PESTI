using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppTour.Agents.Service.Core;
using AppTour.Agents.Service.Factory;
using AppTour.Agents.Service.Interface;
using AppTour.Business.Services.Agent;
using AppTour.Model.Models.Agent;

namespace AppTour.Agents.Service.Controller
{
    public class AgentsController
    {
        #region + Construtor
        public AgentsController()
        {
        }
        #endregion

        #region + StartProcess
        public void StartProcess()
        {
            IList<AgentModel> Agents = new AgentService().GetAgents();



            int number = 0;

            Agents.ToList().ForEach(x =>
            {
                if (!x.LastExecutionDate.HasValue)
                {
                    number++;
                    StartAgent(x);
                }

                else
                {
                    TimeSpan diff = DateTime.Now.Subtract(x.LastExecutionDate.Value);

                    if (diff.TotalHours >= x.Periodicity)
                    {
                        number++;
                        StartAgent(x);
                    }
                }
            });
            NoticeService.Instance.Notify("Started " + number + " Agents!");
        }
        #endregion

        #region - StartAgent(AgentModel agentModel)
        private void StartAgent(AgentModel agentModel)
        {
            Task.Factory.StartNew(() =>
            {
                IAgentAdapter agent = AgentsFactory.Instance.CreateAgent(agentModel);

                if (agent != null)
                {
                    string lastReference = agent.Execute(agentModel.MaxRequestPerDay, agentModel.Rating, (string)agentModel.LastReference, agentModel.Name);

                    // Terminou e actualiza dados
                    agentModel.LastExecutionDate = DateTime.Now;
                    agentModel.LastReference = lastReference;
                    new AgentService().UpdateAgent(agentModel);

                    // Notify
                    NoticeService.Instance.Notify("Agent " + agentModel.Name + " ended!");
                    NoticeService.Instance.Notify("Next Parse: " + DateTime.Now.AddHours(agentModel.Periodicity).ToString());

                }
            });
        }
        #endregion
    }
}
