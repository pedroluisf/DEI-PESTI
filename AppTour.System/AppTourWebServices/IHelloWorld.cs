using System.ServiceModel;

namespace AppTour.WebServices.AppTourWebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IHelloWorld" in both code and config file together.
    [ServiceContract]
    public interface IHelloWorld
    {
        [OperationContract]
        string DoWork();
    }
}
