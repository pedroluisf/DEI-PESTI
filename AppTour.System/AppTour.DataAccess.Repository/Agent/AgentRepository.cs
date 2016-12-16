using System;
using System.Collections.Generic;
using System.Linq;
using AppTour.DataAccess.Entity;
using AppTour.Model.Models.Agent;

namespace AppTour.DataAccess.Repository.Agent
{
    public sealed class AgentRepository
    {
        #region - IQueryable<AgentModel> GetAgents(AppTourEntities data)
        private IQueryable<AgentModel> GetAgents(AppTourEntities data)
        {

            var query = (from c in data.AGENTS
                         orderby c.NAME
                         select new AgentModel
                         {
                             Id = c.ID,
                             Name = c.NAME,
                             FullClassName = c.FULL_CLASS_NAME,
                             DLLFile = c.DLL_FILE,
                             MaxRequestPerDay = c.MAX_REQUEST_PER_DAY,
                             LastReference = c.LAST_REFERENCE,
                             Periodicity = c.PERIODICITY,
                             Rating = c.RATING,
                             IsActive = c.IS_ACTIVE,
                             LastExecutionDate = c.LAST_EXECUTION_DATE,
                             CreationDate = c.CREATION_DATE
                         });

            return query;
        }
        #endregion

        #region + IList<AgentModel> GetAgents()
        public IList<AgentModel> GetAgents()
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetAgents(data).Where(x => x.IsActive).ToList();
            }
        }
        #endregion

        #region + GetAgent(Guid Id)
        public AgentModel GetAgent(Guid Id)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                return this.GetAgents(data).SingleOrDefault(x => x.Id == Id && x.IsActive);
            }
        }
        #endregion

        #region + UpdateAgent(AgentModel agentModel)
        public void UpdateAgent(AgentModel agentModel)
        {
            using (AppTourEntities data = new AppTourEntities())
            {
                AGENTS current = data.AGENTS.SingleOrDefault(x => x.ID == agentModel.Id);

                if (current != null)
                {
                    try
                    {
                        current.NAME = agentModel.Name;
                        current.IS_ACTIVE = agentModel.IsActive;
                        current.LAST_EXECUTION_DATE = agentModel.LastExecutionDate;
                        current.MAX_REQUEST_PER_DAY = agentModel.MaxRequestPerDay;
                        current.PERIODICITY = agentModel.Periodicity;
                        current.RATING = agentModel.Rating;
                        current.LAST_REFERENCE = agentModel.LastReference;

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
    }
}
