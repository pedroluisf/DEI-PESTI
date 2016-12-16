using System;
using System.Collections.Generic;
using AppTour.Business.ServiceContracts.Enterprise;
using AppTour.DataAccess.Repository.Enterprise;
using AppTour.Model.Models.Enterprise;

namespace AppTour.Business.Services.Enterprise
{
    public class EnterpriseService : IEnterpriseService
    {
        public IList<EnterpriseModel> GetEnterprises()
        {
            return new EnterpriseRepository().GetEnterprises();
        }

        public EnterpriseModel GetEnterprise(Guid id)
        {
            return new EnterpriseRepository().GetEnterprise(id);
        }

        public void UpdateEnterprise(EnterpriseModel enterprise)
        {
            new EnterpriseRepository().UpdateEnterprise(enterprise);
        }

        public Guid InsertEnterprise(EnterpriseModel enterprise)
        {
            return new EnterpriseRepository().InsertEnterprise(enterprise);
        }

        public void DeleteEnterprise(EnterpriseModel enterprise)
        {
            new EnterpriseRepository().DeleteEnterprise(enterprise);
        }
    }
}
