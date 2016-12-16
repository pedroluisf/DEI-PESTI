using System;
using System.Collections.Generic;
using System.ServiceModel;
using AppTour.Model.Models.Enterprise;

namespace AppTour.Business.ServiceContracts.Enterprise
{
    [ServiceContract]
    public interface IEnterpriseService
    {
        [OperationContract]
        IList<EnterpriseModel> GetEnterprises();

        [OperationContract]
        EnterpriseModel GetEnterprise(Guid id);
        
        [OperationContract]
        void UpdateEnterprise(EnterpriseModel enterprise);
        
        [OperationContract]
        Guid InsertEnterprise(EnterpriseModel enterprise);
        
        [OperationContract]
        void DeleteEnterprise(EnterpriseModel enterprise);
    }
}
