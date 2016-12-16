using System.ServiceModel;
using AppTour.Model.Models.User;

namespace AppTour.WebServices.AppTourWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILogin" in both code and config file together.
    [ServiceContract]
    public interface ILogin
    {
        [OperationContract]
        UserModel Authentication(string username, string password);
    }
}
