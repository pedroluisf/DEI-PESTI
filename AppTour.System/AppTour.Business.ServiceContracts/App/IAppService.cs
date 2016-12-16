using System;
using System.Collections.Generic;
using System.ServiceModel;
using AppTour.Model.Models.App;

namespace AppTour.Business.ServiceContracts.App
{
    [ServiceContract]
    public interface IAppService
    {
        [OperationContract]
        IList<AppModel> GetApps();
        [OperationContract]
        AppModel GetApp(Guid id);
        [OperationContract]
        void UpdateApp(AppModel app);
        [OperationContract]
        Guid InsertApp(AppModel app);
        [OperationContract]
        void DeleteApp(AppModel app);
    }
}
