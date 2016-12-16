using System;
using System.IO;
using System.Reflection;
using AppTour.Agents.Service.Interface;
using AppTour.Model.Models.Agent;
namespace AppTour.Agents.Service.Factory
{
    public sealed class AgentsFactory
    {
        private static string PATH = Directory.GetCurrentDirectory() + @"\adapters\";

        private static readonly AgentsFactory _instance = new AgentsFactory();
        public static AgentsFactory Instance
        {
            get
            {
                return _instance;
            }
        }
        private AgentsFactory()
        {
        }

        public IAgentAdapter CreateAgent(AgentModel agentModel)
        {
            if (agentModel == null)
                return null;

            try
            {
                Assembly assembly = Assembly.LoadFile(PATH + agentModel.Name + "\\" + agentModel.DLLFile);
                Type agentType = assembly.GetType(agentModel.FullClassName);

                IAgentAdapter agent = (IAgentAdapter)Activator.CreateInstance(agentType);
                return agent;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

    }
}
