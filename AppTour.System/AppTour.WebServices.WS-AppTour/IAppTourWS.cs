using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using AppTour.Model.Models.User;

namespace AppTour.WebServices.WS_AppTour
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAppTourWS" in both code and config file together.
    [ServiceContract]
    public interface IAppTourWS
    {
        [OperationContract]
        [WebGet]
        void DoWork();

        [OperationContract]
        [WebGet]
        UserModel Authentication(string username, string password);
    }
}
